using System;
using Playground.Extensions;
using Xamarin.Forms;

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
            this.ShowPopupOver(button, "asda");
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            
        }

        ContentPage GetContentPage(Page page)
        {
            if (page is ContentPage)
            {
                return (ContentPage)page;
            }

            if (page is MultiPage<Page>)
            {
                return GetContentPage(((MultiPage<Page>)page).CurrentPage);
            }
            if (page is NavigationPage)
            {
                return GetContentPage((page as NavigationPage).CurrentPage);
            }
            throw new ArgumentException("Cannot find page in view stack");
        }

    }
}
