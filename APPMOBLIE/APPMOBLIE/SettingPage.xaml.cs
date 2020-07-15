using APPMOBLIE.Model;
using Plugin.Media;
using Plugin.Media.Abstractions;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;

namespace APPMOBLIE
{
    public partial class SettingPage : ContentPage
    {

        public SQLiteAsyncConnection connection;

        public SettingPage()
        {
            InitializeComponent();
           
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