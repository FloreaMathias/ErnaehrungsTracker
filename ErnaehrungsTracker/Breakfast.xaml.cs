using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace ErnaehrungsTracker
{
    public partial class Breakfast : Window
    {
        private Dictionary<string, int> meals;
        private List<int> mealCalories;
        private List<string> savedEntries;

        private string savedEntriesFilePath = "savedEntries.txt";

        public Breakfast()
        {
            InitializeComponent();

            meals = new Dictionary<string, int>
            {
                { "Oatmeal", 150 },
                { "Scrambled Eggs", 200 },
                { "Fruit Salad", 100 }
            };

            mealCalories = new List<int>();
            savedEntries = new List<string>();

            LoadSavedEntries(); 

            foreach(var meal in meals)
            {
                OurMelasComboBoxBreakfast.Items.Add(meal.Key);
            }

            OurMelasComboBoxBreakfast.SelectionChanged += OurMelasComboBoxBreakfast_SelectionChanged;
        }

        private void AddButtonBreakfast_Click(object sender, RoutedEventArgs e)
        {
            if(OurMelasComboBoxBreakfast.SelectedItem != null)
            {
                string selectedMeal = OurMelasComboBoxBreakfast.SelectedItem.ToString();
                AddMealToList(selectedMeal, meals[selectedMeal]);
                OurMelasComboBoxBreakfast.SelectedIndex = -1;
            }
            else
            {
                string otherMealName = OtherMealTextBoxBreakfast.Text.Trim();
                string otherKcalText = OtherKclaTexBoxBreakfast.Text.Trim();

                if(!string.IsNullOrWhiteSpace(otherMealName) && !string.IsNullOrWhiteSpace(otherKcalText) && int.TryParse(otherKcalText, out int otherKcal))
                {
                    AddMealToList(otherMealName, otherKcal);
                    OtherMealTextBoxBreakfast.Clear();
                    OtherKclaTexBoxBreakfast.Clear();
                }
                else
                {
                    MessageBox.Show("Bitte wählen Sie entweder eine Mahlzeit aus der Liste oder geben Sie den Namen und die Kalorien einer eigenen Mahlzeit ein.");
                }
            }
        }

        private void AddMealToList(string mealName, int kcal)
        {
            mealCalories.Add(kcal);
            string entry = $"{mealName}: {kcal} kcal";
            BreakfastListBox.Items.Add(entry);
            savedEntries.Add(entry);
            SaveEntriesToFile();
        }

        private void RemoveButtonBreakfast_Click(object sender, RoutedEventArgs e)
        {
            if(BreakfastListBox.SelectedItem != null)
            {
                string selectedEntry = BreakfastListBox.SelectedItem.ToString();
                BreakfastListBox.Items.Remove(selectedEntry);
                savedEntries.Remove(selectedEntry);
                SaveEntriesToFile(); 
            }
            else
            {
                MessageBox.Show("Bitte wählen Sie eine Mahlzeit zum Entfernen aus der Liste aus.");
            }
        }

        private void LoadSavedEntries()
        {
            if(File.Exists(savedEntriesFilePath))
            {
                savedEntries.AddRange(File.ReadAllLines(savedEntriesFilePath));
                foreach(var entry in savedEntries)
                {
                    BreakfastListBox.Items.Add(entry);
                }
            }
        }

        private void SaveEntriesToFile()
        {
            File.WriteAllLines(savedEntriesFilePath, savedEntries);
        }
        private void ExitButtonBreakfast_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public int GetTotalCalories()
        {
            int totalCalories = 0;
            foreach(int kcal in mealCalories)
            {
                totalCalories += kcal;
            }
            return totalCalories;
        }
        private void OurMelasComboBoxBreakfast_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if(OurMelasComboBoxBreakfast.SelectedItem != null)
            {
                OtherMealTextBoxBreakfast.Clear();
                OtherKclaTexBoxBreakfast.Clear();
                OtherMealTextBoxBreakfast.IsEnabled = false;
                OtherKclaTexBoxBreakfast.IsEnabled = false;
            }
            else
            {
                OtherMealTextBoxBreakfast.IsEnabled = true;
                OtherKclaTexBoxBreakfast.IsEnabled = true;
            }
        }
    }
}
