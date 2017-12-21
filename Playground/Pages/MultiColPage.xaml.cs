using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Playground.ViewModels;

namespace Playground.Pages {
    public partial class MultiColPage : ContentPage {
        public MultiColPage() {
            BindingContext = new MultiColPageViewModel(this);
            InitializeComponent();
        }

        void AddClicked(object sender, System.EventArgs e) {
            throw new NotImplementedException();
        }

        void RemoveClicked(object sender, EventArgs args) {
            throw new NotImplementedException();
        }
    }
}
