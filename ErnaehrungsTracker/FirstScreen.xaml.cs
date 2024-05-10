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
    /// Interaktionslogik für Dinner.xaml
    /// </summary>
    public partial class FirstScreen : Window
    {

        public double goalWeight;
        public double currentWeight;
        public string inputName;
        public DateTime startDate;


        public FirstScreen()
        {
            InitializeComponent();
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if(int.TryParse(GoalWeightTextBox.Text, out int goalWeightValue))
            {
                goalWeight = goalWeightValue;
            }
            else
            {
                MessageBox.Show("Bitte gib eine gültige Zahl für das Zielgewicht ein.");
                return;
            }

            if(int.TryParse(CurrentWeightTextBox.Text, out int currentWeightValue))
            {
                currentWeight = currentWeightValue;
            }
            else
            {
                MessageBox.Show("Bitte gib eine gültige Zahl für das aktuelle Gewicht ein.");
                return;
            }

            inputName = InputNameTextBox.Text;   
            startDate = StartDatePicker.SelectedDate ?? DateTime.Now;
            this.Close();
        }
    }
}
