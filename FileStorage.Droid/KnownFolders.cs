
using DevAzt.FormsX.Storage.Device;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(DevAzt.FormsX.Droid.Storage.Device.KnownFolders))]
namespace DevAzt.FormsX.Droid.Storage.Device
{
	public class KnownFolders : IKnownFolders
    {

        public KnownFolders()
        {
        }

        public IStorageFolder Documents {
			get{
				return GetFolder(Folder.Documents);
			}
		}

		public IStorageFolder Downloads {
			get{
				return GetFolder(Folder.Downloads);
			}
		}

		public IStorageFolder Music {
			get{
				return GetFolder(Folder.Music);
			}
		}

		public IStorageFolder Pictures {
			get{
				return GetFolder(Folder.Pictures);
			}
		}

		public IStorageFolder CameraRoll
        {
			get{
				return GetFolder(Folder.CameraRoll);
			}
		}

		public IStorageFolder Movies {
			get{
				return GetFolder(Folder.Movie);
			}
		}

		public IStorageFolder Home
        {
			get{
				string path = Path.Combine (Android.OS.Environment.ExternalStorageDirectory.AbsolutePath);
                IStorageFolder storagefolder = new StorageFolder();
				string[] element = path.Split ('/');
				string foldername = element [element.Length - 1];
				if (Directory.Exists (path)) {
					storagefolder.FullPath = path;
					storagefolder.Name = foldername;
				} else {
					throw new DirectoryNotFoundException ("No existe el directorio " + foldername);
				}
				return storagefolder;
			}
		}

		public IStorageFolder SDCard
        {
			get{
				string path = Path.Combine ("/sdcard/");
				StorageFolder storagefolder = new StorageFolder();
				string[] element = path.Split ('/');
				string foldername = element [element.Length - 1];
				if (Directory.Exists (path)) {
					storagefolder.FullPath = path;
					storagefolder.Name = foldername;
				} else {
					throw new DirectoryNotFoundException ("No existe el directorio " + foldername);
				}
				return storagefolder;
			}
		}

		public IStorageFolder Root{
			get{
				string path = Path.Combine (Android.OS.Environment.RootDirectory.AbsolutePath);
                IStorageFolder storagefolder = new StorageFolder();
				string[] element = path.Split ('/');
				string foldername = element [element.Length - 1];
				if (Directory.Exists (path)) {
					storagefolder.FullPath = path;
					storagefolder.Name = foldername;
				} else {
					throw new DirectoryNotFoundException ("No existe el directorio " + foldername);
				}
				return storagefolder;
			}
		}

		private IStorageFolder GetFolder(Folder folder = Folder.Downloads){

            var home = Home;
            string foldername = "";
            switch (folder) {
			case Folder.Downloads:
                    foldername = "Download";
                    break;

			case Folder.Movie:
                    foldername = "Movies";
                    break;

                case Folder.Music:
                    foldername = "Music";
                    break;

                case Folder.Pictures:
                    foldername = "Pictures";
                    break;

                case Folder.CameraRoll:
                    foldername = "DCIM";
                    break;

			    default:
				    return Home;
			}

            return new StorageFolder
            {
                FullPath = Path.Combine(home.FullPath, foldername),
                Name = foldername
            };
		}
	}

	public enum Folder{
		Downloads, Music, Pictures, Documents, Movie, CameraRoll
	}
}

