namespace idojaras_arpas
{
    public class Item
    {
        public string Name { get; set; }
        public int Temperature { get; set; }
        public int Humidity { get; set; }
        public int WindSpeed { get; set; }
        public Item(string name, int temp, int humidity, int windSpeed)
        {
            Name = name;
            Temperature = temp;
            Humidity = humidity;
            WindSpeed = windSpeed;
        }
    }
}