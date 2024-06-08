using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace ErnaehrungsTracker
{
    /// <summary>
    /// Interaktionslogik für Breakfast.xaml
    /// </summary>
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
                { "Oatmeal with Berries", 220 },
                { "Pancakes with Butter", 300 },
                { "Avocado Toast", 250 },
                { "Smoothie Bowl", 180 },
                { "Bagel with Cream Cheese", 320 },
                { "Greek Yogurt Parfait", 200 },
                { "Bacon and Eggs", 350 },
                { "Cereal with Milk", 220 },
                { "Breakfast Burrito", 280 },
                { "Vegetable Omelette", 230 },
                { "Sausage and Hash Browns", 400 },
                { "Croissant with Jam", 260 },
                { "Muffins", 180 },
                { "Quinoa Porridge", 240 },
                { "Fried Rice with Egg", 300 },
                { "Breakfast Quesadilla", 320 },
                { "English Breakfast", 450 },
                { "Toasted Sandwich", 280 },
                { "Frittata", 220 },
                { "Crepes with Nutella", 350 }
            };

            mealCalories = new List<int>();
            savedEntries = new List<string>();

            LoadSavedEntries();

            foreach (var meal in meals)
            {
                OurMelasComboBox.Items.Add(meal.Key);
            }

            OurMelasComboBox.SelectionChanged += OurMelasComboBox_SelectionChanged;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (OurMelasComboBox.SelectedItem != null)
            {
                string selectedMeal = OurMelasComboBox.SelectedItem.ToString();
                AddMealToList(selectedMeal, meals[selectedMeal]);
                OurMelasComboBox.SelectedIndex = -1;
            }
            else
            {
                string otherMealName = OtherMealTextBox.Text.Trim();
                string otherKcalText = OtherKclaTexBox.Text.Trim();

                if (!string.IsNullOrWhiteSpace(otherMealName) && !string.IsNullOrWhiteSpace(otherKcalText) && int.TryParse(otherKcalText, out int otherKcal))
                {
                    AddMealToList(otherMealName, otherKcal);
                    OtherMealTextBox.Clear();
                    OtherKclaTexBox.Clear();
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
            ListBox.Items.Add(entry);
            savedEntries.Add(entry);
            SaveEntriesToFile();
        }

        private void RemoveButtonClick(object sender, RoutedEventArgs e)
        {
            if (ListBox.SelectedItem != null)
            {
                string selectedEntry = ListBox.SelectedItem.ToString();
                ListBox.Items.Remove(selectedEntry);
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
            if (File.Exists(savedEntriesFilePath))
            {
                savedEntries.AddRange(File.ReadAllLines(savedEntriesFilePath));
                foreach (var entry in savedEntries)
                {
                    ListBox.Items.Add(entry);
                }
            }
        }

        private void SaveEntriesToFile()
        {
            File.WriteAllLines(savedEntriesFilePath, savedEntries);
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public int GetTotalCalories()
        {
            int totalCalories = 0;
            foreach (int kcal in mealCalories)
            {
                totalCalories += kcal;
            }
            return totalCalories;
        }

        private void OurMelasComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (OurMelasComboBox.SelectedItem != null)
            {
                OtherMealTextBox.Clear();
                OtherKclaTexBox.Clear();
                OtherMealTextBox.IsEnabled = false;
                OtherKclaTexBox.IsEnabled = false;
            }
            else
            {
                OtherMealTextBox.IsEnabled = true;
                OtherKclaTexBox.IsEnabled = true;
            }
        }
    }
}
