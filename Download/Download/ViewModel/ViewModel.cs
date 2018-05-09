using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Download.ViewModel
{
    public class ViewModel
    {

        private Page _page;

        public ViewModel(Page page)
        {
            _page = page;
        }

        public async Task Toast(string message)
        {
            await _page.DisplayAlert("Download Manager", message, "Aceptar");
        }

    }
}
