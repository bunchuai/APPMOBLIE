using APPMOBLIE.Model;
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
            var name = Application.Current.Properties["Username"].ToString();
            Username.Text = name;
            OldUsername.Text = name;

            connection = DependencyService.Get<InterfaceSQLite>().GetConnection();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
               
        }
    }
}