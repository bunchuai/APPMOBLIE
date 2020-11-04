using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
    public partial class Forgotpassword : ContentPage
    {
        public Forgotpassword()
        {
            InitializeComponent();
        }

        private async void SendOTP(object sender, EventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                // set param
                JObject Json = new JObject() {
                    {"Mail", this.Email.Text }
                };

                string Url = "http://203.151.166.97/api/Account/ForgotPassword";
                client.BaseAddress = new Uri(Url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "oLQKZ-tbA58nvbw7PuZhSsy3sr_H28YB3U3XGbpEc5pGIpMrB1nKNjpS_mRhDiFt2QOBZw3IntJ3dmrozZKsw6hd2VuLvoS8-0HxMCVsMUbZ6QZD782Ig1rfFFWPJ13qDq-cMoUgE2t-PdFEp_85aqa8crtVD6aRwntMPjDOgOriFBbzCYjeXyQ3JECl4pOZGd2KYhpCM7n4hXjfCA0t2YeQyvbuId1-e-qhltjEzCkRk7uffgtbwC2KAImsw7jrBYFfxeu1DCRRYdi2AsSZVyBHk0pAqcekzv5jlxLaK2Z-5hFVN0EzSA86Z2MkAq_vXPnJMq0ZrlGfZG6l-hJYb7NjGZCKD44euOf4l-dGQqi40wd8oIhacT1WIrr2RoSAxQn3t1TLDU2TNbgd_pW89JAHd9fmF9k-aZt9tCJuFQs-sW7eJQ1spYqQWHEbKYFbf2Aih5ZBDrIbLeh4hRRFOd_zYZgQKqIZ1tpZ_82UwYUG8FyPn9ZexVzr4t4At4cP");
                HttpResponseMessage response = await client.PostAsync(Url, new StringContent(Json.ToString(), Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    var ResponseData = await response.Content.ReadAsStringAsync();
                    var Result = JsonConvert.DeserializeObject<ResponseMessage>(ResponseData);
                    if (Result.Valid == true)
                    {
                        await DisplayAlert("สำเร็จ", "ดำเนินการเสร็จสิ้นกรุณาตรวจสอบอีเมลของท่าน", "ตกลง");
                        var Page = new NewPassword();
                        await Navigation.PushAsync(Page);
                        NavigationPage.SetHasNavigationBar(Page, true);
                    }
                    else
                    {
                        Email.Text = string.Empty;
                        await DisplayAlert("แจ้งเตือน", "กรุณาตรวจสอบข้อมูลอีกครั้ง", "ตกลง");
                    }
                }
                else
                {
                    Email.Text = string.Empty;
                    await DisplayAlert("แจ้งเตือน", "กรุณาตรวจสอบข้อมูลอีกครั้ง", "ตกลง");
                }
            }
        }
        #region helper
        private class ResponseMessage
        {
            public bool Valid { get; set; }

            public string Message { get; set; }
        }
        #endregion
    }
}