
using DevAzt.FormsX.Net.HttpClient;
using Download.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Download
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
            Main main = new Main(this);
            BindingContext = main;
		}
    }
}
