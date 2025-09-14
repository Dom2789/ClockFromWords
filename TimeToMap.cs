using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace qlocktwo
{
    internal class TimeToMap
    {
        #region arrays for minutes
        // oberen fünf Zeilen zur Anzeige der Minuten
        bool[,] es_ist =
            {
                {true, true, false, true, true, true, false, false, false, false, false },
                {false, false, false, false, false, false, false, false, false, false, false },
                {false, false, false, false, false, false, false, false, false, false, false },
                {false, false, false, false, false, false, false, false, false, false, false },
                {false, false, false, false, false, false, false, false, false, false, false },
            };

        bool[,] es_ist_fünf_nach=
            {
                {true, true, false, true, true, true, false, true, true, true, true },
                {false, false, false, false, false, false, false, false, false, false, false },
                {false, false, false, false, false, false, false, false, false, false, false },
                {false, false, false, false, false, false, false, true, true, true, true },
                {false, false, false, false, false, false, false, false, false, false, false },
            };

        bool[,] es_ist_zehn_nach = 
            {
                {true, true, false, true, true, true, false, false, false, false, false },
                {true, true, true, true, false, false, false, false, false, false, false },
                {false, false, false, false, false, false, false, false, false, false, false },
                {false, false, false, false, false, false, false, true, true, true, true },
                {false, false, false, false, false, false, false, false, false, false, false },
            };

        bool[,] es_ist_viertel =
            {
                {true, true, false, true, true, true, false, false, false, false, false },
                {false, false, false, false, false, false, false, false, false, false, false },
                {false, false, false, false, true, true, true, true, true, true, true },
                {false, false, false, false, false, false, false, false, false, false, false },
                {false, false, false, false, false, false, false, false, false, false, false },
            };

        bool[,] es_ist_zwanzig_nach =
            {
                {true, true, false, true, true, true, false, false, false, false, false },
                {false, false, false, false, true, true, true, true, true, true, true },
                {false, false, false, false, false, false, false, false, false, false, false },
                {false, false, false, false, false, false, false, true, true, true, true },
                {false, false, false, false, false, false, false, false, false, false, false },
            };

        bool[,] es_ist_zehn_vor_halb =
    {
                {true, true, false, true, true, true, false, false, false, false, false },
                {true, true, true, true, false, false, false, false, false, false, false },
                {false, false, false, false, false, false, false, false, false, false, false },
                {true, true, true, false, false, false, false, false, false, false, false },
                {true, true, true, true, false, false, false, false, false, false, false },
            };


        bool[,] es_ist_fünf_vor_halb =
            {
                {true, true, false, true, true, true, false, true, true, true, true },
                {false, false, false, false, false, false, false, false, false, false, false },
                {false, false, false, false, false, false, false, false, false, false, false },
                {true, true, true, false, false, false, false, false, false, false, false },
                {true, true, true, true, false, false, false, false, false, false, false },
            };

        bool[,] es_ist_halb =
            {
                {true, true, false, true, true, true, false, false, false, false, false },
                {false, false, false, false, false, false, false, false, false, false, false },
                {false, false, false, false, false, false, false, false, false, false, false },
                {false, false, false, false, false, false, false, false, false, false, false },
                {true, true, true, true, false, false, false, false, false, false, false },
            };

        bool[,] es_ist_fünf_nach_halb =
            {
                {true, true, false, true, true, true, false, true, true, true, true },
                {false, false, false, false, false, false, false, false, false, false, false },
                {false, false, false, false, false, false, false, false, false, false, false },
                {false, false, false, false, false, false, false, true, true, true, true },
                {true, true, true, true, false, false, false, false, false, false, false },
            };

        bool[,] es_ist_zehn_nach_halb =
            {
                {true, true, false, true, true, true, false, false, false, false, false },
                {true, true, true, true, false, false, false, false, false, false, false },
                {false, false, false, false, false, false, false, false, false, false, false },
                {false, false, false, false, false, false, false, true, true, true, true },
                {true, true, true, true, false, false, false, false, false, false, false },
            };

        bool[,] es_ist_dreiviertel = 
            {
                {true, true, false, true, true, true, false, false, false, false, false },
                {false, false, false, false, false, false, false, false, false, false, false },
                {true, true, true, true, true, true, true, true, true, true, true },
                {false, false, false, false, false, false, false, false, false, false, false },
                {false, false, false, false, false, false, false, false, false, false, false },
            };

        bool[,] es_ist_zehn_vor =
            {
                {true, true, false, true, true, true, false, false, false, false, false },
                {true, true, true, true, false, false, false, false, false, false, false },
                {false, false, false, false, false, false, false, false, false, false, false },
                {true, true, true, false, false, false, false, false, false, false, false },
                {false, false, false, false, false, false, false, false, false, false, false },
            };

        bool[,] es_ist_fünf_vor =
            {
                {true, true, false, true, true, true, false, true, true, true, true },
                {false, false, false, false, false, false, false, false, false, false, false },
                {false, false, false, false, false, false, false, false, false, false, false },
                {true, true, true, false, false, false, false, false, false, false, false },
                {false, false, false, false, false, false, false, false, false, false, false },
            };

        #endregion

        #region arrays for hours
        // unteren sechs Zeilen zur Anzeige der Stunden
        bool[,] ein =
    {
                {false, false, false, false, false, false, false, false, false, false, false },
                {true, true, true, false, false, false, false, false, false, false, false },
                {false, false, false, false, false, false, false, false, false, false, false },
                {false, false, false, false, false, false, false, false, false, false, false },
                {false, false, false, false, false, false, false, false, false, false, false },
                {false, false, false, false, false, false, false, false, false, false, false },
            };

        bool[,] eins =
            {
                {false, false, false, false, false, false, false, false, false, false, false },
                {true, true, true, true, false, false, false, false, false, false, false },
                {false, false, false, false, false, false, false, false, false, false, false },
                {false, false, false, false, false, false, false, false, false, false, false },
                {false, false, false, false, false, false, false, false, false, false, false },
                {false, false, false, false, false, false, false, false, false, false, false },
            };

        bool[,] zwei =
            {
                {false, false, false, false, false, false, false, false, false, false, false },
                {false, false, false, false, false, false, false, true, true, true, true },
                {false, false, false, false, false, false, false, false, false, false, false },
                {false, false, false, false, false, false, false, false, false, false, false },
                {false, false, false, false, false, false, false, false, false, false, false },
                {false, false, false, false, false, false, false, false, false, false, false },
            };

        bool[,] drei =
            {
                {false, false, false, false, false, false, false, false, false, false, false },
                {false, false, false, false, false, false, false, false, false, false, false },
                {true, true, true, true, false, false, false, false, false, false, false },
                {false, false, false, false, false, false, false, false, false, false, false },
                {false, false, false, false, false, false, false, false, false, false, false },
                {false, false, false, false, false, false, false, false, false, false, false },
            };

        bool[,] vier =
            {
                {false, false, false, false, false, false, false, false, false, false, false },
                {false, false, false, false, false, false, false, false, false, false, false },
                {false, false, false, false, false, false, false, true, true, true, true },
                {false, false, false, false, false, false, false, false, false, false, false },
                {false, false, false, false, false, false, false, false, false, false, false },
                {false, false, false, false, false, false, false, false, false, false, false },
            };

        bool[,] fünf =
            {
                {false, false, false, false, false, false, false, true, true, true, true },
                {false, false, false, false, false, false, false, false, false, false, false },
                {false, false, false, false, false, false, false, false, false, false, false },
                {false, false, false, false, false, false, false, false, false, false, false },
                {false, false, false, false, false, false, false, false, false, false, false },
                {false, false, false, false, false, false, false, false, false, false, false },
            };

        bool[,] sechs =
            {
                {false, false, false, false, false, false, false, false, false, false, false },
                {false, false, false, false, false, false, false, false, false, false, false },
                {false, false, false, false, false, false, false, false, false, false, false },
                {true, true, true, true, true, false, false, false, false, false, false },
                {false, false, false, false, false, false, false, false, false, false, false },
                {false, false, false, false, false, false, false, false, false, false, false },
            };

        bool[,] sieben =
            {
                {false, false, false, false, false, false, false, false, false, false, false },
                {false, false, false, false, false, false, false, false, false, false, false },
                {false, false, false, false, false, false, false, false, false, false, false },
                {false, false, false, false, false, false, false, false, false, false, false },
                {true, true, true, true, true, true, false, false, false, false, false },
                {false, false, false, false, false, false, false, false, false, false, false },
            };

        bool[,] acht =
            {
                {false, false, false, false, false, false, false, false, false, false, false },
                {false, false, false, false, false, false, false, false, false, false, false },
                {false, false, false, false, false, false, false, false, false, false, false },
                {false, false, false, false, false, false, false, true, true, true, true },
                {false, false, false, false, false, false, false, false, false, false, false },
                {false, false, false, false, false, false, false, false, false, false, false },
            };

        bool[,] neun =
            {
                {false, false, false, false, false, false, false, false, false, false, false },
                {false, false, false, false, false, false, false, false, false, false, false },
                {false, false, false, false, false, false, false, false, false, false, false },
                {false, false, false, false, false, false, false, false, false, false, false },
                {false, false, false, false, false, false, false, false, false, false, false },
                {false, false, false, true, true, true, true, false, false, false, false },
            };

        bool[,] zehn =
            {
                {false, false, false, false, false, false, false, false, false, false, false },
                {false, false, false, false, false, false, false, false, false, false, false },
                {false, false, false, false, false, false, false, false, false, false, false },
                {false, false, false, false, false, false, false, false, false, false, false },
                {false, false, false, false, false, false, false, false, false, false, false },
                {true, true, true, true, false, false, false, false, false, false, false },
            };

        bool[,] elf =
            {
                {false, false, false, false, false, true, true, true, false, false, false },
                {false, false, false, false, false, false, false, false, false, false, false },
                {false, false, false, false, false, false, false, false, false, false, false },
                {false, false, false, false, false, false, false, false, false, false, false },
                {false, false, false, false, false, false, false, false, false, false, false },
                {false, false, false, false, false, false, false, false, false, false, false },
            };

        bool[,] zwölf =
            {
                {false, false, false, false, false, false, false, false, false, false, false },
                {false, false, false, false, false, false, false, false, false, false, false },
                {false, false, false, false, false, false, false, false, false, false, false },
                {false, false, false, false, false, false, false, false, false, false, false },
                {false, false, false, false, false, false, true, true, true, true, true },
                {false, false, false, false, false, false, false, false, false, false, false },
            };

        #endregion

        private bool[,] MapMinutes(int minutes) 
        {
            bool[,] map_minutes = new bool[5, 11];

            switch (minutes)
            {
                case 0:                   
                case 1:
                case 2:
                case 3:
                case 4:
                    map_minutes = es_ist;
                    break;
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                    map_minutes = es_ist_fünf_nach;
                    break;
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                    map_minutes = es_ist_zehn_nach;
                    break;
                case 15:
                case 16:
                case 17:
                case 18:
                case 19:
                    map_minutes = es_ist_viertel;
                    break;
                case 20:
                case 21:
                case 22:
                case 23:
                case 24:
                    map_minutes = es_ist_zehn_vor_halb;
                    break;
                case 25:
                case 26:
                case 27:
                case 28:
                case 29:
                    map_minutes = es_ist_fünf_vor_halb;
                    break;
                case 30:
                case 31:
                case 32:
                case 33:
                case 34:
                    map_minutes = es_ist_halb;
                    break;
                case 35:
                case 36:
                case 37:
                case 38:
                case 39:
                    map_minutes = es_ist_fünf_nach_halb;
                    break;
                case 40:
                case 41:
                case 42:
                case 43:
                case 44:
                    map_minutes = es_ist_zehn_nach_halb;
                    break;
                case 45:
                case 46:
                case 47:
                case 48:
                case 49:
                    map_minutes = es_ist_dreiviertel;
                    break;
                case 50:
                case 51:
                case 52:
                case 53:
                case 54:
                    map_minutes = es_ist_zehn_vor;
                    break;
                case 55:
                case 56:
                case 57:
                case 58:
                case 59:
                    map_minutes = es_ist_fünf_vor;
                    break;
                default:
                    // code block
                    break;
            }

            return map_minutes;
        }

        private bool[,] MapHours(int hours, int minutes)
        {
            // Stunde bestimmen
            bool[,] map_hours = new bool[6, 11];
            if (minutes < 15) map_hours = MapHoursSwitch(hours, minutes);
            else
            {
                hours += 1;
                if (hours == 24) hours = 0;
                map_hours = MapHoursSwitch(hours, minutes);
            }

            return map_hours;
        }

        private bool[,] MapHoursSwitch(int hours, int minutes) 
        {
            bool[,] map_hours = new bool[6, 11];

            switch (hours)
            {
                case 0:
                case 12:
                    map_hours = zwölf;
                    break;
                case 1:
                case 13:
                    if (minutes <= 4 && minutes >= 0) map_hours = ein;
                    else map_hours = eins;
                    break;
                case 2:
                case 14:
                    map_hours = zwei;
                    break;
                case 3:
                case 15:
                    map_hours = drei;
                    break;
                case 4:
                case 16:
                    map_hours = vier;
                    break;
                case 5:
                case 17:
                    map_hours = fünf;
                    break;
                case 6:
                case 18:
                    map_hours = sechs;
                    break;
                case 7:
                case 19:
                    map_hours = sieben;
                    break;
                case 8:
                case 20:
                    map_hours = acht;
                    break;
                case 9:
                case 21:
                    map_hours = neun;
                    break;
                case 10:
                case 22:
                    map_hours = zehn;
                    break;
                case 11:
                case 23:
                    map_hours = elf;
                    break;
                default:
                    // code
                    break;
            }

            return map_hours;
        }


        // takes in current time and generates array of bools for output on panel
        public bool[,] GenerateMap(int hours, int minutes) 
        {
            bool[,] map_minutes = MapMinutes(minutes);
            bool[,] map_hours = MapHours(hours, minutes);

            bool[] uhr = { false, false, false, false, false, false, false, false, true, true, true };
            // create final array from map_minutes[5,11] and map_hours[6,11]
            // row 4 of map has to be OR of both arrays
            bool[,] map = new bool[10, 11];
            for (int i = 0; i < 10; i++)
            {
                if (i < 4)
                {
                    for (int j = 0; j < 11; j++)
                        map[i, j] = map_minutes[i, j];
                }
                // Zeile die sich bei den beiden Arrays überschneidet verodern
                else if (i == 4)
                {
                    for (int j = 0; j < 11; j++)
                        map[i, j] = map_minutes[i, j] | map_hours[i-4, j];
                }
                //"UHR" in letzter Zeile anhängen, wenn nötig
                else if (i == 9)
                {
                    if (minutes <= 4 & minutes >= 0)
                    {
                        for (int j = 0; j < 11; j++)
                            map[i, j] = map_hours[i-4, j] | uhr[j];
                    }
                    else
                    {
                        for (int j = 0; j < 11; j++)
                            map[i, j] = map_hours[i - 4, j];
                    }
                }
                else
                {
                    for (int j = 0; j < 11; j++)
                        map[i, j] = map_hours[i-4, j];
                }
            }

            return map;
        }


    }
}
