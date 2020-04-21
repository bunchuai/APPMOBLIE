using APPMOBLIE.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace APPMOBLIE
{
    public partial class Checkproduct : ContentPage
    {
        public string CompanyId;
        public Checkproduct()
        {
            InitializeComponent();
            CompanyId = Application.Current.Properties["CompanyId"].ToString();
            Alert.Text = "--- No Data ---";
        }

        protected override async void OnAppearing()
        {
            Username.Text = Application.Current.Properties["Username"].ToString();
            base.OnAppearing();


            using (HttpClient client = new HttpClient())
            {
                var CompanyId = Application.Current.Properties["CompanyId"];

                string Url = "http://203.151.166.97/api/Products/TransactionBySku?CompanyId=" + CompanyId + "&Take=" + 5 + "&Sku=" + 8857098203187;
                client.BaseAddress = new Uri(Url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "oLQKZ-tbA58nvbw7PuZhSsy3sr_H28YB3U3XGbpEc5pGIpMrB1nKNjpS_mRhDiFt2QOBZw3IntJ3dmrozZKsw6hd2VuLvoS8-0HxMCVsMUbZ6QZD782Ig1rfFFWPJ13qDq-cMoUgE2t-PdFEp_85aqa8crtVD6aRwntMPjDOgOriFBbzCYjeXyQ3JECl4pOZGd2KYhpCM7n4hXjfCA0t2YeQyvbuId1-e-qhltjEzCkRk7uffgtbwC2KAImsw7jrBYFfxeu1DCRRYdi2AsSZVyBHk0pAqcekzv5jlxLaK2Z-5hFVN0EzSA86Z2MkAq_vXPnJMq0ZrlGfZG6l-hJYb7NjGZCKD44euOf4l-dGQqi40wd8oIhacT1WIrr2RoSAxQn3t1TLDU2TNbgd_pW89JAHd9fmF9k-aZt9tCJuFQs-sW7eJQ1spYqQWHEbKYFbf2Aih5ZBDrIbLeh4hRRFOd_zYZgQKqIZ1tpZ_82UwYUG8FyPn9ZexVzr4t4At4cP");
                HttpResponseMessage response = await client.GetAsync(Url);
                if (response.IsSuccessStatusCode)
                {
                    var ResponseData = await response.Content.ReadAsStringAsync();
                    var TrnsactionResult = JsonConvert.DeserializeObject<List<TransactionModels>>(ResponseData);
                    listview.ItemsSource = TrnsactionResult;
                }
            }
        }

        private async void Button_Scan(object sender, EventArgs e)
        {
            Skuinfo.Text = string.Empty;
            Prodn.Text = string.Empty;
            Prodb.Text = string.Empty;
            Prodm.Text = string.Empty;
            Proin.Text = string.Empty;
            Proout.Text = string.Empty;
            Available.Text = string.Empty;
            titletrans.Text = string.Empty;
            listview.ItemsSource = null;


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

                            // minimum product
                            HttpResponseMessage ProductMin = await client.GetAsync("http://203.151.166.97/api/Products/CheckProductMin?CompanyId=" + CompanyId + "&Sku=" + Result.Sku);
                            var Min = await ProductMin.Content.ReadAsStringAsync();
                            //Transaction
                            HttpResponseMessage TransactionFive = await client.GetAsync("http://203.151.166.97/api/Products/TransactionBySku?CompanyId=" + CompanyId+ "&Take="+ 5 +"&Sku=" + result.Text);
                            var Five = await TransactionFive.Content.ReadAsStringAsync();
                            var TrnsactionResult = JsonConvert.DeserializeObject<List<TransactionModels>>(Five);
                            listview.ItemsSource = TrnsactionResult;

                            titletrans.Text = "Transaction";

                            Skuinfo.Text = "SKU Id : " + Result.Sku;
                            Skuinfo.FontSize = 16;

                            Prodn.Text = "Product Name : " + Result.Name;
                            Prodn.FontSize = 20;

                            Prodb.Text = "Brand : " + Result.Brand;
                            Prodb.FontSize = 16;

                            Prodm.Text = "Model  : " + Result.Model;
                            Prodm.FontSize = 16;

                            Proin.Text = "In Stock : " + Result.ProductIn.ToString() + " Unit";
                            Proin.TextColor = Color.Green;
                            Proin.FontSize = 16;

                            Proout.Text = "Out Stock : " + Result.ProductOut.ToString() + " Unit";
                            Proout.TextColor = Color.Red;
                            Proout.FontSize = 16;

                            Available.Text = "Available : " + Result.Amount.ToString() + " Unit";
                            Available.TextColor = (Result.Amount <= Convert.ToInt32(Min) ? Color.Red : Color.Green);
                            Available.FontSize = 16;

                            Alert.Text = string.Empty;

                        }
                        else
                        {
                            Alert.Text = "--- No Data ---";
                        }
                    }
                });
            };
        }
        private async void findtext_Completed(object sender, EventArgs e)
        {
            var find = findtext.Text;
            if (find == "" || find == null)
            {
                Skuinfo.Text = string.Empty;
                Prodn.Text = string.Empty;
                Prodb.Text = string.Empty;
                Prodm.Text = string.Empty;
                Proin.Text = string.Empty;
                Proout.Text = string.Empty;
                Available.Text = string.Empty;
                Alert.Text = string.Empty;
                listview.ItemsSource = null;
                titletrans.Text = string.Empty;
            }
            else
            {
                Skuinfo.Text = string.Empty;
                Prodn.Text = string.Empty;
                Prodb.Text = string.Empty;
                Prodm.Text = string.Empty;
                Proin.Text = string.Empty;
                Proout.Text = string.Empty;
                Available.Text = string.Empty;
                Alert.Text = string.Empty;
                listview.ItemsSource = null;
                titletrans.Text = string.Empty;
            }
            
            using (HttpClient client = new HttpClient())
            {
                var CompanyId = Application.Current.Properties["CompanyId"];
                string Url = "http://203.151.166.97/api/Products/CheclProduct?Sku=" + find + "&CompanyId=" + CompanyId;
                client.BaseAddress = new Uri(Url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "oLQKZ-tbA58nvbw7PuZhSsy3sr_H28YB3U3XGbpEc5pGIpMrB1nKNjpS_mRhDiFt2QOBZw3IntJ3dmrozZKsw6hd2VuLvoS8-0HxMCVsMUbZ6QZD782Ig1rfFFWPJ13qDq-cMoUgE2t-PdFEp_85aqa8crtVD6aRwntMPjDOgOriFBbzCYjeXyQ3JECl4pOZGd2KYhpCM7n4hXjfCA0t2YeQyvbuId1-e-qhltjEzCkRk7uffgtbwC2KAImsw7jrBYFfxeu1DCRRYdi2AsSZVyBHk0pAqcekzv5jlxLaK2Z-5hFVN0EzSA86Z2MkAq_vXPnJMq0ZrlGfZG6l-hJYb7NjGZCKD44euOf4l-dGQqi40wd8oIhacT1WIrr2RoSAxQn3t1TLDU2TNbgd_pW89JAHd9fmF9k-aZt9tCJuFQs-sW7eJQ1spYqQWHEbKYFbf2Aih5ZBDrIbLeh4hRRFOd_zYZgQKqIZ1tpZ_82UwYUG8FyPn9ZexVzr4t4At4cP");
                HttpResponseMessage response = await client.GetAsync(Url);
                if (response.IsSuccessStatusCode)
                {
                    var ResponseData = await response.Content.ReadAsStringAsync();
                    var Result = JsonConvert.DeserializeObject<ProductDetail>(ResponseData);

                    // minimum product
                    HttpResponseMessage ProductMin = await client.GetAsync("http://203.151.166.97/api/Products/CheckProductMin?CompanyId=" + CompanyId + "&Sku=" + Result.Sku);
                    var Min = await ProductMin.Content.ReadAsStringAsync();
                    //Transaction
                    HttpResponseMessage TransactionFive = await client.GetAsync("http://203.151.166.97/api/Products/TransactionBySku?CompanyId=" + CompanyId + "&Take=" + 5 + "&Sku=" + find);
                    var Five = await TransactionFive.Content.ReadAsStringAsync();
                    var TrnsactionResult = JsonConvert.DeserializeObject<List<TransactionModels>>(Five);
                   
                    listview.ItemsSource = TrnsactionResult;
                    titletrans.Text = "Transaction";

                    Skuinfo.Text = "SKU Id : " + Result.Sku;
                    Skuinfo.FontSize = 16;

                    Prodn.Text = "Product Name : " + Result.Name;
                    Prodn.FontSize = 20;

                    Prodb.Text = "Brand : " + Result.Brand;
                    Prodb.FontSize = 16;

                    Prodm.Text = "Model  : " + Result.Model;
                    Prodm.FontSize = 16;

                    Proin.Text = "In Stock : " + Result.ProductIn.ToString() + " Unit";
                    Proin.TextColor = Color.Green;
                    Proin.FontSize = 16;

                    Proout.Text = "Out Stock : " + Result.ProductOut.ToString() + " Unit";
                    Proout.TextColor = Color.Red;
                    Proout.FontSize = 16;

                    Available.Text = "Available : " + Result.Amount.ToString() + " Unit";
                    Available.TextColor = (Result.Amount <= Convert.ToInt32(Min) ? Color.Red : Color.Green);
                    Available.FontSize = 16;

                    Alert.Text = string.Empty;

                }
                else
                {
                    Alert.Text = "--- No Data ---";
                    Alert.FontSize = 16;


                }
            }
        }
       
        private class ProductDetail
        {
            public int ProductIn { get; set; }
            public string LastDateIn { get; set; }
            public string RefIn { get; set; }
            public int ProductOut { get; set; }
            public string LastDateOut { get; set; }
            public string RefOut { get; set; }
            public string Name { get; set; }
            public string Brand { get; set; }
            public string Model { get; set; }
            public int Amount { get; set; }
            public string Sku { get; set; }
            public string Location { get; set; }

        }

      

     
    }
}