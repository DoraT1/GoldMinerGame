using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;

namespace OTTER
{
    public partial class Form3 : Form
    {
        public string ime;
        public int bodovi;
        string datoteka = "ranglista.txt";

        public Form3(string i, int b)
        {
            InitializeComponent();

            this.ime = i;
            this.bodovi = b;


            using (StreamReader sr = File.OpenText(datoteka))
            {
                string linija = sr.ReadLine();
                while (linija != null)
                {
                    string[] niz = linija.Split(' ');
                    string igrac = niz[0];
                    int bodovi = int.Parse(niz[1]);
                    listBox1.Items.Add(linija + "\n");
                    linija = sr.ReadLine();
                }
            }
        }

        private void btnRetry_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Restart();

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            StreamWriter sr = File.CreateText(datoteka);
            sr.Flush();
            sr.Close();
            Application.Exit();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
    }
}
