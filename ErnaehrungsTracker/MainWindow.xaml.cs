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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public double goalWeight;
        public double currentWeight;
        public string inputName;
        public DateTime startDate;
        private static int breakfastTotalCalories;

        public MainWindow()
        {
            InitializeComponent();
            startFirstScreen();
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
            //currentText.Text = $"{currentWeightKcal}";

            foodText.Text = "";
            trainingText.Text = "";
            remainingText.Text = "";


        }


        #region WaterCounter & Steps

        private double waterCounter = 0; 

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

        private double stepsCounter = 0;

        private void addSteps_Click(object sender, RoutedEventArgs e)
        {
            if(double.TryParse(countSteps.Text, out double stepsToAdd))
            {
                stepsCounter += stepsToAdd;
                currentSteps.Text = stepsCounter.ToString("0.0");
            }
            else
            {
                MessageBox.Show("Bitte geben Sie eine gültige Zahl für die Wassermenge ein.");
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


        #endregion

        private void openBreakfastMenu_Click(object sender, RoutedEventArgs e)
        {
            var openBreakfastScreen = new Breakfast();
            openBreakfastScreen.ShowDialog();
            breakfastTotalCalories = openBreakfastScreen.GetTotalCalories();
            ((MainWindow)Application.Current.MainWindow).BreakFastKcal.Text = $"{breakfastTotalCalories} kcal";
        }

        private void ProfilButton_Click(object sender, RoutedEventArgs e)
        {
            Profil profil = new Profil();
            profil.Show();
            Close();
        }
    }
}
