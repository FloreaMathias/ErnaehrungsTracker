using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace ErnaehrungsTracker
{
    /// <summary>
    /// Interaktionslogik für Snacks.xaml
    /// </summary>
    public partial class Snacks : Window
    {
        private Dictionary<string, int> meals;
        private List<int> mealCalories;
        private List<string> savedEntries;

        private string savedEntriesFilePath = "savedEntries.txt";

        public Snacks()
        {
            InitializeComponent();

            meals = new Dictionary<string, int>
            {
                { "Greek Yogurt with Berries", 150 },
                { "Apple Slices with Almond Butter", 180 },
                { "Carrot Sticks with Hummus", 120 },
                { "String Cheese with Whole Grain Crackers", 200 },
                { "Trail Mix (Nuts, Seeds, Dried Fruit)", 250 },
                { "Rice Cakes with Peanut Butter", 160 },
                { "Hard-Boiled Eggs", 70 },
                { "Cottage Cheese with Pineapple", 130 },
                { "Mixed Nuts (Almonds, Walnuts, Cashews)", 200 },
                { "Popcorn (Air-Popped)", 100 },
                { "Edamame", 120 },
                { "Whole Grain Toast with Avocado", 180 },
                { "Granola Bar (Low-Sugar)", 150 },
                { "Frozen Grapes", 80 },
                { "Chocolate-Covered Strawberries", 120 },
                { "Celery Sticks with Peanut Butter", 140 },
                { "Sliced Cucumber with Cream Cheese", 100 },
                { "Protein Shake", 200 },
                { "Rice Crackers with Salsa", 160 },
                { "Pita Chips with Hummus", 180 },
                { "Dried Seaweed Snacks", 50 },
                { "Pumpkin Seeds", 160 },
                { "Smoothie (Fruit, Spinach, Protein Powder)", 250 },
                { "Cherry Tomatoes with Mozzarella Cheese", 150 },
                { "Chocolate Milk (Low-Fat)", 180 },
                { "Sliced Bell Peppers with Guacamole", 160 },
                { "Whole Grain Crackers with Cottage Cheese", 170 },
                { "Roasted Chickpeas", 140 }
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
        
                UpdateCalories();
            }
            else
            {
                MessageBox.Show("Bitte wählen Sie eine Mahlzeit zum Entfernen aus der Liste aus.");
            }
        }

        private void UpdateCalories()
        {
            int totalCalories = GetTotalCalories();
    
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
    
            if (mainWindow != null)
            {
                mainWindow.snacksTotalCalories = totalCalories;
                mainWindow.SnacksKcal.Text = $"{totalCalories} kcal";
        
                mainWindow.Calc_kg_to_kcal();
            }
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
