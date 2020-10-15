using APPMOBLIE.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plugin.Media;
using Plugin.Media.Abstractions;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace APPMOBLIE
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditUser : ContentPage
    {
        public SQLiteAsyncConnection connection;
        public byte[] ImgBytes;
        public EditUser()
        {
            GetDataUser();

            InitializeComponent();
         
        }
        protected override  void OnAppearing()
        {
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

                  
                    OldUsername.Text = Result.Nickname == string.Empty ? Application.Current.Properties["Username"].ToString() : Result.Nickname;
                    Username.Text = Result.Nickname == string.Empty ? Application.Current.Properties["Username"].ToString() : Result.Nickname;
                    ImgUser.Source = Result.Userimage == null ? ImageSource.FromResource("userpic.png") : ImageSource.FromStream(() => new MemoryStream(Result.Userimage));
                }

            }
        }
        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            string action = await DisplayActionSheet("Select Photo From ?", "Cancel", "", "Camera", "Gallery");
            await CrossMedia.Current.Initialize();

            if (action == "Camera")
            {
                var mediaOption = new StoreCameraMediaOptions()
                {
                    SaveToAlbum = true,
                    PhotoSize = PhotoSize.Small,
                };
                var TakeImageFile = await CrossMedia.Current.TakePhotoAsync(mediaOption);
                if (TakeImageFile != null)
                {
                    ImgUser.Source = ImageSource.FromStream(() => TakeImageFile.GetStream());
                }
              

            }
            else if(action == "Gallery")
            {
                var mediaOptions = new PickMediaOptions()
                {
                    PhotoSize = PhotoSize.Small
                };
                var SelectedImageFile = await CrossMedia.Current.PickPhotoAsync(mediaOptions);
                if (SelectedImageFile != null)
                {
                    ImgUser.Source = ImageSource.FromStream(() => SelectedImageFile.GetStream());
                }
                
            }
        }
        private async void Save_Clicked(object sender, EventArgs e)
        {
            ImgBytes = ImageSourceToBytes(ImgUser.Source);

            using (HttpClient client = new HttpClient())
            {
                var oJsonObject = new JObject
                {
                    { "Nickname", this.OldUsername.Text },
                    { "Userimage", ImgBytes }
                };

                string Url = "http://203.151.166.97/api/USers/UpdateUserProfile?UserId="+ Application.Current.Properties["UserId"].ToString();
                client.BaseAddress = new Uri(Url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "oLQKZ-tbA58nvbw7PuZhSsy3sr_H28YB3U3XGbpEc5pGIpMrB1nKNjpS_mRhDiFt2QOBZw3IntJ3dmrozZKsw6hd2VuLvoS8-0HxMCVsMUbZ6QZD782Ig1rfFFWPJ13qDq-cMoUgE2t-PdFEp_85aqa8crtVD6aRwntMPjDOgOriFBbzCYjeXyQ3JECl4pOZGd2KYhpCM7n4hXjfCA0t2YeQyvbuId1-e-qhltjEzCkRk7uffgtbwC2KAImsw7jrBYFfxeu1DCRRYdi2AsSZVyBHk0pAqcekzv5jlxLaK2Z-5hFVN0EzSA86Z2MkAq_vXPnJMq0ZrlGfZG6l-hJYb7NjGZCKD44euOf4l-dGQqi40wd8oIhacT1WIrr2RoSAxQn3t1TLDU2TNbgd_pW89JAHd9fmF9k-aZt9tCJuFQs-sW7eJQ1spYqQWHEbKYFbf2Aih5ZBDrIbLeh4hRRFOd_zYZgQKqIZ1tpZ_82UwYUG8FyPn9ZexVzr4t4At4cP");
                HttpResponseMessage response = await client.PutAsync(Url, new StringContent(oJsonObject.ToString(), Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    var ResponseData = await response.Content.ReadAsStringAsync();
                    var Result = JsonConvert.DeserializeObject<RespondMessage>(ResponseData);
                    if (Result.Valid == true)
                    {
                        await DisplayAlert("สำเร็จ", "ดำเนินการเสร็จสิ้น", "ตกลง");
                        await Navigation.PopToRootAsync();
                    }
                    else
                    {
                        await DisplayAlert("แจ้งเตือน", "กรุณาทำรายการใหม่อีกครั้ง", "ตกลง");
                    }
                }
            }
        }
        private void Cancel_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
        public byte[] ImageSourceToBytes(ImageSource imageSource)
        {
            StreamImageSource streamImageSource = (StreamImageSource)imageSource;
            System.Threading.CancellationToken cancellationToken =
            System.Threading.CancellationToken.None;
            Task<Stream> task = streamImageSource.Stream(cancellationToken);
            Stream stream = task.Result;
            byte[] bytesAvailable = new byte[stream.Length];
            stream.Read(bytesAvailable, 0, bytesAvailable.Length);
            return bytesAvailable;
        }
        private class RespondMessage
        {
            public bool Valid { get; set; }
            public string Message { get; set; }
        }


    }
}