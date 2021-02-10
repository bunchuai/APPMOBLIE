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
    public partial class TransectionIn : ContentPage
    {

       public ICollection<TransactionInOut> TransactionInResult { get; set; }
        public TransectionIn()
        {
            InitializeComponent();
           
        }

        

        protected override async void OnAppearing()
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
                    var ReponseData = await responsedatain.Content.ReadAsStringAsync();
                    var Result = JsonConvert.DeserializeObject<List<TransactionInOut>>(ReponseData);
                    TransactionInResult = Result;
                    ListTranIn.ItemsSource = Result;
                }
            }
        }
      

        private void BtnStockin(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new StockIn());
        }


        IEnumerable<TransactionInOut> GetTransactionIn(string searchtext = null)
        {
            var transactiondata = TransactionInResult;
            if (String.IsNullOrWhiteSpace(searchtext))
            {
                return (List<TransactionInOut>)transactiondata;
            }

            return transactiondata.Where(w => w.ProductName.ToUpper().StartsWith(searchtext.ToUpper()) || w.ProductCode.ToUpper().StartsWith(searchtext.ToUpper()) || w.Ref.ToUpper().StartsWith(searchtext.ToUpper()));
        }



        private void SearchIn_TextChanged(object sender, TextChangedEventArgs e)
        {
            ListTranIn.ItemsSource = GetTransactionIn(e.NewTextValue.ToUpper());
        }
    }
}