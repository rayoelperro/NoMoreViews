using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace NoMoreViews
{
    public partial class Menu : Form
    {
        private Encoder inc;

        public Menu(string pass)
        {
            InitializeComponent();
            inc = new Encoder(pass);
            textBox1.Focus();
        }

        public Menu()
        {
            InitializeComponent();
            inc = new Encoder();
            textBox1.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (new FileInfo(textBox1.Text).Exists)
                inc.FileEncode(textBox1.Text);
            else
                MessageBox.Show("El archivo no existe");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (new FileInfo(textBox1.Text).Exists)
                if (new FileInfo(textBox1.Text).Extension == ".nmv")
                    inc.FileDecode(textBox1.Text);
                else
                    MessageBox.Show("El archivo no tiene un formato valido");
            else
                MessageBox.Show("El archivo no existe"); 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            inc.FolderEncode(textBox1.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            inc.FolderDecode(textBox1.Text);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog of = new FolderBrowserDialog();
            if (of.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = of.SelectedPath;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            if (of.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = of.FileName;
            }
        }
    }
}
