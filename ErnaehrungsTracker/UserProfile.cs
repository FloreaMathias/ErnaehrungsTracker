using System;

namespace ErnaehrungsTracker.Models
{
    public class UserProfile
    {
        public string Name { get; set; }
        public double CurrentWeight { get; set; }
        public double StartWeight { get; set; }
        public double GoalWeight { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime GoalDate { get; set; }


        public UserProfile(string name, double startWeight , double currentWeight, double goalWeight, DateTime startDate, DateTime goalDate)
        {
            Name = name;
            StartWeight = startWeight;
            CurrentWeight = currentWeight;
            GoalWeight = goalWeight;
            StartDate = startDate;
            GoalDate = goalDate;

        }

        public string Serialize()
        {
            return $"{Name};{StartWeight};{CurrentWeight};{GoalWeight};{StartDate:O};{GoalDate:0}";
        }

        public static UserProfile Deserialize(string serialized)
        {
            string[] parts = serialized.Split(';');
            if (parts.Length == 6)
            {
                string name = parts[0];
                double startWeight = double.Parse(parts[1]);
                double currentWeight = double.Parse(parts[2]);
                double goalWeight = double.Parse(parts[3]);
                DateTime startDate = DateTime.Parse(parts[4]);
                DateTime goalDate = DateTime.Parse(parts[5]);

                return new UserProfile(name, startWeight, currentWeight, goalWeight, startDate, goalDate);
            }
            else
            {
                throw new Exception("Die Datei hat ein ungültiges Format.");
            }
        }
    }
}
