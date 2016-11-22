using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSAMobileApp.Models {
    class WeatherObject {
        public class Coord {
            public double lon { get; set; }
            public double lat { get; set; }
        }

        public class Weather {
            public int id { get; set; }
            public string main { get; set; }
            public string description { get; set; }
            public string icon { get; set; }
        }

        public class Main {
            public double temp { get; set; }
            public double pressure { get; set; }
            public double humidity { get; set; }
            public double temp_min { get; set; }
            public double temp_max { get; set; }
        }

        public class Wind {
            public double speed { get; set; }
            public double deg { get; set; }
        }

        public class Clouds {
            public int all { get; set; }
        }

        public class Sys {
            public int type { get; set; }
            public int id { get; set; }
            public double message { get; set; }
            public string country { get; set; }
            public double sunrise { get; set; }
            public double sunset { get; set; }
        }

        public class RootObject {
            public Coord coord { get; set; }
            public List<Weather> weather { get; set; }
            public string @base { get; set; }
            public Main main { get; set; }
            public Wind wind { get; set; }
            public Clouds clouds { get; set; }
            public double dt { get; set; }
            public Sys sys { get; set; }
            public int id { get; set; }
            public string name { get; set; }
            public double cod { get; set; }
        }
    }
}
