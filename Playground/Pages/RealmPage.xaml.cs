using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Playground.ViewModels;

namespace Playground.Helpers {
    public partial class RealmBindingPage : ContentPage {

        RealmBindingPageViewModel Model { get { return BindingContext as RealmBindingPageViewModel; } }

        public RealmBindingPage() {
            BindingContext = new RealmBindingPageViewModel(this);
            InitializeComponent();
        }

        void Handle_ItemTapped(object sender, ItemTappedEventArgs e) {

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
