using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DevAzt.FormsX.Storage.Device
{
    public interface IStorageFile
    {
        DateTime DateCreated { get; set; }
        string DisplayName { get; set; }
        string DisplayType { get; set; }
        string FullPath { get; set; }
        string Name { get; set; }

        Task<IStorageFile> CopyTo(IStorageFolder folderdestination);
        Task<IStorageFile> CopyTo(IStorageFolder folderdestination, string filenamewithextension);
        Task<IStorageFile> CopyTo(IStorageFolder folderdestination, string filenamewithextension, NameCollisionOption option);
        Task Delete();
        Task<Stream> Open(FileAccessMode mode);
    }
}
