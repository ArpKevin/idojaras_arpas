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

            // Sample data
            Items = new ObservableCollection<Item>
            {
                new Item { Name = "Item 1", Attribute1 = "Attribute 1 Value", Attribute2 = "Attribute 2 Value", Attribute3 = "Attribute 3 Value" },
                new Item { Name = "Item 2", Attribute1 = "Attribute 4 Value", Attribute2 = "Attribute 2 Value", Attribute3 = "Attribute 3 Value" },
                new Item { Name = "Item 3", Attribute1 = "Attribute 5 Value", Attribute2 = "Attribute 2 Value", Attribute3 = "Attribute 3 Value" }
            };

            listBox.ItemsSource = Items;
            listBox.SelectionChanged += ListBox_SelectionChanged;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBox.SelectedItem != null)
            {
                UpdateAttributeText();
            }
        }

        private void UpdateAttributeText()
        {
            if (listBox.SelectedItem != null)
            {
                Item selectedItem = (Item)listBox.SelectedItem;
                attributeTextBlock.Text = $"Attribute 1: {selectedItem.Attribute1}\n" +
                                          $"Attribute 2: {selectedItem.Attribute2}\n" +
                                          $"Attribute 3: {selectedItem.Attribute3}";
            }
        }
    }

    public class Item
    {
        public string Name { get; set; }
        public string Attribute1 { get; set; }
        public string Attribute2 { get; set; }
        public string Attribute3 { get; set; }
    }
}