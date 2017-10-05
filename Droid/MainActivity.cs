using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Xamarin.Forms.Platform.Android;

namespace Playground.Droid {
    [Activity(Label = "Playground", Icon = "@drawable/icon", Theme = "@style/MyTheme",
              MainLauncher = true, LaunchMode = LaunchMode.SingleTask,
              ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity {
        public static Activity Instance;

        protected override void OnCreate(Bundle bundle) {
            Instance = this;
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            LoadApplication(new App());
        }


        protected override void OnNewIntent(Intent intent) {
            base.OnNewIntent(intent);
        }
    }
}