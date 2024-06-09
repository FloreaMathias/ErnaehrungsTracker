using System;
using ErnaehrungsTracker.Models;

namespace ErnaehrungsTracker
{
    public class CalorieCalculator
    {
        public int TotalFoodCalories { get; private set; }
        private const double ConversionFactor = 7716.179176;

        public double CalculateGoalWeightKcal(double goalWeight)
        {
            return Math.Round(goalWeight * ConversionFactor);
        }

        public double CalculateCurrentWeightKcal(double currentWeight)
        {
            return Math.Round(currentWeight * ConversionFactor);
        }

        public double CalculateDailyCaloriesGoal(UserProfile userProfile)
        {
            double goalWeightKcal = CalculateGoalWeightKcal(userProfile.GoalWeight);
            double currentWeightKcal = CalculateCurrentWeightKcal(userProfile.CurrentWeight);

            TimeSpan timeSpan = userProfile.StartDate - DateTime.Now;
            int daysDifference = Math.Abs(timeSpan.Days);

            double weightDifferenceKcal = Math.Abs(goalWeightKcal - currentWeightKcal);

            return weightDifferenceKcal / daysDifference;
        }

        public int CalculateRemainingCalories(double dailyCaloriesGoal, double trainingCalories)
        {
            return (int)Math.Round(dailyCaloriesGoal - TotalFoodCalories + trainingCalories);
        }

        public int UpdateTotalFoodCalories(int breakfastCalories, int lunchCalories, int dinnerCalories, int snacksCalories)
        {
            return (TotalFoodCalories = breakfastCalories + lunchCalories + dinnerCalories + snacksCalories);
        }
    }
}