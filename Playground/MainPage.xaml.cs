using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Diagnostics;
using Playground.Helpers;

namespace Playground.Pages {

    /// <summary>
    /// This is the Main Page to help navigate to the other demonstrations
    /// </summary>
    public partial class MainPage : ContentPage {

        // The Pages
        public List<Type> Pages { get; }

        public MainPage() {
            // Rememeber to add your new page in here so that it shows up in the
            // list
            Pages = new List<Type>{
                typeof(PlaygroundPage),
                typeof(RealmPage),
                typeof(TaskAroundPage),
                typeof(MultiColPage)
            };
            BindingContext = this;
            InitializeComponent();
        }

        // Navigate to the new page, or explain why it failed!
        void Handle_ItemTapped(object sender, ItemTappedEventArgs args) {
            var page = args.Item as Type;
            if (page == null) {
                DisplayAlert("Error", "Invalid Page", "Ok");
                return;
            }
            try {
                Navigation.PushAsync((Page)Activator.CreateInstance(page));
            } catch (Exception e) {
                Debug.WriteLine(e);
                DisplayAlert("Failed to Open page", e.Message, "ok");
            }
        }
    }
}
