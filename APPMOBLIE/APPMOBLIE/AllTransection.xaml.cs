using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace APPMOBLIE
{
    public partial class AllTransection : ContentPage
    {
        public AllTransection()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            Username.Text = Application.Current.Properties["Username"].ToString();
            base.OnAppearing();
        }
    }
}