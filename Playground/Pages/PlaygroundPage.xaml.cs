using Xamarin.Forms;
using Playground.Helpers;
using System;

namespace Playground {
    public partial class PlaygroundPage : ContentPage {

        public PlaygroundPage() {
            InitializeComponent();
        }

        void SetFrame1(object sender, EventArgs agrs) {
            ContentFrame.Content = GetLayout("Text");
        }

        void SetFrame2(object sender, EventArgs agrs) {
            ContentFrame.Content = GetLayout("Changed!");
        }

        StackLayout GetLayout(string insidetext){
            var layout = new StackLayout();
            layout.Children.Add(new Label() { Text = insidetext });
            return layout;
        }
    }
}
