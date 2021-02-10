using APPMOBLIE.Model;
using Newtonsoft.Json;
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
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TransectionOut : ContentPage
    {

        public ICollection<TransactionInOut> TransactionOutResult { get; set; }
        public TransectionOut()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
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
                    var Result = JsonConvert.DeserializeObject<List<TransactionInOut>>(Dataout);
                    TransactionOutResult = Result;
                    ListTranOut.ItemsSource = Result;
                }
            }
        }

        private void BtnStockOut(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new StockOut());
        }

        IEnumerable<TransactionInOut> GetTransactionOut(string searchtext = null)
        {
            var transactiondata = TransactionOutResult;
            if (String.IsNullOrWhiteSpace(searchtext))
            {
                return (List<TransactionInOut>)transactiondata;
            }

            return transactiondata.Where(w => w.ProductName.ToUpper().StartsWith(searchtext.ToUpper()) || w.ProductCode.ToUpper().StartsWith(searchtext.ToUpper()) || w.Ref.ToUpper().StartsWith(searchtext.ToUpper()));
        }


        private void SearchBar_TranOut(object sender, TextChangedEventArgs e)
        {
            ListTranOut.ItemsSource = GetTransactionOut(e.NewTextValue.ToUpper());
        }
    }
}