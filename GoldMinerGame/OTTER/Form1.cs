using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OTTER
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        string name;
        private void buttonStart_Click(object sender, EventArgs e)
        {
            name = textBoxIme.Text;
            if (name != "")
            {
                if (name.Contains(" "))
                {
                    MessageBox.Show("Ime mora biti jedna riječ");
                    return;
                }
                this.Hide();
                BGL bgl = new BGL(name,1,0,6); //prima ime, trenutni level, bodove i preostale zivote
                BGL.allSprites.Clear();
                bgl.ShowDialog();

                bgl.Dispose();

                this.Close();
            }
            
            else
            {
                MessageBox.Show("Niste unijeli ime :)");
                return;
            }

        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Klikom miša odredi smjer gdje će se kretati lopata.\n" +
                "Skupi što više novčića i poluga u 30 sekundi,\na izbjegavaj " +
                "bombe i miševe koji ti\noduzimaju živote.\n\n" +
                "Poluga - 10 bodova\n" +
                "Veliki novčić - 5 bodova\n" +
                "Mali novčić - 1 bod\n" +
                "Bomba - oduzima 1 život\n" +
                "Miš - oduzima 2 života\n\n" +
                "Ako na kraju levela imaš svih 6 života,\ndobit ćeš 10 bonus bodova :)" +
                "\n\n" +
                "SRETNO!");
        }

        private void button3_Click(object sender, EventArgs e)
        {
        }
    }
}
