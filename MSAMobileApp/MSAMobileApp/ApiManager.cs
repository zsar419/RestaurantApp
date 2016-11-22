using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MSAMobileApp.Models;

namespace MSAMobileApp {
    class ApiManager {
        private static ApiManager instance;
        private HttpClient client;

        private ApiManager() {
            client = new HttpClient();
        }

        public static ApiManager ApiManagerInstance {
            get {
                if (instance == null) 
                    instance = new ApiManager();
                return instance;
            }
        }

        public async Task<WeatherObject.RootObject> GetJsonWeatherObjectByCity(string city) {
            string res = await client.GetStringAsync(new Uri("http://api.openweathermap.org/data/2.5/weather?q=" + city + "&units=metric&APPID=3118bd0d9c7e3dfd7535f6f99afec169"));
            WeatherObject.RootObject rootObject = JsonConvert.DeserializeObject<WeatherObject.RootObject>(res);
            return rootObject;
        }

        public async Task<WeatherObject.RootObject> GetJsonWeatherObjectByCoords(double lat, double lon) {
            string res = await client.GetStringAsync(new Uri("http://api.openweathermap.org/data/2.5/weather?lat="+lat+"&lon="+lon+ "&units=metric&APPID=3118bd0d9c7e3dfd7535f6f99afec169"));
            WeatherObject.RootObject rootObject = JsonConvert.DeserializeObject<WeatherObject.RootObject>(res);
            return rootObject;
        }

    }
}
