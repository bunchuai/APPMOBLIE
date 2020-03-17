ฟกาusing System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace APPMOBLIE
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
        }

        private async void button_SignIn(object sender, EventArgs e)
        {
            var Username = "admin";
            var Password = "1234";
            if (this.Email.Text == Username && this.Password.Text == Password) 
            {
                await Navigation.PushAsync(new MainPage());
            }
            else
            {
                await DisplayAlert("Warning","Username or password invalit.","OK");
            }
        }

        private void Button_Reset(object sender, EventArgs e)
        {
            Email.Text = string.Empty;
            Password.Text = string.Empty;
        }
    }
}