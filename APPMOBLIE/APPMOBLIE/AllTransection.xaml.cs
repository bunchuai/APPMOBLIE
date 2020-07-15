using APPMOBLIE.Model;
using Microcharts;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using SkiaSharp;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Entry = Microcharts.Entry;

namespace APPMOBLIE
{
    public partial class AllTransection : ContentPage
    {
        public SQLiteAsyncConnection connection;
        public bool dashboardIn { get; set; }
        public bool dashboardOut { get; set; }
        public bool dashboardRe { get; set; }
        public List<TransactionInOut> InResultData = new List<TransactionInOut>();
        public List<TransactionInOut> OutResultData = new List<TransactionInOut>();
        public List<TransactionLows> LowResultData = new List<TransactionLows>();

        async Task<List<TransactionInOut>> GetDataInList()
        {
            using (HttpClient client = new HttpClient())
            {
                var CompanyId = Application.Current.Properties["CompanyId"];

                string Url = "http://203.151.166.97/api/Products/TransactionInProduct?CompanyId=" + CompanyId + "&Take=" + 15;
                client.BaseAddress = new Uri(Url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "oLQKZ-tbA58nvbw7PuZhSsy3sr_H28YB3U3XGbpEc5pGIpMrB1nKNjpS_mRhDiFt2QOBZw3IntJ3dmrozZKsw6hd2VuLvoS8-0HxMCVsMUbZ6QZD782Ig1rfFFWPJ13qDq-cMoUgE2t-PdFEp_85aqa8crtVD6aRwntMPjDOgOriFBbzCYjeXyQ3JECl4pOZGd2KYhpCM7n4hXjfCA0t2YeQyvbuId1-e-qhltjEzCkRk7uffgtbwC2KAImsw7jrBYFfxeu1DCRRYdi2AsSZVyBHk0pAqcekzv5jlxLaK2Z-5hFVN0EzSA86Z2MkAq_vXPnJMq0ZrlGfZG6l-hJYb7NjGZCKD44euOf4l-dGQqi40wd8oIhacT1WIrr2RoSAxQn3t1TLDU2TNbgd_pW89JAHd9fmF9k-aZt9tCJuFQs-sW7eJQ1spYqQWHEbKYFbf2Aih5ZBDrIbLeh4hRRFOd_zYZgQKqIZ1tpZ_82UwYUG8FyPn9ZexVzr4t4At4cP");
                HttpResponseMessage responsedatain = await client.GetAsync(Url);
                if (responsedatain.IsSuccessStatusCode)
                {
                    //In List Product//
                    var Datain = await responsedatain.Content.ReadAsStringAsync();
                    InResultData = JsonConvert.DeserializeObject<List<TransactionInOut>>(Datain);
                }
            }
            return InResultData;
        }
        async Task<List<TransactionInOut>> GetDataOutList()
        {
            using (HttpClient client = new HttpClient())
            {
                var CompanyId = Application.Current.Properties["CompanyId"];

                string Url = "http://203.151.166.97/api/Products/TransactionOutProduct?CompanyId=" + CompanyId + "&Take=" + 15;
                client.BaseAddress = new Uri(Url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "oLQKZ-tbA58nvbw7PuZhSsy3sr_H28YB3U3XGbpEc5pGIpMrB1nKNjpS_mRhDiFt2QOBZw3IntJ3dmrozZKsw6hd2VuLvoS8-0HxMCVsMUbZ6QZD782Ig1rfFFWPJ13qDq-cMoUgE2t-PdFEp_85aqa8crtVD6aRwntMPjDOgOriFBbzCYjeXyQ3JECl4pOZGd2KYhpCM7n4hXjfCA0t2YeQyvbuId1-e-qhltjEzCkRk7uffgtbwC2KAImsw7jrBYFfxeu1DCRRYdi2AsSZVyBHk0pAqcekzv5jlxLaK2Z-5hFVN0EzSA86Z2MkAq_vXPnJMq0ZrlGfZG6l-hJYb7NjGZCKD44euOf4l-dGQqi40wd8oIhacT1WIrr2RoSAxQn3t1TLDU2TNbgd_pW89JAHd9fmF9k-aZt9tCJuFQs-sW7eJQ1spYqQWHEbKYFbf2Aih5ZBDrIbLeh4hRRFOd_zYZgQKqIZ1tpZ_82UwYUG8FyPn9ZexVzr4t4At4cP");
                HttpResponseMessage responsedataout = await client.GetAsync(Url);
                if (responsedataout.IsSuccessStatusCode)
                {
                    //In List Product//
                    var Dataout = await responsedataout.Content.ReadAsStringAsync();
                    OutResultData = JsonConvert.DeserializeObject<List<TransactionInOut>>(Dataout);
                }
            }
            return OutResultData;
        }
        async Task<List<TransactionLows>> GetDataLowList()
        {
            using (HttpClient client = new HttpClient())
            {
                var CompanyId = Application.Current.Properties["CompanyId"];

                string Url = "http://203.151.166.97/api/Products/ProductMin?CompanyId=" + CompanyId;
                client.BaseAddress = new Uri(Url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "oLQKZ-tbA58nvbw7PuZhSsy3sr_H28YB3U3XGbpEc5pGIpMrB1nKNjpS_mRhDiFt2QOBZw3IntJ3dmrozZKsw6hd2VuLvoS8-0HxMCVsMUbZ6QZD782Ig1rfFFWPJ13qDq-cMoUgE2t-PdFEp_85aqa8crtVD6aRwntMPjDOgOriFBbzCYjeXyQ3JECl4pOZGd2KYhpCM7n4hXjfCA0t2YeQyvbuId1-e-qhltjEzCkRk7uffgtbwC2KAImsw7jrBYFfxeu1DCRRYdi2AsSZVyBHk0pAqcekzv5jlxLaK2Z-5hFVN0EzSA86Z2MkAq_vXPnJMq0ZrlGfZG6l-hJYb7NjGZCKD44euOf4l-dGQqi40wd8oIhacT1WIrr2RoSAxQn3t1TLDU2TNbgd_pW89JAHd9fmF9k-aZt9tCJuFQs-sW7eJQ1spYqQWHEbKYFbf2Aih5ZBDrIbLeh4hRRFOd_zYZgQKqIZ1tpZ_82UwYUG8FyPn9ZexVzr4t4At4cP");
                HttpResponseMessage responsedatalow = await client.GetAsync(Url);
                if (responsedatalow.IsSuccessStatusCode)
                {
                    //In List Product//
                    var Datalow = await responsedatalow.Content.ReadAsStringAsync();
                    LowResultData = JsonConvert.DeserializeObject<List<TransactionLows>>(Datalow);
                }
            }
            return LowResultData;
        }
        public AllTransection()
        {
            InitializeComponent();
            dashboardIn = false;
            dashboardOut = false;
            dashboardRe = false;
            Dashboardin.IsVisible = dashboardIn;
            Dashboardout.IsVisible = dashboardOut;
            Dashboardreorder.IsVisible = dashboardRe;
            connection = DependencyService.Get<InterfaceSQLite>().GetConnection();

        }


