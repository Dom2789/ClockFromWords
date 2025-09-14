using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using System.Security.Cryptography.X509Certificates;
using qlocktwo;


namespace weatherapi
{
    internal class WeatherAPI
    {
        private string url;
        private string data;
        private ActualWeather weatherData;
        public ForecastWeather Forecast;
        private bool isDataValid = false;
        private readonly Dictionary<string, string> windDirectionLong = new Dictionary<string, string>
        {
            {"N", "Norden"},
            {"NO", "Nordosten"},
            {"O", "Osten"},
            {"SO", "Südosten"},
            {"S", "Süden"},
            {"SW", "Südwesten"},
            {"W", "Westen"},
            {"NW", "Nordwesten"}
        };

        // Konstruktor
        public WeatherAPI(string url, bool testWithoutFetchingData = false)
        {
            this.url = url;
            //if (testWithoutFetchingData) data = "{\"cod\":\"200\",\"message\":0,\"cnt\":40,\"list\":[{\"dt\":1738616400,\"main\":{\"temp\":-2.48,\"feels_like\":-2.48,\"temp_min\":-2.72,\"temp_max\":-2.48,\"pressure\":1030,\"sea_level\":1030,\"grnd_level\":956,\"humidity\":84,\"temp_kf\":0.24},\"weather\":[{\"id\":800,\"main\":\"Clear\",\"description\":\"Klarer Himmel\",\"icon\":\"01n\"}],\"clouds\":{\"all\":6},\"wind\":{\"speed\":0.47,\"deg\":167,\"gust\":0.46},\"visibility\":10000,\"pop\":0,\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2025-02-03 21:00:00\"},{\"dt\":1738627200,\"main\":{\"temp\":-2.79,\"feels_like\":-2.79,\"temp_min\":-3,\"temp_max\":-2.79,\"pressure\":1031,\"sea_level\":1031,\"grnd_level\":956,\"humidity\":77,\"temp_kf\":0.21},\"weather\":[{\"id\":800,\"main\":\"Clear\",\"description\":\"Klarer Himmel\",\"icon\":\"01n\"}],\"clouds\":{\"all\":3},\"wind\":{\"speed\":0.71,\"deg\":199,\"gust\":0.68},\"visibility\":10000,\"pop\":0,\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2025-02-04 00:00:00\"},{\"dt\":1738638000,\"main\":{\"temp\":-3.26,\"feels_like\":-3.26,\"temp_min\":-3.26,\"temp_max\":-3.26,\"pressure\":1031,\"sea_level\":1031,\"grnd_level\":956,\"humidity\":70,\"temp_kf\":0},\"weather\":[{\"id\":800,\"main\":\"Clear\",\"description\":\"Klarer Himmel\",\"icon\":\"01n\"}],\"clouds\":{\"all\":0},\"wind\":{\"speed\":0.6,\"deg\":190,\"gust\":0.62},\"visibility\":10000,\"pop\":0,\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2025-02-04 03:00:00\"},{\"dt\":1738648800,\"main\":{\"temp\":-3.45,\"feels_like\":-3.45,\"temp_min\":-3.45,\"temp_max\":-3.45,\"pressure\":1031,\"sea_level\":1031,\"grnd_level\":956,\"humidity\":68,\"temp_kf\":0},\"weather\":[{\"id\":800,\"main\":\"Clear\",\"description\":\"Klarer Himmel\",\"icon\":\"01n\"}],\"clouds\":{\"all\":0},\"wind\":{\"speed\":1.04,\"deg\":202,\"gust\":0.96},\"visibility\":10000,\"pop\":0,\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2025-02-04 06:00:00\"},{\"dt\":1738659600,\"main\":{\"temp\":0.34,\"feels_like\":0.34,\"temp_min\":0.34,\"temp_max\":0.34,\"pressure\":1031,\"sea_level\":1031,\"grnd_level\":957,\"humidity\":51,\"temp_kf\":0},\"weather\":[{\"id\":800,\"main\":\"Clear\",\"description\":\"Klarer Himmel\",\"icon\":\"01d\"}],\"clouds\":{\"all\":0},\"wind\":{\"speed\":1.12,\"deg\":204,\"gust\":1.68},\"visibility\":10000,\"pop\":0,\"sys\":{\"pod\":\"d\"},\"dt_txt\":\"2025-02-04 09:00:00\"},{\"dt\":1738670400,\"main\":{\"temp\":4.06,\"feels_like\":2.92,\"temp_min\":4.06,\"temp_max\":4.06,\"pressure\":1030,\"sea_level\":1030,\"grnd_level\":957,\"humidity\":37,\"temp_kf\":0},\"weather\":[{\"id\":800,\"main\":\"Clear\",\"description\":\"Klarer Himmel\",\"icon\":\"01d\"}],\"clouds\":{\"all\":0},\"wind\":{\"speed\":1.47,\"deg\":218,\"gust\":2.18},\"visibility\":10000,\"pop\":0,\"sys\":{\"pod\":\"d\"},\"dt_txt\":\"2025-02-04 12:00:00\"},{\"dt\":1738681200,\"main\":{\"temp\":2.5,\"feels_like\":2.5,\"temp_min\":2.5,\"temp_max\":2.5,\"pressure\":1030,\"sea_level\":1030,\"grnd_level\":956,\"humidity\":49,\"temp_kf\":0},\"weather\":[{\"id\":800,\"main\":\"Clear\",\"description\":\"Klarer Himmel\",\"icon\":\"01d\"}],\"clouds\":{\"all\":2},\"wind\":{\"speed\":0.75,\"deg\":226,\"gust\":0.74},\"visibility\":10000,\"pop\":0,\"sys\":{\"pod\":\"d\"},\"dt_txt\":\"2025-02-04 15:00:00\"},{\"dt\":1738692000,\"main\":{\"temp\":-1.04,\"feels_like\":-1.04,\"temp_min\":-1.04,\"temp_max\":-1.04,\"pressure\":1031,\"sea_level\":1031,\"grnd_level\":957,\"humidity\":58,\"temp_kf\":0},\"weather\":[{\"id\":800,\"main\":\"Clear\",\"description\":\"Klarer Himmel\",\"icon\":\"01n\"}],\"clouds\":{\"all\":1},\"wind\":{\"speed\":0.52,\"deg\":173,\"gust\":0.57},\"visibility\":10000,\"pop\":0,\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2025-02-04 18:00:00\"},{\"dt\":1738702800,\"main\":{\"temp\":-1.33,\"feels_like\":-1.33,\"temp_min\":-1.33,\"temp_max\":-1.33,\"pressure\":1033,\"sea_level\":1033,\"grnd_level\":958,\"humidity\":58,\"temp_kf\":0},\"weather\":[{\"id\":800,\"main\":\"Clear\",\"description\":\"Klarer Himmel\",\"icon\":\"01n\"}],\"clouds\":{\"all\":2},\"wind\":{\"speed\":0.85,\"deg\":230,\"gust\":0.84},\"visibility\":10000,\"pop\":0,\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2025-02-04 21:00:00\"},{\"dt\":1738713600,\"main\":{\"temp\":-1.54,\"feels_like\":-1.54,\"temp_min\":-1.54,\"temp_max\":-1.54,\"pressure\":1033,\"sea_level\":1033,\"grnd_level\":959,\"humidity\":57,\"temp_kf\":0},\"weather\":[{\"id\":800,\"main\":\"Clear\",\"description\":\"Klarer Himmel\",\"icon\":\"01n\"}],\"clouds\":{\"all\":1},\"wind\":{\"speed\":0.93,\"deg\":241,\"gust\":0.94},\"visibility\":10000,\"pop\":0,\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2025-02-05 00:00:00\"},{\"dt\":1738724400,\"main\":{\"temp\":-1.98,\"feels_like\":-4.06,\"temp_min\":-1.98,\"temp_max\":-1.98,\"pressure\":1034,\"sea_level\":1034,\"grnd_level\":959,\"humidity\":60,\"temp_kf\":0},\"weather\":[{\"id\":800,\"main\":\"Clear\",\"description\":\"Klarer Himmel\",\"icon\":\"01n\"}],\"clouds\":{\"all\":2},\"wind\":{\"speed\":1.53,\"deg\":233,\"gust\":1.43},\"visibility\":10000,\"pop\":0,\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2025-02-05 03:00:00\"},{\"dt\":1738735200,\"main\":{\"temp\":-2.4,\"feels_like\":-4.54,\"temp_min\":-2.4,\"temp_max\":-2.4,\"pressure\":1036,\"sea_level\":1036,\"grnd_level\":961,\"humidity\":64,\"temp_kf\":0},\"weather\":[{\"id\":800,\"main\":\"Clear\",\"description\":\"Klarer Himmel\",\"icon\":\"01n\"}],\"clouds\":{\"all\":2},\"wind\":{\"speed\":1.53,\"deg\":242,\"gust\":1.45},\"visibility\":10000,\"pop\":0,\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2025-02-05 06:00:00\"},{\"dt\":1738746000,\"main\":{\"temp\":1.31,\"feels_like\":-0.17,\"temp_min\":1.31,\"temp_max\":1.31,\"pressure\":1037,\"sea_level\":1037,\"grnd_level\":962,\"humidity\":53,\"temp_kf\":0},\"weather\":[{\"id\":800,\"main\":\"Clear\",\"description\":\"Klarer Himmel\",\"icon\":\"01d\"}],\"clouds\":{\"all\":2},\"wind\":{\"speed\":1.44,\"deg\":243,\"gust\":2.17},\"visibility\":10000,\"pop\":0,\"sys\":{\"pod\":\"d\"},\"dt_txt\":\"2025-02-05 09:00:00\"},{\"dt\":1738756800,\"main\":{\"temp\":4.85,\"feels_like\":3.51,\"temp_min\":4.85,\"temp_max\":4.85,\"pressure\":1037,\"sea_level\":1037,\"grnd_level\":963,\"humidity\":47,\"temp_kf\":0},\"weather\":[{\"id\":800,\"main\":\"Clear\",\"description\":\"Klarer Himmel\",\"icon\":\"01d\"}],\"clouds\":{\"all\":1},\"wind\":{\"speed\":1.71,\"deg\":257,\"gust\":2.57},\"visibility\":10000,\"pop\":0,\"sys\":{\"pod\":\"d\"},\"dt_txt\":\"2025-02-05 12:00:00\"},{\"dt\":1738767600,\"main\":{\"temp\":3.15,\"feels_like\":3.15,\"temp_min\":3.15,\"temp_max\":3.15,\"pressure\":1037,\"sea_level\":1037,\"grnd_level\":963,\"humidity\":61,\"temp_kf\":0},\"weather\":[{\"id\":800,\"main\":\"Clear\",\"description\":\"Klarer Himmel\",\"icon\":\"01d\"}],\"clouds\":{\"all\":0},\"wind\":{\"speed\":1.05,\"deg\":263,\"gust\":1},\"visibility\":10000,\"pop\":0,\"sys\":{\"pod\":\"d\"},\"dt_txt\":\"2025-02-05 15:00:00\"},{\"dt\":1738778400,\"main\":{\"temp\":-0.16,\"feels_like\":-0.16,\"temp_min\":-0.16,\"temp_max\":-0.16,\"pressure\":1039,\"sea_level\":1039,\"grnd_level\":965,\"humidity\":72,\"temp_kf\":0},\"weather\":[{\"id\":800,\"main\":\"Clear\",\"description\":\"Klarer Himmel\",\"icon\":\"01n\"}],\"clouds\":{\"all\":1},\"wind\":{\"speed\":0.66,\"deg\":305,\"gust\":0.65},\"visibility\":10000,\"pop\":0,\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2025-02-05 18:00:00\"},{\"dt\":1738789200,\"main\":{\"temp\":1.45,\"feels_like\":-0.19,\"temp_min\":1.45,\"temp_max\":1.45,\"pressure\":1040,\"sea_level\":1040,\"grnd_level\":965,\"humidity\":80,\"temp_kf\":0},\"weather\":[{\"id\":804,\"main\":\"Clouds\",\"description\":\"Bedeckt\",\"icon\":\"04n\"}],\"clouds\":{\"all\":89},\"wind\":{\"speed\":1.56,\"deg\":344,\"gust\":2.28},\"visibility\":10000,\"pop\":0,\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2025-02-05 21:00:00\"},{\"dt\":1738800000,\"main\":{\"temp\":1.01,\"feels_like\":-0.93,\"temp_min\":1.01,\"temp_max\":1.01,\"pressure\":1040,\"sea_level\":1040,\"grnd_level\":965,\"humidity\":100,\"temp_kf\":0},\"weather\":[{\"id\":804,\"main\":\"Clouds\",\"description\":\"Bedeckt\",\"icon\":\"04n\"}],\"clouds\":{\"all\":94},\"wind\":{\"speed\":1.73,\"deg\":43,\"gust\":3.74},\"visibility\":10000,\"pop\":0,\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2025-02-06 00:00:00\"},{\"dt\":1738810800,\"main\":{\"temp\":0.44,\"feels_like\":-1.91,\"temp_min\":0.44,\"temp_max\":0.44,\"pressure\":1039,\"sea_level\":1039,\"grnd_level\":965,\"humidity\":98,\"temp_kf\":0},\"weather\":[{\"id\":804,\"main\":\"Clouds\",\"description\":\"Bedeckt\",\"icon\":\"04n\"}],\"clouds\":{\"all\":100},\"wind\":{\"speed\":1.97,\"deg\":36,\"gust\":5.34},\"pop\":0,\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2025-02-06 03:00:00\"},{\"dt\":1738821600,\"main\":{\"temp\":-0.32,\"feels_like\":-4.35,\"temp_min\":-0.32,\"temp_max\":-0.32,\"pressure\":1040,\"sea_level\":1040,\"grnd_level\":965,\"humidity\":93,\"temp_kf\":0},\"weather\":[{\"id\":804,\"main\":\"Clouds\",\"description\":\"Bedeckt\",\"icon\":\"04n\"}],\"clouds\":{\"all\":100},\"wind\":{\"speed\":3.56,\"deg\":69,\"gust\":8.79},\"visibility\":6465,\"pop\":0,\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2025-02-06 06:00:00\"},{\"dt\":1738832400,\"main\":{\"temp\":0.73,\"feels_like\":-3.44,\"temp_min\":0.73,\"temp_max\":0.73,\"pressure\":1039,\"sea_level\":1039,\"grnd_level\":964,\"humidity\":82,\"temp_kf\":0},\"weather\":[{\"id\":804,\"main\":\"Clouds\",\"description\":\"Bedeckt\",\"icon\":\"04d\"}],\"clouds\":{\"all\":100},\"wind\":{\"speed\":4.08,\"deg\":73,\"gust\":9.39},\"visibility\":10000,\"pop\":0,\"sys\":{\"pod\":\"d\"},\"dt_txt\":\"2025-02-06 09:00:00\"},{\"dt\":1738843200,\"main\":{\"temp\":2.32,\"feels_like\":-1.61,\"temp_min\":2.32,\"temp_max\":2.32,\"pressure\":1038,\"sea_level\":1038,\"grnd_level\":963,\"humidity\":75,\"temp_kf\":0},\"weather\":[{\"id\":804,\"main\":\"Clouds\",\"description\":\"Bedeckt\",\"icon\":\"04d\"}],\"clouds\":{\"all\":100},\"wind\":{\"speed\":4.28,\"deg\":67,\"gust\":8.38},\"visibility\":10000,\"pop\":0,\"sys\":{\"pod\":\"d\"},\"dt_txt\":\"2025-02-06 12:00:00\"},{\"dt\":1738854000,\"main\":{\"temp\":0.84,\"feels_like\":-3.51,\"temp_min\":0.84,\"temp_max\":0.84,\"pressure\":1037,\"sea_level\":1037,\"grnd_level\":962,\"humidity\":85,\"temp_kf\":0},\"weather\":[{\"id\":804,\"main\":\"Clouds\",\"description\":\"Bedeckt\",\"icon\":\"04d\"}],\"clouds\":{\"all\":100},\"wind\":{\"speed\":4.4,\"deg\":64,\"gust\":9.98},\"visibility\":10000,\"pop\":0,\"sys\":{\"pod\":\"d\"},\"dt_txt\":\"2025-02-06 15:00:00\"},{\"dt\":1738864800,\"main\":{\"temp\":-0.1,\"feels_like\":-5.07,\"temp_min\":-0.1,\"temp_max\":-0.1,\"pressure\":1037,\"sea_level\":1037,\"grnd_level\":962,\"humidity\":90,\"temp_kf\":0},\"weather\":[{\"id\":804,\"main\":\"Clouds\",\"description\":\"Bedeckt\",\"icon\":\"04n\"}],\"clouds\":{\"all\":100},\"wind\":{\"speed\":5.05,\"deg\":59,\"gust\":10.86},\"visibility\":10000,\"pop\":0,\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2025-02-06 18:00:00\"},{\"dt\":1738875600,\"main\":{\"temp\":-0.36,\"feels_like\":-5.45,\"temp_min\":-0.36,\"temp_max\":-0.36,\"pressure\":1036,\"sea_level\":1036,\"grnd_level\":961,\"humidity\":91,\"temp_kf\":0},\"weather\":[{\"id\":804,\"main\":\"Clouds\",\"description\":\"Bedeckt\",\"icon\":\"04n\"}],\"clouds\":{\"all\":100},\"wind\":{\"speed\":5.14,\"deg\":64,\"gust\":11.7},\"visibility\":10000,\"pop\":0,\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2025-02-06 21:00:00\"},{\"dt\":1738886400,\"main\":{\"temp\":-0.51,\"feels_like\":-5.8,\"temp_min\":-0.51,\"temp_max\":-0.51,\"pressure\":1034,\"sea_level\":1034,\"grnd_level\":959,\"humidity\":93,\"temp_kf\":0},\"weather\":[{\"id\":600,\"main\":\"Snow\",\"description\":\"Mäßiger Schnee\",\"icon\":\"13n\"}],\"clouds\":{\"all\":100},\"wind\":{\"speed\":5.43,\"deg\":67,\"gust\":11.49},\"visibility\":387,\"pop\":0.2,\"snow\":{\"3h\":0.14},\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2025-02-07 00:00:00\"},{\"dt\":1738897200,\"main\":{\"temp\":-0.72,\"feels_like\":-6.57,\"temp_min\":-0.72,\"temp_max\":-0.72,\"pressure\":1032,\"sea_level\":1032,\"grnd_level\":958,\"humidity\":89,\"temp_kf\":0},\"weather\":[{\"id\":804,\"main\":\"Clouds\",\"description\":\"Bedeckt\",\"icon\":\"04n\"}],\"clouds\":{\"all\":100},\"wind\":{\"speed\":6.42,\"deg\":73,\"gust\":12.41},\"visibility\":5828,\"pop\":0,\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2025-02-07 03:00:00\"},{\"dt\":1738908000,\"main\":{\"temp\":-0.89,\"feels_like\":-6.86,\"temp_min\":-0.89,\"temp_max\":-0.89,\"pressure\":1031,\"sea_level\":1031,\"grnd_level\":956,\"humidity\":90,\"temp_kf\":0},\"weather\":[{\"id\":600,\"main\":\"Snow\",\"description\":\"Mäßiger Schnee\",\"icon\":\"13n\"}],\"clouds\":{\"all\":100},\"wind\":{\"speed\":6.58,\"deg\":71,\"gust\":12.71},\"visibility\":2378,\"pop\":0.2,\"snow\":{\"3h\":0.14},\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2025-02-07 06:00:00\"},{\"dt\":1738918800,\"main\":{\"temp\":-0.5,\"feels_like\":-6.48,\"temp_min\":-0.5,\"temp_max\":-0.5,\"pressure\":1029,\"sea_level\":1029,\"grnd_level\":955,\"humidity\":84,\"temp_kf\":0},\"weather\":[{\"id\":600,\"main\":\"Snow\",\"description\":\"Mäßiger Schnee\",\"icon\":\"13d\"}],\"clouds\":{\"all\":100},\"wind\":{\"speed\":6.82,\"deg\":76,\"gust\":13.28},\"visibility\":10000,\"pop\":0.21,\"snow\":{\"3h\":0.2},\"sys\":{\"pod\":\"d\"},\"dt_txt\":\"2025-02-07 09:00:00\"},{\"dt\":1738929600,\"main\":{\"temp\":-0.14,\"feels_like\":-6,\"temp_min\":-0.14,\"temp_max\":-0.14,\"pressure\":1028,\"sea_level\":1028,\"grnd_level\":954,\"humidity\":90,\"temp_kf\":0},\"weather\":[{\"id\":600,\"main\":\"Snow\",\"description\":\"Mäßiger Schnee\",\"icon\":\"13d\"}],\"clouds\":{\"all\":100},\"wind\":{\"speed\":6.79,\"deg\":81,\"gust\":13.19},\"visibility\":5825,\"pop\":0.47,\"snow\":{\"3h\":0.39},\"sys\":{\"pod\":\"d\"},\"dt_txt\":\"2025-02-07 12:00:00\"},{\"dt\":1738940400,\"main\":{\"temp\":0.1,\"feels_like\":-5.57,\"temp_min\":0.1,\"temp_max\":0.1,\"pressure\":1028,\"sea_level\":1028,\"grnd_level\":954,\"humidity\":87,\"temp_kf\":0},\"weather\":[{\"id\":600,\"main\":\"Snow\",\"description\":\"Mäßiger Schnee\",\"icon\":\"13d\"}],\"clouds\":{\"all\":97},\"wind\":{\"speed\":6.52,\"deg\":87,\"gust\":12},\"visibility\":10000,\"pop\":0.49,\"snow\":{\"3h\":0.35},\"sys\":{\"pod\":\"d\"},\"dt_txt\":\"2025-02-07 15:00:00\"},{\"dt\":1738951200,\"main\":{\"temp\":-0.78,\"feels_like\":-6.71,\"temp_min\":-0.78,\"temp_max\":-0.78,\"pressure\":1028,\"sea_level\":1028,\"grnd_level\":954,\"humidity\":87,\"temp_kf\":0},\"weather\":[{\"id\":804,\"main\":\"Clouds\",\"description\":\"Bedeckt\",\"icon\":\"04n\"}],\"clouds\":{\"all\":97},\"wind\":{\"speed\":6.54,\"deg\":89,\"gust\":12.39},\"visibility\":10000,\"pop\":0.15,\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2025-02-07 18:00:00\"},{\"dt\":1738962000,\"main\":{\"temp\":-0.71,\"feels_like\":-6.82,\"temp_min\":-0.71,\"temp_max\":-0.71,\"pressure\":1028,\"sea_level\":1028,\"grnd_level\":954,\"humidity\":89,\"temp_kf\":0},\"weather\":[{\"id\":804,\"main\":\"Clouds\",\"description\":\"Bedeckt\",\"icon\":\"04n\"}],\"clouds\":{\"all\":100},\"wind\":{\"speed\":6.99,\"deg\":86,\"gust\":12.59},\"visibility\":10000,\"pop\":0,\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2025-02-07 21:00:00\"},{\"dt\":1738972800,\"main\":{\"temp\":-0.84,\"feels_like\":-6.49,\"temp_min\":-0.84,\"temp_max\":-0.84,\"pressure\":1028,\"sea_level\":1028,\"grnd_level\":953,\"humidity\":87,\"temp_kf\":0},\"weather\":[{\"id\":804,\"main\":\"Clouds\",\"description\":\"Bedeckt\",\"icon\":\"04n\"}],\"clouds\":{\"all\":100},\"wind\":{\"speed\":5.95,\"deg\":83,\"gust\":11.57},\"visibility\":10000,\"pop\":0,\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2025-02-08 00:00:00\"},{\"dt\":1738983600,\"main\":{\"temp\":-2.94,\"feels_like\":-8.71,\"temp_min\":-2.94,\"temp_max\":-2.94,\"pressure\":1028,\"sea_level\":1028,\"grnd_level\":953,\"humidity\":89,\"temp_kf\":0},\"weather\":[{\"id\":803,\"main\":\"Clouds\",\"description\":\"Überwiegend bewölkt\",\"icon\":\"04n\"}],\"clouds\":{\"all\":57},\"wind\":{\"speed\":5.19,\"deg\":81,\"gust\":11.61},\"visibility\":10000,\"pop\":0,\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2025-02-08 03:00:00\"},{\"dt\":1738994400,\"main\":{\"temp\":-3.43,\"feels_like\":-9.11,\"temp_min\":-3.43,\"temp_max\":-3.43,\"pressure\":1029,\"sea_level\":1029,\"grnd_level\":954,\"humidity\":89,\"temp_kf\":0},\"weather\":[{\"id\":803,\"main\":\"Clouds\",\"description\":\"Überwiegend bewölkt\",\"icon\":\"04n\"}],\"clouds\":{\"all\":57},\"wind\":{\"speed\":4.86,\"deg\":79,\"gust\":11.4},\"visibility\":10000,\"pop\":0,\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2025-02-08 06:00:00\"},{\"dt\":1739005200,\"main\":{\"temp\":-1.73,\"feels_like\":-7.5,\"temp_min\":-1.73,\"temp_max\":-1.73,\"pressure\":1029,\"sea_level\":1029,\"grnd_level\":954,\"humidity\":81,\"temp_kf\":0},\"weather\":[{\"id\":804,\"main\":\"Clouds\",\"description\":\"Bedeckt\",\"icon\":\"04d\"}],\"clouds\":{\"all\":100},\"wind\":{\"speed\":5.72,\"deg\":80,\"gust\":11.16},\"visibility\":10000,\"pop\":0,\"sys\":{\"pod\":\"d\"},\"dt_txt\":\"2025-02-08 09:00:00\"},{\"dt\":1739016000,\"main\":{\"temp\":0.78,\"feels_like\":-4.28,\"temp_min\":0.78,\"temp_max\":0.78,\"pressure\":1027,\"sea_level\":1027,\"grnd_level\":953,\"humidity\":68,\"temp_kf\":0},\"weather\":[{\"id\":804,\"main\":\"Clouds\",\"description\":\"Bedeckt\",\"icon\":\"04d\"}],\"clouds\":{\"all\":100},\"wind\":{\"speed\":5.63,\"deg\":81,\"gust\":8.64},\"visibility\":10000,\"pop\":0,\"sys\":{\"pod\":\"d\"},\"dt_txt\":\"2025-02-08 12:00:00\"},{\"dt\":1739026800,\"main\":{\"temp\":0.71,\"feels_like\":-4.23,\"temp_min\":0.71,\"temp_max\":0.71,\"pressure\":1027,\"sea_level\":1027,\"grnd_level\":953,\"humidity\":71,\"temp_kf\":0},\"weather\":[{\"id\":804,\"main\":\"Clouds\",\"description\":\"Bedeckt\",\"icon\":\"04d\"}],\"clouds\":{\"all\":100},\"wind\":{\"speed\":5.36,\"deg\":80,\"gust\":10.36},\"visibility\":10000,\"pop\":0,\"sys\":{\"pod\":\"d\"},\"dt_txt\":\"2025-02-08 15:00:00\"},{\"dt\":1739037600,\"main\":{\"temp\":-1,\"feels_like\":-6.39,\"temp_min\":-1,\"temp_max\":-1,\"pressure\":1027,\"sea_level\":1027,\"grnd_level\":953,\"humidity\":81,\"temp_kf\":0},\"weather\":[{\"id\":804,\"main\":\"Clouds\",\"description\":\"Bedeckt\",\"icon\":\"04n\"}],\"clouds\":{\"all\":100},\"wind\":{\"speed\":5.38,\"deg\":77,\"gust\":12.49},\"visibility\":10000,\"pop\":0,\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2025-02-08 18:00:00\"}],\"city\":{\"id\":2815317,\"name\":\"Waidhaus\",\"coord\":{\"lat\":49.6422,\"lon\":12.4952},\"country\":\"DE\",\"population\":2467,\"timezone\":3600,\"sunrise\":1738564777,\"sunset\":1738598882}}";
            //else data = GetData();
            //data = "{\"cod\":\"200\",\"message\":0,\"cnt\":40,\"list\":[{\"dt\":1738616400,\"main\":{\"temp\":-2.48,\"feels_like\":-2.48,\"temp_min\":-2.72,\"temp_max\":-2.48,\"pressure\":1030,\"sea_level\":1030,\"grnd_level\":956,\"humidity\":84,\"temp_kf\":0.24},\"weather\":[{\"id\":800,\"main\":\"Clear\",\"description\":\"Klarer Himmel\",\"icon\":\"01n\"}],\"clouds\":{\"all\":6},\"wind\":{\"speed\":0.47,\"deg\":167,\"gust\":0.46},\"visibility\":10000,\"pop\":0,\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2025-02-03 21:00:00\"},{\"dt\":1738627200,\"main\":{\"temp\":-2.79,\"feels_like\":-2.79,\"temp_min\":-3,\"temp_max\":-2.79,\"pressure\":1031,\"sea_level\":1031,\"grnd_level\":956,\"humidity\":77,\"temp_kf\":0.21},\"weather\":[{\"id\":800,\"main\":\"Clear\",\"description\":\"Klarer Himmel\",\"icon\":\"01n\"}],\"clouds\":{\"all\":3},\"wind\":{\"speed\":0.71,\"deg\":199,\"gust\":0.68},\"visibility\":10000,\"pop\":0,\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2025-02-04 00:00:00\"},{\"dt\":1738638000,\"main\":{\"temp\":-3.26,\"feels_like\":-3.26,\"temp_min\":-3.26,\"temp_max\":-3.26,\"pressure\":1031,\"sea_level\":1031,\"grnd_level\":956,\"humidity\":70,\"temp_kf\":0},\"weather\":[{\"id\":800,\"main\":\"Clear\",\"description\":\"Klarer Himmel\",\"icon\":\"01n\"}],\"clouds\":{\"all\":0},\"wind\":{\"speed\":0.6,\"deg\":190,\"gust\":0.62},\"visibility\":10000,\"pop\":0,\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2025-02-04 03:00:00\"},{\"dt\":1738648800,\"main\":{\"temp\":-3.45,\"feels_like\":-3.45,\"temp_min\":-3.45,\"temp_max\":-3.45,\"pressure\":1031,\"sea_level\":1031,\"grnd_level\":956,\"humidity\":68,\"temp_kf\":0},\"weather\":[{\"id\":800,\"main\":\"Clear\",\"description\":\"Klarer Himmel\",\"icon\":\"01n\"}],\"clouds\":{\"all\":0},\"wind\":{\"speed\":1.04,\"deg\":202,\"gust\":0.96},\"visibility\":10000,\"pop\":0,\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2025-02-04 06:00:00\"},{\"dt\":1738659600,\"main\":{\"temp\":0.34,\"feels_like\":0.34,\"temp_min\":0.34,\"temp_max\":0.34,\"pressure\":1031,\"sea_level\":1031,\"grnd_level\":957,\"humidity\":51,\"temp_kf\":0},\"weather\":[{\"id\":800,\"main\":\"Clear\",\"description\":\"Klarer Himmel\",\"icon\":\"01d\"}],\"clouds\":{\"all\":0},\"wind\":{\"speed\":1.12,\"deg\":204,\"gust\":1.68},\"visibility\":10000,\"pop\":0,\"sys\":{\"pod\":\"d\"},\"dt_txt\":\"2025-02-04 09:00:00\"},{\"dt\":1738670400,\"main\":{\"temp\":4.06,\"feels_like\":2.92,\"temp_min\":4.06,\"temp_max\":4.06,\"pressure\":1030,\"sea_level\":1030,\"grnd_level\":957,\"humidity\":37,\"temp_kf\":0},\"weather\":[{\"id\":800,\"main\":\"Clear\",\"description\":\"Klarer Himmel\",\"icon\":\"01d\"}],\"clouds\":{\"all\":0},\"wind\":{\"speed\":1.47,\"deg\":218,\"gust\":2.18},\"visibility\":10000,\"pop\":0,\"sys\":{\"pod\":\"d\"},\"dt_txt\":\"2025-02-04 12:00:00\"},{\"dt\":1738681200,\"main\":{\"temp\":2.5,\"feels_like\":2.5,\"temp_min\":2.5,\"temp_max\":2.5,\"pressure\":1030,\"sea_level\":1030,\"grnd_level\":956,\"humidity\":49,\"temp_kf\":0},\"weather\":[{\"id\":800,\"main\":\"Clear\",\"description\":\"Klarer Himmel\",\"icon\":\"01d\"}],\"clouds\":{\"all\":2},\"wind\":{\"speed\":0.75,\"deg\":226,\"gust\":0.74},\"visibility\":10000,\"pop\":0,\"sys\":{\"pod\":\"d\"},\"dt_txt\":\"2025-02-04 15:00:00\"},{\"dt\":1738692000,\"main\":{\"temp\":-1.04,\"feels_like\":-1.04,\"temp_min\":-1.04,\"temp_max\":-1.04,\"pressure\":1031,\"sea_level\":1031,\"grnd_level\":957,\"humidity\":58,\"temp_kf\":0},\"weather\":[{\"id\":800,\"main\":\"Clear\",\"description\":\"Klarer Himmel\",\"icon\":\"01n\"}],\"clouds\":{\"all\":1},\"wind\":{\"speed\":0.52,\"deg\":173,\"gust\":0.57},\"visibility\":10000,\"pop\":0,\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2025-02-04 18:00:00\"},{\"dt\":1738702800,\"main\":{\"temp\":-1.33,\"feels_like\":-1.33,\"temp_min\":-1.33,\"temp_max\":-1.33,\"pressure\":1033,\"sea_level\":1033,\"grnd_level\":958,\"humidity\":58,\"temp_kf\":0},\"weather\":[{\"id\":800,\"main\":\"Clear\",\"description\":\"Klarer Himmel\",\"icon\":\"01n\"}],\"clouds\":{\"all\":2},\"wind\":{\"speed\":0.85,\"deg\":230,\"gust\":0.84},\"visibility\":10000,\"pop\":0,\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2025-02-04 21:00:00\"},{\"dt\":1738713600,\"main\":{\"temp\":-1.54,\"feels_like\":-1.54,\"temp_min\":-1.54,\"temp_max\":-1.54,\"pressure\":1033,\"sea_level\":1033,\"grnd_level\":959,\"humidity\":57,\"temp_kf\":0},\"weather\":[{\"id\":800,\"main\":\"Clear\",\"description\":\"Klarer Himmel\",\"icon\":\"01n\"}],\"clouds\":{\"all\":1},\"wind\":{\"speed\":0.93,\"deg\":241,\"gust\":0.94},\"visibility\":10000,\"pop\":0,\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2025-02-05 00:00:00\"},{\"dt\":1738724400,\"main\":{\"temp\":-1.98,\"feels_like\":-4.06,\"temp_min\":-1.98,\"temp_max\":-1.98,\"pressure\":1034,\"sea_level\":1034,\"grnd_level\":959,\"humidity\":60,\"temp_kf\":0},\"weather\":[{\"id\":800,\"main\":\"Clear\",\"description\":\"Klarer Himmel\",\"icon\":\"01n\"}],\"clouds\":{\"all\":2},\"wind\":{\"speed\":1.53,\"deg\":233,\"gust\":1.43},\"visibility\":10000,\"pop\":0,\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2025-02-05 03:00:00\"},{\"dt\":1738735200,\"main\":{\"temp\":-2.4,\"feels_like\":-4.54,\"temp_min\":-2.4,\"temp_max\":-2.4,\"pressure\":1036,\"sea_level\":1036,\"grnd_level\":961,\"humidity\":64,\"temp_kf\":0},\"weather\":[{\"id\":800,\"main\":\"Clear\",\"description\":\"Klarer Himmel\",\"icon\":\"01n\"}],\"clouds\":{\"all\":2},\"wind\":{\"speed\":1.53,\"deg\":242,\"gust\":1.45},\"visibility\":10000,\"pop\":0,\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2025-02-05 06:00:00\"},{\"dt\":1738746000,\"main\":{\"temp\":1.31,\"feels_like\":-0.17,\"temp_min\":1.31,\"temp_max\":1.31,\"pressure\":1037,\"sea_level\":1037,\"grnd_level\":962,\"humidity\":53,\"temp_kf\":0},\"weather\":[{\"id\":800,\"main\":\"Clear\",\"description\":\"Klarer Himmel\",\"icon\":\"01d\"}],\"clouds\":{\"all\":2},\"wind\":{\"speed\":1.44,\"deg\":243,\"gust\":2.17},\"visibility\":10000,\"pop\":0,\"sys\":{\"pod\":\"d\"},\"dt_txt\":\"2025-02-05 09:00:00\"},{\"dt\":1738756800,\"main\":{\"temp\":4.85,\"feels_like\":3.51,\"temp_min\":4.85,\"temp_max\":4.85,\"pressure\":1037,\"sea_level\":1037,\"grnd_level\":963,\"humidity\":47,\"temp_kf\":0},\"weather\":[{\"id\":800,\"main\":\"Clear\",\"description\":\"Klarer Himmel\",\"icon\":\"01d\"}],\"clouds\":{\"all\":1},\"wind\":{\"speed\":1.71,\"deg\":257,\"gust\":2.57},\"visibility\":10000,\"pop\":0,\"sys\":{\"pod\":\"d\"},\"dt_txt\":\"2025-02-05 12:00:00\"},{\"dt\":1738767600,\"main\":{\"temp\":3.15,\"feels_like\":3.15,\"temp_min\":3.15,\"temp_max\":3.15,\"pressure\":1037,\"sea_level\":1037,\"grnd_level\":963,\"humidity\":61,\"temp_kf\":0},\"weather\":[{\"id\":800,\"main\":\"Clear\",\"description\":\"Klarer Himmel\",\"icon\":\"01d\"}],\"clouds\":{\"all\":0},\"wind\":{\"speed\":1.05,\"deg\":263,\"gust\":1},\"visibility\":10000,\"pop\":0,\"sys\":{\"pod\":\"d\"},\"dt_txt\":\"2025-02-05 15:00:00\"},{\"dt\":1738778400,\"main\":{\"temp\":-0.16,\"feels_like\":-0.16,\"temp_min\":-0.16,\"temp_max\":-0.16,\"pressure\":1039,\"sea_level\":1039,\"grnd_level\":965,\"humidity\":72,\"temp_kf\":0},\"weather\":[{\"id\":800,\"main\":\"Clear\",\"description\":\"Klarer Himmel\",\"icon\":\"01n\"}],\"clouds\":{\"all\":1},\"wind\":{\"speed\":0.66,\"deg\":305,\"gust\":0.65},\"visibility\":10000,\"pop\":0,\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2025-02-05 18:00:00\"},{\"dt\":1738789200,\"main\":{\"temp\":1.45,\"feels_like\":-0.19,\"temp_min\":1.45,\"temp_max\":1.45,\"pressure\":1040,\"sea_level\":1040,\"grnd_level\":965,\"humidity\":80,\"temp_kf\":0},\"weather\":[{\"id\":804,\"main\":\"Clouds\",\"description\":\"Bedeckt\",\"icon\":\"04n\"}],\"clouds\":{\"all\":89},\"wind\":{\"speed\":1.56,\"deg\":344,\"gust\":2.28},\"visibility\":10000,\"pop\":0,\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2025-02-05 21:00:00\"},{\"dt\":1738800000,\"main\":{\"temp\":1.01,\"feels_like\":-0.93,\"temp_min\":1.01,\"temp_max\":1.01,\"pressure\":1040,\"sea_level\":1040,\"grnd_level\":965,\"humidity\":100,\"temp_kf\":0},\"weather\":[{\"id\":804,\"main\":\"Clouds\",\"description\":\"Bedeckt\",\"icon\":\"04n\"}],\"clouds\":{\"all\":94},\"wind\":{\"speed\":1.73,\"deg\":43,\"gust\":3.74},\"visibility\":10000,\"pop\":0,\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2025-02-06 00:00:00\"},{\"dt\":1738810800,\"main\":{\"temp\":0.44,\"feels_like\":-1.91,\"temp_min\":0.44,\"temp_max\":0.44,\"pressure\":1039,\"sea_level\":1039,\"grnd_level\":965,\"humidity\":98,\"temp_kf\":0},\"weather\":[{\"id\":804,\"main\":\"Clouds\",\"description\":\"Bedeckt\",\"icon\":\"04n\"}],\"clouds\":{\"all\":100},\"wind\":{\"speed\":1.97,\"deg\":36,\"gust\":5.34},\"pop\":0,\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2025-02-06 03:00:00\"},{\"dt\":1738821600,\"main\":{\"temp\":-0.32,\"feels_like\":-4.35,\"temp_min\":-0.32,\"temp_max\":-0.32,\"pressure\":1040,\"sea_level\":1040,\"grnd_level\":965,\"humidity\":93,\"temp_kf\":0},\"weather\":[{\"id\":804,\"main\":\"Clouds\",\"description\":\"Bedeckt\",\"icon\":\"04n\"}],\"clouds\":{\"all\":100},\"wind\":{\"speed\":3.56,\"deg\":69,\"gust\":8.79},\"visibility\":6465,\"pop\":0,\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2025-02-06 06:00:00\"},{\"dt\":1738832400,\"main\":{\"temp\":0.73,\"feels_like\":-3.44,\"temp_min\":0.73,\"temp_max\":0.73,\"pressure\":1039,\"sea_level\":1039,\"grnd_level\":964,\"humidity\":82,\"temp_kf\":0},\"weather\":[{\"id\":804,\"main\":\"Clouds\",\"description\":\"Bedeckt\",\"icon\":\"04d\"}],\"clouds\":{\"all\":100},\"wind\":{\"speed\":4.08,\"deg\":73,\"gust\":9.39},\"visibility\":10000,\"pop\":0,\"sys\":{\"pod\":\"d\"},\"dt_txt\":\"2025-02-06 09:00:00\"},{\"dt\":1738843200,\"main\":{\"temp\":2.32,\"feels_like\":-1.61,\"temp_min\":2.32,\"temp_max\":2.32,\"pressure\":1038,\"sea_level\":1038,\"grnd_level\":963,\"humidity\":75,\"temp_kf\":0},\"weather\":[{\"id\":804,\"main\":\"Clouds\",\"description\":\"Bedeckt\",\"icon\":\"04d\"}],\"clouds\":{\"all\":100},\"wind\":{\"speed\":4.28,\"deg\":67,\"gust\":8.38},\"visibility\":10000,\"pop\":0,\"sys\":{\"pod\":\"d\"},\"dt_txt\":\"2025-02-06 12:00:00\"},{\"dt\":1738854000,\"main\":{\"temp\":0.84,\"feels_like\":-3.51,\"temp_min\":0.84,\"temp_max\":0.84,\"pressure\":1037,\"sea_level\":1037,\"grnd_level\":962,\"humidity\":85,\"temp_kf\":0},\"weather\":[{\"id\":804,\"main\":\"Clouds\",\"description\":\"Bedeckt\",\"icon\":\"04d\"}],\"clouds\":{\"all\":100},\"wind\":{\"speed\":4.4,\"deg\":64,\"gust\":9.98},\"visibility\":10000,\"pop\":0,\"sys\":{\"pod\":\"d\"},\"dt_txt\":\"2025-02-06 15:00:00\"},{\"dt\":1738864800,\"main\":{\"temp\":-0.1,\"feels_like\":-5.07,\"temp_min\":-0.1,\"temp_max\":-0.1,\"pressure\":1037,\"sea_level\":1037,\"grnd_level\":962,\"humidity\":90,\"temp_kf\":0},\"weather\":[{\"id\":804,\"main\":\"Clouds\",\"description\":\"Bedeckt\",\"icon\":\"04n\"}],\"clouds\":{\"all\":100},\"wind\":{\"speed\":5.05,\"deg\":59,\"gust\":10.86},\"visibility\":10000,\"pop\":0,\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2025-02-06 18:00:00\"},{\"dt\":1738875600,\"main\":{\"temp\":-0.36,\"feels_like\":-5.45,\"temp_min\":-0.36,\"temp_max\":-0.36,\"pressure\":1036,\"sea_level\":1036,\"grnd_level\":961,\"humidity\":91,\"temp_kf\":0},\"weather\":[{\"id\":804,\"main\":\"Clouds\",\"description\":\"Bedeckt\",\"icon\":\"04n\"}],\"clouds\":{\"all\":100},\"wind\":{\"speed\":5.14,\"deg\":64,\"gust\":11.7},\"visibility\":10000,\"pop\":0,\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2025-02-06 21:00:00\"},{\"dt\":1738886400,\"main\":{\"temp\":-0.51,\"feels_like\":-5.8,\"temp_min\":-0.51,\"temp_max\":-0.51,\"pressure\":1034,\"sea_level\":1034,\"grnd_level\":959,\"humidity\":93,\"temp_kf\":0},\"weather\":[{\"id\":600,\"main\":\"Snow\",\"description\":\"Mäßiger Schnee\",\"icon\":\"13n\"}],\"clouds\":{\"all\":100},\"wind\":{\"speed\":5.43,\"deg\":67,\"gust\":11.49},\"visibility\":387,\"pop\":0.2,\"snow\":{\"3h\":0.14},\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2025-02-07 00:00:00\"},{\"dt\":1738897200,\"main\":{\"temp\":-0.72,\"feels_like\":-6.57,\"temp_min\":-0.72,\"temp_max\":-0.72,\"pressure\":1032,\"sea_level\":1032,\"grnd_level\":958,\"humidity\":89,\"temp_kf\":0},\"weather\":[{\"id\":804,\"main\":\"Clouds\",\"description\":\"Bedeckt\",\"icon\":\"04n\"}],\"clouds\":{\"all\":100},\"wind\":{\"speed\":6.42,\"deg\":73,\"gust\":12.41},\"visibility\":5828,\"pop\":0,\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2025-02-07 03:00:00\"},{\"dt\":1738908000,\"main\":{\"temp\":-0.89,\"feels_like\":-6.86,\"temp_min\":-0.89,\"temp_max\":-0.89,\"pressure\":1031,\"sea_level\":1031,\"grnd_level\":956,\"humidity\":90,\"temp_kf\":0},\"weather\":[{\"id\":600,\"main\":\"Snow\",\"description\":\"Mäßiger Schnee\",\"icon\":\"13n\"}],\"clouds\":{\"all\":100},\"wind\":{\"speed\":6.58,\"deg\":71,\"gust\":12.71},\"visibility\":2378,\"pop\":0.2,\"snow\":{\"3h\":0.14},\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2025-02-07 06:00:00\"},{\"dt\":1738918800,\"main\":{\"temp\":-0.5,\"feels_like\":-6.48,\"temp_min\":-0.5,\"temp_max\":-0.5,\"pressure\":1029,\"sea_level\":1029,\"grnd_level\":955,\"humidity\":84,\"temp_kf\":0},\"weather\":[{\"id\":600,\"main\":\"Snow\",\"description\":\"Mäßiger Schnee\",\"icon\":\"13d\"}],\"clouds\":{\"all\":100},\"wind\":{\"speed\":6.82,\"deg\":76,\"gust\":13.28},\"visibility\":10000,\"pop\":0.21,\"snow\":{\"3h\":0.2},\"sys\":{\"pod\":\"d\"},\"dt_txt\":\"2025-02-07 09:00:00\"},{\"dt\":1738929600,\"main\":{\"temp\":-0.14,\"feels_like\":-6,\"temp_min\":-0.14,\"temp_max\":-0.14,\"pressure\":1028,\"sea_level\":1028,\"grnd_level\":954,\"humidity\":90,\"temp_kf\":0},\"weather\":[{\"id\":600,\"main\":\"Snow\",\"description\":\"Mäßiger Schnee\",\"icon\":\"13d\"}],\"clouds\":{\"all\":100},\"wind\":{\"speed\":6.79,\"deg\":81,\"gust\":13.19},\"visibility\":5825,\"pop\":0.47,\"snow\":{\"3h\":0.39},\"sys\":{\"pod\":\"d\"},\"dt_txt\":\"2025-02-07 12:00:00\"},{\"dt\":1738940400,\"main\":{\"temp\":0.1,\"feels_like\":-5.57,\"temp_min\":0.1,\"temp_max\":0.1,\"pressure\":1028,\"sea_level\":1028,\"grnd_level\":954,\"humidity\":87,\"temp_kf\":0},\"weather\":[{\"id\":600,\"main\":\"Snow\",\"description\":\"Mäßiger Schnee\",\"icon\":\"13d\"}],\"clouds\":{\"all\":97},\"wind\":{\"speed\":6.52,\"deg\":87,\"gust\":12},\"visibility\":10000,\"pop\":0.49,\"snow\":{\"3h\":0.35},\"sys\":{\"pod\":\"d\"},\"dt_txt\":\"2025-02-07 15:00:00\"},{\"dt\":1738951200,\"main\":{\"temp\":-0.78,\"feels_like\":-6.71,\"temp_min\":-0.78,\"temp_max\":-0.78,\"pressure\":1028,\"sea_level\":1028,\"grnd_level\":954,\"humidity\":87,\"temp_kf\":0},\"weather\":[{\"id\":804,\"main\":\"Clouds\",\"description\":\"Bedeckt\",\"icon\":\"04n\"}],\"clouds\":{\"all\":97},\"wind\":{\"speed\":6.54,\"deg\":89,\"gust\":12.39},\"visibility\":10000,\"pop\":0.15,\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2025-02-07 18:00:00\"},{\"dt\":1738962000,\"main\":{\"temp\":-0.71,\"feels_like\":-6.82,\"temp_min\":-0.71,\"temp_max\":-0.71,\"pressure\":1028,\"sea_level\":1028,\"grnd_level\":954,\"humidity\":89,\"temp_kf\":0},\"weather\":[{\"id\":804,\"main\":\"Clouds\",\"description\":\"Bedeckt\",\"icon\":\"04n\"}],\"clouds\":{\"all\":100},\"wind\":{\"speed\":6.99,\"deg\":86,\"gust\":12.59},\"visibility\":10000,\"pop\":0,\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2025-02-07 21:00:00\"},{\"dt\":1738972800,\"main\":{\"temp\":-0.84,\"feels_like\":-6.49,\"temp_min\":-0.84,\"temp_max\":-0.84,\"pressure\":1028,\"sea_level\":1028,\"grnd_level\":953,\"humidity\":87,\"temp_kf\":0},\"weather\":[{\"id\":804,\"main\":\"Clouds\",\"description\":\"Bedeckt\",\"icon\":\"04n\"}],\"clouds\":{\"all\":100},\"wind\":{\"speed\":5.95,\"deg\":83,\"gust\":11.57},\"visibility\":10000,\"pop\":0,\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2025-02-08 00:00:00\"},{\"dt\":1738983600,\"main\":{\"temp\":-2.94,\"feels_like\":-8.71,\"temp_min\":-2.94,\"temp_max\":-2.94,\"pressure\":1028,\"sea_level\":1028,\"grnd_level\":953,\"humidity\":89,\"temp_kf\":0},\"weather\":[{\"id\":803,\"main\":\"Clouds\",\"description\":\"Überwiegend bewölkt\",\"icon\":\"04n\"}],\"clouds\":{\"all\":57},\"wind\":{\"speed\":5.19,\"deg\":81,\"gust\":11.61},\"visibility\":10000,\"pop\":0,\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2025-02-08 03:00:00\"},{\"dt\":1738994400,\"main\":{\"temp\":-3.43,\"feels_like\":-9.11,\"temp_min\":-3.43,\"temp_max\":-3.43,\"pressure\":1029,\"sea_level\":1029,\"grnd_level\":954,\"humidity\":89,\"temp_kf\":0},\"weather\":[{\"id\":803,\"main\":\"Clouds\",\"description\":\"Überwiegend bewölkt\",\"icon\":\"04n\"}],\"clouds\":{\"all\":57},\"wind\":{\"speed\":4.86,\"deg\":79,\"gust\":11.4},\"visibility\":10000,\"pop\":0,\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2025-02-08 06:00:00\"},{\"dt\":1739005200,\"main\":{\"temp\":-1.73,\"feels_like\":-7.5,\"temp_min\":-1.73,\"temp_max\":-1.73,\"pressure\":1029,\"sea_level\":1029,\"grnd_level\":954,\"humidity\":81,\"temp_kf\":0},\"weather\":[{\"id\":804,\"main\":\"Clouds\",\"description\":\"Bedeckt\",\"icon\":\"04d\"}],\"clouds\":{\"all\":100},\"wind\":{\"speed\":5.72,\"deg\":80,\"gust\":11.16},\"visibility\":10000,\"pop\":0,\"sys\":{\"pod\":\"d\"},\"dt_txt\":\"2025-02-08 09:00:00\"},{\"dt\":1739016000,\"main\":{\"temp\":0.78,\"feels_like\":-4.28,\"temp_min\":0.78,\"temp_max\":0.78,\"pressure\":1027,\"sea_level\":1027,\"grnd_level\":953,\"humidity\":68,\"temp_kf\":0},\"weather\":[{\"id\":804,\"main\":\"Clouds\",\"description\":\"Bedeckt\",\"icon\":\"04d\"}],\"clouds\":{\"all\":100},\"wind\":{\"speed\":5.63,\"deg\":81,\"gust\":8.64},\"visibility\":10000,\"pop\":0,\"sys\":{\"pod\":\"d\"},\"dt_txt\":\"2025-02-08 12:00:00\"},{\"dt\":1739026800,\"main\":{\"temp\":0.71,\"feels_like\":-4.23,\"temp_min\":0.71,\"temp_max\":0.71,\"pressure\":1027,\"sea_level\":1027,\"grnd_level\":953,\"humidity\":71,\"temp_kf\":0},\"weather\":[{\"id\":804,\"main\":\"Clouds\",\"description\":\"Bedeckt\",\"icon\":\"04d\"}],\"clouds\":{\"all\":100},\"wind\":{\"speed\":5.36,\"deg\":80,\"gust\":10.36},\"visibility\":10000,\"pop\":0,\"sys\":{\"pod\":\"d\"},\"dt_txt\":\"2025-02-08 15:00:00\"},{\"dt\":1739037600,\"main\":{\"temp\":-1,\"feels_like\":-6.39,\"temp_min\":-1,\"temp_max\":-1,\"pressure\":1027,\"sea_level\":1027,\"grnd_level\":953,\"humidity\":81,\"temp_kf\":0},\"weather\":[{\"id\":804,\"main\":\"Clouds\",\"description\":\"Bedeckt\",\"icon\":\"04n\"}],\"clouds\":{\"all\":100},\"wind\":{\"speed\":5.38,\"deg\":77,\"gust\":12.49},\"visibility\":10000,\"pop\":0,\"sys\":{\"pod\":\"n\"},\"dt_txt\":\"2025-02-08 18:00:00\"}],\"city\":{\"id\":2815317,\"name\":\"Waidhaus\",\"coord\":{\"lat\":49.6422,\"lon\":12.4952},\"country\":\"DE\",\"population\":2467,\"timezone\":3600,\"sunrise\":1738564777,\"sunset\":1738598882}}";
            if (data != null)
            {
                isDataValid = true;
            }
            else
            {
                isDataValid = false;
            }
        }

