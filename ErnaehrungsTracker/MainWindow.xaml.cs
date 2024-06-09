using ErnaehrungsTracker.Models;
using System;
using System.IO;
using System.Windows;

namespace ErnaehrungsTracker
{
    public partial class MainWindow : Window
    {
        public UserProfile UserProfile { get; private set; }
        public WaterCounter WaterCounter { get; private set; } = new WaterCounter();
        public StepsCounter StepsCounter { get; private set; } = new StepsCounter();
        
        private static int breakfastTotalCalories = 0;
        private static int lunchTotalCalories = 0;
        private static int dinnerTotalCalories = 0;
        private static int snacksTotalCalories = 0;
        private double stepsCounter = 0;
        private double trainingCalories = 0;
        private static bool isFirstRun = true;

    // nicht erlauben von fenster vergrößern/verkleiner
    // logging
    // kcal zentrieren water/km
    
    
    // wenn essen hinzugefügt dann wieder remove, dann wird die kcal nicht mit removed
    //  fenster close 
    // Name absichern eingabe firstscreen
    
    
        public MainWindow()
        {
            InitializeComponent();
            SetupUI();
        }

        public MainWindow(UserProfile userProfile)
        {
            InitializeComponent();
            UserProfile = userProfile;
            SetupUI();
        }

        private void SetupUI()
        {
            if (isFirstRun)
            {
                btnstart.Visibility = Visibility.Visible;
            }
            else
            {
                btnstart.Visibility = Visibility.Hidden;
            }

            if (UserProfile != null)
            {
                welcomeTextBox.Text = $"Welcome, {UserProfile.Name}!";
                Calc_kg_to_kcal();
                BreakFastKcal.Text = $"{breakfastTotalCalories} kcal";  
                LunchKcal.Text = $"{lunchTotalCalories} kcal";  
                DinnerKcal.Text = $"{dinnerTotalCalories} kcal";  
                SnacksKcal.Text = $"{snacksTotalCalories} kcal";  
            }
        }

        public void startFirstScreen()
        {
            var startScreen = new FirstScreen();
            startScreen.ShowDialog();

            UserProfile = new UserProfile(startScreen.inputName, startScreen.currentWeight, startScreen.goalWeight, startScreen.startDate, startScreen.goalDate);
            welcomeTextBox.Text = $"Welcome, {UserProfile.Name}!";
            
            Calc_kg_to_kcal();
            
        }

        public void Calc_kg_to_kcal()
        {
            double conversionFactor = 7716.179176;

            double goalWeightKcal = Math.Round(UserProfile.GoalWeight * conversionFactor);
            double currentWeightKcal = Math.Round(UserProfile.CurrentWeight * conversionFactor);

            TimeSpan timeSpan = UserProfile.StartDate - DateTime.Now;
            int daysDifference = Math.Abs(timeSpan.Days);

            double weightDifferenceKcal = Math.Abs(goalWeightKcal - currentWeightKcal);

            double dailyCaloriesGoal = weightDifferenceKcal / daysDifference;

            int totalFoodCalories = breakfastTotalCalories + lunchTotalCalories + dinnerTotalCalories + snacksTotalCalories;

            int remainingCalories = (int)Math.Round(dailyCaloriesGoal - totalFoodCalories + trainingCalories);

            goalText.Text = $"{(int)dailyCaloriesGoal}";
            foodText.Text = $"{totalFoodCalories}";
            trainingText.Text = $"{(int)trainingCalories}"; 
            remainingText.Text = $"{remainingCalories}";
            
            UpdateBars(totalFoodCalories, trainingCalories);
        }

        private void UpdateBars(double mealCalories, double trainingCalories)
        {
            double maxHeight = 100.0;
    
            double maxCalories = Math.Max(mealCalories, trainingCalories);
            double mealHeight = (mealCalories / maxCalories) * maxHeight;
            double trainingHeight = (trainingCalories / maxCalories) * maxHeight;
    
            MealsBar.Height = mealHeight;
            TrainingBar.Height = trainingHeight;
        }
       



