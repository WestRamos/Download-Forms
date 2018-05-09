
using System;
using System.Collections.Generic;
using System.Text;

namespace DevAzt.FormsX.Storage.Device
{
    public class KnownFolders : IKnownFolders
    {
        private IKnownFolders Service { get; }

        public KnownFolders(IKnownFolders service)
        {
            Service = service;
        }

        public static KnownFolders Instance
        {
            get
            {
                var service = Xamarin.Forms.DependencyService.Get<IKnownFolders>();
                if (service == null) throw new NullReferenceException("IKnownFolder no puede ser null");
                return new KnownFolders(service);
            }
        }

        public IStorageFolder CameraRoll {
            get
            {
                return Service.CameraRoll;
            }
        }

        public IStorageFolder Documents
        {
            get
            {
                return Service.Documents;
            }
        }

        public IStorageFolder Downloads
        {
            get
            {
                return Service.Downloads;
            }
        }

        public IStorageFolder Home
        {
            get
            {
                return Service.Home;
            }
        }

        public IStorageFolder Movies
        {
            get
            {
                return Service.Movies;
            }
        }

        public IStorageFolder Music
        {
            get
            {
                return Service.Music;
            }
        }

        public IStorageFolder Pictures
        {
            get
            {
                return Service.Pictures;
            }
        }

        public IStorageFolder SDCard
        {
            get
            {
                return Service.SDCard;
            }
        }

        public IStorageFolder Root
        {
            get
            {
                return Service.Root;
            }
        }
    }
}
