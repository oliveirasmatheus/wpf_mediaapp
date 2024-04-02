using Microsoft.EntityFrameworkCore;
using PMEB_Final_Group2.Data;
using PMEB_Final_Group2.Models;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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

        ImdbContext context = new ImdbContext();

        CollectionViewSource genreListSource = new CollectionViewSource();
       
       
        public MainWindow()
        {   
            InitializeComponent();
            LoadHomePage();

        }


        private void LoadHomePage()
        {
            if (mainFrame != null)
            {
                mainFrame.NavigationService.Navigate(new Pages.DashBorad());
            }

            //Create List for Genre
            genreListSource = (CollectionViewSource)FindResource(nameof(genreListSource));

            context.Genres.Load();
            genreListSource.Source = context.Genres.Local.ToObservableCollection();

            var query = from genre in context.Genres select genre;
            genreListSource.Source = query.ToList();
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


        private void RatingComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ratingComboBox.SelectedItem is ComboBoxItem selectedRating)
            {
                int ratingValue = Convert.ToInt32(selectedRating.Tag);


                mainFrame.NavigationService.Navigate(new Pages.MainSearch());
            }
        }


    }
}