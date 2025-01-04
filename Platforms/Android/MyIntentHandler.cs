using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;  // Needed for OnCreate method
using Android.Widget;
using Google.Android.Material.Snackbar;
using System;

namespace ReceiveApplicationVisualStudio
{
    [Activity(
        Name = "com.companyname.ReceiveApplicationVisualStudio.MyIntentHandler",
        Theme = "@style/Maui.SplashTheme",
        LaunchMode = LaunchMode.SingleTask, // Important to prevent crashing
        MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density
    )]
    [IntentFilter(new[] { Intent.ActionSend }, Categories = new[] { Intent.CategoryDefault }, DataMimeType = "text/plain")]
    internal class MyIntentHandler : Activity  // Note that MyIntentHandler must inherit from Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Handle the received intent here
            if (Intent?.Action == Intent.ActionSend && Intent.Type == "text/plain")
            {
                string sharedText = Intent.GetStringExtra(Intent.ExtraText);
                if (!string.IsNullOrEmpty(sharedText))
                {
                    // Process the shared text as needed
                    Console.WriteLine("this is MyIntentHandler.cs: " + sharedText);
                    //Toast.MakeText(Android.App.Application.Context, "Received text: " + sharedText, ToastLength.Long).Show();
                }
                MinimizeApp(); //use this to minimize app
            }

            // Optionally, finish the activity if there's no UI to show
            Finish();
        }

        public void MinimizeApp()
        {
            Intent startMain = new Intent(Intent.ActionMain);
            startMain.AddCategory(Intent.CategoryHome);
            startMain.SetFlags(ActivityFlags.NewTask);
            StartActivity(startMain);
        }
    }
}
