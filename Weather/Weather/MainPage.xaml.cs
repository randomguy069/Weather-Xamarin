using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json;

namespace Weather
{
    public partial class MainPage : ContentPage
    {
        public class WeatherDetails
        {
            public string summary { get; set; }
            public float temperature { get; set; }
            public string city { get; set; }
        }
        public MainPage()
        {
            InitializeComponent();
        }

        private async void GetWeatherButton_Clicked(object sender, EventArgs e)
        {
            var cityName = weatherEntry.Text;
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.GetAsync("http://kams-weather-api.herokuapp.com/"+cityName))
                {
                    using(HttpContent content = response.Content)
                    {
                        var myContent = await content.ReadAsStringAsync();
                        WeatherDetails details = JsonConvert.DeserializeObject<WeatherDetails>(myContent);
                        float celTemp = returnTemperatureInCelsius(details.temperature);
                        string tempDetails = "Currently the temperature is at " + details.temperature + " in " + details.city + " ." + details.summary;
                        await DisplayAlert("Weather details", tempDetails, "Okay!");
                    }
                }

            }
            
        }
        private static float returnTemperatureInCelsius(float temp)
        {
            float celTemp = (temp - 32) * (5 / 9);
            return celTemp;
        }
    }
}
