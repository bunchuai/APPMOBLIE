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
                        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "hU2zRiAtNuI_JRWyfTK2ZHy9zSw3uZRRQyStAm5_E-to0xQ1yh8dL-he4RsaXIglM3s0NWTeJG9xGQovrM7iwaDvspm8LDPCi6edMtWddb5ITHFy6uaHq0Ir7KiGNJYbvQqiiwTTj2o8QsHO_0rWtMkEnhRvKKjD5WWGWW0MwSzy96tizhW1BXy1AUSxEmMMX4NJxyA6EOICpq2I0QYbPOy979BU9yfA41QKSvTFbDDUNdiKpJKvC-DJ2fShIYD1_h8ZnG4WfWF_6i-NiHNBIlg1Pn5qRABOkhfHQwgxLUXTAqDuth5tUS9QYLDhxDn4i2p3LM_Vp_j6UTAjWkzIZqcZ0bxdzdOEwAST5ht5_9plSmrUTEcNTBy0sYc0cFBXHRG7Nd_eNLsRRD8M3KxXwU_JAqx4hYJuiErk3C9q--ogqCym7QhaXYVhEsGzAVPqrq8L5MZ2fTyruHKNA08FYS2YG52Slm083DtOTpK5VM4EYAMmlCkFUnaTdLG4Syrn");
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
