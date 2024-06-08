namespace ErnaehrungsTracker.Models
{
    public class WaterCounter
    {
        public double TotalWater { get; private set; }

        public void AddWater(double amount)
        {
            TotalWater += amount;
        }

        public void RemoveWater(double amount)
        {
            if (amount <= TotalWater)
            {
                TotalWater -= amount;
            }
            else
            {
                throw new InvalidOperationException("Die eingegebene Menge ist größer als die aktuelle Wassermenge.");
            }
        }
    }
}
