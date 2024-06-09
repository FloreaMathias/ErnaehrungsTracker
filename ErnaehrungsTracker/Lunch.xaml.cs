using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace ErnaehrungsTracker
{
    /// <summary>
    /// Interaktionslogik für Lunch.xaml
    /// </summary>
    public partial class Lunch : Window
    {
        private Dictionary<string, int> meals;
        private List<int> mealCalories;
        private List<string> savedEntries;

        private string savedEntriesFilePath = "savedEntries.txt";

        public Lunch()
        {
            InitializeComponent();

            meals = new Dictionary<string, int>
            {
                { "Grilled Chicken Salad", 300 },
                { "Spaghetti Bolognese", 400 },
                { "Steak with Roasted Vegetables", 450 },
                { "Salmon with Quinoa", 380 },
                { "Stir-Fry Tofu with Rice", 320 },
                { "Vegetable Curry with Naan", 350 },
                { "Pasta Primavera", 320 },
                { "Hamburger with Sweet Potato Fries", 500 },
                { "Sushi Roll Combo", 420 },
                { "Roast Beef with Mashed Potatoes", 450 },
                { "Shrimp Scampi", 370 },
                { "Veggie Pizza", 380 },
                { "Tacos with Guacamole", 350 },
                { "Lemon Herb Roasted Chicken", 380 },
                { "Vegetable Stir-Fry", 300 },
                { "Eggplant Parmesan", 320 },
                { "Teriyaki Salmon", 400 },
                { "Beef Stew", 420 },
                { "Lasagna", 450 },
                { "Mushroom Risotto", 350 }
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
                mainWindow.lunchTotalCalories = totalCalories;
                mainWindow.LunchKcal.Text = $"{totalCalories} kcal";
        
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
