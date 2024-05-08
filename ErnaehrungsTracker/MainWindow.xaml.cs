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
        public MainWindow()
        {
            InitializeComponent();ValidateValueCallback:; 
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            MainWindow firstscreen = new MainWindow();
            firstscreen.Show();
        }

        private void mam_Click(object sender, RoutedEventArgs e)
        {
            Breakfast breakfast = new Breakfast();
            breakfast.Show();
        }

        private void mama_Click(object sender, RoutedEventArgs e)
        {
            Lunch lunch = new Lunch();
            lunch.Show();
        }

        private void mamam_Click(object sender, RoutedEventArgs e)
        {
            Dinner dinner = new Dinner();
            dinner.Show();
        }

        private void mamama_Click(object sender, RoutedEventArgs e)
        {
            
        }
    } 
}