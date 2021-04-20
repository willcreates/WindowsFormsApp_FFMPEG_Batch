using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace WindowsFormsApp_FFMPEG_Batch
{
    public partial class Form1 : Form
    {
        private List<string> mjpegFiles = new List<string>();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            DialogResult dialogResult = folderBrowserDialog.ShowDialog();
            if (dialogResult == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
            {
                textBox1.Text = folderBrowserDialog.SelectedPath;
                button2.Enabled = true;
                string[] files = Directory.GetFiles(folderBrowserDialog.SelectedPath);
                mjpegFiles.Clear();
                foreach(string s in files)
                {
                    string filename = Path.GetFileName(s);
                    mjpegFiles.Add(filename);
                }
                System.Windows.Forms.MessageBox.Show("Files found: " + files.Length.ToString(), "Message");

                foreach(string file in mjpegFiles)
                {
                    Console.WriteLine(file);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            DialogResult dialogResult = folderBrowserDialog.ShowDialog();
            if (dialogResult == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
            {
                textBox2.Text = folderBrowserDialog.SelectedPath;
                MessageBox.Show(folderBrowserDialog.SelectedPath);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo
            {
                WorkingDirectory = textBox1.Text,
                WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden,
                FileName = textBox2.Text + @"\ffmpeg.exe"
            };

            progressBar1.Value = 0;
            progressBar1.Maximum = mjpegFiles.Count();
            progressBar1.Step = 1;            

            foreach(string file in mjpegFiles)
            {
                string cmdToSend = "-i " + file + " " + Path.GetFileNameWithoutExtension(file) + ".mp4";
                startInfo.Arguments = cmdToSend;
                process.StartInfo = startInfo;
                process.Start();
                progressBar1.Value += 1;
            }
        }
    }
}
