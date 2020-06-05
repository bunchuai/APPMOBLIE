using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace APPMOBLIE
{

    public partial class HomePage : TabbedPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
         //   Username.Text = Application.Current.Properties["Username"].ToString();
            base.OnAppearing();

        }

        protected override bool OnBackButtonPressed()
        {
            return false;
        }

        private async void Button_Checkproduct(object sender, EventArgs e)
        {
            var Page = new Checkproduct();
            await Navigation.PushAsync(Page);
        }

    }
}