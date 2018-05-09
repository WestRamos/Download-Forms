using DevAzt.FormsX.UWP.Net.HttpClient;

namespace Download.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            // Iniciamos el plugin de downloads
            DownloadManager.Init(this);

            LoadApplication(new Download.App());
        }
    }
}
