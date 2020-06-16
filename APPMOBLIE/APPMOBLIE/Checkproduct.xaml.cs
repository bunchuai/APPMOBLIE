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
        public string SkuId;
       
        public Checkproduct(string Getsku = null)
        {
            SkuId = Getsku;
            InitializeComponent();
            CompanyId = Application.Current.Properties["CompanyId"].ToString();
            Alert.Text = "---ไม่พบข้อมูล ---";
            FindingProduct(SkuId);


        }

        protected override  void OnAppearing()
        {

            Username.Text = Application.Current.Properties["Username"].ToString();
            base.OnAppearing();
        }

        public async void FindingProduct(string find)
        {
            using (HttpClient client = new HttpClient())
            {
                var CompanyId = Application.Current.Properties["CompanyId"];
                string Url = "http://203.151.166.97/api/Products/CheckProduct?Sku=" + find + "&CompanyId=" + CompanyId;
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
                    HttpResponseMessage TransactionFive = await client.GetAsync("http://203.151.166.97/api/Products/TransactionBySku?CompanyId=" + CompanyId + "&Take=" + 10 + "&Sku=" + find);
                    var Five = await TransactionFive.Content.ReadAsStringAsync();
                    var TrnsactionResult = JsonConvert.DeserializeObject<List<TransactionModels>>(Five);


                    if (TrnsactionResult.Count > 0)
                    {
                        listview.ItemsSource = TrnsactionResult;
                        Fram1.IsVisible = true;
                        Titletrans.Text = "รายการสินค้า เข้า - ออก";

                        Procd.Text = "รหัสสินค้า : " + Result.ProductCode;
                        Skuinfo.Text = "หมายเลข Barcode : " + Result.Sku;
                        Prodn.Text = "ชื่อสินค้า : " + Result.Name;
                        Prodb.Text = "ยี่ห้อสินค้า : " + Result.Brand;
                        Prodm.Text = "รุ่นสินค้า : " + Result.Model;
                        Prolo.Text = "ตำแหน่งที่จัดเก็บ : " + Result.Location;
                        Proin.Text = "จำนวนในคลัง : " + Result.ProductIn.ToString() + " " + TrnsactionResult.Select(s => s.Unit).FirstOrDefault();
                        Proin.TextColor = Color.Green;
                        Proout.Text = "จำนวนการเบิก : " + Result.ProductOut.ToString() + " " + TrnsactionResult.Select(s => s.Unit).FirstOrDefault();
                        Proout.TextColor = Color.Red;
                        Available.Text = "จำนวนคงเหลือ : " + Result.Amount.ToString() + " " + TrnsactionResult.Select(s => s.Unit).FirstOrDefault();
                        Available.TextColor = (Result.Amount <= Convert.ToInt32(Min) ? Color.Red : Color.Green);
                        Alert.Text = string.Empty;
                    }
                    else
                    {
                        Alert.Text = "--- ไม่พบรายการสินค้า เข้า - ออก ---";
                    }

                }
               
            }
        }

        async private  void Button_Scan(object sender, EventArgs e)
        {
            Procd.Text = string.Empty;
            Skuinfo.Text = string.Empty;
            Prodn.Text = string.Empty;
            Prodb.Text = string.Empty;
            Prodm.Text = string.Empty;
            Prolo.Text = string.Empty;
            Proin.Text = string.Empty;
            Proout.Text = string.Empty;
            Available.Text = string.Empty;
            Titletrans.Text = string.Empty;
            listview.ItemsSource = null;
            Fram1.IsVisible = false;


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
                        string Url = "http://203.151.166.97/api/Products/CheckProduct?Sku=" + result.Text + "&CompanyId=" + CompanyId;
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

                            if (TrnsactionResult.Count > 0)
                            {
                                listview.ItemsSource = TrnsactionResult;
                                Fram1.IsVisible = true;
                                Titletrans.Text = "รายการสินค้า เข้า - ออก";

                                Skuinfo.Text = "หมายเลข Barcode : " + Result.Sku;
                                Procd.Text = "รหัสสินค้า : " + Result.ProductCode;
                                Prodn.Text = "ชื่อสินค้า : " + Result.Name;
                                Prodb.Text = "ยี่ห้อสินค้า : " + Result.Brand;
                                Prodm.Text = "รุ่นสินค้า : " + Result.Model;
                                Prolo.Text = "ตำแหน่งที่จัดเก็บ : " + Result.Location;
                                Proin.Text = "จำนวนในคลัง : " + Result.ProductIn.ToString() + " " + TrnsactionResult.Select(s => s.Unit).FirstOrDefault();
                                Proin.TextColor = Color.Green;
                                Proout.Text = "จำนวนการเบิก : " + Result.ProductOut.ToString() + " " + TrnsactionResult.Select(s => s.Unit).FirstOrDefault();
                                Proout.TextColor = Color.Red;
                                Available.Text = "จำนวนคงเหลือ : " + Result.Amount.ToString() + " " + TrnsactionResult.Select(s => s.Unit).FirstOrDefault();
                                Available.TextColor = (Result.Amount <= Convert.ToInt32(Min) ? Color.Red : Color.Green);
                                Alert.Text = string.Empty;
                            }
                            else
                            {
                                Alert.Text = "--- ไม่พบรายการสินค้า เข้า - ออก ---";
                            }
                        }
                        else
                        {
                            Alert.Text = "--- ไม่พบข้อมูล ---";
                        }
                    }
                });
            };
        }

        async private void findtext_Completed(object sender, EventArgs e)
        {
            var find = findtext.Text;
            if (find == "" || find == null)
            {
                Procd.Text = string.Empty;
                Skuinfo.Text = string.Empty;
                Prodn.Text = string.Empty;
                Prodb.Text = string.Empty;
                Prodm.Text = string.Empty;
                Prolo.Text = string.Empty;
                Proin.Text = string.Empty;
                Proout.Text = string.Empty;
                Available.Text = string.Empty;
                Alert.Text = string.Empty;
                listview.ItemsSource = null;
                Titletrans.Text = string.Empty;
                Fram1.IsVisible = false;
            }
            else
            {
                Procd.Text = string.Empty;
                Skuinfo.Text = string.Empty;
                Prodn.Text = string.Empty;
                Prodb.Text = string.Empty;
                Prodm.Text = string.Empty;
                Prolo.Text = string.Empty;
                Proin.Text = string.Empty;
                Proout.Text = string.Empty;
                Available.Text = string.Empty;
                Alert.Text = string.Empty;
                listview.ItemsSource = null;
                Titletrans.Text = string.Empty;
                Fram1.IsVisible = false;
            }

            using (HttpClient client = new HttpClient())
            {
                var CompanyId = Application.Current.Properties["CompanyId"];
                string Url = "http://203.151.166.97/api/Products/CheckProduct?Sku=" + find + "&CompanyId=" + CompanyId;
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
                    HttpResponseMessage TransactionFive = await client.GetAsync("http://203.151.166.97/api/Products/TransactionBySku?CompanyId=" + CompanyId + "&Take=" + 10 + "&Sku=" + find);
                    var Five = await TransactionFive.Content.ReadAsStringAsync();
                    var TrnsactionResult = JsonConvert.DeserializeObject<List<TransactionModels>>(Five);


                    if (TrnsactionResult.Count > 0)
                    {
                        listview.ItemsSource = TrnsactionResult;
                        Fram1.IsVisible = true;
                        Titletrans.Text = "รายการสินค้า เข้า - ออก";

                        Procd.Text = "รหัสสินค้า : " + Result.ProductCode;
                        Skuinfo.Text = "หมายเลข Barcode : " + Result.Sku;
                        Prodn.Text = "ชื่อสินค้า : " + Result.Name;
                        Prodb.Text = "ยี่ห้อสินค้า : " + Result.Brand;
                        Prodm.Text = "รุ่นสินค้า : " + Result.Model;
                        Prolo.Text = "ตำแหน่งที่จัดเก็บ : " + Result.Location;
                        Proin.Text = "จำนวนในคลัง : " + Result.ProductIn.ToString() + " " + TrnsactionResult.Select(s => s.Unit).FirstOrDefault();
                        Proin.TextColor = Color.Green;
                        Proout.Text = "จำนวนการเบิก : " + Result.ProductOut.ToString() + " " + TrnsactionResult.Select(s => s.Unit).FirstOrDefault();
                        Proout.TextColor = Color.Red;
                        Available.Text = "จำนวนคงเหลือ : " + Result.Amount.ToString() + " " + TrnsactionResult.Select(s => s.Unit).FirstOrDefault();
                        Available.TextColor = (Result.Amount <= Convert.ToInt32(Min) ? Color.Red : Color.Green);
                        Alert.Text = string.Empty;
                    }
                    else
                    {
                        Alert.Text = "--- ไม่พบรายการสินค้า เข้า - ออก ---";
                    }
                  
                }
                else
                {
                    Alert.Text = "--- ไม่พบข้อมูล ---";
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
            public string ProductCode { get; set; }


        }

    }
}