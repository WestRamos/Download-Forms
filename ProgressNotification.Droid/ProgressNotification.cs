using Android.App;
using Android.Content;
using Android.Support.V4.App;
using DevAzt.FormsX.UI.Notifications;

[assembly: Xamarin.Forms.Dependency(typeof(DevAzt.FormsX.Droid.UI.Notifications.ProgressNotification))]
namespace DevAzt.FormsX.Droid.UI.Notifications
{
    public class ProgressNotification : IProgressNotification
    {
        private string _title;
        private string _messageprogress;
        private string _messagecomplete;
        NotificationManager notificationManager;
        private NotificationCompat.Builder notificationBuilder;

        private double _progress = 0;
        public double Progress
        {
            get
            {
                return _progress;
            }

            set
            {
                _progress = value;
                SendNotification();
            }
        }

        private double _total = 0;
        public double Total
        {
            get => _total;
            set => _total = value;
        }

        public ProgressNotification() { }

        private static Context Context { get; set; }
        
        public static void Init(Context context)
        {
            Context = context;
        }

        public void Show(string title, string messageprogress, string messagecomplete)
        {
            _title = title;
            _messageprogress = messageprogress;
            _messagecomplete = messagecomplete;
            Total = 100;
            notificationManager = (NotificationManager) Context.GetSystemService(Context.NotificationService);
            notificationBuilder = new NotificationCompat.Builder(Context)
                    .SetSmallIcon(Android.Resource.Drawable.StatSysDownload)
                    .SetContentTitle(title)
                    .SetContentText(messageprogress)
                    .SetAutoCancel(false);
            notificationManager.Notify(0, notificationBuilder.Build());
        }

        private void SendNotification()
        {
            notificationBuilder.SetProgress(100, (int)Progress, false);
            notificationBuilder.SetContentText($"{_messageprogress} - {Progress} / {Total}");
            notificationManager.Notify(0, notificationBuilder.Build());
        }

        public void Complete()
        {
            System.Diagnostics.Debug.WriteLine("Complete");
            notificationManager.Cancel(0);
            notificationBuilder.SetProgress(0, 0, false);
            notificationBuilder.SetContentText(_messagecomplete);
            notificationBuilder.SetSmallIcon(Android.Resource.Drawable.StatSysDownloadDone);
            notificationManager.Notify(0, notificationBuilder.Build());
        }
        
    }
}