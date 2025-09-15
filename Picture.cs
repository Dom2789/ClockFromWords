using RPiRgbLEDMatrix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qlocktwo
{
    internal class Picture
    {
        // Deklaration eines zweidimensionalen Arrays, in dem jedes Element ein Array aus drei Integer-Werten ist
        private int[,][] pixelRbgValues = new int[128, 54][];

        // Initialisierung des Arrays pixelRbgValues
        public Picture(string path)
        {            
            string[] lines = File.ReadAllLines(path);
            bool first = true;
            int xMax, yMax, x, y, red, green, blue;

            foreach (var line in lines)
            {
                if (first)
                {
                    first = false;
                    xMax = int.Parse(line.Split(';')[1]);
                    yMax = int.Parse(line.Split(';')[2]);
                    if (xMax != 128 || yMax != 54)
                    {
                        throw new Exception("Invalid dimensions");
                    }
                    continue;
                }
            
                string[] parts = line.Split(',');
                x = int.Parse(parts[0]);
                y = int.Parse(parts[1]);
                red = int.Parse(parts[2]);
                green = int.Parse(parts[3]);
                blue = int.Parse(parts[4]);

                pixelRbgValues[x, y] = [red, green, blue];

            }
        }

        public RGBLedCanvas drawPicture(RGBLedCanvas canvas, int offset)
        {
            RPiRgbLEDMatrix.Color color;

            for (int x = 0; x < 128; x++)
            {
                for (int y = 0; y < 54; y++)
                {
                    color = new RPiRgbLEDMatrix.Color(pixelRbgValues[x, y][0], pixelRbgValues[x, y][1], pixelRbgValues[x, y][2]);
                    canvas.SetPixel(x+offset, y, color);
                }
            }
            return canvas;
        }
    }

}

