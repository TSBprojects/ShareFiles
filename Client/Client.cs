using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    delegate object Invoke(Delegate d);

    class Client
    {
        Invoke Invoke;
        BackgroundWorker bw;
        RichTextBox console;

        Socket socket;
        IPEndPoint ipEndPoint;
        const int _BUFFERSIZE = 8192;
        ToolStripProgressBar progressBar;
        int progressBarTimeout = 700;

        public Client(Invoke invoke, ToolStripProgressBar progressBar, RichTextBox console)
        {
            Invoke = invoke;
            this.progressBar = progressBar;
            this.console = console;

            bw = new BackgroundWorker();
            bw.ProgressChanged += Bw_ProgressChanged;
            bw.WorkerReportsProgress = true;
        }

        public void ConnectionToServer(string ip, int port)
        {
            IPAddress ipAddr = IPAddress.Parse(ip);
            ipEndPoint = new IPEndPoint(ipAddr, port);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(ipEndPoint);
        }

        public void DisconnectFromServer()
        {
            if(socket !=  null)
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
                socket = null;
            }
        }

        public string[] ReciveFilesList()
        {
            int bytes = 0;
            int packageSize = 0;

            string[] fileList;
            using (MemoryStream memStream = new MemoryStream())
            {
                byte[] buffer = new byte[_BUFFERSIZE];
                do
                {
                    int received = socket.Receive(buffer);
                    memStream.Write(buffer, 0, received);
                    bytes += received;
                    if (bytes >= 2048 && packageSize == 0)
                    {
                        packageSize = GetPackageSize(memStream.ToArray());
                    }
                    if (bytes != 0)
                        bw.ReportProgress((bytes*100)/packageSize);
                }
                while (bytes < packageSize);
                packageSize = 0;
                fileList = GetReciveFilesList(memStream.ToArray());
            }
            Thread.Sleep(progressBarTimeout);

            return fileList;
        }

        public void DownloadFile(string path, string saveTo)
        {

            int packageSize = 0;
            int bytes = 0;

            socket.Send(GetSendPathPackage(path));

            NetFile file;
            using (MemoryStream memStream = new MemoryStream())
            {
                byte[] buffer = new byte[_BUFFERSIZE];
                do
                {
                    int received = socket.Receive(buffer);
                    memStream.Write(buffer, 0, received);
                    bytes += received;
                    if (bytes >= 2048 && packageSize == 0)
                    {
                        packageSize = GetPackageSize(memStream.ToArray());
                    }
                    if (bytes != 0)
                        bw.ReportProgress((bytes * 100) / packageSize);
                }
                while (bytes < packageSize);
                packageSize = 0;
                file = new NetFile(GetReciveFilePackage(memStream.ToArray()));
            }

            using (FileStream stream = new FileStream(saveTo + "\\" + file.FileName, FileMode.Create, FileAccess.Write))
            {
                stream.Write(file.Data, 0, file.Data.Length);
            }

            Thread.Sleep(progressBarTimeout);
        }

        public void DownloadFiles(string[] paths, string saveTo)
        {

            int packageSize = 0;
            int bytes = 0;

            byte[] s = GetSendPathPackage(paths);
            socket.Send(s);

            NetFiles files;
            using (MemoryStream memStream = new MemoryStream())
            {
                byte[] buffer = new byte[_BUFFERSIZE];
                do
                {
                    int received = socket.Receive(buffer);
                    memStream.Write(buffer, 0, received);
                    bytes += received;
                    if (bytes >= 2048 && packageSize == 0)
                    {
                        packageSize = GetPackageSize(memStream.ToArray());
                    }
                    if(bytes != 0)
                        bw.ReportProgress((bytes * 100) / packageSize);
                }
                while (bytes < packageSize);
                packageSize = 0;
                files = new NetFiles(GetReciveFilePackage(memStream.ToArray()));
            }

            foreach (NetFile file in files)
            {
                using (FileStream stream = new FileStream(saveTo+"\\"+file.FileName, FileMode.Create, FileAccess.Write))
                {
                    stream.Write(file.Data, 0, file.Data.Length);
                }
            }

            Thread.Sleep(progressBarTimeout);
        }


        int GetPackageSize(byte[] data)
        {
            byte[] res = new byte[2048];
            Array.Copy(data, 0, res, 0, 2048);
            string s = Encoding.UTF8.GetString(res);
            return Convert.ToInt32(s) + 2048;
        }

        string[] GetReciveFilesList(byte[] data)
        {
            byte[] res = new byte[data.Length - 2048];
            Array.Copy(data, 2048, res, 0, data.Length - 2048);
            string pathsStr = Encoding.UTF8.GetString(res);
            return pathsStr.Split('|');
        }

        byte[] GetSendPathPackage(string[] paths)
        {
            string res = "";
            for (int i = 0; i < paths.Length-1; i++)
            {
                res += paths[i] + "|";
            }
            res += paths[paths.Length-1];

            byte[] pathFlag = new byte[1] { 1 };
            byte[] byteCount = new byte[2048];
            byte[] bCount = Encoding.UTF8.GetBytes(res.Length.ToString());
            byte[] data = Encoding.UTF8.GetBytes(res);
            bCount.CopyTo(byteCount, 0);

            using (MemoryStream memStream = new MemoryStream())
            {
                memStream.Write(pathFlag, 0, pathFlag.Length);
                memStream.Write(byteCount, 0, byteCount.Length);
                memStream.Write(data, 0, data.Length);
                return memStream.ToArray();
            }
        }

        byte[] GetSendPathPackage(string path)
        {
            byte[] pathFlag = new byte[1] { 0 };
            byte[] byteCount = new byte[2048];
            byte[] bCount = Encoding.UTF8.GetBytes(path.Length.ToString());
            byte[] data = Encoding.UTF8.GetBytes(path);
            bCount.CopyTo(byteCount, 0);

            using (MemoryStream memStream = new MemoryStream())
            {
                memStream.Write(pathFlag, 0, pathFlag.Length);
                memStream.Write(byteCount, 0, byteCount.Length);
                memStream.Write(data, 0, data.Length);
                return memStream.ToArray();
            }
        }

        byte[] GetReciveFilePackage(byte[] data)
        {
            byte[] res = new byte[data.Length - 2048];
            Array.Copy(data, 2048, res, 0, data.Length - 2048);
            return res;
        }


        private void Bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Invoke(new Action(() =>
            {
                progressBar.Value = e.ProgressPercentage;
            }));
        }

        void ConsoleWrite(string text)
        {
            Invoke(new Action(() =>
            {
                console.Text += string.Format("\n[{0}] - {1}", DateTime.Now.ToString("HH:mm"), text);
                console.SelectionStart = console.Text.Length;
                console.ScrollToCaret();
            }));
        }
    }
}
