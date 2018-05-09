
using DevAzt.FormsX.Storage.Device;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UWPFolder = Windows.Storage.StorageFolder;
using UWPFile = Windows.Storage.StorageFile;

namespace DevAzt.FormsX.UWP.Storage.Device
{
    public class StorageFolder : IStorageFolder
    {
        public StorageFolder()
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

        public async Task<List<IStorageFile>> GetFiles()
        {
            List<IStorageFile> files = new List<IStorageFile>();
            var winfolder = await UWPFolder.GetFolderFromPathAsync(FullPath);
            var winfiles = await winfolder.GetFilesAsync();
            foreach (var file in winfiles)
            {
                files.Add(new StorageFile
                {
                    Attributes = FileAttributes.Normal,
                    DisplayName = file.DisplayName,
                    DisplayType = file.DisplayType,
                    FullPath = file.Path,
                    Name = file.Name,
                    DateCreated = file.DateCreated.DateTime
                });
            }
            return files;
        }

		public async Task<IStorageFile> GetFile(string filenamewithextension){

            var winfolder = await UWPFolder.GetFolderFromPathAsync(FullPath);
            var file = await winfolder.GetFileAsync(filenamewithextension);
            return await StorageFile.GetFileFromPath (file.Path);
		}

		public async Task<IStorageFile> CreateFileAsync(string filename){
			return await CreateFile (filename, CreationCollisionOption.FailIfExists);
		}

		public async Task<IStorageFile> CreateFile(string filename, CreationCollisionOption option){
            var folder = await GetUWPFolder(FullPath);
            UWPFile file = null;
            switch (option)
            {
                case CreationCollisionOption.GenerateUniqueName:
                    file = await folder.CreateFileAsync(filename, Windows.Storage.CreationCollisionOption.GenerateUniqueName);
                    break;
                case CreationCollisionOption.ReplaceExisting:
                    file = await folder.CreateFileAsync(filename, Windows.Storage.CreationCollisionOption.ReplaceExisting);
                    break;
                case CreationCollisionOption.FailIfExists:
                    file = await folder.CreateFileAsync(filename, Windows.Storage.CreationCollisionOption.FailIfExists);
                    break;
                case CreationCollisionOption.OpenIfExists:
                    file = await folder.CreateFileAsync(filename, Windows.Storage.CreationCollisionOption.OpenIfExists);
                    break;
            }
            return new StorageFile
            {
                Attributes = (FileAttributes)((int)file.Attributes),
                DateCreated = file.DateCreated.DateTime,
                DisplayName = file.DisplayName,
                DisplayType = file.DisplayType,
                FullPath = file.Path,
                Name = file.Name
            };
        }

		public async Task<IStorageFolder> CreateFolder(string foldername){
			return await CreateFolder (foldername, CreationCollisionOption.FailIfExists);
		}

		public async Task<IStorageFolder> CreateFolder(string foldername, CreationCollisionOption option){
            var folder = await GetUWPFolder(FullPath);
            UWPFolder newfolder = null;
            switch (option)
            {
                case CreationCollisionOption.GenerateUniqueName:
                    newfolder = await folder.CreateFolderAsync(foldername, Windows.Storage.CreationCollisionOption.GenerateUniqueName);
                    break;
                case CreationCollisionOption.ReplaceExisting:
                    newfolder = await folder.CreateFolderAsync(foldername, Windows.Storage.CreationCollisionOption.ReplaceExisting);
                    break;
                case CreationCollisionOption.FailIfExists:
                    newfolder = await folder.CreateFolderAsync(foldername, Windows.Storage.CreationCollisionOption.FailIfExists);
                    break;
                case CreationCollisionOption.OpenIfExists:
                    newfolder = await folder.CreateFolderAsync(foldername, Windows.Storage.CreationCollisionOption.OpenIfExists);
                    break;
            }

            return new StorageFolder
            {
                FullPath = newfolder.Path,
                Name = newfolder.Name
            };
        }

		public async Task Delete(){
            var folder = await GetUWPFolder(FullPath);
            await folder.DeleteAsync();
        }

		public async Task<IStorageFolder> GetFolder(string name){
            var folder = await GetUWPFolder(FullPath);
            var uwpfolder = await folder.GetFolderAsync(name);
            return new StorageFolder
            {
                FullPath = uwpfolder.Path,
                Name = uwpfolder.Name
            };
        }

		public async Task<List<IStorageFolder>> GetFolders(){
			List<IStorageFolder> folders = new List<IStorageFolder> ();
            var folder = await GetUWPFolder(FullPath);
            var uwpfolders = await folder.GetFoldersAsync();
			foreach (var uwpfolder in uwpfolders) {
				folders.Add(new StorageFolder
                {
                    FullPath = uwpfolder.Path,
                    Name = uwpfolder.Name
                });
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
            var folder = await GetUWPFolder(dirpath);
            return new StorageFolder
            {
                FullPath = folder.Path,
                Name = folder.Name
            };
		}

        private static async Task<UWPFolder> GetUWPFolder(string path)
        {
            var folder = await UWPFolder.GetFolderFromPathAsync(path);
            return folder;
        }
    }
}

