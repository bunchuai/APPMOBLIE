using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace APPMOBLIE
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListLocation : ContentPage
    {
        readonly ObservableCollection<ProLocation> list = new ObservableCollection<ProLocation>();

        public ListLocation()
        {
            InitializeComponent();

            list.Add(new ProLocation { DisplayName = "ชั้น1" });
            list.Add(new ProLocation { DisplayName = "ชั้น2" });
            list.Add(new ProLocation { DisplayName = "ชั้น3" });
            ListViewLocation.ItemsSource = list;

        }

        private void Delete_Clicked(object sender, EventArgs e)
        {
            var model = ((MenuItem)sender);
            list.Remove((ProLocation)model.CommandParameter);
        }

        private async void Edit_Clicked(object sender, EventArgs e)
        {
            var model = ((MenuItem)sender);

            await Navigation.PushAsync(new EditLocation());
        }

        public class ProLocation
        {
            public string DisplayName { get; set; }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new AddLocation());

        }

       
    }
}