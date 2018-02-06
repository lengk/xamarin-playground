using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

namespace Playground.Pages
{
    public partial class OverlayView : ContentPage
    {
        Frame f = new Frame();
            
        public OverlayView()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            ShowPopupOver(button,"asda");
        }

        void ShowPopupOver(View v, string text, int padding = 5){
            Frame f = new Frame();
            f.Content = new Label
            {
                Text = text
            };
            f.BackgroundColor = Color.Accent;
            f.Opacity = 0.2;
            f.TranslationX = v.Width + v.X + padding;
            f.TranslationY = v.Height + v.Y + padding;
            if (Application.Current.MainPage is Page)
            {
                if (Content is Layout<View>)
                {

                    var layout = Content as Layout<View>;
                    layout.Children.Add(f);
                }
            }
        }
    }
}
