using Microsoft.Maui.Controls;
#if ANDROID
using Android.Content;
#endif
using System;
using System.Threading.Tasks;
#nullable disable

namespace ReceiveApplicationVisualStudio
{
    public partial class MainPage : ContentPage
    {
        IServiceFG Services;
        public MainPage(IServiceFG services)
        {
            InitializeComponent();

            // Start listening for incoming strings
            //Task.Run(() => ListenForIncomingIntents());
            Services = services;
            Services.Start();
        }

        //        private async Task ListenForIncomingIntents()
        //        {
        //#if ANDROID
        //            while (true)
        //            {
        //                await Task.Delay(500); // Polling interval

        //                // Get the current activity context
        //                var activity = Platform.CurrentActivity;
        //                var intent = activity?.Intent;

        //                if (intent != null && intent.Action == Intent.ActionSend)
        //                {
        //                    var receivedText = intent.GetStringExtra(Intent.ExtraText);

        //                    if (!string.IsNullOrEmpty(receivedText))
        //                    {
        //                        Console.WriteLine("Received text: " + receivedText.ToString());
        //                        Console.WriteLine("Received text: " + receivedText.ToString());
        //                        Console.WriteLine("Received text: " + receivedText.ToString());
        //                        Console.WriteLine("Received text: " + receivedText.ToString());

        //                        // Clear the intent after processing it
        //                        activity.Intent = null;
        //                    }
        //                }
        //            }
        //#endif
        //        }
    }
}
