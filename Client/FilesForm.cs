using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class FilesForm : Form
    {
        ClientForm cf;
        public FilesForm(ClientForm cf)
        {
            this.cf = cf;
            InitializeComponent();
        }

        private void download_Click(object sender, EventArgs e)
        {        
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                cf.selectedfilesPath = new string[filesListBox.SelectedItems.Count];
                for (int i = 0; i < filesListBox.SelectedItems.Count; i++)
                {
                    cf.selectedfilesPath[i] = filesListBox.SelectedItems[i].ToString();
                }
                cf.saveToPath = folderBrowserDialog1.SelectedPath;
                Close();
            }
          
        }

        private void filesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(filesListBox.SelectedItems.Count == 0)
            {
                download.Enabled = false;
            }
            else
            {
                download.Enabled = true;
            }
        }
    }
}
