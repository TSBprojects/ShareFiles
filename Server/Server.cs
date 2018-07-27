using Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    delegate object Invoke(Delegate d);

    class Server
    {
        Invoke Invoke;
        RichTextBox console;

        Socket handler;
        Socket listenSoc;
        IPAddress ipAddr;
        IPEndPoint ipEndPoint;
        const int _BUFFERSIZE = 8192;

        public int ListenPort { get; private set; }
        string[] filesList;
        bool listen = false;

        public Server(Invoke Invoke, RichTextBox console)
        {
            this.Invoke = Invoke;
            this.console = console;
            filesList = GetFilesList(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory));
            IPAddress[] addr = Dns.GetHostEntry(Dns.GetHostName()).AddressList;
            ipAddr = IPAddress.Parse(addr.Last().ToString());
        }

        public void Stop()
        {
            listen = false;

            listenSoc.Close();
            if (handler != null)
            {
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }
        }

        public void Start(int listenPort)
        {
            ListenPort = listenPort;
            ipEndPoint = new IPEndPoint(ipAddr, ListenPort);
            listenSoc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            listenSoc.Bind(ipEndPoint);
            listenSoc.Listen(10);

            listen = true;

            byte[] bytePath;
            int bytes = 0;
            int packageSize = 0;
            const int _BUFFERSIZE = 8192;
            while (true)
            {
                //Thread t = new Thread(delegate()
                //{
                //});
                try
                {
                    handler = listenSoc.Accept();
                }
                catch(Exception e)
                {
                    if(listen)
                    {
                        throw e;
                    }
                    else
                    {
                        break;
                    }
                }

                Invoke(new Action(() =>
                {
                    ConsoleWrite(handler.RemoteEndPoint.ToString() + " connected");
                }));

                handler.Send(GetSendFileListPackage(filesList));

                using (MemoryStream memStream = new MemoryStream())
                {
                    byte[] buffer = new byte[_BUFFERSIZE];
                    do
                    {
                        int received = handler.Receive(buffer);
                        memStream.Write(buffer, 0, received);
                        bytes += received;
                        if (bytes >= 2049 && packageSize == 0)
                        {
                            packageSize = GetPackageSize(memStream.ToArray());
                        }
                    }
                    while (bytes < packageSize);           
                    packageSize = 0;
                    bytes = 0;
                    bytePath = memStream.ToArray();
                    if (bytePath.Length == 0) continue;
                }

                if (IsManyFiles(bytePath))
                {
                    string[] paths = GetReceivePaths(bytePath);
                    NetFiles files = new NetFiles();
                    foreach (string path in paths)
                    {
                        using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                        {
                            byte[] byteFile = new byte[stream.Length];
                            int length = stream.Read(byteFile, 0, byteFile.Length);
                            NetFile file = new NetFile();
                            file.FileName = Path.GetFileName(stream.Name);
                            file.Data = byteFile;
                            files.Add(file);
                        }
                    }
                    byte[] fullPackage = GetSendFilePackage(files.ToArray());
                    handler.Send(fullPackage);
                    Invoke(new Action(() =>
                    {
                        ConsoleWrite(string.Format("Отправлено {0} байт", fullPackage.Length));
                    }));
                }
                else
                {
                    string path = GetReceivePath(bytePath);
                    NetFile file = new NetFile();
                    using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                    {
                        byte[] byteFile = new byte[stream.Length];
                        int length = stream.Read(byteFile, 0, byteFile.Length);
                        file.FileName = Path.GetFileName(stream.Name);
                        file.Data = byteFile;
                    }
                    byte[] fullPackage = GetSendFilePackage(file.ToArray());
                    handler.Send(fullPackage);
                    Invoke(new Action(() =>
                    {
                        ConsoleWrite(string.Format("Отправлено {0} байт", fullPackage.Length));
                    }));
                }
            }
        }


        public void SetFilesDirectory(string path)
        {
            filesList = GetFilesList(path);
        }

        string[] GetFilesList(string path)
        {
            return Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
        }

        int GetPackageSize(byte[] data)
        {
            byte[] res = new byte[2048];
            Array.Copy(data, 1, res, 0, 2048);
            return Convert.ToInt32(Encoding.UTF8.GetString(res)) + 2049;
        }

        bool IsManyFiles(byte[] data)
        {
            return data[0] == 1 ? true : false;
        }

        string GetReceivePath(byte[] data)
        {
            byte[] res = new byte[data.Length - 2049];
            Array.Copy(data, 2049, res, 0, data.Length - 2049);
            return Encoding.UTF8.GetString(res);
        }

        string[] GetReceivePaths(byte[] data)
        {
            byte[] res = new byte[data.Length - 2049];
            Array.Copy(data, 2049, res, 0, data.Length - 2049);
            string pathsStr = Encoding.UTF8.GetString(res);
            return pathsStr.Split('|');
        }

        byte[] GetSendFileListPackage(string[] paths)
        {
            string res = "";
            for (int i = 0; i < paths.Length - 1; i++)
            {
                res += paths[i] + "|";
            }
            res += paths[paths.Length - 1];

            byte[] byteCount = new byte[2048];
            byte[] data = Encoding.UTF8.GetBytes(res);
            byte[] bCount = Encoding.UTF8.GetBytes(data.Length.ToString());
            bCount.CopyTo(byteCount, 0);

            using (MemoryStream memStream = new MemoryStream())
            {
                memStream.Write(byteCount, 0, byteCount.Length);
                memStream.Write(data, 0, data.Length);
                return memStream.ToArray();
            }
        }

        byte[] GetSendFilePackage(byte[] data)
        {
            byte[] byteCount = new byte[2048];
            byte[] bCount = Encoding.UTF8.GetBytes(data.Length.ToString());
            bCount.CopyTo(byteCount, 0);
            using (MemoryStream memStream = new MemoryStream())
            {
                memStream.Write(byteCount, 0, byteCount.Length);
                memStream.Write(data, 0, data.Length);
                return memStream.ToArray();
            }
        }


        void ConsoleWrite(string text)
        {
            console.Text += string.Format("\n[{0}] - {1}", DateTime.Now.ToString("hh:mm"), text);
            console.SelectionStart = console.Text.Length;
            console.ScrollToCaret();
        }
    }
}
