using System;
using System.Windows.Input;
using Xamarin.Forms;
namespace Playground.Extensions
{
    public static class ContentPageExtensions
    {

        public static void ShowPopupOver(this ContentPage page, View v, string text, int padding = 5, short xpos = 1, short ypos = 1)
        {
            v.IsEnabled = false;
            Frame popupframe = new Frame();
            popupframe.Content = new Label
            {
                Text = text,
                TextColor = Color.White,
                FontSize= 15
            };
            popupframe.BackgroundColor = Color.Transparent;

            var content = page.Content;

            if (content is Layout<View>)
            {

                var layout = content as Layout<View>;
                var contentview = new ContentView();

                ICommand oldcommand = null;
                var newcommand = new Command(() =>
                {
                    layout.Children.Remove(contentview);
                    layout.Children.Remove(popupframe);
                    layout.ForceLayout();
                    if (oldcommand != null)
                    {   
                        oldcommand.Execute(null);
                    }
                    if (v is Button)
                    {
                        ((Button)v).SendClicked();
                    }
                });

                foreach (var GR in v.GestureRecognizers)
                {
                    if (GR is TapGestureRecognizer)
                    {
                        oldcommand = ((TapGestureRecognizer)GR).Command;
                        ((TapGestureRecognizer)GR).Command = newcommand;
                        break;
                    }
                }

                if (v is Button)
                {
                    oldcommand = new Command(() =>
                    {
                        ((Button)v).SendClicked();
                    });
                }

                v.IsEnabled = true;

                contentview.WidthRequest = page.Width;
                contentview.HeightRequest = page.Height;
                contentview.TranslationX = page.X;
                contentview.TranslationY = page.Y;
                contentview.BackgroundColor = Color.Black;
                contentview.Opacity = 0.8;


                layout.Children.Add(contentview);
                layout.RaiseChild(v);
                layout.Children.Add(popupframe);

                popupframe.GestureRecognizers.Add(new TapGestureRecognizer()
                {
                    Command = newcommand
                });

                popupframe.TranslationX = v.Width + v.X + padding;
                popupframe.TranslationY = v.Height + v.Y + padding;
            }

            if (!v.IsEnabled)
            {
                v.IsEnabled = true;
            }
        }
    }
}
