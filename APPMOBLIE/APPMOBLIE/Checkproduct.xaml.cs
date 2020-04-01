using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace APPMOBLIE
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Checkproduct : ContentPage
    {
        public Checkproduct()
        {
            InitializeComponent();
            //Skuinfo.Text = "Product SKU : "+ "34290534445";
            //Prodn.Text = "Pen";
            //Prodb.Text = "Quantum";
            //Prodm.Text = "Geloplus";
            //Proin.Text = "20";
            //Proout.Text = "10";
            //Total.Text = "10";


        }

        private async void Button_Scan(object sender, EventArgs e)
        {
            var Scan = new ZXingScannerPage();
            await Navigation.PushAsync(Scan);
            Scan.OnScanResult += (result) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    //await Navigation.PopAsync();
                    using (HttpClient client = new HttpClient())
                    {
                        var CompanyId = Application.Current.Properties["CompanyId"];
                        string Url = "http://203.151.166.97/api/Products/CheclProduct?Sku=" + result.Text + "&CompanyId=" + CompanyId;
                        client.BaseAddress = new Uri(Url);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "oLQKZ-tbA58nvbw7PuZhSsy3sr_H28YB3U3XGbpEc5pGIpMrB1nKNjpS_mRhDiFt2QOBZw3IntJ3dmrozZKsw6hd2VuLvoS8-0HxMCVsMUbZ6QZD782Ig1rfFFWPJ13qDq-cMoUgE2t-PdFEp_85aqa8crtVD6aRwntMPjDOgOriFBbzCYjeXyQ3JECl4pOZGd2KYhpCM7n4hXjfCA0t2YeQyvbuId1-e-qhltjEzCkRk7uffgtbwC2KAImsw7jrBYFfxeu1DCRRYdi2AsSZVyBHk0pAqcekzv5jlxLaK2Z-5hFVN0EzSA86Z2MkAq_vXPnJMq0ZrlGfZG6l-hJYb7NjGZCKD44euOf4l-dGQqi40wd8oIhacT1WIrr2RoSAxQn3t1TLDU2TNbgd_pW89JAHd9fmF9k-aZt9tCJuFQs-sW7eJQ1spYqQWHEbKYFbf2Aih5ZBDrIbLeh4hRRFOd_zYZgQKqIZ1tpZ_82UwYUG8FyPn9ZexVzr4t4At4cP");
                        HttpResponseMessage response = await client.GetAsync(Url);
                        if (response.IsSuccessStatusCode)
                        {
                            var ResponseData = await response.Content.ReadAsStringAsync();
                            var Result = JsonConvert.DeserializeObject<ProductDetail>(ResponseData);

                            Skuinfo.Text = "Product SKU : " + Result.Sku;
                            Prodn.Text = Result.Name;
                            Prodb.Text = Result.Brand;
                            Prodm.Text = Result.Model;
                            Proin.Text = Result.ProductIn.ToString();
                            Proout.Text = Result.ProductOut.ToString();
                            Total.Text = Result.Amount.ToString();
                        }
                       
                    }
                });
            };
        }


        private class ProductDetail
        {
            public int ProductIn { get; set; }
            public int ProductOut { get; set; }
            public string Name { get; set; }
            public string Brand { get; set; }
            public string Model { get; set; }
            public int Amount { get; set; }
            public string Sku { get; set; }
        }
    }
}