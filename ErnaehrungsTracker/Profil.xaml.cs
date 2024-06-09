using ErnaehrungsTracker.Models;
using System;
using System.Windows;
using System.IO;
namespace ErnaehrungsTracker
{
    public partial class Profil : Window
    {
        public UserProfile UserProfile { get; private set; }
        private CalorieCalculator calorieCalculator = new CalorieCalculator();

        public int BreakfastTotalCalories { get; private set; }
        public int LunchTotalCalories { get; private set; }
        public int DinnerTotalCalories { get; private set; }
        public int SnacksTotalCalories { get; private set; }

        public Profil(UserProfile userProfile, int breakfastTotalCalories, int lunchTotalCalories, int dinnerTotalCalories, int snacksTotalCalories)
        {
            InitializeComponent();
            UserProfile = userProfile;
            BreakfastTotalCalories = breakfastTotalCalories;
            LunchTotalCalories = lunchTotalCalories;
            DinnerTotalCalories = dinnerTotalCalories;
            SnacksTotalCalories = snacksTotalCalories;
            UpdateProfileFields();
            UpdateRemainingWeight();
        }
        private void UpdateProfileFields()
        {
            ProfilWeightTextBoxINPUT.Text = UserProfile.CurrentWeight.ToString();
            CurrentextBox.Text = UserProfile.CurrentWeight.ToString();
            GoalWeightTextBox.Text = UserProfile.GoalWeight.ToString();
            ProfileName.Content = UserProfile.Name;
            StartWeighttextBox.Text = UserProfile.StartWeight.ToString();
            StartDate.Content = DateTime.Now.ToString("dd.MM.yyyy");

            GoalDate.Content = UserProfile.GoalDate.ToString("dd.MM.yyyy"); 
        }


        private void UpdateRemainingWeight()
        {
            double remainingWeight = UserProfile.GoalWeight - UserProfile.CurrentWeight;
            kgremain.Text = $"{remainingWeight} kg remains";
        }

        private void AddWeightButton_Click(object sender, RoutedEventArgs e)
        {
            UserProfile.CurrentWeight += 1;
            UpdateProfileFields();
            UpdateRemainingWeight();
        }

        private void RemoveWeightButton_Click(object sender, RoutedEventArgs e)
        {
            UserProfile.CurrentWeight -= 1;
            UpdateProfileFields();
            UpdateRemainingWeight();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow(UserProfile);
            main.Show();
            this.Close();
        }

        private void ProfilSaveButton_Click(object sender, RoutedEventArgs e)
        {
            string filename = "profile.txt";
            using (StreamWriter file = new StreamWriter(filename))
            {
                file.WriteLine(UserProfile.Serialize());
            }
            MessageBox.Show("Profile saved successfully.");
        }

        private void ProfilLoadButton_Click(object sender, RoutedEventArgs e)
        {
            string filename = "profile.txt";
            using (StreamReader file = new StreamReader(filename))
            {
                string line = file.ReadLine();
                UserProfile = UserProfile.Deserialize(line);
            }
            UpdateProfileFields();
            UpdateRemainingWeight();
            MessageBox.Show("Profile loaded successfully.");
        }
        
        
        /*
         * private void DrawStatisticBars(double TotalFoodCalories, double kmValue, double lValue, double ProfiledailyCaloriesGoal)
        {
            double maxWidth = 420.0;
            double foodWidth = (TotalFoodCalories / ProfiledailyCaloriesGoal) * maxWidth;
            double kmWidth = (kmValue / ProfiledailyCaloriesGoal) * maxWidth;
            double lWidth = (lValue / ProfiledailyCaloriesGoal) * maxWidth;

            foodWidth = Math.Min(foodWidth, maxWidth);
            kmWidth = Math.Min(kmWidth, maxWidth);
            lWidth = Math.Min(lWidth, maxWidth);

            Klc.Width = foodWidth;
            KM.Width = kmWidth;
            L.Width = lWidth;

            GoalTextLabel.Text = $"{(int)ProfiledailyCaloriesGoal}";
            KmTextLabel.Text = $"{kmValue}";
            LTextLabel.Text = $"{lValue}";
        }
        */
    
        
        
        
        
        
    }
}
