using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace idojaras_arpas
{
    // Az időjárás osztály, amely tartalmazza az időjárási adatokat
    public class WeatherData : INotifyPropertyChanged
    {
        private string cityName;
        public string CityName
        {
            get { return cityName; }
            set
            {
                cityName = value;
                OnPropertyChanged("CityName");
            }
        }

        private double temperature;
        public double Temperature
        {
            get { return temperature; }
            set
            {
                temperature = value;
                OnPropertyChanged("Temperature");
            }
        }

        private int humidity;
        public int Humidity
        {
            get { return humidity; }
            set
            {
                humidity = value;
                OnPropertyChanged("Humidity");
            }
        }

        private double windSpeed;
        public double WindSpeed
        {
            get { return windSpeed; }
            set
            {
                windSpeed = value;
                OnPropertyChanged("WindSpeed");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    // Az alkalmazás fő ViewModel osztálya
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<WeatherData> Cities { get; set; }

        private WeatherData selectedCity;
        public WeatherData SelectedCity
        {
            get { return selectedCity; }
            set
            {
                selectedCity = value;
                OnPropertyChanged("SelectedCity");
            }
        }

        private string newCityName;
        public string NewCityName
        {
            get { return newCityName; }
            set
            {
                newCityName = value;
                OnPropertyChanged("NewCityName");
            }
        }

        private double newTemperature;
        public double NewTemperature
        {
            get { return newTemperature; }
            set
            {
                newTemperature = value;
                OnPropertyChanged("NewTemperature");
            }
        }

        private int newHumidity;
        public int NewHumidity
        {
            get { return newHumidity; }
            set
            {
                newHumidity = value;
                OnPropertyChanged("NewHumidity");
            }
        }

        private double newWindSpeed;
        public double NewWindSpeed
        {
            get { return newWindSpeed; }
            set
            {
                newWindSpeed = value;
                OnPropertyChanged("NewWindSpeed");
            }
        }

        public MainViewModel()
        {
            Cities = new ObservableCollection<WeatherData>();
        }

        public void AddCity()
        {
            Cities.Add(new WeatherData
            {
                CityName = NewCityName,
                Temperature = NewTemperature,
                Humidity = NewHumidity,
                WindSpeed = NewWindSpeed
            });
        }

        public void RemoveCity()
        {
            if (SelectedCity != null)
            {
                Cities.Remove(SelectedCity);
                SelectedCity = null;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public partial class MainWindow : Window
    {
        public MainViewModel ViewModel { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new MainViewModel();
            DataContext = ViewModel;
        }

        private void AddCityButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.AddCity();
        }

        private void RemoveCityButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.RemoveCity();
        }
    }
}
