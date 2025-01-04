using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using System;

namespace ReceiveApplicationVisualStudio;

//[Activity(
//    Name = "com.companyname.ReceiveApplicationVisualStudio.MainActivity",
//    Theme = "@style/Maui.SplashTheme",
//    LaunchMode = LaunchMode.SingleTask, //important to prevent crashing
//    MainLauncher = true,
//    ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density
//)]
//[IntentFilter(new[] { Intent.ActionSend }, Categories = new[] { Intent.CategoryDefault }, DataMimeType = "text/plain")]
[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    public static MainActivity ActivityCurrent { get; private set; }
    private MyBroadcastReceiver _receiver;

    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        ActivityCurrent = this;

        // Handle any intents that were used to start this activity
        HandleIntent(Intent);

        // Dynamic registration example
        _receiver = new MyBroadcastReceiver();
        var filter = new IntentFilter("com.companyname.RECEIVE_TEXT_ACTION");
        RegisterReceiver(_receiver, filter,ReceiverFlags.Exported);

    }

    protected override void OnNewIntent(Intent intent)
    {
        base.OnNewIntent(intent);
        HandleIntent(intent);
    }

    private void HandleIntent(Intent intent)
    {
        if (intent.Action == Intent.ActionSend)
        {
            var receivedText = intent.GetStringExtra(Intent.ExtraText);

            if (!string.IsNullOrEmpty(receivedText))
            {
                // Handle the received text, e.g., update MAUI page
                Console.WriteLine("this is MainActivity.cs: " + receivedText);
                Console.WriteLine(receivedText);
            }
        }
    }
}
