using Microsoft.VisualBasic;
using RPiRgbLEDMatrix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace qlocktwo
{
    internal class Panel
    {
        private byte brightness = 0;
        private RGBLedMatrix matrix;
        private RGBLedCanvas canvas;
        RPiRgbLEDMatrix.Color color_active = new RPiRgbLEDMatrix.Color(240, 3, 252);
        RPiRgbLEDMatrix.Color color_not_active = new RPiRgbLEDMatrix.Color(52, 225, 235);

        private RGBLedFont font = new RGBLedFont("/home/pi/rpi-rgb-led-matrix/fonts/4x6.bdf");

        // Array, das die aktive Farbe für die unterschiedlichen Panel enthält
        RPiRgbLEDMatrix.Color[] ar_color_active =
            {
                new RPiRgbLEDMatrix.Color(240, 3, 252),
                new RPiRgbLEDMatrix.Color(58, 168, 50),
                new RPiRgbLEDMatrix.Color(189, 19, 19)
               
            };

        readonly private char[,] chars_clock =
            {
                {'E','S','K','I','S','T','A','F','Ü','N','F'},
                {'Z','E','H','N','Z','W','A','N','Z','I','G'},
                {'D','R','E','I','V','I','E','R','T','E','L'},
                {'V','O','R','F','U','N','K','N','A','C','H'},
                {'H','A','L','B','A','E','L','F','Ü','N','F'},
                {'E','I','N','S','X','A','M','Z','W','E','I'},
                {'D','R','E','I','P','M','J','V','I','E','R'},
                {'S','E','C','H','S','N','L','A','C','H','T'},
                {'S','I','E','B','E','N','Z','W','Ö','L','F'},
                {'Z','E','H','N','E','U','N','K','U','H','R'},
            };

        // Konstruktor RGBLedCanvas canvas,
        public Panel(RGBLedMatrix matrix, byte brightness)
        {
            this.matrix = matrix;
            this.brightness = brightness;
            canvas = matrix.CreateOffscreenCanvas();

        }

        public void Draw(byte panel,  bool[,] mapping_time, int hours, int minutes, int seconds, DayOfWeek dayOfWeek, int day, int month, int year, bool switchFlag)
        {
            var picture = new Picture();
            int offset_panel = 0;
            brightness = SetBrightness(0.75, hours);
             
            // Darstellung der Uhrzeit auf dem Panel
            switch(panel-1)
            {
                case 0:
                    Bars_for_minutes(minutes, offset_panel, true, false);
                    break;
                case 1:
                    offset_panel += 64;
                    Bars_for_minutes(minutes, offset_panel, false, false);
                    break;
                case 2:
                    offset_panel += 128;
                    Bars_for_minutes(minutes, offset_panel, false, true);
                    break;
            }

            Text(mapping_time, offset_panel, ar_color_active[panel - 1]);
            Seconds(seconds, offset_panel);

            // Darstellung Bild auf Panel
            offset_panel += 64;
            canvas = picture.drawPicture(canvas, offset_panel);

            // Zeile unterhalb des Bildes
            canvas = lineBelowPicture(canvas, offset_panel, switchFlag, dayOfWeek, day, month, year);


            // Die "Leinwand" auf das Panel übertragen
            matrix.SwapOnVsync(canvas);
            brightness = main.Brightness;
            canvas.Clear();

            //Console.WriteLine(DataExchange.instance.roomtemperature);
        }


        // Anpassung der Helligkeit an die Tageszeit, mit "scale" Anpassung der Helligkeit für komplette Anzeige
        private byte SetBrightness(double scale, int hours)
        {
            byte byBrightness = 0;

            if (!(hours >= 8 && hours <= 19)) scale *= 0.75;
            byBrightness = ScaleBrightness(scale);

            return byBrightness;
        }

        private byte ScaleBrightness(double scale)
        {
            double doBrightness = 0.0d;
            byte byBrightness = 0;
            
            doBrightness = this.brightness * scale;
            if (doBrightness > 100) doBrightness = 100;
            byBrightness = (byte) doBrightness;
            
            return byBrightness;
        }

        #region methods qlocktwo

        private void Text(bool[,] mapping_time, int offset_panel, RPiRgbLEDMatrix.Color color_active)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    if (mapping_time[i, j])
                    {
                        matrix.Brightness = ScaleBrightness(1.5);
                        canvas.DrawText(font, (5 + 5 * j + offset_panel), (7 + 6 * i), color_active, chars_clock[i, j].ToString());
                    }
                    else
                    {
                        matrix.Brightness = brightness;
                        canvas.DrawText(font, (5 + 5 * j + offset_panel), (7 + 6 * i), color_not_active, chars_clock[i, j].ToString());
                    }
                }
            }
            matrix.Brightness = brightness;
        }

        private void Bars_for_minutes(int minutes, int offset_panel, bool diplay_right, bool display_left)
        {
            int x0 = 0 + offset_panel;
            int x1 = 1 + offset_panel;
            int x62 = 62 + offset_panel;
            int x63 = 63 + offset_panel;

            // rechte Balken
            if (diplay_right)
            {
                switch (minutes % 5)
                {
                    case 0:
                        break;
                    case 1:
                        //rechte Balken
                        canvas.DrawLine(x0, 48, x0, 60, color_not_active);
                        canvas.DrawLine(x1, 48, x1, 60, color_not_active);
                        goto case 0;
                    case 2:
                        //rechte Balken
                        canvas.DrawLine(x0, 33, x0, 45, color_not_active);
                        canvas.DrawLine(x1, 33, x1, 45, color_not_active);
                        goto case 1;
                    case 3:
                        //rechte Balken
                        canvas.DrawLine(x0, 18, x0, 30, color_not_active);
                        canvas.DrawLine(x1, 18, x1, 30, color_not_active);
                        goto case 2;
                    case 4:
                        //rechte Balken
                        canvas.DrawLine(x0, 3, x0, 15, color_not_active);
                        canvas.DrawLine(x1, 3, x1, 15, color_not_active);
                        goto case 3;
                }
            }

            if (display_left)
            {
                switch (minutes % 5)
                {
                    case 0:
                        break;
                    case 1:
                        //link Balken
                        canvas.DrawLine(x62, 2, x62, 14, color_not_active);
                        canvas.DrawLine(x63, 2, x63, 14, color_not_active);
                        goto case 0;
                    case 2:
                        //link Balken
                        canvas.DrawLine(x62, 17, x62, 29, color_not_active);
                        canvas.DrawLine(x63, 17, x63, 29, color_not_active);
                        goto case 1;
                    case 3:
                        //link Balken
                        canvas.DrawLine(x62, 32, x62, 44, color_not_active);
                        canvas.DrawLine(x63, 32, x63, 44, color_not_active);
                        goto case 2;
                    case 4:
                        //link Balken
                        canvas.DrawLine(x62, 47, x62, 59, color_not_active);
                        canvas.DrawLine(x63, 47, x63, 59, color_not_active);
                        goto case 3;
                }
            }


        }

        private void Seconds(int seconds, int offset_panel) 
        {
            for (int i = 0; i < seconds; i++) 
            {
                canvas.SetPixel(3 + i + offset_panel, 63, color_not_active);
            }
        }

        #endregion

        #region methods for the rest of the panel
        private RGBLedCanvas weekdays(RGBLedCanvas canvas, int offset, DayOfWeek dayOfWeek)
        {
            int counter = 0;
            string weekdays = "MO DI MI DO FR SA SO";
            bool[] activeDay = new bool[20];

            for (int i = 0; i < 20; i++)
            {
                activeDay[i] = false;
            }

            switch (dayOfWeek)
            {
                case DayOfWeek.Monday:
                    activeDay[0] = true;
                    activeDay[1] = true;
                    break;
                case DayOfWeek.Tuesday:
                    activeDay[3] = true;
                    activeDay[4] = true;
                    break;
                case DayOfWeek.Wednesday:
                    activeDay[6] = true;
                    activeDay[7] = true;
                    break;
                case DayOfWeek.Thursday:
                    activeDay[9] = true;
                    activeDay[10] = true;
                    break;
                case DayOfWeek.Friday:
                    activeDay[12] = true;
                    activeDay[13] = true;
                    break;
                case DayOfWeek.Saturday:
                    activeDay[15] = true;
                    activeDay[16] = true;
                    break;
                case DayOfWeek.Sunday:
                    activeDay[18] = true;
                    activeDay[19] = true;
                    break;
            }

            foreach (char cha in weekdays)
            {
                if (activeDay[counter])
                    canvas.DrawText(font, offset, 61, color_active, cha.ToString());
                else
                    canvas.DrawText(font, offset, 61, color_not_active, cha.ToString());

                offset += 4;
                counter++;
            }

            return canvas;
        }

        private RGBLedCanvas date(RGBLedCanvas canvas, int offset, int day, int month, int year)
        {
            string date = $"{day}.{month}.{year}";
            canvas.DrawText(font, 0 + offset, 61, color_not_active, date);
            return canvas;
        }

        private RGBLedCanvas temperature(RGBLedCanvas canvas, int offset)
        {
            if (DataExchange.instance.roomtemperature == "NoData")
            {
                canvas.DrawText(font, 0 + offset, 61, color_not_active, "No Data");
                return canvas;
            }

            string indoorTemperature = DataExchange.instance.roomtemperature.Substring(12, 6);
            string indoorHumidity = DataExchange.instance.roomtemperature.Substring(33,6);

            canvas.DrawText(font, 0 + offset, 61, color_active, $"{indoorTemperature} {indoorHumidity}");
            canvas.DrawText(font, 60 + offset, 61, color_not_active, DataExchange.instance.weather);

            return canvas;
        }

        private RGBLedCanvas lineBelowPicture(RGBLedCanvas canvas, int offset, bool switchFlag, DayOfWeek dayOfWeek, int day, int month, int year)
        {
            var font = new RGBLedFont("/home/pi/rpi-rgb-led-matrix/fonts/4x6.bdf");
            if (switchFlag)
            {
                // Darstellung Wochentage auf Panel
                canvas = weekdays(canvas, offset, dayOfWeek);

                // Darstellung des Datums auf Panel
                canvas = date(canvas, offset + 93, day, month, year);
            }
            else
            {
                canvas = temperature(canvas, offset);
            }
            return canvas;
        }




        #endregion


    }

}
