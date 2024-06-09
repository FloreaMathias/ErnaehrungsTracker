namespace ErnaehrungsTracker.Models
{
    public class StepsCounter
    {
        public double TotalSteps { get; private set; }

        public void AddSteps(double amount)
        {
            TotalSteps += amount;
        }

        public void RemoveSteps(double amount)
        {
            if (TotalSteps >= amount)
            {
                TotalSteps -= amount;
            }
            else
            {
                throw new InvalidOperationException("Die eingegebene Menge ist größer als die aktuellen Schritte.");
            }
        }
    }
}