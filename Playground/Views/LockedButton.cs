using System;

using Xamarin.Forms;
using System.Diagnostics;

namespace Playground.Views {
    public class LockedButton : Button {

        public LockedButton() {
            base.Clicked += (s, e) => {
                Debug.WriteLine("Disabling Button");
                IsEnabled = false;
            };
            base.Clicked += EndLock;
        }

        public new event EventHandler Clicked {
            add {
                base.Clicked -= EndLock;
                base.Clicked += value;
                base.Clicked += EndLock;
            }
            remove {
                base.Clicked -= value;
            }
        }


        void EndLock(object sender, EventArgs args) {
            Debug.WriteLine("Enabling Button");
            IsEnabled = true;
        }


        protected override void OnPropertyChanged(string propertyName = null) {
            base.OnPropertyChanged(propertyName);
            Debug.WriteLine(propertyName);
        }
    }
}

