using System;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace Server
{

    public delegate void OnPreExecute();
    public delegate void DoInBackground();
    public delegate void OnPostExecute();


    public partial class ServerForm : Form
    {
        int timer = 0;
        Server server;
        bool portCorrect = false;
        public ServerForm()
        {
            InitializeComponent();
            IPAddress[] addr = Dns.GetHostEntry(Dns.GetHostName()).AddressList;
            localip.Text = addr.Last().ToString();
            filesDirPath.Text = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            Thread load = new Thread(delegate ()
            {
                string ip = GetIP("https://2ip.ru/");
                timer1.Stop();
                Invoke(new Action(() =>
                {
                    internetip.Text = ip;
                }));
            });
            load.Start();
        }

        private void startServer_Click(object sender, EventArgs e)
        {
            if(portCorrect)
            {
                if(server == null)
                {
                    server = new Server(Invoke, conslole);
                    Thread serverListen = new Thread(delegate()
                    {
                        try
                        {
                            server.SetFilesDirectory(filesDirPath.Text);
                            server.Start(Convert.ToInt32(port.Text));
                        }
                        catch (Exception ex)
                        {
                            //MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            server = null;
                            Invoke(new Action(() =>
                            {
                                ConsoleWrite(ex.Message);
                            }));
                        }
                    });
                    serverListen.Start();
                    ConsoleWrite("Сервер слушает порт "+port.Text);
                }
                else
                {
                    ConsoleWrite("Сервер уже запущен");
                }
            }
            else
            {
                ConsoleWrite("Некорректный port");
            }
        }

        private void stopServer_Click(object sender, EventArgs e)
        {
            StopServer();
        }

        private void port_TextChanged(object sender, EventArgs e)
        {
            if(IsCorrectPort(port.Text))
            {
                port.ForeColor = Color.Black;
            }
            else
            {
                port.ForeColor = Color.Red;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                filesDirPath.Text = folderBrowserDialog1.SelectedPath;
                if(server != null) server.SetFilesDirectory(filesDirPath.Text);
            }
        }

        void ConsoleWrite(string text)
        {
            conslole.Text += string.Format("\n[{0}] - {1}", DateTime.Now.ToString("HH:mm"), text);
            conslole.SelectionStart = conslole.Text.Length;
            conslole.ScrollToCaret();
        }

        bool IsCorrectPort(string port)
        {
            if (Regex.IsMatch(port, @"^\d+$"))
            {
                int p = int.Parse(port);
                if ((p > 0) && (p <= 65535))
                {
                    portCorrect = true;
                    return true;
                }
                else
                {
                    portCorrect = false;
                    return false;
                }
            }
            else
            {
                portCorrect = false;
                return false;
            }
        }

        private void ServerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopServer();
        }

        private void StopServer()
        {
            if (server != null)
            {
                server.Stop();
                server = null;
                ConsoleWrite("Сервер остановлен");
            }
            else
            {
                ConsoleWrite("Сервер уже отключён");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (timer)
            {
                case (0): { internetip.Text = "."; break; }
                case (1): { internetip.Text = ".."; break; }
                case (2): { internetip.Text = "..."; break; }
                case (3): { internetip.Text = "...."; break; }
                case (4): { internetip.Text = "......"; break; }
                case (5): { internetip.Text = "......."; break; }
            }
            timer++;
            if (timer == 6) timer = 0;
        }

        private string GetIP(string uri)
        {
            string html;
            try
            {
                 html = new WebClient().DownloadString(uri);
            }
            catch
            {
                return "сервер не ответил";
            }
            string div = Regex.Match(html, "<big .+d_clip_button.+>.+</big>").Value;
            string ip = Regex.Match(div, "(<big .+?>)(.+)(<.?big>)").Groups[2].Value;
            return ip;
        }



        public void Task(OnPreExecute onPreExecute, DoInBackground doInBackground, OnPostExecute onPostExecute)
        {
            onPreExecute();
            Thread t = new Thread(() =>
            {
                doInBackground();
                Invoke(new Action(() =>
                {
                    onPostExecute();
                }));
            });
            t.Start();
        }
    }
}
