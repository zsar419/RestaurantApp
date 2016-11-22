﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSAMobileApp.Models;

using Xamarin.Forms;
using System.Threading;

namespace MSAMobileApp.Views {
    public partial class OrderPage : ContentPage {

        private View initialContent;

        public OrderPage() {
            InitializeComponent();
            ToMenuBtn.Clicked += (sender, e) => { MenuPage.ChangePage(MenuPage.pages[1], 1); };
            initialContent = Content;
        }

        protected override async void OnAppearing() {
            UpdateCart();
        }

        void UpdateCart() {
            if (Food.CartInstance.Count > 0) {
                cartInfo.Text = "Your Cart:";
                ToMenuBtn.IsVisible = false;
                CreateLinkableCells(Food.CartInstance);
            } else Content = initialContent;
        }

        private void CreateLinkableCells(List<Food> FoodDB) {
            // Creating page header
            Label header = new Label {
                Text = "Your Cart:",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center
            };

            // Adding all food items to imagecells
            List<ImageCell> foodDBLinks = new List<ImageCell>();
            foreach (Food item in FoodDB) {
                ImageCell cellItem = new ImageCell {
                    // Some differences with loading images in initial release.
                    ImageSource = item.Photo,
                    Text = $"{item.Name} - ${item.Price}",
                    Detail = $"{item.Description}",
                };
                cellItem.Tapped += async (sender, args) => {
                    var answer = await DisplayAlert($"{item.Name}", "Would you like to remove item from cart?", "Yes", "No");
                    if(answer) {
                        Food.CartInstance.Remove(item);
                        if (Food.CartInstance.Count == 0)
                            MenuPage.ChangePage(new NavigationPage(new OrderPage())); // FIX THIS
                        else OnAppearing();
                    }
                };
                foodDBLinks.Add(cellItem);
            }

            TableView tableView = new TableView {
                Intent = TableIntent.Form,
                Root = new TableRoot { new TableSection { foodDBLinks } }
            };

            double total = FoodDB.Sum(s => s.Price);
            Button orderButton = new Button {
                Text = $"Place Order - [ ${total} ]",
                BackgroundColor = Color.Green,
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Fill,
            };

            ActivityIndicator AI = new ActivityIndicator();
            AI.IsRunning = false;

            string foodOrderIDs = "";
            foreach (Food item in FoodDB) foodOrderIDs += (item.MenuId+" ");

            orderButton.Clicked += async (sender, e) => {
                Order newOrder = new Order() {
                    Name = User.CurrentUserInstance.Name,
                    Email = User.CurrentUserInstance.Email,
                    FoodItemIDs = foodOrderIDs,
                    Date = DateTime.Now
                };
                AI.IsRunning = true;
                await AzureManager.AzureManagerInstance.PlaceOrder(newOrder);
                await DisplayAlert("Success", "Your order has been successfully placed", "OK");
                // foreach (var f in Food.CartInstance) Food.CartInstance.Remove(f);
                Food.CartInstance.Clear();
                AI.IsRunning = false;
                MenuPage.ChangePage(new NavigationPage(new OrderPage())); // FIX THIS
            };
            // Build the page.
            Content = new StackLayout { Children = { header, tableView, AI, orderButton } };
        }

    }
}
