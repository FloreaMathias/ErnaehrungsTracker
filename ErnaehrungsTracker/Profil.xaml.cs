using System;
using System.Windows;

namespace ErnaehrungsTracker
{
    public partial class Profil : Window
    {
        public double currentWeight;
        public double goalWeight;
        public string profileName;
        public DateTime profileDate;
        public Profil(double weight, double goal, string name, DateTime date)
            {
                InitializeComponent();
                currentWeight = weight;
                goalWeight = goal;
                profileName = name;
                profileDate = date;
                UpdateProfileFields();
                UpdateRemainingWeight();
            }

            private void UpdateProfileFields()
            {
                CurrentextBox.Text = currentWeight.ToString();
                GoalWeightTextBox.Text = goalWeight.ToString();
                ProfileName.Text = profileName;
                ProfilDate.Content = profileDate.ToString("dd.MM.yyyy");
            }

            private void UpdateRemainingWeight()
            {
                double remainingWeight = goalWeight - currentWeight;
                kgremain.Text = $"{remainingWeight} kg remains";
            }

            private void BackButton_Click(object sender, RoutedEventArgs e)
            {
                MainWindow main = new MainWindow();
                main.Show();
                Close();
            }

            private void ProfilLoadButton_Click(object sender, RoutedEventArgs e)
            {
                UpdateProfileFields();
                UpdateRemainingWeight();
            }

            private void ProfilSaveButton_Click(object sender, RoutedEventArgs e)
            {
              
            }

        private double weightCounter = 0;
        private void addWeight_Click(object sender, RoutedEventArgs e)
        {
            if(double.TryParse(ProfilWeightTextBoxINPUT.Text, out double weightToAdd))
            {
                weightCounter += weightToAdd;
                kgremain.Text = weightCounter.ToString("0.0");
            }
            else
            {
                MessageBox.Show("Bitte geben Sie eine gültige Zahl für die Wassermenge ein.");
            }
        }

        private void removeWeight_Click(object sender, RoutedEventArgs e)
        {

        }
    }
    }