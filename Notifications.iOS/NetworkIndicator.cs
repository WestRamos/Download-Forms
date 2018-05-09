using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Foundation;
using UIKit;

namespace DevAzt.FormsX.iOS.UI.Notifications
{
    public static class NetworkIndicator
    {
        static int _counter;

        public static void EnterActivity()
        {
            Interlocked.Increment(ref _counter);
            RefreshIndicator();
        }

        public static void LeaveActivity()
        {
            Interlocked.Decrement(ref _counter);
            RefreshIndicator();
        }

        public static void AttachToTask(Task task)
        {
            if (task.IsCanceled || task.IsCanceled || task.IsFaulted)
                return;

            EnterActivity();
            task.ContinueWith(t => {
                LeaveActivity();
            });
        }

        static void RefreshIndicator()
        {
            UIApplication.SharedApplication.NetworkActivityIndicatorVisible =
                (_counter > 0);
        }
    }

    public static class TaskExtensions
    {
        public static Task WithNetworkIndicator(this Task task)
        {
            NetworkIndicator.AttachToTask(task);
            return task;
        }

        public static Task<TResult> WithNetworkIndicator<TResult>(this Task<TResult> task)
        {
            NetworkIndicator.AttachToTask(task);
            return task;
        }
    }
}