using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace APPMOBLIE
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
       
        public ICollection<ProductDetail> TrnsactionResult  { get;set;}
        public class ProductDetail
        {
            public string SKUId { get; set; }
            public string ProductCode { get; set; }
            public string ProductBrand { get; set; }
            public string ProductName { get; set; }
            public string ProductModel { get; set; }

        }

        public MainPage()
        {
            InitializeComponent();
            Username.Text = Application.Current.Properties["Username"].ToString();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            using (HttpClient client = new HttpClient())
            {
                var CompanyId = Application.Current.Properties["CompanyId"];

                string Url = "http://203.151.166.97/api/Products/GetProductAll?CompanyId=" + CompanyId;
                client.BaseAddress = new Uri(Url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "oLQKZ-tbA58nvbw7PuZhSsy3sr_H28YB3U3XGbpEc5pGIpMrB1nKNjpS_mRhDiFt2QOBZw3IntJ3dmrozZKsw6hd2VuLvoS8-0HxMCVsMUbZ6QZD782Ig1rfFFWPJ13qDq-cMoUgE2t-PdFEp_85aqa8crtVD6aRwntMPjDOgOriFBbzCYjeXyQ3JECl4pOZGd2KYhpCM7n4hXjfCA0t2YeQyvbuId1-e-qhltjEzCkRk7uffgtbwC2KAImsw7jrBYFfxeu1DCRRYdi2AsSZVyBHk0pAqcekzv5jlxLaK2Z-5hFVN0EzSA86Z2MkAq_vXPnJMq0ZrlGfZG6l-hJYb7NjGZCKD44euOf4l-dGQqi40wd8oIhacT1WIrr2RoSAxQn3t1TLDU2TNbgd_pW89JAHd9fmF9k-aZt9tCJuFQs-sW7eJQ1spYqQWHEbKYFbf2Aih5ZBDrIbLeh4hRRFOd_zYZgQKqIZ1tpZ_82UwYUG8FyPn9ZexVzr4t4At4cP");
                HttpResponseMessage responselow = await client.GetAsync(Url);
                if (responselow.IsSuccessStatusCode)
                {
                    //Lows List Product//
                    var Datalows = await responselow.Content.ReadAsStringAsync();
                    var Trnsaction = JsonConvert.DeserializeObject<List<ProductDetail>>(Datalows);
                    TrnsactionResult = Trnsaction;
                    Productlist.ItemsSource = Trnsaction;

                }
            }
        }

        IEnumerable<ProductDetail> GetProductDetails(string searchtext = null)
        {
            var productdata = TrnsactionResult;
            if (String.IsNullOrWhiteSpace(searchtext))
            {
                return (List<ProductDetail>)productdata;
            }

            return productdata.Where(w => w.ProductName.ToUpper().StartsWith(searchtext.ToUpper()) || w.ProductBrand.ToUpper().StartsWith(searchtext.ToUpper()) || w.ProductCode.ToUpper().StartsWith(searchtext.ToUpper()) || w.ProductModel.ToUpper().StartsWith(searchtext.ToUpper()));
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
          Productlist.ItemsSource = GetProductDetails(e.NewTextValue.ToUpper());
        }

        private void Productlist_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var detail = e.Item as ProductDetail;
            Navigation.PushAsync(new Checkproduct(detail.SKUId));
        }

        async private void Button_Scan(object sender, EventArgs e)
        {
          
            var Scan = new ZXingScannerPage();
            await Navigation.PushAsync(Scan);
            Scan.OnScanResult += (result) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PushAsync(new Checkproduct(result.ToString()));
                    
                });
            };
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EditUser());
        }
    }
}
