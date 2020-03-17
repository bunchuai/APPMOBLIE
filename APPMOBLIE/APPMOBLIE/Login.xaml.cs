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
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button_SignIn(object sender, EventArgs e)
        {

        }

        private void Button_Reset(object sender, EventArgs e)
        {
            Email.Text = string.Empty;
            Password.Text = string.Empty;
        }
    }
}