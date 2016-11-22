using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MSAMobileApp.Models {
    //enum Category { starters, mains, desserts, drinks };
    public class Food {

        [JsonIgnore]
        public static List<Food> Cart = new List<Food>();
        public static List<Food> CartInstance {
            get {
                if (Cart == null) {
                    Cart = new List<Food>();
                }
                return Cart;
            }
        }

        [JsonProperty(PropertyName = "Id")]
        public string ID { get; set; }

        [JsonProperty(PropertyName = "menuid")]
        public int MenuId { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "photo")]
        public string Photo { get; set; }

        [JsonProperty(PropertyName = "price")]
        public double Price { get; set; }

        [JsonProperty(PropertyName = "category")]
        public string Category { get; set; }

        [JsonProperty(PropertyName = "type")] // 1 = cold, 2 = hot
        public int Type { get; set; }

        [JsonProperty(PropertyName = "sale")]
        public bool Sale { get; set; }

        [JsonIgnore]
        public DateTime Date { get; set; }
    }
}
