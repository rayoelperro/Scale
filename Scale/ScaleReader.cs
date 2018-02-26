using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Scale
{
    class ScaleReader
    {
        private Bitmap source;
        private int[,] Cords;

        private readonly string[] gtype = {"text","c#","java","python"};

        public ScaleReader(string path)
        {
            source = new Bitmap(Image.FromFile(path));
            Cords = ColorsToCords(GetColors());
        }

        public string GetFormatType()
        {
            return gtype[Cords[0, 0]];
        }

        public string Decrypt()
        {
            string final = "";
            if(GetFormatType() == "text")
            {
                List<byte> by = new List<byte>();
                for(int x = 1; x < Cords.GetUpperBound(0) + 1; x++)
                    for(int y = 0; y < 3; y++)
                        by.Add((byte)Cords[x, y]);
                final = Encoding.UTF8.GetString(by.ToArray());
            }
            else
            {
                throw new Exception("Formato Desconocido: " + GetFormatType());
            }
            return final;
        }

        private int[,] ColorsToCords(Color[] cls)
        {
            int[,] cords = new int[cls.Length,3];
            for (int w = 0; w < cls.Length; w++)
            {
                cords[w, 0] = cls[w].R;
                cords[w, 1] = cls[w].G;
                cords[w, 2] = cls[w].B;
            }
            return cords;
        }

        private Color[] GetColors()
        {
            List<Color> cls = new List<Color>();
            for (int y = 0; y < source.Height; y++)
            {
                for (int x = 0; x < source.Width; x++)
                {
                    cls.Add(source.GetPixel(x, y));
                }
            }
            return cls.ToArray();
        }
    }
}
