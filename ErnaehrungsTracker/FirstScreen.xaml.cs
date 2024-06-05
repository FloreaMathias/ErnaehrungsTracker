using System;
using System.Windows;

namespace ErnaehrungsTracker
{
    public partial class FirstScreen : Window
    {
        public double goalWeight;
        public double currentWeight;
        public string inputName;
        public DateTime startDate;

        public FirstScreen()
        {
            InitializeComponent();
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if(double.TryParse(GoalWeightTextBox.Text, out double goalWeightValue))
            {
                goalWeight = goalWeightValue;
            }
            else
            {
                MessageBox.Show("Bitte gib eine gültige Zahl für das Zielgewicht ein.");
                return;
            }

            if(double.TryParse(CurrentWeightTextBox.Text, out double currentWeightValue))
            {
                currentWeight = currentWeightValue;
            }
            else
            {
                MessageBox.Show("Bitte gib eine gültige Zahl für das aktuelle Gewicht ein.");
                return;
            }

            inputName = InputNameTextBox.Text;
            startDate = StartDatePicker.SelectedDate ?? DateTime.Now;

            double dailyCalories = CalculateDailyCalories(currentWeight, goalWeight, startDate);

            MainWindow mainWindow = new MainWindow(goalWeight, currentWeight, inputName, dailyCalories);
            mainWindow.Show();
            this.Close();
        }

        private double CalculateDailyCalories(double currentWeight, double goalWeight, DateTime startDate)
        {
            double weightDifference = currentWeight - goalWeight;
            double daysToGoal = (startDate - DateTime.Now).TotalDays;

            double totalCalorieDeficit = weightDifference * 7700;
            double dailyCalorieDeficit = totalCalorieDeficit / daysToGoal;

            double dailyCalories = 2000 - dailyCalorieDeficit;

            return dailyCalories;
        }
    }
}
