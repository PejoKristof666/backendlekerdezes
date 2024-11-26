using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Http;
using Newtonsoft.Json;

namespace backendlekerdezes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        async void CreateTextBlock()
        {
            Kacsak.Children.Clear();
            HttpClient client = new HttpClient();
            string url = "http://127.0.0.1:4444/doga";
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                string stringResponse = await response.Content.ReadAsStringAsync();
                List<KacsaClass> kacsaList = JsonConvert.DeserializeObject<List<KacsaClass>>(stringResponse);
                foreach (KacsaClass item in kacsaList)
                {
                    TextBlock oneBlock = new TextBlock();
                    oneBlock.Text = $"Kacsa neve: {item.name}, kacsa hossza: {item.price}";
                    Kacsak.Children.Add(oneBlock);
                }
            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }
        }

        async void AddKacsa(object s, EventArgs e)
        {
            HttpClient client = new HttpClient();
            string url = "http://127.1.1.1:4444/doga";

            try
            {
                var jsonObject = new
                {
                    name = nev.Text,
                    price = hossz.Text
                };

                string jsonData = JsonConvert.SerializeObject(jsonObject);
                StringContent data = new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(url, data);
                response.EnsureSuccessStatusCode();
                CreateTextBlock();
            }
            catch (Exception error)
            {

                MessageBox.Show(error.Message);
            }

            

            //MessageBox.Show($"Kacsa neve: {nev.Text}, Kacsa hossza: {hossz.Text}cm");
        }
    }
}
