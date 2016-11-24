using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Android.Content;
using XamarinAlarmDemo.BroadCast;

namespace XamarinAlarmDemo
{
    [Activity(Label = "XamarinAlarmDemo", MainLauncher = true, Icon = "@drawable/icon",Theme ="@style/Theme.AppCompat.Light.NoActionBar")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView (Resource.Layout.Main);

            var rdiNotification = FindViewById<RadioButton>(Resource.Id.rdiNotification);
            var rdiToast = FindViewById<RadioButton>(Resource.Id.rdiToast);
            var btnOneTime = FindViewById<Button>(Resource.Id.btnOneTime);
            var btnRepeating = FindViewById<Button>(Resource.Id.btnRepeating);

            btnOneTime.Click += delegate
            {
                if (rdiNotification.Checked == true)
                    StartAlarm(true, false);
                else
                    StartAlarm(false, false);
            };

            btnRepeating.Click += delegate
             {
                 if (rdiNotification.Checked == true)
                     StartAlarm(true, true);
                 else
                     StartAlarm(false, true);
             };
        }

        private void StartAlarm(bool isNotification, bool isRepeating)
        {
            AlarmManager manager = (AlarmManager)GetSystemService(Context.AlarmService);
            Intent myIntent;
            PendingIntent pendingIntent;

            if(!isNotification)
            {
                myIntent = new Intent(this, typeof(AlarmToastReceiver));
                pendingIntent = PendingIntent.GetBroadcast(this, 0, myIntent, 0);
            }
            else
            {
                myIntent = new Intent(this, typeof(AlarmNotificationReceiver));
                pendingIntent = PendingIntent.GetBroadcast(this, 0, myIntent, 0);
            }

            if(!isRepeating)
            {
                manager.Set(AlarmType.RtcWakeup, SystemClock.ElapsedRealtime() + 3000, pendingIntent);
            }
            else
            {
                manager.SetRepeating(AlarmType.RtcWakeup, SystemClock.ElapsedRealtime() + 3000, 60 * 1000, pendingIntent);
            }

        }
    }
}

