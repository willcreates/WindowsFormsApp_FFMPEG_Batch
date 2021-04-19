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
                    mjpegFiles.Add(s);
                }
                System.Windows.Forms.MessageBox.Show("Files found: " + files.Length.ToString(), "Message");
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
            //ProcessStartInfo startInfo = new ProcessStartInfo();
            //startInfo.WorkingDirectory = @"C:\mjpeg mp4 test files";
            //startInfo.CreateNoWindow = false;
            //startInfo.UseShellExecute = false;
            //startInfo.FileName = @"C:\ffmpeg-4.4-full_build\bin\ffmpeg.exe";
            //startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            //startInfo.Arguments = @"-i 20210208074105.mjpeg output.mp4";

            //startInfo.RedirectStandardOutput = true;
            //startInfo.RedirectStandardError = true;

            //using(Process exeProcess = Process.Start(startInfo))
            //{
            //    string error = exeProcess.StandardError.ReadToEnd();
            //    string output = exeProcess.StandardError.ReadToEnd();
            //    exeProcess.WaitForExit();

            //    MessageBox.Show("Error:" + error);
            //    MessageBox.Show("Output:" + output);
            //}

            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo
            {
                WorkingDirectory = textBox1.Text,
                //WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden,
                FileName = textBox2.Text + @"\ffmpeg.exe"
            };
            string cmdToSend = @"-i 20210208074105.mjpeg output.mp4";
            startInfo.Arguments = cmdToSend;
            process.StartInfo = startInfo;
            process.Start();
        }
    }
}
