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
using Xamarin.Forms.Xaml;

namespace APPMOBLIE
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditUser : ContentPage
    {
        public SQLiteAsyncConnection connection;
        public EditUser()
        {
            InitializeComponent();
           

            connection = DependencyService.Get<InterfaceSQLite>().GetConnection();
        }

        protected override async void OnAppearing()
        {
            await connection.CreateTableAsync<PersonInfo>();
            if (await connection.Table<PersonInfo>().CountAsync() == 0)
            {
                var name = Application.Current.Properties["Username"].ToString();
                Username.Text = name;
                OldUsername.Text = name;
                ImgUser.Source = ImageSource.FromResource("APPMOBLIE.Images.userpic.png");

            }
            base.OnAppearing();
        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            var mediaOptions = new PickMediaOptions()
            {
                PhotoSize = PhotoSize.Medium
            };
            var SelectedImageFile = await CrossMedia.Current.PickPhotoAsync(mediaOptions);
            ImgUser.Source = ImageSource.FromStream(() => SelectedImageFile.GetStream());

        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            string action = await DisplayActionSheet("Select Photo?", "Cancel", "Camera", "Gallery");

        }

        private void Cancel_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}