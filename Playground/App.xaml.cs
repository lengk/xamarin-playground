using Xamarin.Forms;
using Playground.Pages;
using Playground.Helpers;

namespace Playground {
    public partial class App : Application {
        public App() {
            InitializeComponent();
            MainPage = new RealmPage();
        }

        protected override void OnStart() {
            // Handle when your app starts
        }

        protected override void OnSleep() {
            // Handle when your app sleeps
        }

        protected override void OnResume() {
            // Handle when your app resumes
        }
    }
}
