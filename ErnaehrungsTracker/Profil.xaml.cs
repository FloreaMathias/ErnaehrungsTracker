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
    /// Interaktionslogik für Profil.xaml
    /// </summary>
    public partial class Profil : Window
    {
        
        public Profil()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            Close();
        }
        public void startFirstScreen()
        {
            var startScreen = new FirstScreen();
            startScreen.ShowDialog();
            ProfilWeightTextBox.Text = Convert.ToString(startScreen.currentWeight);
            ProfilDate.Content = startScreen.startDate;
            ProfileName.Text = startScreen.inputName;
           
        }
        
    }
}
