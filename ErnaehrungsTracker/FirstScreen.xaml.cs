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
            if(int.TryParse(GoalWeightTextBox.Text, out int goalWeightValue))
            {
                goalWeight = goalWeightValue;
            }
            else
            {
                MessageBox.Show("Bitte gib eine gültige Zahl für das Zielgewicht ein.");
                return;
            }

            if(int.TryParse(CurrentWeightTextBox.Text, out int currentWeightValue))
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

            this.Close();
        }
    }
}
