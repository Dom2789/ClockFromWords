using System;
using System.Collections.Generic;
using Newtonsoft.Json;


public class ForecastWeather
{

    [JsonProperty("cod")]
    public string Cod { get; set; }

    [JsonProperty("message")]
    public int Message { get; set; }

    [JsonProperty("cnt")]
    public int Cnt { get; set; }

    [JsonProperty("list")]
    public List<WeatherEntry> List { get; set; }

    [JsonProperty("city")]
    public City City { get; set; }

}

        public class WeatherEntry
        {
            [JsonProperty("dt")]
            public long Dt { get; set; }

            [JsonProperty("main")]
            public MainData Main { get; set; }

            [JsonProperty("weather")]
            public List<WeatherDescription> Weather { get; set; }

            [JsonProperty("clouds")]
            public Clouds Clouds { get; set; }

            [JsonProperty("wind")]
            public Winds Wind { get; set; }

            [JsonProperty("visibility")]
            public int Visibility { get; set; }

            [JsonProperty("pop")]
            public double Pop { get; set; }

            [JsonProperty("sys")]
            public Sys Sys { get; set; }

            [JsonProperty("dt_txt")]
            public string DtTxt { get; set; }
        }

        public class MainData
        {
            [JsonProperty("temp")]
            public double Temp { get; set; }

            [JsonProperty("feels_like")]
            public double FeelsLike { get; set; }

            [JsonProperty("temp_min")]
            public double TempMin { get; set; }

            [JsonProperty("temp_max")]
            public double TempMax { get; set; }

            [JsonProperty("pressure")]
            public int Pressure { get; set; }

            [JsonProperty("sea_level")]
            public int SeaLevel { get; set; }

            [JsonProperty("grnd_level")]
            public int GrndLevel { get; set; }

            [JsonProperty("humidity")]
            public int Humidity { get; set; }

            [JsonProperty("temp_kf")]
            public double TempKf { get; set; }
        }

        public class WeatherDescription
        {
            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("main")]
            public string Main { get; set; }

            [JsonProperty("description")]
            public string Description { get; set; }

            [JsonProperty("icon")]
            public string Icon { get; set; }
        }


        public class Winds
        {
            [JsonProperty("speed")]
            public double Speed { get; set; }

            [JsonProperty("deg")]
            public int Deg { get; set; }

            [JsonProperty("gust")]
            public double Gust { get; set; }
        }

        public class City
        {
            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("coord")]
            public Coord Coord { get; set; }

            [JsonProperty("country")]
            public string Country { get; set; }

            [JsonProperty("population")]
            public int Population { get; set; }

            [JsonProperty("timezone")]
            public int Timezone { get; set; }

            [JsonProperty("sunrise")]
            public int Sunrise { get; set; }

            [JsonProperty("sunset")]
            public int Sunset { get; set; }
        }