        private string GetData()
        {
            try
            {
                WebRequest request = WebRequest.Create(url);

                // Festlegen der Methode (GET, POST, etc.)
                request.Method = "GET";

                // Senden der Anfrage und Empfangen der Antwort
                using (WebResponse response = request.GetResponse())
                {
                    // Abrufen des Antwortstreams
                    using (Stream dataStream = response.GetResponseStream())
                    {
                        // Lesen des Antwortstreams
                        using (StreamReader reader = new StreamReader(dataStream))
                        {
                            return reader.ReadToEnd();
                        }
                    }
                }
            }
            catch (WebException webEx)
            {
                // Behandlung von Web-spezifischen Ausnahmen
                Console.WriteLine($"WebException: {webEx.Message}");
                return null;
            }
            catch (Exception ex)
            {
                // Behandlung aller anderen Ausnahmen
                Console.WriteLine($"Exception: {ex.Message}");
                return null;
            }

        }


        private void ParseWeatherData(string data)
        {
            string lines = "";
            string path = DataExchange.instance.pathProt;
            DateTime time;

            ActualWeather weatherData = JsonConvert.DeserializeObject<ActualWeather>(data);

            Console.WriteLine($"Ort: {weatherData.name}");
            Console.WriteLine($"Temperatur: {weatherData.main.temp}°C");
            Console.WriteLine($"Wetter: {weatherData.weather[0].description}");

            string direction = degreeToDirection(weatherData.wind.deg);

            DataExchange.instance.weather = $"{weatherData.main.temp}C {weatherData.wind.speed}m/s {direction}";

            time = UnixTimeStampToDateTime(weatherData.dt);
            lines += time + "\n";

            lines += $"Temperatur: {weatherData.main.temp}°C\n";
            lines += $"gefühlte Temperatur: {weatherData.main.feels_like}°C\n";
            lines += $"Luftfeuchtigkeit: {weatherData.main.humidity}%\n";
            lines += $"Luftdruck: {weatherData.main.pressure} hPa\n";
            lines += $"Sichtweite: {weatherData.visibility} m\n";
            lines += $"Windgeschwindigkeit: {weatherData.wind.speed} m/s\n";
            lines += $"Windrichtung: {degreeToDirection(weatherData.wind.deg, true)}\n";
            lines += $"Wetter: {weatherData.weather[0].description}\n\n";

            // Schreibe die Daten in die Datei
            FileUtility.Append($"{path}weather_{DateTime.Now.Year}_{DateTime.Now.Month}_{DateTime.Now.Day}.txt", lines);
            
        }

