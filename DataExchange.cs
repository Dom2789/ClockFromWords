using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using weatherapi;

namespace qlocktwo
{
    public class DataExchange
    {
        private readonly string pathConfigFile = "/home/pi/_config/qlocktwo.txt";
        private static DataExchange _instance;
        private static readonly object _lock = new object();
        public string roomtemperature{ get; set; } = "NoData";
        public string weather{ get; set; } = "NoData";
        // read from config-file only writeable in Constructor
        public  string deskpiIP{ get; }
        public int deskpiSendPort{ get; }
        public int deskpiListenPort{ get; }
        public string deskpiTrigger{ get; }
        public string UrlForecast{ get; }
        public string UrlWeather{ get; }
        public string pathProt{ get; }
        public string pathPicture{ get; }
        // Public property to access the single instance
        public static DataExchange instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new DataExchange();
                    }

                    return _instance;
                }
            }
        }

        // Private constructor to prevent external instantiation
        private DataExchange()
        {
            string[] lines = File.ReadAllLines(this.pathConfigFile);
            deskpiIP = lines[0].Split(' ')[1];
            deskpiSendPort = int.Parse(lines[1].Split(' ')[1]);
            deskpiListenPort = int.Parse(lines[2].Split(' ')[1]);
            deskpiTrigger = lines[3].Split(' ')[1];
            UrlForecast = lines[4].Split(' ')[1];
            UrlWeather = lines[5].Split(' ')[1];
            pathProt = lines[6].Split(' ')[1];
            pathPicture = lines[7].Split(' ')[1];
        }
    }
}
