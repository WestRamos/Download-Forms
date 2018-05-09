# DownloadForms

# Uso

Agrega los dll de la carpeta plugin a los proyectos correspondientes

# Android

En MainActivity, antes del metodo loadaplication(new App()) pon la siguiente linea:

``
DevAzt.FormsX.Droid.Net.HttpClient.DownloadManager.Init(this);
``

# iOS

En AppDelegate, igual, antes del loadaplication

```csharp
DownloadManager.Init();
```

# UWP

En MainPage.xaml.cs, antes del metodo loadaplication

```csharp
DownloadManager.Init(this);
```

# NetStandar

En el proyecto de net standar, solo hay que hacer una pequeña llamada como la siguiente.

```csharp
using DevAzt.FormsX.Net.HttpClient;
using DevAzt.FormsX.Storage.Device;

var url = "http://img.com/24jasdw.jpg";
IStorageFile file = await DownloadManager.Current.Download("Descargando", "File", "Descarga completa", url, "download", "jpg");
if (file != null)
{
    // la descarga se ha completado, aqui podemos acceder a algunas propiedades del archivo "file"
}
```
