using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NoMoreViews
{
    public partial class Password : Form
    {
        public Password()
        {
            InitializeComponent();
            textBox1.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Menu m = new Menu(textBox1.Text);
            m.Show();
            m.FormClosing += (se, ne) =>
            {
                Close();
            };
            Hide();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = e.SuppressKeyPress = true;
                button1_Click(null, null);
            }
        }
    }
}
