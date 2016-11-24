using MSAMobileApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace MSAMobileApp.Views {
    public partial class PlacedOrdersPage : ContentPage {

        private View initialContent;

        public PlacedOrdersPage() {
            InitializeComponent();
            initialContent = Content;
        }

        protected override async void OnAppearing() {
            // GET ORDERS BASED ON EMAIL
            List<Order> orders = await AzureManager.AzureManagerInstance.GetOrders();
            List<Order> userOrders = orders.Where(o => o.Email == User.CurrentUserInstance.Email).ToList();

            if (userOrders.Count > 0)
                GenerateOrdersPage(userOrders);
            else
                Content = initialContent;
        }

        private void GenerateOrdersPage(List<Order> orders) {
                // Generating page of orders
                Label header = new Label {
                    Text = "Your Cart:",
                    FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                    HorizontalOptions = LayoutOptions.Center
                };

                ActivityIndicator AI = new ActivityIndicator();
                AI.IsRunning = false;

                // Adding all food items to imagecells
                List<ImageCell> orderDBLinks = new List<ImageCell>();
                foreach (Order order in orders) {
                    ImageCell cellItem = new ImageCell {
                        // Some differences with loading images in initial release.
                        ImageSource = new Uri("http://www.lexingtoncolony.com/wp-content/uploads/2014/07/dinner-thumbnail.png"),
                        Text = $"Your Order [ {order.Date} ] - Total: ${order.TotalPrice}",
                        Detail = $"Food Items: {order.FoodNames}"
                    };
                    cellItem.Tapped += async (sender, args) => {
                        var answer = await DisplayAlert($"Cancel Order", "Would you like to cancel this order?", "Yes", "No");
                        if (answer) {
                            // Remove it from database
                            await AzureManager.AzureManagerInstance.CancelOrder(order);
                            orders.Remove(order);
                            await DisplayAlert("SUCCESS", "Successfuly cancelled your order", "OK");
                            if (orders.Count == 0)
                                MenuPage.ChangePage(new NavigationPage(new TabbedPage { Children = { new OrderPage(), new PlacedOrdersPage() } })); // FIX THIS
                            else OnAppearing();
                        }
                    };
                    orderDBLinks.Add(cellItem);
                }

                TableView tableView = new TableView {
                    Intent = TableIntent.Form,
                    Root = new TableRoot { new TableSection { orderDBLinks } }
                };

                // Build the page.
                Content = new StackLayout { Children = { header, tableView, AI } };
        }


    }
}
