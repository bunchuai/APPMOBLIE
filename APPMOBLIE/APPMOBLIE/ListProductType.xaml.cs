using APPMOBLIE.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace APPMOBLIE
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class ListProductType : ContentPage
    {
        readonly ObservableCollection<ProductType> list = new ObservableCollection<ProductType>();

        public ListProductType()

        {
            InitializeComponent();

            list.Add(new ProductType { DisplayName = "คอมพิวเตอร์" });         
            list.Add(new ProductType { DisplayName = "ฮาร์ดดิส" });
            list.Add(new ProductType { DisplayName = "List2" });
            ListViewType.ItemsSource = list;
            
           

        }

       

        private void Delete_Clicked(object sender, EventArgs e)
        {
            var model = ((MenuItem)sender);
            list.Remove((ProductType)model.CommandParameter);
        }

        private void Edit_Clicked(object sender, EventArgs e)
        {
            var menuitem = sender as MenuItem;
            if (menuitem != null)
            {
                var name = menuitem.BindingContext as string;
                DisplayAlert("Alert", "Edit " + name, "Ok");
            }
        }

        public class ProductType
        {
            public string DisplayName { get; set; }
        }

       
    }
}