
using DevAzt.FormsX.Storage.Device;
using Foundation;
using System;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(DevAzt.FormsX.iOS.Storage.Device.KnownFolders))]
namespace DevAzt.FormsX.iOS.Storage.Device
{
	public class KnownFolders : IKnownFolders
    {

        public KnownFolders()
        {
        }

        public IStorageFolder Documents {
			get{
                string personalFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                return new StorageFolder
                {
                    FullPath = personalFolder,
                    Name = "Documents"
                };
            }
		}

		public IStorageFolder Downloads {
			get{
                throw new NotImplementedException("iOS not support get Downloads Folder");
            }
		}

		public IStorageFolder Music {
			get{
                string personalFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
                return new StorageFolder
                {
                    FullPath = personalFolder,
                    Name = "Music"
                };
            }
		}

		public IStorageFolder Pictures {
			get{
                string personalFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                return new StorageFolder
                {
                    FullPath = personalFolder,
                    Name = "Pictures"
                };
            }
		}

		public IStorageFolder CameraRoll
        {
			get{
                throw new NotImplementedException("iOS not support get CameraRollFolder");
            }
		}

		public IStorageFolder Movies {
			get{
                string personalFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
                return new StorageFolder
                {
                    FullPath = personalFolder,
                    Name = "Movies"
                };
            }
		}

		public IStorageFolder Home
        {
			get{
                string personalFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                return new StorageFolder
                {
                    FullPath = personalFolder,
                    Name = "Home"
                };
            }
		}

		public IStorageFolder SDCard
        {
			get{
                string personalFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                return new StorageFolder
                {
                    FullPath = personalFolder,
                    Name = "SDCard"
                };
            }
		}

		public IStorageFolder Root{
			get{
                return SDCard;
            }
		}

		private IStorageFolder GetFolder(Folder folder = Folder.Downloads){
            throw new NotImplementedException("iOS not support GetFolder");
		}
	}

	public enum Folder{
		Downloads, Music, Pictures, Documents, Movie, CameraRoll
	}
}

