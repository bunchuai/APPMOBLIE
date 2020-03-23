using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace APPMOBLIE
{
    public partial class AddProduct : ContentPage
    {
        public AddProduct()
        {
            InitializeComponent();
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
                    Mycode.Text = result.Text;
                });
            };
        }

        private async void Button_Save(object sender, EventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                string sContentType = "application/json";
                JObject oJsonObject = new JObject();
                oJsonObject.Add("SKUId", this.Mycode.Text);
                oJsonObject.Add("ProductName", this.ProductName.Text);
                oJsonObject.Add("ProductBrand", this.ProductBrand.Text);
                oJsonObject.Add("ProductModel", this.ProductModel.Text);
                oJsonObject.Add("ProductTypeId", this.ProductTypeId.Text);
                oJsonObject.Add("ProductLocation", this.ProductLocation.Text);
                oJsonObject.Add("ProductUnit", this.ProductUnit.Text);
                oJsonObject.Add("ProductDescription", this.ProductDescription.Text);
                oJsonObject.Add("ProductExpireDate", this.ProductExpireDate.Date);

                string Url = "http://203.151.166.97/api/Products/PostProduct";
                client.BaseAddress = new Uri(Url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "oLQKZ-tbA58nvbw7PuZhSsy3sr_H28YB3U3XGbpEc5pGIpMrB1nKNjpS_mRhDiFt2QOBZw3IntJ3dmrozZKsw6hd2VuLvoS8-0HxMCVsMUbZ6QZD782Ig1rfFFWPJ13qDq-cMoUgE2t-PdFEp_85aqa8crtVD6aRwntMPjDOgOriFBbzCYjeXyQ3JECl4pOZGd2KYhpCM7n4hXjfCA0t2YeQyvbuId1-e-qhltjEzCkRk7uffgtbwC2KAImsw7jrBYFfxeu1DCRRYdi2AsSZVyBHk0pAqcekzv5jlxLaK2Z-5hFVN0EzSA86Z2MkAq_vXPnJMq0ZrlGfZG6l-hJYb7NjGZCKD44euOf4l-dGQqi40wd8oIhacT1WIrr2RoSAxQn3t1TLDU2TNbgd_pW89JAHd9fmF9k-aZt9tCJuFQs-sW7eJQ1spYqQWHEbKYFbf2Aih5ZBDrIbLeh4hRRFOd_zYZgQKqIZ1tpZ_82UwYUG8FyPn9ZexVzr4t4At4cP");
                HttpResponseMessage response = await client.PostAsync(Url, new StringContent(oJsonObject.ToString(), Encoding.UTF8, sContentType));
                if (response.IsSuccessStatusCode)
                {
                    var ResponseData = await response.Content.ReadAsStringAsync();
                    var Result = JsonConvert.DeserializeObject<CheckLogin>(ResponseData);
                    if (Result.valid == true)
                    {
                        await DisplayAlert("Success", "Insert product success", "OK");
                        await Navigation.PushAsync(new MainPage());
                    }
                    else
                    {
                        await DisplayAlert("Warning", "Username or password invalit.", "OK");
                    }
                }
            }
        }

        private class CheckLogin
        {
            public bool valid { get; set; }
            public string Username { get; set; }
            public string UserId { get; set; }
        }
    }
}