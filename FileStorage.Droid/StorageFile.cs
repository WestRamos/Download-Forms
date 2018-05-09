using DevAzt.FormsX.Storage.Device;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DevAzt.FormsX.Droid.Storage.Device
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
				string outfilepath = Path.Combine (folderdestination.FullPath, filenamewithextension);
				if (option == NameCollisionOption.FailIfExists) {
					if (File.Exists (outfilepath)) {
						throw new Exception ("El archivo que tratas de copiar ya existe");
					}
				} else {
					if (option == NameCollisionOption.GenerateUniqueName) {
						outfilepath = Path.Combine (folderdestination.FullPath, UniqueString() + filenamewithextension);
					} else {
						File.Delete (outfilepath);
					}
				}
				File.Copy (FullPath, outfilepath);
				if (File.Exists (outfilepath)) {
					return await GetFileFromPath (outfilepath);
				} else {
					throw new FileNotFoundException ("No fue posible copiar el archivo");
				}
			}
            await HackAwait();
            return null;
		}

		public async Task Delete(){
			if (await Exists()) {
				File.Delete (FullPath);
                await HackAwait();
            }
		}

		public static async Task<StorageFile> GetFileFromPath(string path){
			string[] element = path.Split ('/');
			string filenameext = element [element.Length - 1];
			string[] elementfile = filenameext.Split ('.');
			string filename = elementfile [0];
			string ext = elementfile [1];
			var filedatetime = File.GetCreationTime (path);
			var fileAttr = File.GetAttributes (path);
            await HackAwait();
            return new StorageFile {
				Name = filenameext,
				DisplayName = filename,
				DisplayType = ext,
				DateCreated = filedatetime,
				Attributes = fileAttr,
				FullPath = path
			};
		}

		public async Task<Stream> Open(FileAccessMode mode){
			if (await Exists ()) {
                await HackAwait();
                if (mode == FileAccessMode.Read) {
					return File.OpenRead (FullPath);    
				} else {
					return File.OpenWrite (FullPath);
				}	
			}else {
				throw new FileNotFoundException ("El archivo que tratas de copiar no existe, path: " + FullPath + ", name: " + Name);
			}
		}

		private async Task<bool> Exists(){
            await HackAwait();
            if (File.Exists (FullPath)) {
				return true;
			} else {
				throw new FileNotFoundException ("El archivo que tratas de copiar no existe, path: " + FullPath + ", name: " + Name);
			}
		}

		private string UniqueString(){
			Guid g = Guid.NewGuid ();
			string GuidString = Convert.ToBase64String (g.ToByteArray ());
			GuidString = GuidString.Replace ("=", "");
			GuidString = GuidString.Replace ("+", "");
			return GuidString;
		}

        private static async Task HackAwait()
        {
            await Task.Run(async () =>
            {
                await Task.Delay(1);
            });
        }
	}

}