        private void addWater_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(countWater.Text, out double waterToAdd))
            {
                WaterCounter.AddWater(waterToAdd);
                currentWater.Text = WaterCounter.TotalWater.ToString("0.0");
            }
            else
            {
                MessageBox.Show("Bitte geben Sie eine gültige Zahl für die Wassermenge ein.");
            }
        }

        private void removeWater_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(countWater.Text, out double waterToRemove))
            {
                try
                {
                    WaterCounter.RemoveWater(waterToRemove);
                    currentWater.Text = WaterCounter.TotalWater.ToString("0.0");
                }
                catch (InvalidOperationException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Bitte geben Sie eine gültige Zahl für die Wassermenge ein.");
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


        private void addSteps_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(countSteps.Text, out double stepsToAdd))
            {
                StepsCounter.AddSteps(stepsToAdd);
                currentSteps.Text = StepsCounter.TotalSteps.ToString("0.0");

                double kilometers = StepsToKilometers(stepsToAdd);
                double calories = KilometersToCalories(kilometers);

                trainingCalories += calories;
                trainingText.Text = trainingCalories.ToString("0.0");

                Calc_kg_to_kcal();
            }
            else
            {
                MessageBox.Show("Bitte geben Sie eine gültige Zahl für die Schrittzahl ein.");
            }
        }


        private void removeSteps_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(countSteps.Text, out double stepsToRemove))
            {
                try
                {
                    StepsCounter.RemoveSteps(stepsToRemove);
                    currentSteps.Text = StepsCounter.TotalSteps.ToString("0.0");

                    double kilometers = StepsToKilometers(stepsToRemove);
                    double calories = KilometersToCalories(kilometers);

                    trainingCalories -= calories;
                    trainingText.Text = trainingCalories.ToString("0.0");

                    Calc_kg_to_kcal();
                }
                catch (InvalidOperationException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Bitte geben Sie eine gültige Zahl für die Schrittzahl ein.");
            }
        }


     
        private void ProfilButton_Click(object sender, RoutedEventArgs e)
        {
            Profil profilWindow = new Profil(UserProfile);
            profilWindow.Show();
            this.Close();
        }

        private void btnstart_Click(object sender, RoutedEventArgs e)
        {
            btnstart.Visibility = Visibility.Hidden;
            isFirstRun = false;
            startFirstScreen();
        }

        public void Load(string filename)
        {
            using (StreamReader file = new StreamReader(filename))
            {
                string line = file.ReadLine();
                UserProfile = UserProfile.Deserialize(line);
                MainWindow main = new MainWindow(UserProfile);
                main.Show();
                this.Close();
            }
        }

        public void Save(string filename)
        {
            using (StreamWriter file = new StreamWriter(filename))
            {
                file.WriteLine(UserProfile.Serialize());
            }
        }

        private void openBreakfastMenu_Click(object sender, RoutedEventArgs e)
        {
            var openBreakfastScreen = new Breakfast();
            openBreakfastScreen.ShowDialog();
            breakfastTotalCalories += openBreakfastScreen.GetTotalCalories();
            BreakFastKcal.Text = $"{breakfastTotalCalories} kcal";
            Calc_kg_to_kcal(); 
        }


        private void openLunchMenu_Click(object sender, RoutedEventArgs e)
        {
            var openLunchScreen = new Lunch();
            openLunchScreen.ShowDialog();
            lunchTotalCalories += openLunchScreen.GetTotalCalories();
            LunchKcal.Text = $"{lunchTotalCalories} kcal";
            Calc_kg_to_kcal(); 
        }

        private void openDinnerMenu_Click(object sender, RoutedEventArgs e)
        {
            var openDinnerScreen = new Dinner();
            openDinnerScreen.ShowDialog();
            dinnerTotalCalories += openDinnerScreen.GetTotalCalories();
            DinnerKcal.Text = $"{dinnerTotalCalories} kcal";
            Calc_kg_to_kcal(); 
        }
        
        
        
        private void OpenSnacksMenu_Click(object sender, RoutedEventArgs e)
        {
            var OpenSnacksScreen = new Snacks();
            OpenSnacksScreen.ShowDialog();
            snacksTotalCalories += OpenSnacksScreen.GetTotalCalories();
            SnacksKcal.Text = $"{snacksTotalCalories} kcal";
            Calc_kg_to_kcal(); 
        }

    }
}
