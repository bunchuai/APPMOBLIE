using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace APPMOBLIE
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StockIn : PopupPage
    {
        public StockIn()
        {
            InitializeComponent();
        }

        void BtnBtnStockin(object sender, System.EventArgs e)
        {
            PopupNavigation.Instance.PopAsync(true);
        }
    }
}