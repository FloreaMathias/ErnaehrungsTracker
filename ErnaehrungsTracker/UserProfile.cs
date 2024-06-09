using System;

namespace ErnaehrungsTracker.Models
{
    public class UserProfile
    {
        public string Name { get; set; }
        public double CurrentWeight { get; set; }
        public double GoalWeight { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime GoalDate { get; set; }
        public double StartWeight { get; set; }

        public UserProfile(string name, double currentWeight, double goalWeight, DateTime startDate, DateTime goalDate)
        {
            Name = name;
            CurrentWeight = currentWeight;
            GoalWeight = goalWeight;
            StartDate = startDate;
            GoalDate = goalDate;
            StartWeight = currentWeight; 
        }

        public string Serialize()
        {
            return $"{Name},{CurrentWeight},{GoalWeight},{StartDate:o},{GoalDate:o},{StartWeight}";
        }

        public static UserProfile Deserialize(string data)
        {
            var parts = data.Split(',');
            return new UserProfile(
                parts[0],
                double.Parse(parts[1]),
                double.Parse(parts[2]),
                DateTime.Parse(parts[3]),
                DateTime.Parse(parts[4])
            )
            {
                StartWeight = double.Parse(parts[5])
            };
        }
    }
}