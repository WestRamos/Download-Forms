using DevAzt.FormsX.UI.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[assembly: Xamarin.Forms.Dependency(typeof(DevAzt.FormsX.iOS.UI.Notifications.ProgressNotification))]
namespace DevAzt.FormsX.iOS.UI.Notifications
{
    public class ProgressNotification : IProgressNotification
    {
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

        public static void Init()
        {
        }

        public ProgressNotification() { }

        public void Show(string title, string messageprogress, string messagecomplete)
        {
            NetworkIndicator.EnterActivity();
        }

        public void SendNotification()
        {
        }

        public void Complete()
        {
            NetworkIndicator.LeaveActivity();
        }
    }
}