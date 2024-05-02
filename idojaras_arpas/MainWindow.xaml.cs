using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace idojaras_arpas
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Item> Items { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            varosok.SelectionChanged += varosok_SelectionChanged;
            btn_torol.IsEnabled = false;

            // Sample data
            Items = new ObservableCollection<Item>
            {
                new Item { Name = "New York", Temperature = "25", Humidity = "65", WindSpeed = "10" },
                new Item { Name = "London", Temperature = "18", Humidity = "70", WindSpeed = "12" },
                new Item { Name = "Tokyo", Temperature = "30", Humidity = "80", WindSpeed = "8" }
            };


            varosok.ItemsSource = Items;
            varosok.SelectionChanged += (sender, e) => { if (varosok.SelectedItem != null) UpdateAttributeText(); };
        }
        private void UpdateAttributeText()
        {
            if (varosok.SelectedItem != null)
            {
                Item selectedItem = (Item)varosok.SelectedItem;
                attributeTextBlock.Text = $"Hőmérséklet: {selectedItem.Temperature} °C\n" +
                                          $"Páratartalom: {selectedItem.Humidity} g/m^3 \n" +
                                          $"Szélsebesség: {selectedItem.WindSpeed} km/h";
            }
        }

        private void btn_hozzaad_Click(object sender, RoutedEventArgs e)
        {
            (string varos, string homerseklet, string paratartalom, string szelsebesseg) = (
                textbox_varos.Text,
                textbox_homerseklet.Text,
                textbox_paratartalom.Text,
                textbox_szelsebesseg.Text
            );

            if (!string.IsNullOrWhiteSpace(varos) &&
                !string.IsNullOrWhiteSpace(homerseklet) &&
                !string.IsNullOrWhiteSpace(paratartalom) &&
                !string.IsNullOrWhiteSpace(szelsebesseg)
                )
            {
                Items.Add(new Item { Name = varos, Temperature = homerseklet, Humidity = paratartalom, WindSpeed = szelsebesseg });
            }
        }

        private void btn_torol_Click(object sender, RoutedEventArgs e)
        {
            varosok.Items.Remove(varosok.SelectedItem);
        }

        private void varosok_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btn_torol.IsEnabled = varosok.SelectedItem != null;
        }
    }

    public class Item
    {
        public string Name { get; set; }
        public string Temperature { get; set; }
        public string Humidity { get; set; }
        public string WindSpeed { get; set; }
    }
}