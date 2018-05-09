using DevAzt.FormsX.Storage.Device;
using DevAzt.FormsX.UI.Notifications;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DevAzt.FormsX.Net.HttpClient
{
    public class DownloadManager
    {

        public static DownloadManager Current
        {
            get
            {
                return new DownloadManager();
            }
        }


        public async Task<IStorageFile> Download(string title, string messagedownloading, string messagecomplete, string url, string filename, string fileextension)
        {
            var notification = ProgressNotification.Current;
            notification.Show(title, messagedownloading, messagecomplete);
            var file = await CreateDownloadTask(url, filename, fileextension, new Progress<DownloadBytesProgress>((progress) =>
            {
                notification.Progress = progress.PercentComplete * 100;
            }));

            notification.Complete();

            return file;
        }

        private async Task<IStorageFile> CreateDownloadTask(string urltodownload, string filename, string fileextension, IProgress<DownloadBytesProgress> progessreporter)
        {
            IStorageFile file = null;
            try
            {
                var knwonfolders = KnownFolders.Instance;
                file = await knwonfolders.Downloads.CreateFile($"{filename}.{fileextension}", CreationCollisionOption.GenerateUniqueName);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }

            int receivedBytes = 0;
            int totalBytes = 0;
            WebClient client = new WebClient();
            using (var stream = await client.OpenReadTaskAsync(urltodownload))
            {

                if (file != null)
                {
                    // abrimos el archivo para escribirlo
                    var streamforwrite = await file.Open(FileAccessMode.Write);
                    // copiamos los datos del archivo
                    try
                    {
                        totalBytes = Int32.Parse(client.ResponseHeaders[HttpResponseHeader.ContentLength]);
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                    }

                    byte[] buffer = new byte[32768];
                    int read;
                    while ((read = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                    {
                        await streamforwrite.WriteAsync(buffer, 0, read);
                        receivedBytes += read;
                        if (progessreporter != null)
                        {
                            DownloadBytesProgress args = new DownloadBytesProgress(urltodownload, receivedBytes, totalBytes);
                            progessreporter.Report(args);
                        }
                    }

                    try { await streamforwrite.FlushAsync(); } catch (Exception ex) { System.Diagnostics.Debug.WriteLine(ex.StackTrace); }
                    try { streamforwrite.Dispose(); } catch(Exception ex) { System.Diagnostics.Debug.WriteLine(ex.StackTrace); }
                }

                /*
                byte[] buffer = new byte[4096];
                try
                {
                    totalBytes = Int32.Parse(client.ResponseHeaders[HttpResponseHeader.ContentLength]);
                }
                catch(Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                }
                while (true)
                {
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                    if (bytesRead == 0)
                    {
                        await Task.Yield();
                        break;
                    }
                    receivedBytes += bytesRead;
                    if (progessreporter != null)
                    {
                        DownloadBytesProgress args = new DownloadBytesProgress(urltodownload, receivedBytes, totalBytes);
                        progessreporter.Report(args);
                    }
                }
                */

            }
            return file;
        }

    }
}
