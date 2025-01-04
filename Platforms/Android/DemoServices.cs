using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;

namespace ReceiveApplicationVisualStudio
{
    [Service(ForegroundServiceType = Android.Content.PM.ForegroundService.TypeDataSync)]
    public class DemoServices : Service, IServiceFG
    {
        public override IBinder OnBind(Intent intent)
        {
            throw new NotImplementedException();
        }
        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            if (intent.Action == "START_SERVICE")
            {
                System.Diagnostics.Debug.WriteLine("!! SERVICE HAS STARTED !!");
                Task.Run(() => RegisterNotificationAsync());
                Task.Run(() => ListenForIncomingIntents());
            }
            else if (intent.Action == "STOP_SERVICE")
            {
                System.Diagnostics.Debug.WriteLine("!! SERVICE HAS STOPPED !!");
                StopForeground(true);
                StopSelfResult(startId);
            }
            return StartCommandResult.NotSticky; //Try to restart the service if it gets killed with different StartCommandResult values
        }

        public async void Start()
        {
            CheckAndRequestLocationPermission();
            Intent startService = new Intent(MainActivity.ActivityCurrent, typeof(DemoServices));
            startService.SetAction("START_SERVICE");
            MainActivity.ActivityCurrent.StartService(startService);
        }

        public async void Stop()
        {
            Intent stopService = new Intent(MainActivity.ActivityCurrent, this.Class);
            stopService.SetAction("STOP_SERVICE");
            MainActivity.ActivityCurrent.StartService(stopService);
        }
        private async Task RegisterNotificationAsync()
        {
            // Run indefinitely
            while (true)
            {
                // Log the message
                Console.WriteLine("I am running");

                // Wait for 3 seconds
                await Task.Delay(3000); // 3000 milliseconds = 3 seconds
            }
        }
        private async Task ListenForIncomingIntents()
        {
#if ANDROID
            while (true)
            {
                try
                {

                    await Task.Delay(2000); // Polling interval

                    // Get the current activity context
                    var activity = Platform.CurrentActivity;
                    var intent = activity?.Intent;

                    if (intent != null && intent.Action == Intent.ActionSend)
                    {
                        var receivedText = intent.GetStringExtra(Intent.ExtraText);

                        if (!string.IsNullOrEmpty(receivedText))
                        {

                            Console.WriteLine("this is DemoServices.cs: " + receivedText);

                            // Clear the intent after processing it
                            activity.Intent = null;
                        }
                    }
                    else
                    {
                        Console.WriteLine("I am empty");
                    }
                }
                catch (Exception ex) { Console.WriteLine(ex.ToString()); }
            }
#endif
        }
        public async Task<PermissionStatus> CheckAndRequestLocationPermission()
        {
            PermissionStatus status = await Permissions.CheckStatusAsync<Permissions.PostNotifications>();

            if (status == PermissionStatus.Granted)
            {
                System.Diagnostics.Debug.WriteLine("!! Permission status granted !!");
                return status;
            }

            if (status == PermissionStatus.Granted)
            {
                System.Diagnostics.Debug.WriteLine("!! Permission status granted !!");
                return status;
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("!! Permission status not granted !!");
            }

            if (status == PermissionStatus.Denied && DeviceInfo.Platform == DevicePlatform.iOS)
            {
                //Here we should put code to prompt the user to turn on in settings
                //On iOS once a permmission has been denied, it cannot be requested again from the application
                return status;
            }
            if (Permissions.ShouldShowRationale<Permissions.PostNotifications>())
            {
                //Here we should put code to prompt the user with additional information as to why the permission is needed
            }

            status = await Permissions.RequestAsync<Permissions.PostNotifications>();

            System.Diagnostics.Debug.WriteLine($"!! Permission status: {status} !!");

            return status;
        }
    }
}