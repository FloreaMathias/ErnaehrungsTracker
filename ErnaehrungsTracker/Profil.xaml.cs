using ErnaehrungsTracker.Models;
using System;
using System.Windows;

namespace ErnaehrungsTracker
{
    public partial class Profil : Window
    {
        public UserProfile UserProfile { get; private set; }

        public Profil(UserProfile userProfile)
        {
            InitializeComponent();
            UserProfile = userProfile;
            UpdateProfileFields();
            UpdateRemainingWeight();
        }
        
        private void UpdateProfileFields()
        {
            ProfilWeightTextBoxINPUT.Text = UserProfile.CurrentWeight.ToString();
            CurrentextBox.Text = UserProfile.CurrentWeight.ToString();
            GoalWeightTextBox.Text = UserProfile.GoalWeight.ToString();
            ProfileName.Content = UserProfile.Name;
            StartWeighttextBox.Text = UserProfile.CurrentWeight.ToString(); //wenn man weight added sött des nd +1 go also es sött uf dia standard eingabe blieba 
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
            MainWindow main = new MainWindow(UserProfile);
            main.Save(filename);
        }

        private void ProfilLoadButton_Click(object sender, RoutedEventArgs e)
        {
            string filename = "profile.txt"; 
            MainWindow main = new MainWindow();
            main.Load(filename);
        }        
    }
}
