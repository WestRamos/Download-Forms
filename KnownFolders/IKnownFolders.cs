using System;
using System.Collections.Generic;
using System.Text;

namespace DevAzt.FormsX.Storage.Device
{
    public interface IKnownFolders
    {
        IStorageFolder CameraRoll { get; }
        IStorageFolder Documents { get; }
        IStorageFolder Downloads { get; }
        IStorageFolder Home { get; }
        IStorageFolder Movies { get; }
        IStorageFolder Music { get; }
        IStorageFolder Pictures { get; }
        IStorageFolder Root { get; }
        IStorageFolder SDCard { get; }
    }
}
