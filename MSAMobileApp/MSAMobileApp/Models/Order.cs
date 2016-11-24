using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MSAMobileApp.Models {
    public class Order {

        [JsonProperty(PropertyName = "Id")]
        public string ID { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string FoodNames { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "menuitems")]
        public string TotalPrice { get; set; }

        [JsonIgnore]
        public DateTime Date { get; set; }
    }
}
