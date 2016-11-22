using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using MSAMobileApp.Models;

namespace MSAMobileApp {
    public class AzureManager {
        private static AzureManager instance;
        private MobileServiceClient client;
        private IMobileServiceTable<User> userTable;
        private IMobileServiceTable<Food> foodTable;
        private IMobileServiceTable<Order> orderTable;

        private AzureManager() {
            this.client = new MobileServiceClient("https://msarestaurantapp.azurewebsites.net");
            this.userTable = this.client.GetTable<User>();
            this.foodTable = this.client.GetTable<Food>();
            this.orderTable = this.client.GetTable<Order>();
        }

        public MobileServiceClient AzureClient {
            get { return client; }
        }

        public static AzureManager AzureManagerInstance {
            get {
                if (instance == null) {
                    instance = new AzureManager();
                }
                return instance;
            }
        }

        // GET Users - email validation
        public async Task<List<User>> GetUsers() {
            return await this.userTable.ToListAsync();
        }

        // POST Users - user creation
        public async Task AddUser(User user) {
            await this.userTable.InsertAsync(user);
        }

        // GET food items
        public async Task<List<Food>> GetFoodItems() {
            return await this.foodTable.ToListAsync();
        }

        // POST Order - order creation
        public async Task PlaceOrder(Order order) {
            await this.orderTable.InsertAsync(order);
        }

        // Example query
        public async Task<List<User>> GetVaidPasswordUsers() {
            return await userTable.Where(s => s.Password != "123").ToListAsync();
        }

    }
}
