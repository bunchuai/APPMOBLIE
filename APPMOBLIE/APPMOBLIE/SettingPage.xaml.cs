using APPMOBLIE.Model;
using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;

namespace APPMOBLIE
{
    public partial class SettingPage : ContentPage
    {
        public SettingPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
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
            base.OnAppearing();
          
        }
       
        //private async void TakePhoto_Clicked(object sender, EventArgs e)
        //{
        //    await CrossMedia.Current.Initialize();

        //    if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
        //    {
        //        await DisplayAlert("No Camera", ":( No camera avaialble.", "OK");
        //        return;
        //    }

        //    var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
        //    {
        //        Directory = "Sample",
        //        Name = "test.jpg",
        //        PhotoSize = PhotoSize.Medium,
        //        SaveToAlbum = true,
        //        AllowCropping = true
        //    });
        //    if (file == null)
        //        return;
        //    await DisplayAlert("File Location", file.Path, "OK");
        //    image.Source = ImageSource.FromStream(() =>
        //    {
        //        var stream = file.GetStream();
        //        file.Dispose();
        //        return stream;
        //    });
        //}
        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EditUser());
        }

        private async void Button_Signout(object sender, EventArgs e)
        {
            Application.Current.Properties.Clear();
            var LoginPage = new Login();
            await Navigation.PushAsync(LoginPage);
            NavigationPage.SetHasNavigationBar(LoginPage, false);
        }

        private void SetProType(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new ListProductType());

        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new ListLocation());
        }
    }
}