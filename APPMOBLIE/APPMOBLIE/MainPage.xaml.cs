using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace APPMOBLIE
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public string currentuserID;
        HttpClient client = new HttpClient();
        public MainPage()
        {
            InitializeComponent();
            currentuserID = (Application.Current.Properties["Username"].ToString());
        }

        private async void Button_Scan(object sender, EventArgs e)
        {
            var Scan = new ZXingScannerPage();
            await Navigation.PushAsync(Scan);
            Scan.OnScanResult += (result) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PopAsync();

                    using (HttpClient client = new HttpClient())
                    {
                        string Url = "http://192.168.201.33:8080/api/Products/Product?Sku=" + result.Text;
                        client.BaseAddress = new Uri(Url);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "oLQKZ-tbA58nvbw7PuZhSsy3sr_H28YB3U3XGbpEc5pGIpMrB1nKNjpS_mRhDiFt2QOBZw3IntJ3dmrozZKsw6hd2VuLvoS8-0HxMCVsMUbZ6QZD782Ig1rfFFWPJ13qDq-cMoUgE2t-PdFEp_85aqa8crtVD6aRwntMPjDOgOriFBbzCYjeXyQ3JECl4pOZGd2KYhpCM7n4hXjfCA0t2YeQyvbuId1-e-qhltjEzCkRk7uffgtbwC2KAImsw7jrBYFfxeu1DCRRYdi2AsSZVyBHk0pAqcekzv5jlxLaK2Z-5hFVN0EzSA86Z2MkAq_vXPnJMq0ZrlGfZG6l-hJYb7NjGZCKD44euOf4l-dGQqi40wd8oIhacT1WIrr2RoSAxQn3t1TLDU2TNbgd_pW89JAHd9fmF9k-aZt9tCJuFQs-sW7eJQ1spYqQWHEbKYFbf2Aih5ZBDrIbLeh4hRRFOd_zYZgQKqIZ1tpZ_82UwYUG8FyPn9ZexVzr4t4At4cP");
                        HttpResponseMessage response = await client.GetAsync(Url);
                        if (response.IsSuccessStatusCode)
                        {
                            var ResponseData = await response.Content.ReadAsStringAsync();
                            var Results = JsonConvert.DeserializeObject<Product>(ResponseData);
                            if (Results != null)
                            {
                                Mycode.Text = Results.SKUId;
                                ProductName.Text = Results.ProductName;
                                ProductBrand.Text = Results.ProductBrand;
                                Volume.Text = Results.ProductUnit;
                                ProductExpireDate.Text = Results.ProductExpireDate.ToString();
                            }
                            else
                            {
                                await DisplayAlert("Warning", "No product.", "OK");
                            }
                        }
                    }
                });
            };
        }

        private void Button_Reset(object sender, EventArgs e)
        {
            Mycode.Text = string.Empty;
            ProductName.Text = string.Empty;
            ProductBrand.Text = string.Empty;
            Volume.Text = string.Empty;
            ProductExpireDate.Text = string.Empty;
        }

        private async void Button_Add(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddProduct());
        }

        public class Product
        {
            public string SKUId { get; set; }
            public string ProductName { get; set; }
            public string ProductBrand { get; set; }
            public string ProductModel { get; set; }
            public int ProductTypeId { get; set; }
            public int ProductLocation { get; set; }
            public string ProductUnit { get; set; }
            public string ProductDescription { get; set; }
            public DateTime ProductExpireDate { get; set; }
        }

        
    }
}
