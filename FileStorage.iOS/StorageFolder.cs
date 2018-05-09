
using DevAzt.FormsX.Storage.Device;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace DevAzt.FormsX.iOS.Storage.Device
{
	public class StorageFolder : IStorageFolder
    {
		public StorageFolder ()
		{

		}

		public string FullPath {
			get;
			set;
		}

		public string Name {
			get;
			set;
		}

		public async Task<List<IStorageFile>> GetFiles(){
			List<IStorageFile> files = new List<IStorageFile> ();
			if (Directory.Exists (FullPath)) {
				var enumeratefiles = Directory.EnumerateFiles (FullPath);
				foreach (var file in enumeratefiles) {
					files.Add(await StorageFile.GetFileFromPath (file));
				}
			} else {
				throw new DirectoryNotFoundException ("No es posible obtener los archivos, al parecer, el directorio ya no existe");
			}
			return files;
		}

		public async Task<IStorageFile> GetFile(string filenamewithextension){
			if (Directory.Exists (FullPath)) {
				var filepath = Path.Combine (FullPath, filenamewithextension);
				return await StorageFile.GetFileFromPath (filepath);
			}else {
				throw new DirectoryNotFoundException ("No es posible obtener los archivos, al parecer, el directorio ya no existe");
			}
		}

		public async Task<IStorageFile> CreateFileAsync(string filename){
			return await CreateFile (filename, CreationCollisionOption.FailIfExists);
		}

		public async Task<IStorageFile> CreateFile(string filename, CreationCollisionOption option){
			var filepath = Path.Combine (FullPath, filename);
			if (option == CreationCollisionOption.OpenIfExists) {
				if (File.Exists (filepath)) {
					return await StorageFile.GetFileFromPath (filepath);
				}
			} else {
				if (option == CreationCollisionOption.FailIfExists) {
					if (File.Exists (filepath)) {
						throw new Exception ("El archivo ya existe");
					}
				} else {
					if (option == CreationCollisionOption.ReplaceExisting) {
						File.Delete(filepath);
					} else {
						filepath = Path.Combine (FullPath, UniqueString() + filename);
					}
				}
			}

			if (Directory.Exists (FullPath)) {
				using (var fileStream = File.Create (filepath)) {
					return await StorageFile.GetFileFromPath (filepath);
				}
			}else {
				throw new DirectoryNotFoundException ("El directorio actual no existe, posiblemente haya sido borrado");
			}
		}

		public async Task<IStorageFolder> CreateFolder(string foldername){
			return await CreateFolder (foldername, CreationCollisionOption.FailIfExists);
		}

		public async Task<IStorageFolder> CreateFolder(string foldername, CreationCollisionOption option){
			var dirpath = Path.Combine (FullPath, foldername);
			if (option == CreationCollisionOption.FailIfExists) {
				if (Directory.Exists (dirpath)) {
					throw new Exception ("El directorio que intentas crear ya existe");
				}
			} else {
				if (option == CreationCollisionOption.OpenIfExists) {
					await GetFolderFromPath (dirpath);
				} else {
					if (option == CreationCollisionOption.ReplaceExisting) {
						Directory.Delete (dirpath);	
					} else {
						dirpath = Path.Combine (FullPath, UniqueString() + foldername);
					}
				}
			}

			if (Directory.Exists (FullPath)) {
				var directoryInfo = Directory.CreateDirectory (dirpath);
				if (directoryInfo.Exists) {
					return await GetFolderFromPath (dirpath);
				} else {
					throw new DirectoryNotFoundException ("Imposible crear el directorio");
				}
			}else {
				throw new DirectoryNotFoundException ("El directorio actual no existe, posiblemente haya sido borrado");
			}
		}

		public async Task Delete(){
            await HackAwait();
            if (Directory.Exists (FullPath)) {
				Directory.Delete (FullPath);
			}else {
				throw new DirectoryNotFoundException ("El directorio actual no existe, posiblemente haya sido borrado");
			}
		}

		public async Task<IStorageFolder> GetFolder(string name){
			if (Directory.Exists (FullPath)) {
				var dir = Path.Combine (FullPath, name);
				if (Directory.Exists (dir)) {
					return await GetFolderFromPath (dir);
				} else {
					throw new DirectoryNotFoundException ("El directorio a buscar no existe");
				}
			}else {
				throw new DirectoryNotFoundException ("El directorio actual no existe, posiblemente haya sido borrado");
			}
		}

		public async Task<List<IStorageFolder>> GetFolders(){
			List<IStorageFolder> folders = new List<IStorageFolder> ();
			if (Directory.Exists (FullPath)) {
				var directorios = Directory.EnumerateDirectories (FullPath);
				foreach (var directorio in directorios) {
					folders.Add(await GetFolderFromPath (directorio));
				}
			}else {
				throw new DirectoryNotFoundException ("El directorio actual no existe, posiblemente haya sido borrado");
			}
			return folders;
		}

		private string UniqueString(){
			Guid g = Guid.NewGuid ();
			string GuidString = Convert.ToBase64String (g.ToByteArray ());
			GuidString = GuidString.Replace ("=", "");
			GuidString = GuidString.Replace ("+", "");
			return GuidString;
		}

		public static async Task<IStorageFolder> GetFolderFromPath(string dirpath){
            await HackAwait();
            IStorageFolder storagefolder = new StorageFolder();
			string[] element = dirpath.Split ('/');
			string foldername = element [element.Length - 1];
			if (System.IO.Directory.Exists (dirpath)) {
				storagefolder.FullPath = dirpath;
				storagefolder.Name = foldername;
			} else {
				throw new DirectoryNotFoundException ("No existe el directorio " + foldername);
			}
			return storagefolder;
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

