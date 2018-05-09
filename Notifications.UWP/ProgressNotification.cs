using DevAzt.FormsX.UI.Notifications;
using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Notifications;
using Windows.UI.Xaml.Controls;

[assembly: Xamarin.Forms.Dependency(typeof(DevAzt.FormsX.UWP.UI.Notifications.ProgressNotification))]
namespace DevAzt.FormsX.UWP.UI.Notifications
{
    public class ProgressNotification : IProgressNotification
    {
        private static Page _page;
        private string _title;
        private string _messageprogress;
        private string _messagecomplete;
        
        private double _progress = 0.0;
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
        
        private double _total = 0.0;
        public double Total
        {
            get => _total;
            set => _total = value;
        }
        
        public static void Init(Page page)
        {
            _page = page;
        }

        public ProgressNotification() { }

        public void Show(string title, string messageprogress, string messagecomplete)
        {
            _title = title;
            _messageprogress = messageprogress;
            _messagecomplete = messagecomplete;
            Total = 1;

            // Define a tag (and optionally a group) to uniquely identify the notification, in order update the notification data later;
            string tag = title;
            string group = "downloads";

            // Construct the toast content with data bound fields
            var content = new ToastContent()
            {
                Visual = new ToastVisual()
                {
                    BindingGeneric = new ToastBindingGeneric()
                    {
                        Children =
                        {
                            new AdaptiveText()
                            {
                                Text = title
                            },

                            new AdaptiveProgressBar()
                            {
                                Title = title,
                                Value = new BindableProgressBarValue("ProgressValue"),
                                ValueStringOverride = new BindableString("ProgressValueString"),
                                Status = new BindableString("ProgressStatus")
                            }
                        }
                    }
                }
            };

            // Generate the toast notification
            var toast = new ToastNotification(content.GetXml())
            {
                // Assign the tag and group
                Tag = tag,
                Group = group,
                // Assign initial NotificationData values
                // Values must be of type string
                Data = new NotificationData()
            };

            toast.Data.Values["ProgressValue"] = "0.0";
            toast.Data.Values["ProgressValueString"] = $"{_messageprogress} - {Progress} / {Total}";
            toast.Data.Values["ProgressStatus"] = "";

            // Provide sequence number to prevent out-of-order updates, or assign 0 to indicate "always update"
            toast.Data.SequenceNumber = 0;

            // Show the toast notification to the user
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }

        public async void SendNotification()
        {
            // Construct a NotificationData object;
            string tag = _title;
            string group = "downloads";

            // Create NotificationData and make sure the sequence number is incremented
            // since last update, or assign 0 for updating regardless of order
            var data = new NotificationData
            {
                SequenceNumber = 0
            };

            // Assign new values
            // Note that you only need to assign values that changed. In this example
            // we don't assign progressStatus since we don't need to change it
            data.Values["ProgressValue"] = $"{Progress / 100}";
            var progressvar = Math.Round(Progress, 0);
            Total = 100;

            data.Values["ProgressValueString"] = $"{_messageprogress} - {progressvar}% / {Total}%";

            // Update the existing notification's data by using tag/group

            await _page.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                ToastNotificationManager.CreateToastNotifier().Update(data, tag, group);
            });
        }

        public async void Complete()
        {
            Progress = 1;
            await _page.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                ToastNotificationManager.History.Remove(_title, "downloads");
            });
        }
    }
}
