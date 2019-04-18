using System;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Media;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V7.App;
using Android.Widget;

using Java.Lang;


using TaskStackBuilder = Android.Support.V4.App.TaskStackBuilder;
using NotificationCompact = Android.Support.V4.App.NotificationCompat;

namespace Notifications
{
    [Activity(Label = "Notifications", MainLauncher = true, Icon = "@drawable/Icon")]
    public class MainActivity : AppCompatActivity
    {
        // Unique ID for our notification: 
        static readonly int NOTIFICATION_ID1 = 1001;
        static readonly int NOTIFICATION_ID2 = 1002;
        static readonly int NOTIFICATION_ID3 = 1003;
        static readonly int NOTIFICATION_ID4 = 1004;
        static readonly int NOTIFICATION_ID5 = 1005;
        static readonly string CHANNEL_ID = "location_notification";
        static readonly string CHANNEL_ID2 = "location_notification2";
        internal static readonly string COUNT_KEY = "count";

        // Number of times the button is tapped (starts with first tap):
        int count1 = 1;
        int count2 = 1;
        int count3 = 1;
        int count4 = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            CreateNotificationChannel();
            CreateNotificationChannel2();
            // Display the "Hello World, Click Me!" button and register its event handler:
            var button1 = FindViewById<Button>(Resource.Id.MyButton1);
            var button2 = FindViewById<Button>(Resource.Id.MyButton2);
            var button3 = FindViewById<Button>(Resource.Id.MyButton3);
            var button4 = FindViewById<Button>(Resource.Id.MyButton4);
            var button5 = FindViewById<Button>(Resource.Id.MyButton5);
            var button6 = FindViewById<Button>(Resource.Id.MyButton6);
            var button7 = FindViewById<Button>(Resource.Id.MyButton7);
            button1.Click += ButtonOnClick1;
            button2.Click += ButtonOnClick2;
            button3.Click += ButtonOnClick3;
            button4.Click += ButtonOnClick4;
            button5.Click += ButtonOnClick5;
            button6.Click += ButtonOnClick6;
            button7.Click += ButtonOnClick7;
        }

        // Handler for button click events.
        void ButtonOnClick1(object sender, EventArgs eventArgs)
        {
            // Pass the current button press count value to the next activity:
            var valuesForActivity = new Bundle();
            valuesForActivity.PutInt(COUNT_KEY, count1);

            // When the user clicks the notification, SecondActivity will start up.
            var resultIntent = new Intent(this, typeof(PlainNotificationActivity));

            // Pass some values to SecondActivity:
            resultIntent.PutExtras(valuesForActivity);

            // Construct a back stack for cross-task navigation:
            var stackBuilder = TaskStackBuilder.Create(this);
            stackBuilder.AddParentStack(Class.FromType(typeof(PlainNotificationActivity)));
            stackBuilder.AddNextIntent(resultIntent);

            // Create the PendingIntent with the back stack:            
            var resultPendingIntent = stackBuilder.GetPendingIntent(0, (int) PendingIntentFlags.UpdateCurrent);

            //Intent launchIntent = PackageManager.GetLaunchIntentForPackage("com.xamarin.sample.splashscreen");
            //PendingIntent contentIntent = PendingIntent.GetActivity(this, 0, launchIntent, PendingIntentFlags.OneShot);
            
            // Build the notification:
            var builder = new NotificationCompat.Builder(this, CHANNEL_ID)
                          .SetAutoCancel(true) // Dismiss the notification from the notification area when the user clicks on it
                          .SetContentIntent(resultPendingIntent) // Start up this activity when the user clicks the intent.
                          .SetContentTitle("Button1 Clicked") // Set the title
                          .SetSmallIcon(Resource.Drawable.pic6) // This is the icon to display
                          .SetLargeIcon(BitmapFactory.DecodeResource(Resources, Resource.Drawable.pic1))
                          .SetContentText($"The button has been clicked {count1} times."); // the message to display.
            builder.SetVisibility(NotificationCompact.VisibilityPrivate);
            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Lollipop)
            {
                //builder.SetCategory(NotificationCompact.CategoryEmail);
            }
            // Finally, publish the notification:
            var notificationManager = NotificationManagerCompat.From(this);
            notificationManager.Notify(NOTIFICATION_ID1, builder.Build());


            //*********************无用的********************
            //.SetLights(unchecked((int)0xFFFFFFF0), 1, 1)  // new   .SetLights(unchecked((int)0xFFFFFFF0), 500, 1000)
            //.Build(); // the message to display.               // new 
            //builder.Defaults = NotificationDefaults.All;

