using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace idojaras_arpas
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Item> Items { get; set; }


        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            var data = File.ReadAllLines(@"..\..\..\src\data.txt");

            Items = new ObservableCollection<Item>();

            foreach (var item in data)
            {
                var d = item.Split(";");

                string name = d[1];
                int temp = int.Parse(d[2]);
                int humidity = int.Parse(d[3]);
                int windSpeed = int.Parse(d[4]);

                Items.Add(new Item(name, temp, humidity, windSpeed));
            }

            varosok.SelectionChanged += varosok_SelectionChanged;
            btn_torol.IsEnabled = false;


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
                textbox_varos.Text.Trim(),
                textbox_homerseklet.Text.Trim(),
                textbox_paratartalom.Text.Trim(),
                textbox_szelsebesseg.Text.Trim()
            );

            if (!string.IsNullOrWhiteSpace(varos) &&
                !string.IsNullOrWhiteSpace(homerseklet) &&
                !string.IsNullOrWhiteSpace(paratartalom) &&
                !string.IsNullOrWhiteSpace(szelsebesseg)
                )
            {
                int temperature, humidity, windSpeed;

                if (int.TryParse(homerseklet, out temperature) &&
                    int.TryParse(paratartalom, out humidity) &&
                    int.TryParse(szelsebesseg, out windSpeed))
                {
                    Items.Add(new Item(varos, temperature, humidity, windSpeed));

                    textbox_varos.Clear();
                    textbox_homerseklet.Clear();
                    textbox_paratartalom.Clear();
                    textbox_szelsebesseg.Clear();

                    if (lblError.Visibility == Visibility.Visible)
                    {
                        lblError.Visibility = Visibility.Collapsed;
                    }
                }
                else
                {
                    lblError.Visibility = Visibility.Visible;
                    lblError.Content = "A megadott adatok valamelyike hibás!";
                }
            }
            else
            {
                lblError.Visibility = Visibility.Visible;
                lblError.Content = "Hiba! Nem maradhat egyik mező sem üresen.";
            }
        }

        private void btn_torol_Click(object sender, RoutedEventArgs e)
        {
           
            Items.Remove((Item)varosok.SelectedItem); // varosok.Items.Remove((Item)varosok.SelectedItem); incompatible
            attributeTextBlock.Text = "";
        }

        private void varosok_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btn_torol.IsEnabled = varosok.SelectedItem != null;
        }
    }
}