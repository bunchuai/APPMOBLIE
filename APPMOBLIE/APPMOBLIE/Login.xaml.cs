using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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

        private async void button_SignIn(object sender, EventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                string sContentType = "application/json";
                JObject oJsonObject = new JObject();
                oJsonObject.Add("UserName", this.Email.Text);
                oJsonObject.Add("Password", this.Password.Text);

                string Url = "http://192.168.201.33:8080//api/Account/Login";
                client.BaseAddress = new Uri(Url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "hU2zRiAtNuI_JRWyfTK2ZHy9zSw3uZRRQyStAm5_E-to0xQ1yh8dL-he4RsaXIglM3s0NWTeJG9xGQovrM7iwaDvspm8LDPCi6edMtWddb5ITHFy6uaHq0Ir7KiGNJYbvQqiiwTTj2o8QsHO_0rWtMkEnhRvKKjD5WWGWW0MwSzy96tizhW1BXy1AUSxEmMMX4NJxyA6EOICpq2I0QYbPOy979BU9yfA41QKSvTFbDDUNdiKpJKvC-DJ2fShIYD1_h8ZnG4WfWF_6i-NiHNBIlg1Pn5qRABOkhfHQwgxLUXTAqDuth5tUS9QYLDhxDn4i2p3LM_Vp_j6UTAjWkzIZqcZ0bxdzdOEwAST5ht5_9plSmrUTEcNTBy0sYc0cFBXHRG7Nd_eNLsRRD8M3KxXwU_JAqx4hYJuiErk3C9q--ogqCym7QhaXYVhEsGzAVPqrq8L5MZ2fTyruHKNA08FYS2YG52Slm083DtOTpK5VM4EYAMmlCkFUnaTdLG4Syrn");
                HttpResponseMessage response = await client.PostAsync(Url, new StringContent(oJsonObject.ToString(), Encoding.UTF8, sContentType));
                if (response.IsSuccessStatusCode)
                {
                    var ResponseData = await response.Content.ReadAsStringAsync();
                    var Result = JsonConvert.DeserializeObject<CheckLogin>(ResponseData);
                    if (Result.valid == true)
                    {
                        Application.Current.Properties["Username"] = Result.Username;
                        Application.Current.Properties["UserId"] = Result.UserId;
                        await Application.Current.SavePropertiesAsync();

                        await Navigation.PushAsync(new MainPage());
                    }
                    else
                    {
                        await DisplayAlert("Warning", "Username or password invalit.", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Warning", "Username or password invalit.", "OK");
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
        }
    }
}