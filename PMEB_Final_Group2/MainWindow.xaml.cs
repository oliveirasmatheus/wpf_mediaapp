using PMEB_Final_Group2.Data;
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

namespace PMEB_Final_Group2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        public MainWindow()
        {
            ImdbContext context = new ImdbContext();

            InitializeComponent();
            LoadHomePage();
            

        }

        private void LoadHomePage()
        {
            if (mainFrame != null)
            {
                mainFrame.NavigationService.Navigate(new Pages.DashBorad());
            }
        }

        private void DashBoradBtn_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.NavigationService.Navigate(new Pages.DashBorad());
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.NavigationService.Navigate(new Pages.MainSearch());
        }

        private void FavoritesBtn_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.NavigationService.Navigate(new Pages.Favorites());
        }

        private void DirectsBtn_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.NavigationService.Navigate(new Pages.Directors());
        }

        private void RatingBtn_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.NavigationService.Navigate(new Pages.MainSearch());
        }
    }
}