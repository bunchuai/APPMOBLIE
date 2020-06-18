using APPMOBLIE.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace APPMOBLIE
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class ListProductType : ContentPage
    {
        readonly ObservableCollection<ProductTypeViewModel> list = new ObservableCollection<ProductTypeViewModel>();

        public ListProductType()
        {
            InitializeComponent();
        }

        protected override  async void OnAppearing()
        {
            using (HttpClient client = new HttpClient())
            {
                var CompanyId = Application.Current.Properties["CompanyId"];
                string Url = "http://203.151.166.97/api/ProductsType/GetProductType?CompanyId="+ CompanyId;
                client.BaseAddress = new Uri(Url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "oLQKZ-tbA58nvbw7PuZhSsy3sr_H28YB3U3XGbpEc5pGIpMrB1nKNjpS_mRhDiFt2QOBZw3IntJ3dmrozZKsw6hd2VuLvoS8-0HxMCVsMUbZ6QZD782Ig1rfFFWPJ13qDq-cMoUgE2t-PdFEp_85aqa8crtVD6aRwntMPjDOgOriFBbzCYjeXyQ3JECl4pOZGd2KYhpCM7n4hXjfCA0t2YeQyvbuId1-e-qhltjEzCkRk7uffgtbwC2KAImsw7jrBYFfxeu1DCRRYdi2AsSZVyBHk0pAqcekzv5jlxLaK2Z-5hFVN0EzSA86Z2MkAq_vXPnJMq0ZrlGfZG6l-hJYb7NjGZCKD44euOf4l-dGQqi40wd8oIhacT1WIrr2RoSAxQn3t1TLDU2TNbgd_pW89JAHd9fmF9k-aZt9tCJuFQs-sW7eJQ1spYqQWHEbKYFbf2Aih5ZBDrIbLeh4hRRFOd_zYZgQKqIZ1tpZ_82UwYUG8FyPn9ZexVzr4t4At4cP");
                HttpResponseMessage response = await client.GetAsync(Url);
                if (response.IsSuccessStatusCode)
                {
                    var ResponseData = await response.Content.ReadAsStringAsync();
                    var Result = JsonConvert.DeserializeObject<List<ProductTypeViewModel>>(ResponseData);
                    ListViewType.ItemsSource = Result;
                }
                else
                {
                    await DisplayAlert("แจ้งเตือน!", "กรุณาตรวจสอบการทำรายการอีกครั้ง", "ตกลง");
                }
            }
        }

        private async void Delete_Clicked(object sender, EventArgs e)
        {
            var model = ((MenuItem)sender);
            using (HttpClient client = new HttpClient())
            {
                string Url = "http://203.151.166.97/api/ProductsType/DeleteProductType?ProductType=" + model.CommandParameter;
                client.BaseAddress = new Uri(Url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "oLQKZ-tbA58nvbw7PuZhSsy3sr_H28YB3U3XGbpEc5pGIpMrB1nKNjpS_mRhDiFt2QOBZw3IntJ3dmrozZKsw6hd2VuLvoS8-0HxMCVsMUbZ6QZD782Ig1rfFFWPJ13qDq-cMoUgE2t-PdFEp_85aqa8crtVD6aRwntMPjDOgOriFBbzCYjeXyQ3JECl4pOZGd2KYhpCM7n4hXjfCA0t2YeQyvbuId1-e-qhltjEzCkRk7uffgtbwC2KAImsw7jrBYFfxeu1DCRRYdi2AsSZVyBHk0pAqcekzv5jlxLaK2Z-5hFVN0EzSA86Z2MkAq_vXPnJMq0ZrlGfZG6l-hJYb7NjGZCKD44euOf4l-dGQqi40wd8oIhacT1WIrr2RoSAxQn3t1TLDU2TNbgd_pW89JAHd9fmF9k-aZt9tCJuFQs-sW7eJQ1spYqQWHEbKYFbf2Aih5ZBDrIbLeh4hRRFOd_zYZgQKqIZ1tpZ_82UwYUG8FyPn9ZexVzr4t4At4cP");
                HttpResponseMessage response = await client.DeleteAsync(Url);
                if (response.IsSuccessStatusCode)
                {
                    var ResponseData = await response.Content.ReadAsStringAsync();
                    var Result = JsonConvert.DeserializeObject<RespondMessage>(ResponseData);
                    if (Result.valid == true)
                    {
                        await DisplayAlert("สำเร็จ", "ดำเนินการเสร็จสิ้น", "ตกลง");
                        await Navigation.PushAsync(new ListProductType());
                    }
                    else
                    {
                        await DisplayAlert("แจ้งเตือน", "กรุณาทำรรายการใหม่อีกครั้ง", "ตกลง");
                    }
                }
            }
        }

        private async void Edit_Clicked(object sender, EventArgs e)
        {
            var model = ((MenuItem)sender);

            await Navigation.PushAsync(new AddProductType());
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new AddProductType());

        }

        private class RespondMessage
        {
            public bool valid { get; set; }
            public string Message { get; set; }
        }
        public class ProductTypeViewModel
        {
            public int ProductType { get; set; }
            public string TypeName { get; set; }
        }
    }
}