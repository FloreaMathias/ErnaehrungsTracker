using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ErnaehrungsTracker
{
    /// <summary>
    /// Interaktionslogik für Breakfast.xaml
    /// </summary>
    public partial class Breakfast : Window
    {
        public Dictionary<string, int> meals = new Dictionary<string, int>();
        public int TotalCalories { get; private set; }
        public Breakfast()
        {
            InitializeComponent();
            meals.Add("Oatmeal", 150);
            meals.Add("Scrambled Eggs", 200);
            meals.Add("Fruit Salad", 100);
            UpdateComboBox();
        }

        private void UpdateComboBox()
        {
            OurMelasComboBoxBreakfast.ItemsSource = meals.Keys;
        }

        private void AddButtonBreakfast_Click(object sender, RoutedEventArgs e)
        {
            string mealName = OtherMealTextBoxBreakfast.Text;
            if(!string.IsNullOrWhiteSpace(mealName))
            {
                int kcal;
                if(int.TryParse(OtherKclaTexBoxBreakfast.Text, out kcal))
                {
                    if(!meals.ContainsKey(mealName))
                    {
                        meals.Add(mealName, kcal);
                        UpdateComboBox();
                    }
                    int count;
                    if(!int.TryParse(CountTextBox.Text, out count) || count < 1)
                    {
                        MessageBox.Show("Please enter a valid number for count.");
                        return;
                    }

                    for(int i = 0; i < count; i++)
                    {
                        BreakfastListBox.Items.Add(mealName + " - " + kcal + " kcal");
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a valid number for kcal.");
                }
            }
            else
            {
                MessageBox.Show("Please enter a meal name.");
            }
        }


        private void RemoveButtonBreakfast_Click(object sender, RoutedEventArgs e)
        {
            if(BreakfastListBox.SelectedItem != null)
            {
                string selectedMeal = BreakfastListBox.SelectedItem.ToString();
                int index = selectedMeal.IndexOf('-');
                if(index != -1)
                {
                    string mealName = selectedMeal.Substring(0, index - 1).Trim();
                    if(meals.ContainsKey(mealName))
                    {
                        meals.Remove(mealName);
                        UpdateComboBox();
                    }
                    BreakfastListBox.Items.Remove(selectedMeal);
                }
            }
            else
            {
                MessageBox.Show("Please select a meal to remove.");
            }
        }

        private void OurMelasComboBoxBreakfast_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(OurMelasComboBoxBreakfast.SelectedItem != null)
            {
                string selectedMeal = OurMelasComboBoxBreakfast.SelectedItem.ToString();

                if(!BreakfastListBox.Items.Contains(selectedMeal + " - " + meals[selectedMeal] + " kcal"))
                {
                    BreakfastListBox.Items.Add(selectedMeal + " - " + meals[selectedMeal] + " kcal");
                    CalculateTotalKcal(); 
                }
            }
        }
        private void CalculateTotalKcal()
        {
            int totalKcal = 0;
            foreach(var item in BreakfastListBox.Items)
            {
                string mealDetails = item.ToString();
                int index = mealDetails.LastIndexOf('-');
                if(index != -1)
                {
                    string kcalString = mealDetails.Substring(index + 1).Trim();
                    if(int.TryParse(kcalString, out int kcal))
                    {
                        totalKcal += kcal;
                    }
                }
            }

            TotalCalories = totalKcal;
        }
        public int GetTotalCalories()
        {
            CalculateTotalKcal(); 
            return TotalCalories; 
        }

        private void ExitButtonBreakfast_Click(object sender, RoutedEventArgs e)
        {
            CalculateTotalKcal();

            ((MainWindow)Application.Current.MainWindow).BreakFastKcal.Text = $"{TotalCalories} kcal";

            this.Close();
        }

    }
}