using APPMOBLIE.Model;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace APPMOBLIE
{
    public partial class AllTransection : ContentPage
    {
        public bool dashboardIn { get; set; }
        public bool dashboardOut { get; set; }
        public bool dashboardRe { get; set; }
        public AllTransection()
        {
            InitializeComponent();
            dashboardIn = false;
            dashboardOut = false;
            dashboardRe = false;
            Dashboardin.IsVisible = dashboardIn;
            Dashboardout.IsVisible = dashboardOut;
            Dashboardreorder.IsVisible = dashboardRe;
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
                    Dashboardin.ItemsSource = TrnsactionResult;
                    Dashboardout.ItemsSource = TrnsactionResult;
                    Dashboardreorder.ItemsSource = TrnsactionResult;
                }
            }
        }

        public void Button_ClickedIn(object sender, EventArgs e)
        {

            if (dashboardIn == false)
            {
                dashboardIn = true;
                Dashboardin.IsVisible = dashboardIn;
            }
            else
            {
                dashboardIn = false;
                Dashboardin.IsVisible = dashboardIn;
            }

        }
        public void Button_ClickedOut(object sender, EventArgs e)
        {

            if (dashboardOut == false)
            {
                dashboardOut = true;
                Dashboardout.IsVisible = dashboardOut;
            }
            else
            {
                dashboardOut = false;
                Dashboardout.IsVisible = dashboardOut;
            }

        }

        public void Button_ClickedReorder(object sender, EventArgs e)
        {

            if (dashboardRe == false)
            {
                dashboardRe = true;
                Dashboardreorder.IsVisible = dashboardRe;
            }
            else
            {
                dashboardRe = false;
                Dashboardreorder.IsVisible = dashboardRe;
            }

        }

        private void BtnStockin(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new StockIn());
        }

        private void BtnStockOut(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new StockOut());
        }
    }


}
    