        protected override async void OnAppearing()
        {
            await connection.CreateTableAsync<PersonInfo>();
            if (await connection.Table<PersonInfo>().CountAsync() == 0)
            {
                Username.Text = Application.Current.Properties["Username"].ToString();
                ImgUser.Source = ImageSource.FromResource("APPMOBLIE.Images.userpic.png");
                
            }

            Dashboardin.ItemsSource = await GetDataInList();
            Dashboardout.ItemsSource = await GetDataOutList();
            Dashboardreorder.ItemsSource = await GetDataLowList();

            base.OnAppearing();

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
        private async void Dashboardin_Refreshing(object sender, EventArgs e)
        {
            Dashboardin.ItemsSource = await GetDataInList();
            Dashboardin.EndRefresh();
        }
        private async void Dashboardout_Refreshing(object sender, EventArgs e)
        {
            Dashboardout.ItemsSource = await GetDataOutList();
            Dashboardout.EndRefresh();
        }
        private async void Dashboardreorder_Refreshing(object sender, EventArgs e)
        {
            Dashboardreorder.ItemsSource = await GetDataLowList();
            Dashboardreorder.EndRefresh();
        }

        private void BtnStockin(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new StockIn());
        }
        private void BtnStockOut(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new StockOut());
        }
        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EditUser());
        }
    }


}
