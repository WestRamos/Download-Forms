using DevAzt.FormsX.Net.HttpClient;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Linq;

namespace Download.ViewModel
{
    public class Main : ViewModel
    {
        public Main(Page page) : base(page)
        {
            Download = new Command(DownloadFile);

            Url = "http://www.sample-videos.com/download-sample-pdf.php";
        }

        public Command Download { get; set; }
        public string Url { get; set; }

        private async void DownloadFile(object obj)
        {
            if (!string.IsNullOrEmpty(Url))
            {

                var url = Url.Split('.');
                var extension = url.LastOrDefault();
                if (string.IsNullOrEmpty(extension)) return;
                var file = await DownloadManager.Current.Download("Descargando", "File", "Descarga completa", Url, "download", extension);
                if (file != null)
                {
                    await Toast(file.Name);
                }
            }
            else
            {
                await Toast("Solo se permiten en este ejemplo url de archivos jpg, pero el plugin permite descargar cualquier tipo de archivo.");
            }
        }
    }
}
