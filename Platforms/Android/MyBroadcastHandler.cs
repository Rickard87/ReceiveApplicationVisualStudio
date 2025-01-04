using Android.App;
using Android.Content;
using System;

namespace ReceiveApplicationVisualStudio
{
    [BroadcastReceiver(Enabled = true, Exported = true)]
    [IntentFilter(new[] { "com.companyname.RECEIVE_TEXT_ACTION" })] // Action string used for the broadcast
    public class MyBroadcastReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            Console.WriteLine("Running MyBroadcastHandler.cs");
            if (intent.Action == "com.companyname.RECEIVE_TEXT_ACTION")
            {
                string receivedText = intent.GetStringExtra("sharedText"); // Use the same key as in the sender
                if (!string.IsNullOrEmpty(receivedText))
                {
                    // Write to console
                    Console.WriteLine("Received broadcast text: " + receivedText);
                }
                else
                {
                    Console.WriteLine("No text received.");
                }
            }
            else
            {
                Console.WriteLine("Unexpected action.");
            }
        }
    }
}
