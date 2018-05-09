
using DevAzt.FormsX.Storage.Device;
using System;
using System.IO;
using Xamarin.Forms;
using UWPFolders = Windows.Storage.KnownFolders;

[assembly: Dependency(typeof(DevAzt.FormsX.UWP.Storage.Device.KnownFolders))]
namespace DevAzt.FormsX.UWP.Storage.Device
{
	public class KnownFolders : IKnownFolders
    {

        public KnownFolders()
        {
        }

        public IStorageFolder Documents {
			get{
				return GetFolder (Folder.Documents);
			}
		}

		public IStorageFolder Downloads {
			get{
				return GetFolder (Folder.Downloads);
			}
		}

		public IStorageFolder Music {
			get{
				return GetFolder (Folder.Music);
			}
		}

		public IStorageFolder Pictures {
			get{
				return GetFolder (Folder.Pictures);
			}
		}

		public IStorageFolder CameraRoll
        {
			get{
				return GetFolder (Folder.CameraRoll);
			}
		}

		public IStorageFolder Movies {
			get{
				return GetFolder (Folder.Movie);
			}
		}

		public IStorageFolder Home
        {
			get{
                return GetFolder(Folder.Documents);
            }
		}

		public IStorageFolder SDCard
        {
			get{
                return GetFolder(Folder.Documents);
			}
		}

		public IStorageFolder Root{
			get{
                return GetFolder(Folder.Documents);
			}
		}

		private IStorageFolder GetFolder(Folder folder = Folder.Downloads){
            Windows.Storage.StorageFolder myfolder = null;
            switch (folder)
            {
                case Folder.Downloads:
                    myfolder = UWPFolders.SavedPictures;
                    break;

                case Folder.Movie:
                    myfolder = UWPFolders.VideosLibrary;
                    break;

                case Folder.Music:
                    myfolder = UWPFolders.MusicLibrary;
                    break;

                case Folder.Pictures:
                    myfolder = UWPFolders.PicturesLibrary;
                    break;

                case Folder.CameraRoll:
                    myfolder = UWPFolders.CameraRoll;
                    break;

                default:
                    myfolder = UWPFolders.DocumentsLibrary;
                    break;
            }
            return new StorageFolder
            {
                FullPath = myfolder.Path,
                Name = myfolder.Name
            };
		}
	}

	public enum Folder{
		Downloads, Music, Pictures, Documents, Movie, CameraRoll
	}
}

