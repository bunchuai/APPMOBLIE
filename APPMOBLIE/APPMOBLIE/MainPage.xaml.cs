using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace APPMOBLIE
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void ClickScan(object sender, EventArgs e)
        {
            var Scan = new ZXingScannerPage();
            await Navigation.PushAsync(Scan);
            Scan.OnScanResult += (result) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PopAsync();
                    Mycode.Text = result.Text;
                });
            };
        }

        void Button_Save(object sender, EventArgs e)
        {
            var Model = new Dictionary<string, string>
            {
                {"Mycode",this.Mycode.Text},
                {"ProductName",this.ProductName.Text},
                {"Location",this.Location.Text},
                {"Gender",this.Gender.SelectedItem.ToString()},
                {"Volume",this.Volume.Text}
            };
        }
    }
}
