using APPMOBLIE.Model;
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
        public string CompanyId;
        public AddProduct()
        {
            InitializeComponent();
            CompanyId = Application.Current.Properties["CompanyId"].ToString();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            Username.Text = Application.Current.Properties["Username"].ToString();

            using (HttpClient client = new HttpClient())
            {
                var CompanyId = Application.Current.Properties["CompanyId"];
                string Url = "http://203.151.166.97/api/ProductUnit/GetUnitAll?CompanyId=" + CompanyId;
                client.BaseAddress = new Uri(Url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "oLQKZ-tbA58nvbw7PuZhSsy3sr_H28YB3U3XGbpEc5pGIpMrB1nKNjpS_mRhDiFt2QOBZw3IntJ3dmrozZKsw6hd2VuLvoS8-0HxMCVsMUbZ6QZD782Ig1rfFFWPJ13qDq-cMoUgE2t-PdFEp_85aqa8crtVD6aRwntMPjDOgOriFBbzCYjeXyQ3JECl4pOZGd2KYhpCM7n4hXjfCA0t2YeQyvbuId1-e-qhltjEzCkRk7uffgtbwC2KAImsw7jrBYFfxeu1DCRRYdi2AsSZVyBHk0pAqcekzv5jlxLaK2Z-5hFVN0EzSA86Z2MkAq_vXPnJMq0ZrlGfZG6l-hJYb7NjGZCKD44euOf4l-dGQqi40wd8oIhacT1WIrr2RoSAxQn3t1TLDU2TNbgd_pW89JAHd9fmF9k-aZt9tCJuFQs-sW7eJQ1spYqQWHEbKYFbf2Aih5ZBDrIbLeh4hRRFOd_zYZgQKqIZ1tpZ_82UwYUG8FyPn9ZexVzr4t4At4cP");
                HttpResponseMessage response = await client.GetAsync(Url);
                if (response.IsSuccessStatusCode)
                {
                    var ResponseData = await response.Content.ReadAsStringAsync();
                    var Results = JsonConvert.DeserializeObject<List<MasterUnit>>(ResponseData);
                    var Items = new List<PickerProductUnit>();
                    foreach (var Data in Results)
                    {
                        var Item = new PickerProductUnit();
                        Item.Name = Data.Name;
                        Item.Code = Data.ProductUnitCode;
                        Items.Add(Item);
                    }

                    ProductUnit.ItemsSource = Items;
                }
            }
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
                            ProductBrand.Text = Result.Brand;
                            ProductModel.Text = Result.Model;
                            Productmin.Text = Result.Productmin.ToString();
                            ProductUnit.SelectedItem = Result.UnitCode;
                        }
                        else
                        {
                            Mycode.Text = result.Text;
                        }
                    }
                });
            };
        }

        private async void Button_Save(object sender, EventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                string sContentType = "application/json";
                JObject oJsonObject = new JObject();
                oJsonObject.Add("SkuId", this.Mycode.Text);
                oJsonObject.Add("Name", this.ProductName.Text);
                oJsonObject.Add("Brand", this.ProductBrand.Text);
                oJsonObject.Add("Model", this.ProductModel.Text);
                oJsonObject.Add("LocationCode", this.ProductLocation.Text);
                oJsonObject.Add("UnitCode", this.ProductUnit.ItemsSource[Convert.ToInt32(ProductUnit.SelectedItem)] as string);
                oJsonObject.Add("ExpireDate", this.ProductExpireDate.Date);
                oJsonObject.Add("UserId", Application.Current.Properties["UserId"].ToString());
                oJsonObject.Add("CompanyId", Application.Current.Properties["CompanyId"].ToString());
                oJsonObject.Add("Description", this.ProductDescription.Text);
                oJsonObject.Add("Quantity", this.Quantity.Text);
                oJsonObject.Add("ReferentNunber", this.ReferentNunber.Text);
                oJsonObject.Add("Productmin", this.Productmin.Text);

                string Url = "http://203.151.166.97/api/Products/AddProduct";
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
                        await Navigation.PushAsync(new AllTransection());
                    }
                    else
                    {
                        await DisplayAlert("Warning", "Insert product fail", "OK");
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

        private class PickerProductUnit
        {
            public string Code { get; set; }
            public string Name { get; set; }
        }
    }
}