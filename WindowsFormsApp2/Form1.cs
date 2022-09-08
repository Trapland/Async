using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        string filename_open;
        string filename_save;
        char []buffer = new char[4096];
        public Form1()
        {
            InitializeComponent();
        }
        private  async void button1_Click(object sender, EventArgs e)
        {
            FileInfo file = new FileInfo(filename_open);
            progressBar1.Maximum = (((int)file.Length) / 4096 + 1) * 2;
            Task t1 = Task.Run(() => Next1()); // выполняется асинхронно
            Task t2 =  Task.Run(() => Next2()); // выполняется асинхронно
            await Task.WhenAll(new[] { t1, t2});
        }
        private void Next1()
        {
            StreamReader sr = new StreamReader(filename_open, Encoding.UTF8);
            for(int i=0;i<progressBar1.Maximum / 2;i++)
            {
                sr.Read(buffer,0,4096);
                progressBar1.Value++;
                Thread.Sleep(50);
            }
            sr.Close();
        }

        private void Next2()
        {
            StreamWriter sw = new StreamWriter(filename_save, false);
            for (int i = 0; i < progressBar1.Maximum / 2; i++)
            {
                sw.Write(buffer);
                progressBar1.Value++;
                Thread.Sleep(50);
            }
            sw.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            filename_open = openFileDialog1.FileName;
            textBox1.Text = filename_open;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            filename_save = openFileDialog1.FileName;
            textBox2.Text = filename_save;
        }
    }
}