        private void ParseForecastData(string data)
        {
            Forecast = JsonConvert.DeserializeObject<ForecastWeather>(data);

            string path = DataExchange.instance.pathProt;
            string lines = "";
            DateTime time;

            Console.WriteLine($"Ort: {Forecast.City.Name}");
            Console.WriteLine($"Temperatur: {Forecast.List[0].Main.Temp}°C");
            Console.WriteLine($"Wetter: {Forecast.List[0].Weather[0].Description}");
            for (int i = 0; i < 40; i++)
            {
                time = UnixTimeStampToDateTime(Forecast.List[i].Dt);
                lines += time + "\n";

                lines += $"Temperatur: {Forecast.List[i].Main.Temp}°C\n";
                lines += $"Windrichtung: {degreeToDirection(Forecast.List[i].Wind.Deg, true)}\n";
                lines += $"Wetter: {Forecast.List[0].Weather[0].Description}\n\n";

                //Console.WriteLine(degreeToDirection(forecastData.List[i].Wind.Deg));
                //Console.WriteLine(degreeToDirection(forecastData.List[i].Wind.Deg, true));
                //Console.WriteLine();
            }
            FileUtility.Append($"{path}forcast_{DateTime.Now.Year}_{DateTime.Now.Month}_{DateTime.Now.Day}.txt", lines);
        }

        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }


