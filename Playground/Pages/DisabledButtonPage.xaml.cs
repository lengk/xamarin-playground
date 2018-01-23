using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Playground.Pages {
    public partial class DisabledButtonPage : ContentPage {
        public DisabledButtonPage() {
            InitializeComponent();
        }


        bool buttondown = false;
        async void Handle_Clicked(object sender, EventArgs args) {
            if (buttondown) {
                Debug.WriteLine("Button Pressed while Pressing!");
            } else {
                Debug.WriteLine("Button Dislabed");
            }
            SetButtonEnabled(false);
            int delay;
            int.TryParse(EntryDelay.Text, out delay);
            buttondown = true;
            await Task.Delay(delay == 0 ? 1000 : delay);
            buttondown = false;
            SetButtonEnabled(true);
            Debug.WriteLine("Button Enabled");
        }

        void SetButtonEnabled(bool val) {
            Button.IsEnabled = val;
        }

        async void LockClicked(object sender, EventArgs args) {
            int delay;
            int.TryParse(EntryDelay.Text, out delay);
            delay = delay == 0 ? 1000 : delay;
            Debug.WriteLine($"Delaying by {delay}");
            await Task.Delay(delay);
        }
    }

}
