using DevAzt.FormsX.Storage.Device;
using System;
using System.IO;
using System.Threading.Tasks;
using UWPFile = Windows.Storage.StorageFile;
using UWPFolder = Windows.Storage.StorageFolder;

namespace DevAzt.FormsX.UWP.Storage.Device
{
	public class StorageFile : IStorageFile
    {

		public string FullPath { 
			get;
			set;
		}

		public string Name {
			get;
			set;
		}

		public string DisplayName {
			get;
			set;
		}

		public string DisplayType {
			get;
			set;
		}

		public DateTime DateCreated {
			get;
			set;
		}

		public FileAttributes Attributes {
			get;
			set;
		}

		public StorageFile ()
		{
			
		}

		public async Task<IStorageFile> CopyTo(IStorageFolder folderdestination){
			return await CopyTo (folderdestination, Name);
		}

		public async Task<IStorageFile> CopyTo(IStorageFolder folderdestination, string filenamewithextension){
			return await CopyTo (folderdestination, filenamewithextension, NameCollisionOption.FailIfExists);
		}

		public async Task<IStorageFile> CopyTo(IStorageFolder folderdestination, string filenamewithextension, NameCollisionOption option){
			if (await Exists()) {
                var file = await UWPFile.GetFileFromPathAsync(FullPath);
                var folder = await UWPFolder.GetFolderFromPathAsync(folderdestination.FullPath);

                switch (option)
                {
                    case NameCollisionOption.GenerateUniqueName:
                        await file.CopyAsync(folder, filenamewithextension, Windows.Storage.NameCollisionOption.GenerateUniqueName);
                        break;
                    case NameCollisionOption.ReplaceExisting:
                        await file.CopyAsync(folder, filenamewithextension, Windows.Storage.NameCollisionOption.ReplaceExisting);
                        break;
                    case NameCollisionOption.FailIfExists:
                        await file.CopyAsync(folder, filenamewithextension, Windows.Storage.NameCollisionOption.FailIfExists);
                        break;
                }
			}
			return null;
		}

		public async Task Delete(){
			if (await Exists()) {
                var file = await UWPFile.GetFileFromPathAsync(FullPath);
                await file.DeleteAsync();
            }
		}

		public static async Task<StorageFile> GetFileFromPath(string path){
            var file = await UWPFile.GetFileFromPathAsync(path);
            return new StorageFile
            {
                Attributes = (FileAttributes) ((int) file.Attributes),
                DateCreated = file.DateCreated.DateTime,
                DisplayName = file.DisplayName,
                DisplayType = file.DisplayType,
                FullPath = file.Path,
                Name = file.Name
            };
		}

		public async Task<Stream> Open(FileAccessMode mode){
			if (await Exists ()) {
                var file = await UWPFile.GetFileFromPathAsync(FullPath);
                switch (mode)
                {
                    case FileAccessMode.Read:
                        return await file.OpenStreamForReadAsync();
                        
                    case FileAccessMode.Write:
                        return await file.OpenStreamForWriteAsync();
                }
                return null;
            }else {
				throw new FileNotFoundException ("El archivo que tratas de copiar no existe, path: " + FullPath + ", name: " + Name);
			}
		}

		private async Task<bool> Exists(){
            var file = await UWPFile.GetFileFromPathAsync(FullPath);
            return file != null;
		}

		private string UniqueString(){
			Guid g = Guid.NewGuid ();
			string GuidString = Convert.ToBase64String (g.ToByteArray ());
			GuidString = GuidString.Replace ("=", "");
			GuidString = GuidString.Replace ("+", "");
			return GuidString;
		}
	}

}

