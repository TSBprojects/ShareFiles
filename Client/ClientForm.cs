using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class ClientForm : Form
    {
        Client client;
        FilesForm ff;
        bool correctPort;
        bool correctIP;
        string[] filesPath;
        public string saveToPath;
        public string[] selectedfilesPath;

        public ClientForm()
        {
            InitializeComponent();
            client = new Client(Invoke,toolStripProgressBar1,conslole);
        }

        private void ipTextbox_TextChanged(object sender, EventArgs e)
        {
            if (!IsCorrectIP(ipTextbox.Text))
            {
                ipTextbox.ForeColor = Color.Red;
            }
            else
            {
                ipTextbox.ForeColor = Color.Black;
            }
        }

        private void portTextbox_TextChanged(object sender, EventArgs e)
        {
            if (!IsCorrectPort(portTextbox.Text))
            {
                portTextbox.ForeColor = Color.Red;
            }
            else
            {
                portTextbox.ForeColor = Color.Black;
            }
        }

        private void showFiles_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(delegate()
            {
                selectedfilesPath = null;
                string[] fullPaths;
                if (IsFieldsCorrect())
                {
                    try
                    {
                        ConsoleWrite("Соединяемся...");
                        client.ConnectionToServer(ipTextbox.Text, Convert.ToInt32(portTextbox.Text));
                        ConsoleWrite("Соединение установлено");
                        ConsoleWrite("Скачиваем список файлов...");
                        filesPath = client.ReciveFilesList();
                        ConsoleWrite("Список файлов получен");
                        Invoke(new Action(() =>
                        {
                            toolStripProgressBar1.Value = 0;
                        }));
                        FillFileList();
                        //ff.ShowDialog();

                        Invoke(new Action(() =>
                        {
                            ff.ShowDialog();
                        }));
                    }
                    catch (Exception ex)
                    {
                        Invoke(new Action(() =>
                        {
                            toolStripProgressBar1.Value = 0;
                        }));
                        ConsoleWrite(ex.Message);
                        return;
                    }

                    if (selectedfilesPath != null)
                    {
                        fullPaths = GetFullPathsFromFileList(selectedfilesPath);
                        if (fullPaths.Length > 1)
                        {
                            try
                            {
                                ConsoleWrite("Cкачиваем...");
                                client.DownloadFiles(fullPaths, saveToPath);
                                ConsoleWrite("Файлы загружены в папку " + saveToPath);
                                Invoke(new Action(() =>
                                {
                                    toolStripProgressBar1.Value = 0;
                                }));
                                client.DisconnectFromServer();
                            }
                            catch (Exception ex)
                            {
                                Invoke(new Action(() =>
                                {
                                    toolStripProgressBar1.Value = 0;
                                }));
                                ConsoleWrite(ex.Message);
                            }
                        }
                        else if (fullPaths.Length == 1)
                        {
                            try
                            {
                                ConsoleWrite("Cкачиваем...");
                                client.DownloadFile(fullPaths[0], saveToPath);
                                ConsoleWrite("Файл загружен в папку " + saveToPath);
                                Invoke(new Action(() =>
                                {
                                    toolStripProgressBar1.Value = 0;
                                }));
                                client.DisconnectFromServer();
                            }
                            catch (Exception ex)
                            {
                                Invoke(new Action(() =>
                                {
                                    toolStripProgressBar1.Value = 0;
                                }));
                                ConsoleWrite(ex.Message);
                            }
                        }
                    }
                    else
                    {
                        client.DisconnectFromServer();
                    }
                }
            });
            t.SetApartmentState(ApartmentState.STA);
            t.Start();          
        }


        bool IsFieldsCorrect()
        {
            if (!correctIP && !correctPort)
            {
                ConsoleWrite("Некорректные port и ip");
                return false;
            }
            else
            {
                if (correctPort)
                {
                    if (correctIP)
                    {
                        return true;
                    }
                    else
                    {
                        ConsoleWrite("Некорректный ip");
                        return false;
                    }
                }
                else
                {
                    ConsoleWrite("Некорректный port");
                    return false;
                }
            }
        }

        string[] GetFullPathsFromFileList(string[] paths)
        {
            List<string> res = new List<string>();
            for (int i = 0; i < filesPath.Length; i++)
            {
                for (int j = 0; j < paths.Length; j++)
                {
                    if (Path.GetFileName(filesPath[i]) == paths[j] && !res.Any(p => Path.GetFileName(p) == paths[j]))
                    {
                        res.Add(filesPath[i]);
                    }
                }
            }
            return res.ToArray();
        }

        void FillFileList()
        {
            ff = new FilesForm(this);
            foreach (string path in filesPath)
            {
                ff.filesListBox.Items.Add(Path.GetFileName(path));
            }
        }

        bool IsCorrectPort(string port)
        {
            if (Regex.IsMatch(port, @"^\d+$"))
            {
                int p = int.Parse(port);
                if ((p > 0) && (p <= 65535))
                {
                    correctPort = true;
                    return true;
                }
                else
                {
                    correctPort = false;
                    return false;
                }
            }
            else
            {
                correctPort = false;
                return false;
            }
        }

        bool IsCorrectIP(string ip)
        {
            if (Regex.IsMatch(ip, @"^(25[0-5]|2[0-4][0-9]|[0-1][0-9]{2}|[0-9]{2}|[0-9])(\.(25[0-5]|2[0-4][0-9]|[0-1][0-9]{2}|[0-9]{2}|[0-9])){3}$"))
            {
                correctIP = true;
                return true;
            }
            else
            {
                correctIP = false;
                return false;
            }
        }

        void ConsoleWrite(string text)
        {
            Invoke(new Action(() =>
            {
                conslole.Text += string.Format("\n[{0}] - {1}", DateTime.Now.ToString("HH:mm"), text);
                conslole.SelectionStart = conslole.Text.Length;
                conslole.ScrollToCaret();
            }));
        }

        private void ClientForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            client.DisconnectFromServer();
        }
    }
}