        public void call()
        {
            data = GetData();

            if (data != null)
            {
                if (url.Contains("forecast")) ParseForecastData(data);
                else ParseWeatherData(data);
            }
            else
            {
                Console.WriteLine("no data form apicall\n}");
            }
        }

        public void threadCall()
        {
            while (true)
            { 
                if (url.Contains("forecast")) ParseForecastData(data);
                else ParseWeatherData(data);
                Thread.Sleep(3600);
            }
        }

        public string degreeToDirection(double degree, bool longString = false)
        {
            string direction = string.Empty;

            if (degree >= 337.5 || degree < 22.5)
            {
                direction = "N";
            }
            else if (degree >= 22.5 && degree < 67.5)
            {
                direction = "NO";
            }
            else if (degree >= 67.5 && degree < 112.5)
            {
                direction = "O";
            }
            else if (degree >= 112.5 && degree < 157.5)
            {
                direction = "SO";
            }
            else if (degree >= 157.5 && degree < 202.5)
            {
                direction = "S";
            }
            else if (degree >= 202.5 && degree < 247.5)
            {
                direction = "SW";
            }
            else if (degree >= 247.5 && degree < 292.5)
            {
                direction = "W";
            }
            else if (degree >= 292.5 && degree < 337.5)
            {
                direction = "NW";
            }
            else
            {
                direction = "N/A";
            }

            if (longString) direction = windDirectionLong[direction];


            return direction;
        }
        

    }
}
