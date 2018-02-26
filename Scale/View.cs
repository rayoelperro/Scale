using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scale
{
    public partial class View : Form
    {
        public View()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sv = new SaveFileDialog();
                sv.Filter = "PNG Image (*.png*)|*.png*";
                sv.DefaultExt = "png";
                sv.AddExtension = true;
                if (sv.ShowDialog() == DialogResult.OK)
                {
                    ScaleWriter sw = new ScaleWriter(sv.FileName);
                    sw.InsertMessage(richTextBox1.Text);
                    sw.SetConfig("text");
                    sw.Save();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog op = new OpenFileDialog();
                op.Filter = "PNG Image (*.png*)|*.png*";
                if (op.ShowDialog() == DialogResult.OK)
                {
                    ScaleReader sr = new ScaleReader(op.FileName);
                    richTextBox1.Text = sr.Decrypt();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void displayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ScaleWriter sd = new ScaleWriter();
                sd.InsertMessage(richTextBox1.Text);
                sd.SetConfig("text");
                Form two = new Form()
                {
                    Text = "Image Display",
                    BackgroundImageLayout = ImageLayout.Stretch,
                    BackgroundImage = sd.Encrypt(),
                    Icon = ((Icon)(new ComponentResourceManager(typeof(View)).GetObject("$this.Icon")))
                };
                two.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void uTF8TranslatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                new utf8_translator().ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
