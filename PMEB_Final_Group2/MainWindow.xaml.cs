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

        private void FavoritesBtn_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.NavigationService.Navigate(new Pages.Favorites());
        }

        private void DirectsBtn_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.NavigationService.Navigate(new Pages.Directors());
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            // Retrieve the text from the TextBox.
            var searchText = txtSearch.Text; // Changed from TextSearch.TextProperty to txtSearch.Text

            // Ensure searchText is not null and convert it to lowercase.
            searchText = searchText?.ToLower() ?? string.Empty;

            // Query the Titles where the PrimaryTitle contains the searchText.
            var query = context.Titles
                    .Include(t => t.Rating) 
                    .Include(t => t.Genres) 
                    .Where(t => t.PrimaryTitle.ToLower().Contains(searchText.ToLower()))
                    .ToList();

            mainFrame.NavigationService.Navigate(new Pages.MainSearch(query));
        }



        private void RatingComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ratingComboBox.SelectedItem is ComboBoxItem selectedRating)
            {
                // Get the tag value
                int ratingValue = Convert.ToInt32(selectedRating.Tag);

                IQueryable<Title> query = context.Titles.Include(t => t.Rating).Include(t => t.Genres);

                //For Five Star
                if (ratingValue == 10)
                {
                    query = query.Where(t => t.Rating.AverageRating == ratingValue);
                }
                else
                {
                    // For 1-4 Stars
                    int maxRating = ratingValue + 2; 
                    query = query.Where(t => t.Rating.AverageRating >= ratingValue && t.Rating.AverageRating < maxRating);
                }

                // Get the answer
                var titles = query.ToList();

                mainFrame.NavigationService.Navigate(new Pages.MainSearch(titles));
            }
        }

        private void GenreListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Get the item user select
            var selectedGenre = genreListView.SelectedItem as Genre;

            if (selectedGenre != null)
            {
                
                var query = context.Titles
                                   .Include(t => t.Genres)
                                   .Where(t => t.Genres.Any(g => g.GenreId == selectedGenre.GenreId))
                                   .ToList();

                mainFrame.NavigationService.Navigate(new Pages.MainSearch(query));
            }
        }


    }
}