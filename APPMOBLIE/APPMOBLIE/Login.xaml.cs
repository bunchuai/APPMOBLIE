using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace APPMOBLIE
{
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
                       
        }

        private async void button_SignIn(object sender, EventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                string sContentType = "application/json";
                JObject oJsonObject = new JObject();
                oJsonObject.Add("UserName", this.Email.Text);
                oJsonObject.Add("Password", this.Password.Text);

                string Url = "http://203.151.166.97/api/Account/Login";
                client.BaseAddress = new Uri(Url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "oLQKZ-tbA58nvbw7PuZhSsy3sr_H28YB3U3XGbpEc5pGIpMrB1nKNjpS_mRhDiFt2QOBZw3IntJ3dmrozZKsw6hd2VuLvoS8-0HxMCVsMUbZ6QZD782Ig1rfFFWPJ13qDq-cMoUgE2t-PdFEp_85aqa8crtVD6aRwntMPjDOgOriFBbzCYjeXyQ3JECl4pOZGd2KYhpCM7n4hXjfCA0t2YeQyvbuId1-e-qhltjEzCkRk7uffgtbwC2KAImsw7jrBYFfxeu1DCRRYdi2AsSZVyBHk0pAqcekzv5jlxLaK2Z-5hFVN0EzSA86Z2MkAq_vXPnJMq0ZrlGfZG6l-hJYb7NjGZCKD44euOf4l-dGQqi40wd8oIhacT1WIrr2RoSAxQn3t1TLDU2TNbgd_pW89JAHd9fmF9k-aZt9tCJuFQs-sW7eJQ1spYqQWHEbKYFbf2Aih5ZBDrIbLeh4hRRFOd_zYZgQKqIZ1tpZ_82UwYUG8FyPn9ZexVzr4t4At4cP");
                HttpResponseMessage response = await client.PostAsync(Url, new StringContent(oJsonObject.ToString(), Encoding.UTF8, sContentType));
                if (response.IsSuccessStatusCode)
                {
                    var ResponseData = await response.Content.ReadAsStringAsync();
                    var Result = JsonConvert.DeserializeObject<CheckLogin>(ResponseData);
                    if (Result.valid == true)
                    {
                        Application.Current.Properties["Username"] = Result.Username;
                        Application.Current.Properties["UserId"] = Result.UserId;
                        Application.Current.Properties["CompanyId"] = Result.ComId;
                        await Application.Current.SavePropertiesAsync();

                        var Page = new HomePage();
                        await Navigation.PushAsync(Page);
                        NavigationPage.SetHasNavigationBar(Page, false);
                    }
                    else
                    {
                        displayError.Text = "ชื่อผู้ใช้ หรือรหัสผ่านไม่ถูกต้อง";
                    }
                }
                else
                {
                    displayError.Text = "ชื่อผู้ใช้ หรือรหัสผ่านไม่ถูกต้อง";
                }
            }
        }

        private void Button_Reset(object sender, EventArgs e)
        {
            Email.Text = string.Empty;
            Password.Text = string.Empty;
        }

        private class UserInfo
        {
            public string Email { get; set; }
            public string HasRegistered { get; set; }
            public string LoginProvider { get; set; }
        }

        private class CheckLogin
        {
            public bool valid { get; set; }
            public string Username { get; set; }
            public string UserId { get; set; }
            public int ComId { get; set; }
        }

        private void button_forgot(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new Forgotpassword());
        }
    }
}