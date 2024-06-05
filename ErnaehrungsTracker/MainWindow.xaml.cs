using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ErnaehrungsTracker
{
    public partial class MainWindow : Window
    {
        public double goalWeight;
        public double currentWeight;
        public string inputName;
        public DateTime startDate;
        private static int breakfastTotalCalories;
        private static bool isFirstRun = true;
        private double waterCounter = 0;
        private double stepsCounter = 0;
        private double trainingCalories = 0;
        private double dailyCalories;


        public MainWindow()
        {
            InitializeComponent();
        }
        public MainWindow(double goalWeight, double currentWeight, string inputName, double dailyCalories)
        {
            InitializeComponent();
            this.goalWeight = goalWeight;
            this.currentWeight = currentWeight;
            this.inputName = inputName;
            this.dailyCalories = dailyCalories;
            
            if(isFirstRun)
            {
                btnstart.Visibility = Visibility.Visible;
            }
            else
            {
                btnstart.Visibility = Visibility.Hidden;
            }
            welcomeTextBox.Text = $"Welcome, {inputName}!";
            goalText.Text = dailyCalories.ToString("F0");

            UpdateFoodCalories();
        }
        private void UpdateFoodCalories()
        {
            if(double.TryParse(BreakFastKcal.Text.Split(' ')[0], out double breakfastCalories) &&
                double.TryParse(LunchKcal.Text.Split(' ')[0], out double lunchCalories) &&
                double.TryParse(DinnerKcal.Text.Split(' ')[0], out double dinnerCalories) &&
                double.TryParse(SnacksKcal.Text.Split(' ')[0], out double snacksCalories))
            {
                double totalFoodCalories = breakfastCalories + lunchCalories + dinnerCalories + snacksCalories;
                foodText.Text = totalFoodCalories.ToString("F0");
            }
        }

        public void startFirstScreen()
        {
            var startScreen = new FirstScreen();
            startScreen.ShowDialog();

            goalWeight = startScreen.goalWeight;
            currentWeight = startScreen.currentWeight;
            startDate = startScreen.startDate;
            inputName = startScreen.inputName;
            welcomeTextBox.Text = $"Welcome, {inputName}!";

            Calc_kg_to_kcal();
        }

        public void Calc_kg_to_kcal()
        {
            double conversionFactor = 7716.179176;

            double goalWeightKcal = Math.Round(goalWeight * conversionFactor);
            double currentWeightKcal = Math.Round(currentWeight * conversionFactor);

            goalText.Text = $"{goalWeightKcal}";
            foodText.Text = "";
            trainingText.Text = "";
            remainingText.Text = "";

            UpdateRemainingCalories();
        }

        #region WaterCounter & Steps

        private void addWater_Click(object sender, RoutedEventArgs e)
        {
            if(double.TryParse(countWater.Text, out double waterToAdd))
            {
                waterCounter += waterToAdd;
                currentWater.Text = waterCounter.ToString("0.0");
            }
            else
            {
                MessageBox.Show("Bitte geben Sie eine gültige Zahl für die Wassermenge ein.");
            }
        }

        private void removeWater_Click(object sender, RoutedEventArgs e)
        {
            if(double.TryParse(countWater.Text, out double waterToRemove))
            {
                if(waterToRemove <= waterCounter)
                {
                    waterCounter -= waterToRemove;
                    currentWater.Text = waterCounter.ToString("0.0");
                }
                else
                {
                    MessageBox.Show("Die eingegebene Menge ist größer als die aktuelle Wassermenge.");
                }
            }
            else
            {
                MessageBox.Show("Bitte geben Sie eine gültige Zahl für die Wassermenge ein.");
            }
        }

        private void addSteps_Click(object sender, RoutedEventArgs e)
        {
            if(double.TryParse(countSteps.Text, out double stepsToAdd))
            {
                stepsCounter += stepsToAdd;
                currentSteps.Text = stepsCounter.ToString("0.0");

                double kilometers = StepsToKilometers(stepsToAdd);
                double calories = KilometersToCalories(kilometers);

                trainingCalories += calories;
                trainingText.Text = trainingCalories.ToString("0.0");

                UpdateRemainingCalories();
            }
            else
            {
                MessageBox.Show("Bitte geben Sie eine gültige Zahl für die Schrittzahl ein.");
            }
        }

        private void removeSteps_Click(object sender, RoutedEventArgs e)
        {
            if(double.TryParse(countSteps.Text, out double stepsToRemove))
            {
                if(stepsToRemove <= stepsCounter)
                {
                    stepsCounter -= stepsToRemove;
                    currentSteps.Text = stepsCounter.ToString("0.0");

                    double kilometers = StepsToKilometers(stepsToRemove);
                    double calories = KilometersToCalories(kilometers);

                    trainingCalories -= calories;
                    trainingText.Text = trainingCalories.ToString("0.0");

                    UpdateRemainingCalories();
                }
                else
                {
                    MessageBox.Show("Die eingegebene Menge ist größer als die aktuellen Schritte.");
                }
            }
            else
            {
                MessageBox.Show("Bitte geben Sie eine gültige Zahl für die Schrittzahl ein.");
            }
        }

        private double StepsToKilometers(double steps)
        {
            return steps;
        }

        private double KilometersToCalories(double kilometers)
        {
            double caloriesPerKilometer = 7.0;

            return kilometers * caloriesPerKilometer;
        }


        private void UpdateRemainingCalories()
        {
            if(double.TryParse(goalText.Text, out double goalCalories) &&
                double.TryParse(foodText.Text, out double foodCalories))
            {
                double remainingCalories = goalCalories - foodCalories + trainingCalories;
                remainingText.Text = remainingCalories.ToString("0.0");
            }
            else
            {
                remainingText.Text = "Error";
            }
        }

        #endregion

        private void openBreakfastMenu_Click(object sender, RoutedEventArgs e)
        {
            /* var openBreakfastScreen = new Breakfast();
             openBreakfastScreen.ShowDialog();
             breakfastTotalCalories = openBreakfastScreen.GetTotalCalories();
             BreakFastKcal.Text = $"{breakfastTotalCalories} kcal";

             if(double.TryParse(foodText.Text, out double foodCalories))
             {
                 foodCalories += breakfastTotalCalories;
                 foodText.Text = foodCalories.ToString("0.0");
                 UpdateRemainingCalories();
             }*/

            Breakfast breakfastWindow = new Breakfast();
            breakfastWindow.ShowDialog();

            BreakFastKcal.Text = $"{breakfastWindow.GetTotalCalories()} kcal";
            UpdateFoodCalories();
            UpdateRemainingCalories();
        }

        private void ProfilButton_Click(object sender, RoutedEventArgs e)
        {
            Profil profilWindow = new Profil(currentWeight, goalWeight, inputName, startDate);
            profilWindow.Show();
            this.Close();
        }

        private void btnstart_Click(object sender, RoutedEventArgs e)
        {
            btnstart.Visibility = Visibility.Hidden;
            isFirstRun = false;
            startFirstScreen();
        }
    }
}
