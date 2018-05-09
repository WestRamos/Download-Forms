using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DevAzt.FormsX.Storage.Device
{
    public interface IStorageFolder
    {
        string FullPath { get; set; }
        string Name { get; set; }

        Task<IStorageFile> CreateFile(string filename, CreationCollisionOption option);
        Task<IStorageFile> CreateFileAsync(string filename);
        Task<IStorageFolder> CreateFolder(string foldername);
        Task<IStorageFolder> CreateFolder(string foldername, CreationCollisionOption option);
        Task Delete();
        Task<IStorageFile> GetFile(string filenamewithextension);
        Task<List<IStorageFile>> GetFiles();
        Task<IStorageFolder> GetFolder(string name);
        Task<List<IStorageFolder>> GetFolders();

    }
}
