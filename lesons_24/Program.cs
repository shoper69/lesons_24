using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace lesons_24
{

    class Program
    {
        static HttpClient client = new HttpClient();
        static readonly string key = "84acd5cec473e3ccf7e7e62e780c21c4\n";

        static async Task Main(string[] args)
        {
            Console.WriteLine("Enter latitude and longitude");
            var lon = double.Parse(Console.ReadLine());
            var lat = double.Parse(Console.ReadLine());
            Uri url = new Uri($"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={key}");
            var data = await GetContent(url);
            Console.Clear();
            ShowInfo(data);
            Console.ReadLine();
        }

        static async Task<Data> GetContent(Uri url)
        {
            var content = await client.GetStringAsync(url);
            Console.WriteLine(content);
            var data = JsonConvert.DeserializeObject<Data>(content);
            return data;
        }

        static void ShowInfo(Data data)
        {
            Console.WriteLine($"Longitude: {data.Coord.Lon}\nLatitude: {data.Coord.Lat}");
            foreach (var weather in data.Weather)
            {
                Console.WriteLine($"Weather: {weather.Main}\nDescription: {weather.Description}");
            }
            Console.WriteLine($"Wind gust: {data.Wind.Gust}\nWind speed: {data.Wind.Speed}");
        }
    }
}