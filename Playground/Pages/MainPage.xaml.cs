using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Diagnostics;
using Playground.Helpers;

namespace Playground.Pages {
    public partial class MainPage : ContentPage {
        public List<Page> Pages { get; }

        public MainPage() {
            Pages = new List<Page>{
                new PlaygroundPage(),
                new RealmPage(),
                new TaskAroundPage()
            };
            BindingContext = this;
            InitializeComponent();
        }

        void Handle_ItemTapped(object sender, ItemTappedEventArgs args) {
            var page = args.Item as Page;
            if (page == null) {
                DisplayAlert("Error", "Invalid Page", "Ok");
                return;
            }
            try {
                Navigation.PushAsync(page);
            } catch (Exception e) {
                Debug.WriteLine(e);
                DisplayAlert("Failed to Open page", e.Message, "ok");
            }
        }
    }
}
