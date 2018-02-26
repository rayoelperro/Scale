using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scale
{
    class ScaleWriter
    {
        private string Path;
        private int[] Config;
        private string Message;
        private readonly string[] gtype = { "text", "c#", "java", "python" };

        public ScaleWriter(string Path)
        {
            this.Path = Path;
        }

        public ScaleWriter()
        {
        }

        public void SetPath(string Path)
        {
            this.Path = Path;
        }

        public void SetConfig(string first)
        {
            Config = ParseConfig(first);
        }

        public void SetConfig(string first, string second)
        {
            Config = ParseConfig(first);
        }

        public void SetConfig(string first, string second, string third)
        {
            Config = ParseConfig(first);
        }

        private int[] ParseConfig(string first)
        {
            return new int[] {gtype.ToList().IndexOf(first),0,0};
        }

        public void InsertMessage(string message)
        {
            Message = message;
        }

        public void Save()
        {
            Encrypt().Save(Path);
        }

        public Bitmap Encrypt()
        {
            List<byte> lb = new List<byte>(Encoding.UTF8.GetBytes(Message));
            while (lb.Count % 3 != 0)
                lb.Add(0);
            int size = lb.Count / 3;
            Bitmap tr = new Bitmap(size+1, 1);
            foreach (int i in Config.Reverse())
                lb.Insert(0, (byte)i);
            int index = 0;
            int[] actual = new int[3];
            int x = 0;
            for (int k = 0; k < lb.Count; k++)
            {
                actual[index] = lb[k];
                if(index == 2)
                {
                    tr.SetPixel(x, 0, Color.FromArgb(actual[0], actual[1], actual[2]));
                    x++;
                    index = 0;
                    continue;
                }
                index++;
            }
            return tr;
        }
    }
}
