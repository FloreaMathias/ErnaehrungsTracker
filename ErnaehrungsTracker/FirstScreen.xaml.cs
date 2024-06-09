using System;
using System.Windows;
using ErnaehrungsTracker.Models;

namespace ErnaehrungsTracker
{
    public partial class FirstScreen : Window
    {
        public double goalWeight;
        public double startWeight;
        public double currentWeight;
        public string inputName;
        public DateTime startDate;
        public DateTime goalDate;

        public FirstScreen()
        {
            InitializeComponent();
            startDate = DateTime.Now;

        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(GoalWeightTextBox.Text, out double goalWeightValue))
            {
                goalWeight = goalWeightValue;
            }
            else
            {
                MessageBox.Show("Bitte gib eine gültige Zahl für das Zielgewicht ein.");
                return;
            }
            inputName = InputNameTextBox.Text;
            if (string.IsNullOrWhiteSpace(inputName))
            {
                MessageBox.Show("Bitte gib einen Namen ein.");
                return;
            }
            if (double.TryParse(CurrentWeightTextBox.Text, out double currentWeightValue))
            {
                currentWeight = currentWeightValue;
            }
            else
            {
                MessageBox.Show("Bitte gib eine gültige Zahl für das aktuelle Gewicht ein.");
                return;
            }
            if (GoalDatePicker.SelectedDate == null)
            {
                MessageBox.Show("Bitte wähle ein Ziel-Datum aus.");
                return;
            }
            else
            {
                goalDate = GoalDatePicker.SelectedDate.Value;

                if (goalDate < DateTime.Now.Date)
                {
                    MessageBox.Show("Das Ziel-Datum muss in der Zukunft liegen.");
                    return;
                }
            }
            inputName = InputNameTextBox.Text;
            startDate = GoalDatePicker.SelectedDate ?? DateTime.Now;
            goalDate = GoalDatePicker.SelectedDate ?? DateTime.Now;
            DialogResult = true;
        }



    }
}
