using RPiRgbLEDMatrix;
using weatherapi;


namespace qlocktwo
{
    public class main
    {
        public const byte Brightness = 50;
        private static readonly Timer weatherUpdateTimer;
		private static WeatherAPI apiWeather;
        private static readonly string UrlForecast = DataExchange.instance.UrlForecast;
        private static readonly string UrlWeather = DataExchange.instance.UrlWeather;
        private static readonly string pathPicture =  DataExchange.instance.pathPicture;
        private static void Main(string[] args)
        {

            #region initializing objects
            
                apiWeather = new WeatherAPI(UrlWeather);

                // Timer initialisieren, der alle 30 Minuten den Wetter-Update-Thread startet
                Timer weatherUpdateTimer = new Timer(UpdateWeather, apiWeather, TimeSpan.FromSeconds(15), TimeSpan.FromHours(0.5));
			    
                // Thread für UDP-Verbindung zu deskpi
                Thread udpThread = new Thread(UDP.GetTemperature);
                udpThread.Start();

                DateTime dateTime = new DateTime();
                DayOfWeek dayOfWeek = new DayOfWeek();
                bool[,] map_time = new bool[10, 11];
                var map_panel = new TimeToMap();
                var matrix = new RGBLedMatrix(new RGBLedMatrixOptions
                {
                    Rows = 64,
                    Cols = 64,
                    GpioSlowdown = 4,
                    LimitRefreshRateHz = 60,
                    Brightness = Brightness,
                    DisableHardwarePulsing = false,
                    ChainLength = 3

                });
                var panel = new Panel(matrix, Brightness);
                int seconds = 0;
                int minutes = 0;
                int hours = 0;
                int day = 0;
                int month = 0;
                int year = 0;
                int switchCounter = 0;
                int switchValue = 10;
                bool switchFlag = true;
                
            #endregion

            #region endless loop for drawing on panels

                while (true)
                {
                    // Uhrzeit bestimmen
                    dateTime = DateTime.Now;
                    dayOfWeek = dateTime.DayOfWeek;
                    hours = dateTime.Hour;
                    minutes = dateTime.Minute;
                    seconds = dateTime.Second;
                    day = dateTime.Day;
                    month = dateTime.Month;
                    year = dateTime.Year;

                    // Anzeige auf Panel
                    map_time = map_panel.GenerateMap(hours, minutes);
                    panel.Draw(1, map_time, hours, minutes, seconds, dayOfWeek, day, month, year, switchFlag, pathPicture);

                    // ca 1 Sekunde warten
                    Thread.Sleep(800);

                    switchCounter++;
                    if (switchCounter == switchValue)
                    {
                        if (switchFlag)
                        {
                            switchValue = 20;
                        }
                        else
                        {
                            switchValue = 10;
                        }

                        switchFlag = !switchFlag;
                        switchCounter = 0;
                    }
                }

            #endregion

            #region fast simulation of complete day cycle
                //TEST CODE
                //while (true)
                //{
                //    for (int j = 0; j < 60; j++)
                //    {
                //        if (j == 59)
                //        {
                //            minutes++;
                //        }

                //        if (minutes == 60)
                //        {
                //            hours++;
                //            minutes = 0;
                //        }

                //        seconds = j;

                //        map_time = map_panel.GenerateMap(hours, minutes);
                //        Console.WriteLine($"{hours}:{minutes}");
                //        clock.Draw(map_time, hours, minutes, seconds);

                //        // 10 Sekunden warten
                //        Thread.Sleep(50);
                //        // Mit Programmende wird das LED-Panel zurückgesetzt.
                //    }
                //}
                
            #endregion
            
        }

        private static void UpdateWeather(object state)
        {
			WeatherAPI apiWeather = (WeatherAPI)state;
            apiWeather.call();
			Console.WriteLine("API called!\n");
        }

    }
}
