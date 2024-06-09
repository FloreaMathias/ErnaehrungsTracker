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
        
        private CalorieCalculator calorieCalculator = new CalorieCalculator();
        public double foodtext;
        
        
        public int breakfastTotalCalories = 0;
        public int lunchTotalCalories = 0;
        public int dinnerTotalCalories = 0;
        public int snacksTotalCalories = 0;
        private double stepsCounter = 0;
        private double trainingCalories = 0;
        private static bool isFirstRun = true;

        
    
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
            calorieCalculator.UpdateTotalFoodCalories(breakfastTotalCalories, lunchTotalCalories, dinnerTotalCalories, snacksTotalCalories);

            double dailyCaloriesGoal = calorieCalculator.CalculateDailyCaloriesGoal(UserProfile);
            int remainingCalories = calorieCalculator.CalculateRemainingCalories(dailyCaloriesGoal, trainingCalories);
            foodtext = calorieCalculator.UpdateTotalFoodCalories(breakfastTotalCalories, lunchTotalCalories, dinnerTotalCalories, snacksTotalCalories);
            goalText.Text = $"{(int)dailyCaloriesGoal}";
            foodText.Text = $"{foodtext}";
            trainingText.Text = $"{(int)trainingCalories}";
            remainingText.Text = $"{remainingCalories}";

            UpdateBars(calorieCalculator.TotalFoodCalories, trainingCalories);
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


        private int savedBreakfastTotalCalories;
        private int savedLunchTotalCalories;
        private int savedDinnerTotalCalories;
        private int savedSnacksTotalCalories;
        private void ProfilButton_Click(object sender, RoutedEventArgs e)
        {
            // Speichern der aktuellen Werte
            int savedBreakfastTotalCalories = breakfastTotalCalories;
            int savedLunchTotalCalories = lunchTotalCalories;
            int savedDinnerTotalCalories = dinnerTotalCalories;
            int savedSnacksTotalCalories = snacksTotalCalories;

            Profil profilWindow = new Profil(UserProfile, savedBreakfastTotalCalories, savedLunchTotalCalories, savedDinnerTotalCalories, savedSnacksTotalCalories);
            profilWindow.Closed += ProfilWindow_Closed; 
            profilWindow.Show();
            this.Hide(); 
        }

        private void ProfilWindow_Closed(object sender, EventArgs e)
        {
            this.Show(); 

            Profil profilWindow = sender as Profil;
            if (profilWindow != null)
            {
                breakfastTotalCalories = profilWindow.BreakfastTotalCalories;
                lunchTotalCalories = profilWindow.LunchTotalCalories;
                dinnerTotalCalories = profilWindow.DinnerTotalCalories;
                snacksTotalCalories = profilWindow.SnacksTotalCalories;

                BreakFastKcal.Text = $"{breakfastTotalCalories} kcal";
                LunchKcal.Text = $"{lunchTotalCalories} kcal";
                DinnerKcal.Text = $"{dinnerTotalCalories} kcal";
                SnacksKcal.Text = $"{snacksTotalCalories} kcal";

                Calc_kg_to_kcal();
            }
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
                SetupUI();
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
