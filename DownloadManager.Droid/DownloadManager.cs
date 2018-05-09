using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DevAzt.FormsX.Droid.UI.Notifications;

namespace DevAzt.FormsX.Droid.Net.HttpClient
{
    public class DownloadManager
    {

        public static void Init(Context context)
        {
            ProgressNotification.Init(context);
        }

    }
}