            //builder.LedARGB = unchecked((int)0xFFFFFFF0);
            //builder.Flags = NotificationFlags.ShowLights | builder.Flags;
            //builder.LedOffMS = 100;
            //builder.LedOnMS = 100;


            //builder.ledARGB = 0xFFff0000;
            //builder.flags = Notification.FLAG_SHOW_LIGHTS;
            //builder.ledOnMS = 100;
            //builder.ledOffMS = 100;

            //*********************无用的********************

            // Increment the button press count1:
            count1++;
        }

        //BigTextStyle Notification
        void ButtonOnClick2(object sender, EventArgs eventArgs)
        {
            // Pass the current button press count value to the next activity:
            var valuesForActivity = new Bundle();
            valuesForActivity.PutInt(COUNT_KEY, count2);

            // When the user clicks the notification, SecondActivity will start up.
            var resultIntent = new Intent(this, typeof(BigTextNotificationActivity));

            // Pass some values to SecondActivity:
            resultIntent.PutExtras(valuesForActivity);


            // Construct a back stack for cross-task navigation:
            var stackBuilder = TaskStackBuilder.Create(this);
            stackBuilder.AddParentStack(Class.FromType(typeof(BigTextNotificationActivity)));
            stackBuilder.AddNextIntent(resultIntent);

            // Create the PendingIntent with the back stack:            
            var resultPendingIntent = stackBuilder.GetPendingIntent(0, (int)PendingIntentFlags.UpdateCurrent);

            // Build the notification:
            var builder = new NotificationCompat.Builder(this, CHANNEL_ID2)
                          .SetAutoCancel(true) // Dismiss the notification from the notification area when the user clicks on it
                          .SetContentIntent(resultPendingIntent) // Start up this activity when the user clicks the intent.
                          .SetContentTitle("Button2 Clicked") // Set the title
                          .SetSmallIcon(Resource.Drawable.logo) // This is the icon to display
                          .SetLargeIcon(BitmapFactory.DecodeResource(Resources, Resource.Drawable.pic1))
                          .SetContentText($"The button has been clicked {count2} times."); // the message to display.
                                                                                           // Instantiate the Big Text style:
            builder.SetVisibility(NotificationCompact.VisibilityPublic);
            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Lollipop)
            {
                builder.SetCategory(NotificationCompact.CategoryEmail);
            }
            NotificationCompact.BigTextStyle textStyle = new NotificationCompact.BigTextStyle();

            // Fill it with text:
            string longTextMessage = @"fkdljfdldkfj;ldaksjfkladj;flja;lkjdfljadslfjaddfdsfafjdfad
            fdl;akjf;lkdf;lkaj;flkjda;lkfjadljflk;adsjfladjflk;dfjlkdjflakdfjdaffjdlfjdjjj
            adjflkjadlkfjad;lkfjad;sljf;ladkjajlkfjad;lksfjl;akdjf;lkdsajf;lkdjfkadj;flkad
            jf;lkadjfkldas;lkfja;dljf;lkdasjf;lkadjs;lfjas;ldkfj;lkadsjfl;kadljfl;kasdjf;l
            jdlskfjklda;fjadslkfj;sdalkfj;ladjf;lajdl;fkajld;kfjlajfl;adjfl;kajdl;fjadl;kfj;";
            //...
            textStyle.BigText(longTextMessage);

            // Set the summary text:
            textStyle.SetSummaryText("The summary text goes here.");

            // Plug this style into the builder:
            builder.SetStyle(textStyle);
 

            // Finally, publish the notification:
            var notificationManager = NotificationManagerCompat.From(this);
            notificationManager.Notify(NOTIFICATION_ID2, builder.Build());

            // Increment the button press count2:
            count2++;
        }

        //Image Style Notification
        void ButtonOnClick3(object sender, EventArgs eventArgs)
        {
            // Pass the current button press count3 value to the next activity:
            var valuesForActivity = new Bundle();
            valuesForActivity.PutInt(COUNT_KEY, count3);

            // When the user clicks the notification, SecondActivity will start up.
            var resultIntent = new Intent(this, typeof(PicNotificationActivity));

            // Pass some values to SecondActivity:
            resultIntent.PutExtras(valuesForActivity);


            // Construct a back stack for cross-task navigation:
            var stackBuilder = TaskStackBuilder.Create(this);
            stackBuilder.AddParentStack(Class.FromType(typeof(PicNotificationActivity)));
            stackBuilder.AddNextIntent(resultIntent);

            // Create the PendingIntent with the back stack:            
            var resultPendingIntent = stackBuilder.GetPendingIntent(0, (int)PendingIntentFlags.UpdateCurrent);

            // Build the notification:
            var builder = new NotificationCompat.Builder(this, CHANNEL_ID)
                          .SetAutoCancel(true) // Dismiss the notification from the notification area when the user clicks on it
                          .SetContentIntent(resultPendingIntent) // Start up this activity when the user clicks the intent.
                          .SetContentTitle("Button3 Clicked") // Set the title
                          .SetSmallIcon(Resource.Drawable.logo) // This is the icon to display
                          .SetLargeIcon(BitmapFactory.DecodeResource(Resources, Resource.Drawable.pic1))
                          .SetContentText($"The button has been clicked {count3} times."); // the message to display.
                                                                                           // Instantiate the Big Text style:
                                                                                           //Notification.BigTextStyle textStyle = new Notification.BigTextStyle();
            builder.SetVisibility(NotificationCompact.VisibilitySecret);
            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Lollipop)
            {
                builder.SetCategory(NotificationCompact.CategoryCall);
            }
            // Instantiate the Image (Big Picture) style:
            NotificationCompact.BigPictureStyle picStyle = new NotificationCompact.BigPictureStyle();

            // Convert the image to a bitmap before passing it into the style:
            picStyle.BigPicture(BitmapFactory.DecodeResource(Resources, Resource.Drawable.pic1));

            // Set the summary text that will appear with the image:
            picStyle.SetSummaryText("The summary text goes here.");

            // Plug this style into the builder:
            builder.SetStyle(picStyle);


            // Finally, publish the notification:
            var notificationManager = NotificationManagerCompat.From(this);
            notificationManager.Notify(NOTIFICATION_ID3, builder.Build());

            // Increment the button press count3:
            count3++;
        }

        //Inbox Style Notification
        void ButtonOnClick4(object sender, EventArgs eventArgs)
        {
            // Pass the current button press count4 value to the next activity:
            var valuesForActivity = new Bundle();
            valuesForActivity.PutInt(COUNT_KEY, count4);

            // When the user clicks the notification, SecondActivity will start up.
            var resultIntent = new Intent(this, typeof(InboxNotificationActivity));

            // Pass some values to SecondActivity:
            resultIntent.PutExtras(valuesForActivity);


            // Construct a back stack for cross-task navigation:
            var stackBuilder = TaskStackBuilder.Create(this);
            stackBuilder.AddParentStack(Class.FromType(typeof(InboxNotificationActivity)));
            stackBuilder.AddNextIntent(resultIntent);

            // Create the PendingIntent with the back stack:            
            var resultPendingIntent = stackBuilder.GetPendingIntent(0, (int)PendingIntentFlags.UpdateCurrent);

            // Build the notification:
            var builder = new NotificationCompat.Builder(this, CHANNEL_ID)
                          .SetAutoCancel(true) // Dismiss the notification from the notification area when the user clicks on it
                          .SetContentIntent(resultPendingIntent) // Start up this activity when the user clicks the intent.
                          .SetContentTitle("Button4 Clicked") // Set the title
                          .SetSmallIcon(Resource.Drawable.logo) // This is the icon to display
                          .SetLargeIcon(BitmapFactory.DecodeResource(Resources, Resource.Drawable.pic1))
                          .SetPriority(2)
                          .SetContentText($"The button has been clicked {count4} times."); // the message to display.
                                                                                           // Instantiate the Big Text style:
                                                                                           //Notification.BigTextStyle textStyle = new Notification.BigTextStyle();

            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Lollipop)
            {
                builder.SetCategory(NotificationCompact.CategoryCall);
            }
            // Convert the image to a bitmap before passing it into the style:
            // Instantiate the Inbox style:
            NotificationCompact.InboxStyle inboxStyle = new NotificationCompact.InboxStyle();

            // Set the title and text of the notification:
            builder.SetContentTitle("5 new messages");
            builder.SetContentText("abbywang@xamarin.com");

            // Generate a message summary for the body of the notification:
            inboxStyle.AddLine("Cheeta: Bananas on sale");
            inboxStyle.AddLine("George: Curious about your blog post");
            inboxStyle.AddLine("Nikko: Need a ride to Evolve?");
            inboxStyle.SetSummaryText("+2 more");

            // Plug this style into the builder:
            builder.SetStyle(inboxStyle);
            //builder.SetPriority(NotificationPriority.High);


            // Finally, publish the notification:
            var notificationManager = NotificationManagerCompat.From(this);
            notificationManager.Notify(NOTIFICATION_ID4, builder.Build());

            // Increment the button press count4:
            count4++;
        }

        //Download file notification
        void ButtonOnClick7(object sender, EventArgs eventArgs)
        {
            var percentage = 0;
            // Build the notification:
            var builder = new NotificationCompat.Builder(this, CHANNEL_ID)
                          .SetAutoCancel(true) // Dismiss the notification from the notification area when the user clicks on it
                          .SetContentTitle($"Donloading file: {percentage}%") 
                          //.SetSmallIcon(Resource.Drawable.logo) // This is the icon to display
                          .SetLargeIcon(BitmapFactory.DecodeResource(Resources, Resource.Drawable.pic1))
                          .SetContentText($"wow! The file has been downloaded: {percentage}%");
            
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                builder.SetSmallIcon(Resource.Drawable.Icon);
                builder.SetColor(Color.Gray);
            }
            else
            {
                builder.SetSmallIcon(Resource.Drawable.Icon);
            }

            builder.SetVisibility(NotificationCompact.VisibilityPublic);
            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Lollipop)
            {
                builder.SetCategory(NotificationCompact.CategoryProgress);
            }
            //NotificationCompact.BigTextStyle textStyle = new NotificationCompact.BigTextStyle();

             // Finally, publish the notification:
            var notificationManager = NotificationManagerCompat.From(this);
            notificationManager.Notify(NOTIFICATION_ID5, builder.Build());

            // Increment the button press percentage:
            while (percentage < 100) {
                percentage += 10;
                builder.SetContentTitle($"Donloading file: {percentage}%");
                notificationManager.Notify(NOTIFICATION_ID5, builder.Build());
                Thread.Sleep(1000);
            }
            
        }

        void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                // Notification channels are new in API 26 (and not a part of the
                // support library). There is no need to create a notification 
                // channel on older versions of Android.
                return;
            }
            //Android.Net.Uri alarmUri = Android.Net.Uri.Parse($"{ ContentResolver.SchemeAndroidResource}://{PackageName}/{Resource.Raw.pizzicato}");
            //Android.Net.Uri alarmUri = Android.Net.Uri.Parse($"{ ContentResolver.SchemeAndroidResource}://{PackageName}/{Resource.Raw.pizzicato}");
            var ringPath = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryRingtones);
            Android.Net.Uri alarmUri = Android.Net.Uri.Parse(ringPath+ "/Growl.ogg");
            //var path1 = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyMusic);


            var alarmAttributes = new AudioAttributes.Builder()
                           .SetContentType(AudioContentType.Sonification)
                           .SetUsage(AudioUsageKind.Notification).Build();
            var name = Resources.GetString(Resource.String.channel_name);
            var description = GetString(Resource.String.channel_description);
            var channel = new NotificationChannel(CHANNEL_ID, name, NotificationImportance.Max)
                          {
                              Description = description
                          };
            channel.SetSound(alarmUri, alarmAttributes);
            channel.SetVibrationPattern(new long[] { 1000, 1000 });
            channel.EnableLights(true);
            channel.LightColor = Color.Red;
            //channel.SetVibrationPattern(null);
            //channel.EnableLights(true);    //LED
            //channel.LightColor = Color.Green;

            var notificationManager = (NotificationManager) GetSystemService(NotificationService);
            notificationManager.CreateNotificationChannel(channel);
        }

        void CreateNotificationChannel2()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                // Notification channels are new in API 26 (and not a part of the
                // support library). There is no need to create a notification 
                // channel on older versions of Android.
                return;
            }
            Android.Net.Uri alarmUri = Android.Net.Uri.Parse($"{ ContentResolver.SchemeAndroidResource}://{PackageName}/{Resource.Raw.MyRingTone}");

            var alarmAttributes = new AudioAttributes.Builder()
                           .SetContentType(AudioContentType.Sonification)
                           .SetUsage(AudioUsageKind.Notification).Build();
            var name = Resources.GetString(Resource.String.channel_name2);
            var description = GetString(Resource.String.channel_description);
            var channel = new NotificationChannel(CHANNEL_ID2, name, NotificationImportance.Low)
            {
                Description = description
            };
            channel.SetSound(alarmUri, alarmAttributes);
            channel.SetVibrationPattern(new long[] { 1000, 1000 });


            var notificationManager2 = (NotificationManager)GetSystemService(NotificationService);
            notificationManager2.CreateNotificationChannel(channel);
        }


        void ButtonOnClick5(object sender, EventArgs eventArgs)
        {
            var notificationManager = (NotificationManager)GetSystemService(NotificationService);
            notificationManager.DeleteNotificationChannel(CHANNEL_ID);
            CreateNotificationChannel();
        }


        void ButtonOnClick6(object sender, EventArgs eventArgs)
        {
            var notificationManager2 = (NotificationManager)GetSystemService(NotificationService);
            notificationManager2.DeleteNotificationChannel(CHANNEL_ID2);
            CreateNotificationChannel2();
        }
    }
}
