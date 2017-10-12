using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Playground.ViewModels;

namespace Playground.Helpers {
    public partial class RealmPage : ContentPage {

        RealmPageViewModel Model { get { return BindingContext as RealmPageViewModel; } }

        public RealmPage() {
            BindingContext = new RealmPageViewModel(this);
            InitializeComponent();
        }

        void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e) {

        }

        void Handle_Clicked(object sender, System.EventArgs e) {
            throw new NotImplementedException();
        }

        void Add(object sender, System.EventArgs e) {
            Model.Add();
        }

        void Clear(object sender, System.EventArgs e) {
            Model.Clear();
        }
    }
}
