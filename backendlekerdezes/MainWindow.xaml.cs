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
            CreateTextBlock();
        }

        async void CreateTextBlock()
        {
            KacsaPanel.Children.Clear();
            HttpClient client = new HttpClient();
            string url = "http://127.0.0.1:4444/doga";
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string stringResponse = await response.Content.ReadAsStringAsync();
                List<KacsaClass> kacsaList = JsonConvert.DeserializeObject<List<KacsaClass>>(stringResponse);
                foreach (KacsaClass item in kacsaList)
                {
                    Border oneBorder = new Border();

                    KacsaPanel.Children.Add(oneBorder);
                    
                    Grid oneGrid = new Grid();

                    oneBorder.Child = oneGrid;

                    RowDefinition firstRow = new RowDefinition();
                    RowDefinition secondRow = new RowDefinition();
                    RowDefinition thirdRow = new RowDefinition();

                    oneGrid.RowDefinitions.Add(firstRow);
                    oneGrid.RowDefinitions.Add(secondRow);
                    oneGrid.RowDefinitions.Add(thirdRow);

                    TextBlock NameTextBlock = new TextBlock();
                    TextBlock PriceTextBlock = new TextBlock();

                    Button SellButton = new Button();


                    oneGrid.Children.Add(NameTextBlock);
                    oneGrid.Children.Add(PriceTextBlock);

                    oneGrid.Children.Add(SellButton);

                    Grid.SetRow(PriceTextBlock, 1);
                    Grid.SetRow(SellButton, 2);

                    NameTextBlock.Text = $"Név: {item.name}";
                    PriceTextBlock.Text = $"Ára: {item.price}";
                    SellButton.Content = "Eladás";

                    oneBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("gray"));
                    oneBorder.Margin = new Thickness(10);
                    oneBorder.CornerRadius = new CornerRadius(20);
                    oneBorder.Padding = new Thickness(10);

                    NameTextBlock.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                    PriceTextBlock.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));

                    //oneBlock.Text = $"Kacsa neve: {item.name}, kacsa hossza: {item.price}";
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
                    name = KacsaNameTextBox.Text,
                    price = KacsaPriceTextBox.Text
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
