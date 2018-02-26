using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scale
{
    public partial class utf8_translator : Form
    {
        private bool IsInText = true;

        public utf8_translator()
        {
            InitializeComponent();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void fromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "All Files (*.*)|*.*";
            if (op.ShowDialog() == DialogResult.OK)
            {
                rText.Text = File.ReadAllText(op.FileName);
                rText_TextChanged(null, null);
            }
        }

        private void rText_TextChanged(object sender, EventArgs e)
        {
            if(IsInText)
                rBytes.Text = String.Join(" ",Encoding.UTF8.GetBytes(rText.Text));
        }

        private void rBytes_TextChanged(object sender, EventArgs e)
        {
            if (!IsInText)
            {
                try
                {
                    List<byte> bytes = new List<byte>();
                    foreach (string b in rBytes.Text.Split(' '))
                        bytes.Add(byte.Parse(b));
                    rText.Text = Encoding.UTF8.GetString(bytes.ToArray());
                }
                catch (Exception ex)
                {
                    rText.Text = ex.Message;
                }
            }
        }

        private void rText_Click(object sender, EventArgs e)
        {
            IsInText = true;
        }

        private void rBytes_Click(object sender, EventArgs e)
        {
            IsInText = false;
        }
    }
}
