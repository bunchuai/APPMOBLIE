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
    public partial class Report : ContentPage
    {
        public Report()
        {
            InitializeComponent();
        }

        public object ItemYear { get; private set; }

        protected override  void OnAppearing()
        {
            var CompanyId = Application.Current.Properties["CompanyId"];
            Username.Text = Application.Current.Properties["Username"].ToString();

            #region month

            string[] Monthes = new string[] { "ม.ค.", "ก.พ.", "มี.ค.", "เม.ย.", "พ.ค.", "มิ.ย.", "ก.ค.", "ส.ค.", "ก.ย.", "ต.ค.", "พ.ย.", "ธ.ค." };
            var _Months = new List<MonthTH>();
            for (int i = 0; i <= 11; i++)
            {
                var _Month = new MonthTH();
                _Month.MonthName = Monthes[i];
                _Months.Add(_Month);

            }

            months.ItemsSource = _Months;

            #endregion

            int ThisYear = DateTime.Now.Year;
            var _Years = new List<YearsTH>();
            int Start = ThisYear - 1;
            for (int Year = ThisYear; Year >= Start; Year--)
            {
                var _Year = new YearsTH();
                _Year.YearDisplay = Year + 543;
                _Years.Add(_Year);

            }

            years.ItemsSource = _Years;
        }

        private async void SendReport(object sender, EventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                var CompanyId = Application.Current.Properties["CompanyId"];
                var UserId = Application.Current.Properties["UserId"];
                string Month = months.Items[months.SelectedIndex];
                string Year = years.Items[years.SelectedIndex];


                string Url = "http://203.151.166.97/api/Reports/StockReport?UserId="+ UserId + "&MonthTH="+ Month + "&YearTH="+Year+"&CompanyId=" + CompanyId;
                client.BaseAddress = new Uri(Url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "oLQKZ-tbA58nvbw7PuZhSsy3sr_H28YB3U3XGbpEc5pGIpMrB1nKNjpS_mRhDiFt2QOBZw3IntJ3dmrozZKsw6hd2VuLvoS8-0HxMCVsMUbZ6QZD782Ig1rfFFWPJ13qDq-cMoUgE2t-PdFEp_85aqa8crtVD6aRwntMPjDOgOriFBbzCYjeXyQ3JECl4pOZGd2KYhpCM7n4hXjfCA0t2YeQyvbuId1-e-qhltjEzCkRk7uffgtbwC2KAImsw7jrBYFfxeu1DCRRYdi2AsSZVyBHk0pAqcekzv5jlxLaK2Z-5hFVN0EzSA86Z2MkAq_vXPnJMq0ZrlGfZG6l-hJYb7NjGZCKD44euOf4l-dGQqi40wd8oIhacT1WIrr2RoSAxQn3t1TLDU2TNbgd_pW89JAHd9fmF9k-aZt9tCJuFQs-sW7eJQ1spYqQWHEbKYFbf2Aih5ZBDrIbLeh4hRRFOd_zYZgQKqIZ1tpZ_82UwYUG8FyPn9ZexVzr4t4At4cP");
                HttpResponseMessage response = await client.GetAsync(Url);
                if (response.IsSuccessStatusCode)
                {
                    var ResponseData = await response.Content.ReadAsStringAsync();
                    var Results = JsonConvert.DeserializeObject<Response> (ResponseData);
                    if (Results.Valid == true)
                    {
                        await DisplayAlert("สำเร็จ", "ดำเนินการเสร็จสิ้น , กรุณาตรวจสอบ Email", "ตกลง");
                        await Navigation.PushAsync(new Report());
                    }
                    else
                    {
                        await DisplayAlert("แจ้งเตือน", "กรุณาลองใหม่อีกครั้ง", "ตกลง");
                    }

                }

            }
        }
        private class Response
        {
            public bool Valid { get; set; }
          
        }
    }
}
        
