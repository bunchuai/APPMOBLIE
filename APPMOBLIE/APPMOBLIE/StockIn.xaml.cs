using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
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
    public partial class StockIn : ContentPage
    {
        public StockIn()
        {
            InitializeComponent();
        }

        public void Cancel_Click(object sender, System.EventArgs e)
        {
            Mycode.Text = string.Empty;
            ProductName.Text = string.Empty;
            Quantity.Text = string.Empty;
            ReferentNumber.Text = string.Empty;
        }

        public async void ScanBarCode(object sender, System.EventArgs e) 
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
                        var CompanyId = Application.Current.Properties["CompanyId"];
                        string Url = "http://203.151.166.97/api/Products/CheckProductInsert?Sku=" + result.Text + "&CompanyId=" + CompanyId;
                        client.BaseAddress = new Uri(Url);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "oLQKZ-tbA58nvbw7PuZhSsy3sr_H28YB3U3XGbpEc5pGIpMrB1nKNjpS_mRhDiFt2QOBZw3IntJ3dmrozZKsw6hd2VuLvoS8-0HxMCVsMUbZ6QZD782Ig1rfFFWPJ13qDq-cMoUgE2t-PdFEp_85aqa8crtVD6aRwntMPjDOgOriFBbzCYjeXyQ3JECl4pOZGd2KYhpCM7n4hXjfCA0t2YeQyvbuId1-e-qhltjEzCkRk7uffgtbwC2KAImsw7jrBYFfxeu1DCRRYdi2AsSZVyBHk0pAqcekzv5jlxLaK2Z-5hFVN0EzSA86Z2MkAq_vXPnJMq0ZrlGfZG6l-hJYb7NjGZCKD44euOf4l-dGQqi40wd8oIhacT1WIrr2RoSAxQn3t1TLDU2TNbgd_pW89JAHd9fmF9k-aZt9tCJuFQs-sW7eJQ1spYqQWHEbKYFbf2Aih5ZBDrIbLeh4hRRFOd_zYZgQKqIZ1tpZ_82UwYUG8FyPn9ZexVzr4t4At4cP");
                        HttpResponseMessage response = await client.GetAsync(Url);
                        if (response.IsSuccessStatusCode)
                        {
                            var ResponseData = await response.Content.ReadAsStringAsync();
                            var Result = JsonConvert.DeserializeObject<ProductDetail>(ResponseData);

                            Mycode.Text = result.Text;
                            ProductName.Text = Result.Name;
                        }
                        else
                        {
                            var Page = new AddProduct();
                            await Navigation.PushAsync(Page);
                            NavigationPage.SetHasNavigationBar(Page, true);
                        }
                    }
                });
            };
        }

        public async void Mycode_Completed(object sender, EventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                var CompanyId = Application.Current.Properties["CompanyId"];
                string Url = "http://203.151.166.97/api/Products/CheckProductInsert?Sku=" + Mycode.Text + "&CompanyId=" + CompanyId;
                client.BaseAddress = new Uri(Url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "oLQKZ-tbA58nvbw7PuZhSsy3sr_H28YB3U3XGbpEc5pGIpMrB1nKNjpS_mRhDiFt2QOBZw3IntJ3dmrozZKsw6hd2VuLvoS8-0HxMCVsMUbZ6QZD782Ig1rfFFWPJ13qDq-cMoUgE2t-PdFEp_85aqa8crtVD6aRwntMPjDOgOriFBbzCYjeXyQ3JECl4pOZGd2KYhpCM7n4hXjfCA0t2YeQyvbuId1-e-qhltjEzCkRk7uffgtbwC2KAImsw7jrBYFfxeu1DCRRYdi2AsSZVyBHk0pAqcekzv5jlxLaK2Z-5hFVN0EzSA86Z2MkAq_vXPnJMq0ZrlGfZG6l-hJYb7NjGZCKD44euOf4l-dGQqi40wd8oIhacT1WIrr2RoSAxQn3t1TLDU2TNbgd_pW89JAHd9fmF9k-aZt9tCJuFQs-sW7eJQ1spYqQWHEbKYFbf2Aih5ZBDrIbLeh4hRRFOd_zYZgQKqIZ1tpZ_82UwYUG8FyPn9ZexVzr4t4At4cP");
                HttpResponseMessage response = await client.GetAsync(Url);
                if (response.IsSuccessStatusCode)
                {
                    var ResponseData = await response.Content.ReadAsStringAsync();
                    var Result = JsonConvert.DeserializeObject<ProductDetail>(ResponseData);

                    Mycode.Text = Mycode.Text;
                    ProductName.Text = Result.Name;
                }
                else
                {
                    var Page = new AddProduct();
                    await Navigation.PushAsync(Page);
                    NavigationPage.SetHasNavigationBar(Page, true);
                }
            }
        }

        public async void Save_Click(object sender, System.EventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                JObject oJsonObject = new JObject();
                oJsonObject.Add("SkuId", this.Mycode.Text);
                oJsonObject.Add("CompanyId", Application.Current.Properties["CompanyId"].ToString());
                oJsonObject.Add("UserId", Application.Current.Properties["UserId"].ToString());
                oJsonObject.Add("Quantitys", this.Quantity.Text);
                oJsonObject.Add("ReferentNumber", this.ReferentNumber.Text);
                oJsonObject.Add("TransectionType", "IN");
                oJsonObject.Add("ProductDescription", "");

                string Url = "http://203.151.166.97/api/Products/AddProductTransaction";
                client.BaseAddress = new Uri(Url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "oLQKZ-tbA58nvbw7PuZhSsy3sr_H28YB3U3XGbpEc5pGIpMrB1nKNjpS_mRhDiFt2QOBZw3IntJ3dmrozZKsw6hd2VuLvoS8-0HxMCVsMUbZ6QZD782Ig1rfFFWPJ13qDq-cMoUgE2t-PdFEp_85aqa8crtVD6aRwntMPjDOgOriFBbzCYjeXyQ3JECl4pOZGd2KYhpCM7n4hXjfCA0t2YeQyvbuId1-e-qhltjEzCkRk7uffgtbwC2KAImsw7jrBYFfxeu1DCRRYdi2AsSZVyBHk0pAqcekzv5jlxLaK2Z-5hFVN0EzSA86Z2MkAq_vXPnJMq0ZrlGfZG6l-hJYb7NjGZCKD44euOf4l-dGQqi40wd8oIhacT1WIrr2RoSAxQn3t1TLDU2TNbgd_pW89JAHd9fmF9k-aZt9tCJuFQs-sW7eJQ1spYqQWHEbKYFbf2Aih5ZBDrIbLeh4hRRFOd_zYZgQKqIZ1tpZ_82UwYUG8FyPn9ZexVzr4t4At4cP");
                HttpResponseMessage response = await client.PostAsync(Url, new StringContent(oJsonObject.ToString(), Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("สำเร็จ", "ดำเนินการเสร็จสิ้น", "ตกลง");
                    await Navigation.PushAsync(new HomePage());
                }
                else
                {
                    await DisplayAlert("แจ้งเตือน", "กรุณาตรวจสอบความถูกต้อง", "ตกลง");
                }
            }
        }

        private class ProductDetail
        {
            public int CompanyId { get; set; }
            public string SkuId { get; set; }
            public string Name { get; set; }
            public string Model { get; set; }
            public string Brand { get; set; }
            public string UnitCode { get; set; }
            public string LocationCode { get; set; }
            public string Description { get; set; }
            public int Productmin { get; set; }
            public int Quantity { get; set; }
            public string ReferentNunber { get; set; }
            public string TransectionType { get; set; }
        }

        private class CheckLogin
        {
            public bool valid { get; set; }
            public string Username { get; set; }
            public string UserId { get; set; }
        }
    }
}