using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace ErnaehrungsTracker
{
    /// <summary>
    /// Interaktionslogik für Dinner.xaml
    /// </summary>
    public partial class Dinner : Window
    {
        private Dictionary<string, int> meals;
        private List<int> mealCalories;
        private List<string> savedEntries;

        private string savedEntriesFilePath = "savedEntries.txt";

        public Dinner()
        {
            InitializeComponent();

            meals = new Dictionary<string, int>
            {
                { "Grilled Steak with Steamed Vegetables", 450 },
                { "Vegetarian Lasagna", 380 },
                { "Teriyaki Tofu with Brown Rice", 320 },
                { "Roast Beef with Mashed Potatoes", 400 },
                { "Lemon Herb Chicken with Couscous", 350 },
                { "Eggplant Parmesan", 380 },
                { "Stuffed Bell Peppers with Quinoa", 330 },
                { "Spaghetti Carbonara", 420 },
                { "Lentil Soup with Whole Grain Bread", 300 },
                { "Cauliflower Fried Rice with Shrimp", 350 },
                { "Barbecue Pork Ribs with Coleslaw", 450 },
                { "Sesame Ginger Glazed Salmon", 380 },
                { "Ratatouille", 320 },
                { "Beef Stroganoff with Egg Noodles", 420 },
                { "Baked Chicken Parmesan", 360 },
                { "Hawaiian Grilled Chicken with Pineapple", 380 },
                { "Vegetable Curry with Naan Bread", 340 },
                { "Sweet and Sour Tofu with Rice", 330 },
                { "Beef Tacos with Guacamole", 400 },
                { "Salisbury Steak with Garlic Mashed Potatoes", 430 }
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
