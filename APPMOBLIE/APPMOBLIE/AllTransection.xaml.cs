using APPMOBLIE.Model;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace APPMOBLIE
{
    public partial class AllTransection : ContentPage
    {
       


        //public bool DashboardIn { get; set; }
        //public bool DashboardOut { get; set; }
        //public bool DashboardRe { get; set; }
        //public List<TransactionInOut> InResultData = new List<TransactionInOut>();
        //public List<TransactionInOut> OutResultData = new List<TransactionInOut>();
        //public List<TransactionLows> LowResultData = new List<TransactionLows>();


        //async Task<List<TransactionInOut>> GetDataInList()
        //{
        //    using (HttpClient client = new HttpClient())
        //    {
        //        var CompanyId = Application.Current.Properties["CompanyId"];

        //        string Url = "http://203.151.166.97/api/Products/TransactionInProduct?CompanyId=" + CompanyId + "&Take=" + 15;
        //        client.BaseAddress = new Uri(Url);
        //        client.DefaultRequestHeaders.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        //        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "oLQKZ-tbA58nvbw7PuZhSsy3sr_H28YB3U3XGbpEc5pGIpMrB1nKNjpS_mRhDiFt2QOBZw3IntJ3dmrozZKsw6hd2VuLvoS8-0HxMCVsMUbZ6QZD782Ig1rfFFWPJ13qDq-cMoUgE2t-PdFEp_85aqa8crtVD6aRwntMPjDOgOriFBbzCYjeXyQ3JECl4pOZGd2KYhpCM7n4hXjfCA0t2YeQyvbuId1-e-qhltjEzCkRk7uffgtbwC2KAImsw7jrBYFfxeu1DCRRYdi2AsSZVyBHk0pAqcekzv5jlxLaK2Z-5hFVN0EzSA86Z2MkAq_vXPnJMq0ZrlGfZG6l-hJYb7NjGZCKD44euOf4l-dGQqi40wd8oIhacT1WIrr2RoSAxQn3t1TLDU2TNbgd_pW89JAHd9fmF9k-aZt9tCJuFQs-sW7eJQ1spYqQWHEbKYFbf2Aih5ZBDrIbLeh4hRRFOd_zYZgQKqIZ1tpZ_82UwYUG8FyPn9ZexVzr4t4At4cP");
        //        HttpResponseMessage responsedatain = await client.GetAsync(Url);
        //        if (responsedatain.IsSuccessStatusCode)
        //        {
        //            //In List Product//
        //            var Datain = await responsedatain.Content.ReadAsStringAsync();
        //            InResultData = JsonConvert.DeserializeObject<List<TransactionInOut>>(Datain);
        //        }
        //    }
        //    return InResultData;
        //}
        //async Task<List<TransactionInOut>> GetDataOutList()
        //{
        //    using (HttpClient client = new HttpClient())
        //    {
        //        var CompanyId = Application.Current.Properties["CompanyId"];

        //        string Url = "http://203.151.166.97/api/Products/TransactionOutProduct?CompanyId=" + CompanyId + "&Take=" + 15;
        //        client.BaseAddress = new Uri(Url);
        //        client.DefaultRequestHeaders.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        //        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "oLQKZ-tbA58nvbw7PuZhSsy3sr_H28YB3U3XGbpEc5pGIpMrB1nKNjpS_mRhDiFt2QOBZw3IntJ3dmrozZKsw6hd2VuLvoS8-0HxMCVsMUbZ6QZD782Ig1rfFFWPJ13qDq-cMoUgE2t-PdFEp_85aqa8crtVD6aRwntMPjDOgOriFBbzCYjeXyQ3JECl4pOZGd2KYhpCM7n4hXjfCA0t2YeQyvbuId1-e-qhltjEzCkRk7uffgtbwC2KAImsw7jrBYFfxeu1DCRRYdi2AsSZVyBHk0pAqcekzv5jlxLaK2Z-5hFVN0EzSA86Z2MkAq_vXPnJMq0ZrlGfZG6l-hJYb7NjGZCKD44euOf4l-dGQqi40wd8oIhacT1WIrr2RoSAxQn3t1TLDU2TNbgd_pW89JAHd9fmF9k-aZt9tCJuFQs-sW7eJQ1spYqQWHEbKYFbf2Aih5ZBDrIbLeh4hRRFOd_zYZgQKqIZ1tpZ_82UwYUG8FyPn9ZexVzr4t4At4cP");
        //        HttpResponseMessage responsedataout = await client.GetAsync(Url);
        //        if (responsedataout.IsSuccessStatusCode)
        //        {
        //            //In List Product//
        //            var Dataout = await responsedataout.Content.ReadAsStringAsync();
        //            OutResultData = JsonConvert.DeserializeObject<List<TransactionInOut>>(Dataout);
        //        }
        //    }
        //    return OutResultData;
        //}
        //async Task<List<TransactionLows>> GetDataLowList()
        //{
        //    using (HttpClient client = new HttpClient())
        //    {
        //        var CompanyId = Application.Current.Properties["CompanyId"];

        //        string Url = "http://203.151.166.97/api/Products/ProductMin?CompanyId=" + CompanyId;
        //        client.BaseAddress = new Uri(Url);
        //        client.DefaultRequestHeaders.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        //        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "oLQKZ-tbA58nvbw7PuZhSsy3sr_H28YB3U3XGbpEc5pGIpMrB1nKNjpS_mRhDiFt2QOBZw3IntJ3dmrozZKsw6hd2VuLvoS8-0HxMCVsMUbZ6QZD782Ig1rfFFWPJ13qDq-cMoUgE2t-PdFEp_85aqa8crtVD6aRwntMPjDOgOriFBbzCYjeXyQ3JECl4pOZGd2KYhpCM7n4hXjfCA0t2YeQyvbuId1-e-qhltjEzCkRk7uffgtbwC2KAImsw7jrBYFfxeu1DCRRYdi2AsSZVyBHk0pAqcekzv5jlxLaK2Z-5hFVN0EzSA86Z2MkAq_vXPnJMq0ZrlGfZG6l-hJYb7NjGZCKD44euOf4l-dGQqi40wd8oIhacT1WIrr2RoSAxQn3t1TLDU2TNbgd_pW89JAHd9fmF9k-aZt9tCJuFQs-sW7eJQ1spYqQWHEbKYFbf2Aih5ZBDrIbLeh4hRRFOd_zYZgQKqIZ1tpZ_82UwYUG8FyPn9ZexVzr4t4At4cP");
        //        HttpResponseMessage responsedatalow = await client.GetAsync(Url);
        //        if (responsedatalow.IsSuccessStatusCode)
        //        {
        //            //In List Product//
        //            var Datalow = await responsedatalow.Content.ReadAsStringAsync();
        //            LowResultData = JsonConvert.DeserializeObject<List<TransactionLows>>(Datalow);
        //        }
        //    }
        //    return LowResultData;
        //}

        public AllTransection()
        {
            GetDataUser();

            InitializeComponent();
            //DashboardIn = false;
            //DashboardOut = false;
            //DashboardRe = false;
            //Dashboardin.IsVisible = DashboardIn;
            //Dashboardout.IsVisible = DashboardOut;
            //Dashboardreorder.IsVisible = DashboardRe;


        }



        protected override async void OnAppearing()
        {
            var CompanyId = Application.Current.Properties["CompanyId"];
            using (HttpClient client = new HttpClient())
            {
                string Url = "http://203.151.166.97/api/Users/GetUserProfile?UserId=" + Application.Current.Properties["UserId"].ToString();
                client.BaseAddress = new Uri(Url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "oLQKZ-tbA58nvbw7PuZhSsy3sr_H28YB3U3XGbpEc5pGIpMrB1nKNjpS_mRhDiFt2QOBZw3IntJ3dmrozZKsw6hd2VuLvoS8-0HxMCVsMUbZ6QZD782Ig1rfFFWPJ13qDq-cMoUgE2t-PdFEp_85aqa8crtVD6aRwntMPjDOgOriFBbzCYjeXyQ3JECl4pOZGd2KYhpCM7n4hXjfCA0t2YeQyvbuId1-e-qhltjEzCkRk7uffgtbwC2KAImsw7jrBYFfxeu1DCRRYdi2AsSZVyBHk0pAqcekzv5jlxLaK2Z-5hFVN0EzSA86Z2MkAq_vXPnJMq0ZrlGfZG6l-hJYb7NjGZCKD44euOf4l-dGQqi40wd8oIhacT1WIrr2RoSAxQn3t1TLDU2TNbgd_pW89JAHd9fmF9k-aZt9tCJuFQs-sW7eJQ1spYqQWHEbKYFbf2Aih5ZBDrIbLeh4hRRFOd_zYZgQKqIZ1tpZ_82UwYUG8FyPn9ZexVzr4t4At4cP");
                HttpResponseMessage response = await client.GetAsync(Url);
                if (response.IsSuccessStatusCode)
                {
                    var ResponseData = await response.Content.ReadAsStringAsync();
                    var Result = JsonConvert.DeserializeObject<PersonInfo>(ResponseData);

                    Username.Text = Result.Nickname == string.Empty ? Application.Current.Properties["Username"].ToString() : Result.Nickname;
                    ImgUser.Source = Result.Userimage == null ? ImageSource.FromResource("userpic.png") : ImageSource.FromStream(() => new MemoryStream(Result.Userimage));
                }

                //Transaction
                HttpResponseMessage CountTransaction = await client.GetAsync("http://203.151.166.97/api/Products/CountProduct?CompanyId=" + CompanyId);
                var ReadTransaction = await CountTransaction.Content.ReadAsStringAsync();
                var TransactionResults = JsonConvert.DeserializeObject<CountTransactions>(ReadTransaction);
                Total.Text = TransactionResults.All;
                CountIn.Text = TransactionResults.In;
                CountOut.Text = TransactionResults.Out;

            }

            using (HttpClient client = new HttpClient())
            {


                string Url = "http://203.151.166.97/api/Products/AllTrasactionsCurrentDate?CompanyId=1";  
                client.BaseAddress = new Uri(Url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "oLQKZ-tbA58nvbw7PuZhSsy3sr_H28YB3U3XGbpEc5pGIpMrB1nKNjpS_mRhDiFt2QOBZw3IntJ3dmrozZKsw6hd2VuLvoS8-0HxMCVsMUbZ6QZD782Ig1rfFFWPJ13qDq-cMoUgE2t-PdFEp_85aqa8crtVD6aRwntMPjDOgOriFBbzCYjeXyQ3JECl4pOZGd2KYhpCM7n4hXjfCA0t2YeQyvbuId1-e-qhltjEzCkRk7uffgtbwC2KAImsw7jrBYFfxeu1DCRRYdi2AsSZVyBHk0pAqcekzv5jlxLaK2Z-5hFVN0EzSA86Z2MkAq_vXPnJMq0ZrlGfZG6l-hJYb7NjGZCKD44euOf4l-dGQqi40wd8oIhacT1WIrr2RoSAxQn3t1TLDU2TNbgd_pW89JAHd9fmF9k-aZt9tCJuFQs-sW7eJQ1spYqQWHEbKYFbf2Aih5ZBDrIbLeh4hRRFOd_zYZgQKqIZ1tpZ_82UwYUG8FyPn9ZexVzr4t4At4cP");
                HttpResponseMessage responsedatain = await client.GetAsync(Url);
                if (responsedatain.IsSuccessStatusCode)
                {
                    var ReponseData = await responsedatain.Content.ReadAsStringAsync();
                    var Result = JsonConvert.DeserializeObject<List<AllTransactions>>(ReponseData);

                    var ViewModels = new List<AllTransactions>();
                    foreach (var Item in Result)
                    {
                        var ViewModel = new AllTransactions();
                        ViewModel.TransactionType = (Item.TransactionType == "IN" ? "plus-circle"  : "minus-circle" ) ;
                  
                        ViewModel.Ref = Item.Ref;
                        ViewModel.ProductName = Item.ProductName;
                        ViewModel.ProductCode = Item.ProductCode;
                        ViewModel.Locations = Item.Locations;
                        ViewModel.Username = Item.Username;
                        ViewModel.Amount = Item.Amount;
                        ViewModel.CreateDate = Item.CreateDate;
                        ViewModel.Description = Item.Description;

                        ViewModels.Add(ViewModel);
                    }
                    

                    ListTranToday.ItemsSource = ViewModels;

                    
                }
            }

            //using (HttpClient client = new HttpClient())
            //{


            //    string Url = "http://203.151.166.97api/Products/TransactionOutProduct?CompanyId=" + CompanyId;
            //    client.BaseAddress = new Uri(Url);
            //    client.DefaultRequestHeaders.Clear();
            //    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            //    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "oLQKZ-tbA58nvbw7PuZhSsy3sr_H28YB3U3XGbpEc5pGIpMrB1nKNjpS_mRhDiFt2QOBZw3IntJ3dmrozZKsw6hd2VuLvoS8-0HxMCVsMUbZ6QZD782Ig1rfFFWPJ13qDq-cMoUgE2t-PdFEp_85aqa8crtVD6aRwntMPjDOgOriFBbzCYjeXyQ3JECl4pOZGd2KYhpCM7n4hXjfCA0t2YeQyvbuId1-e-qhltjEzCkRk7uffgtbwC2KAImsw7jrBYFfxeu1DCRRYdi2AsSZVyBHk0pAqcekzv5jlxLaK2Z-5hFVN0EzSA86Z2MkAq_vXPnJMq0ZrlGfZG6l-hJYb7NjGZCKD44euOf4l-dGQqi40wd8oIhacT1WIrr2RoSAxQn3t1TLDU2TNbgd_pW89JAHd9fmF9k-aZt9tCJuFQs-sW7eJQ1spYqQWHEbKYFbf2Aih5ZBDrIbLeh4hRRFOd_zYZgQKqIZ1tpZ_82UwYUG8FyPn9ZexVzr4t4At4cP");
            //    HttpResponseMessage responsedatain = await client.GetAsync(Url);
            //    if (responsedatain.IsSuccessStatusCode)
            //    {
            //        var ReponseData = await responsedatain.Content.ReadAsStringAsync();
            //        var Result = JsonConvert.DeserializeObject<List<TransactionInOut>>(ReponseData);
            //        ListTranIn.ItemsSource = Result;

            //    }
            //}






            DateTimeNow.Text = DateTime.Now.ToString("dd/MM/yyyy");

            //Dashboardin.ItemsSource = await GetDataInList();
            //Dashboardout.ItemsSource = await GetDataOutList();
            //Dashboardreorder.ItemsSource = await GetDataLowList();

            base.OnAppearing();

        }
        public async void GetDataUser()
        {
            using (HttpClient client = new HttpClient())
            {
                string Url = "http://203.151.166.97/api/Users/GetUserProfile?UserId=" + Application.Current.Properties["UserId"].ToString();
                client.BaseAddress = new Uri(Url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "oLQKZ-tbA58nvbw7PuZhSsy3sr_H28YB3U3XGbpEc5pGIpMrB1nKNjpS_mRhDiFt2QOBZw3IntJ3dmrozZKsw6hd2VuLvoS8-0HxMCVsMUbZ6QZD782Ig1rfFFWPJ13qDq-cMoUgE2t-PdFEp_85aqa8crtVD6aRwntMPjDOgOriFBbzCYjeXyQ3JECl4pOZGd2KYhpCM7n4hXjfCA0t2YeQyvbuId1-e-qhltjEzCkRk7uffgtbwC2KAImsw7jrBYFfxeu1DCRRYdi2AsSZVyBHk0pAqcekzv5jlxLaK2Z-5hFVN0EzSA86Z2MkAq_vXPnJMq0ZrlGfZG6l-hJYb7NjGZCKD44euOf4l-dGQqi40wd8oIhacT1WIrr2RoSAxQn3t1TLDU2TNbgd_pW89JAHd9fmF9k-aZt9tCJuFQs-sW7eJQ1spYqQWHEbKYFbf2Aih5ZBDrIbLeh4hRRFOd_zYZgQKqIZ1tpZ_82UwYUG8FyPn9ZexVzr4t4At4cP");
                HttpResponseMessage response = await client.GetAsync(Url);
                if (response.IsSuccessStatusCode)
                {
                    var ResponseData = await response.Content.ReadAsStringAsync();
                    var Result = JsonConvert.DeserializeObject<PersonInfo>(ResponseData);
                    Username.Text = Result.Nickname == string.Empty ? Application.Current.Properties["Username"].ToString() : Result.Nickname;
                    ImgUser.Source = Result.Userimage == null ? ImageSource.FromResource("userpic.png") : ImageSource.FromStream(() => new MemoryStream(Result.Userimage));
                }

            }


        }
        public void Button_ClickedIn(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new TransectionIn());

            //if (DashboardIn == false)
            //{
            //    DashboardIn = true;
            //    Dashboardin.IsVisible = DashboardIn;
            //}
            //else
            //{
            //    DashboardIn = false;
            //    Dashboardin.IsVisible = DashboardIn;
            //}

        }
        public void Button_ClickedOut(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new TransectionOut());

            //if (DashboardOut == false)
            //{
            //    DashboardOut = true;
            //    Dashboardout.IsVisible = DashboardOut;
            //}
            //else
            //{
            //    DashboardOut = false;
            //    Dashboardout.IsVisible = DashboardOut;
            //}

        }
        public void Button_ClickedReorder(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new TransectionIn());
            //if (DashboardRe == false)
            //{
            //    DashboardRe = true;
            //    Dashboardreorder.IsVisible = DashboardRe;
            //}
            //else
            //{
            //    DashboardRe = false;
            //    Dashboardreorder.IsVisible = DashboardRe;
            //}

        }
        //private async void Dashboardin_Refreshing(object sender, EventArgs e)
        //{
        //    Dashboardin.ItemsSource = await GetDataInList();
        //    Dashboardin.EndRefresh();
        //}
        //private async void Dashboardout_Refreshing(object sender, EventArgs e)
        //{
        //    Dashboardout.ItemsSource = await GetDataOutList();
        //    Dashboardout.EndRefresh();
        //}
        //private async void Dashboardreorder_Refreshing(object sender, EventArgs e)
        //{
        //    Dashboardreorder.ItemsSource = await GetDataLowList();
        //    Dashboardreorder.EndRefresh();
        //}

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

        private void Button_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new Transactions());
        }

       
       

        private void ListTranToday_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            
        }

        private void button_ProductLow(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new ProductOut());
        }
    }


}
