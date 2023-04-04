using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeatherApp
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
            GetCurrentWeather();
        }
        private void FrmMain_load(object sender, EventArgs e)
        {
            // Set up the label to display the current time
            label1.Text = DateTime.Now.ToString("h:mm:ss tt");
            label2.Text = DateTime.Now.ToString("MM/dd/yyyy");
        }

        private void GetCurrentWeather()
        {
            // Replace YOUR_API_KEY with your OpenWeatherMap API key
            string apiKey = "e4e8d3d399369f3fff6ac11d5d6ec57e";

            // Set the city name to Surabaya
            string cityName = "Surabaya";

            // Create an HTTP client to send a request to the OpenWeatherMap API
            HttpClient client = new HttpClient();

            // Send a GET request to the OpenWeatherMap API to get the current weather for Surabaya
            HttpResponseMessage response = client.GetAsync($"http://api.openweathermap.org/data/2.5/weather?q={cityName}&appid={apiKey}&units=metric").Result;

            // Read the response as a string
            string responseContent = response.Content.ReadAsStringAsync().Result;

            // Deserialize the response JSON into a WeatherData object using Newtonsoft.Json
            WeatherData weatherData = JsonConvert.DeserializeObject<WeatherData>(responseContent);

            // Display the current weather in a label control
            label4.Text = $"{weatherData.Main.Temp}°C";
            label5.Text = $"{weatherData.Weather[0].Description}.";
        }

        // Define a class to represent the weather data returned by the OpenWeatherMap API
        public class WeatherData
        {
            public MainData Main { get; set; }
            public Weather[] Weather { get; set; }
        }

        public class MainData
        {
            public float Temp { get; set; }
        }

        public class Weather
        {
            public string Description { get; set; }
        }

    }
}
