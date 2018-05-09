using System;

namespace DevAzt.FormsX.UI.Notifications
{
    public class ProgressNotification : IProgressNotification
    {
        public double Progress { get => _service.Progress; set => _service.Progress = value; }
        public double Total { get => _service.Total; set => _service.Total = value; }

        private IProgressNotification _service;

        private ProgressNotification(IProgressNotification service)
        {
            _service = service;
        }

        public void Complete()
        {
            _service.Complete();
        }

        public void Show(string title, string messageprogress, string messagecomplete)
        {
            _service.Show(title, messageprogress, messagecomplete);
        }

        public static ProgressNotification Current
        {
            get
            {
                var service = Xamarin.Forms.DependencyService.Get<IProgressNotification>(Xamarin.Forms.DependencyFetchTarget.NewInstance);
                if (service == null) throw new NullReferenceException("IProgressNotificationImplementation is null");
                return new ProgressNotification(service);
            }
        }
    }
}
