﻿using Microsoft.EntityFrameworkCore;
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
    public partial class MainWindow : Window
    {

        ImdbContext context = new ImdbContext();

        // CollectionViewSource to manage the display of genres in the UI.
        CollectionViewSource genreListSource = new CollectionViewSource();

        // Fields to hold the current filters applied by the user.
        private string currentSearchText = "";
        private int? currentRatingFilter = null;
        private Genre? currentGenreFilter = null;

        public MainWindow()
        {
            InitializeComponent();
            LoadHomePage();
        }

        // Loads the home page and initializes the genre list.
        private void LoadHomePage()
        {
            if (mainFrame != null)
            {
                mainFrame.NavigationService.Navigate(new Pages.DashBorad());
            }

            // Initializes the genre list from the database.
            genreListSource = (CollectionViewSource)FindResource(nameof(genreListSource));

            context.Genres.Load();
            genreListSource.Source = context.Genres.Local.ToObservableCollection();

            var query = from genre in context.Genres select genre;
            genreListSource.Source = query.ToList();
        }

        // Event handler for clicking the Dashboard button.
        private void DashBoradBtn_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.NavigationService.Navigate(new Pages.DashBorad());
        }

        // Event handler for clicking the Favorites button.
        private void FavoritesBtn_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.NavigationService.Navigate(new Pages.Favorites());
        }

        // Event handler for clicking the Directors button.
        private void DirectsBtn_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.NavigationService.Navigate(new Pages.Directors());
        }

        // Event handler for clicking the Search button.
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            currentSearchText = txtSearch.Text.ToLower();
            FilterMovies();
        }

        // Event handler for changing the selection in the Rating combo box.
        private void RatingComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ratingComboBox.SelectedItem is ComboBoxItem selectedRating)
            {
                
                if (Convert.ToInt32(selectedRating.Tag) == 100)
                {
                    currentRatingFilter = null; // Select all
                }
                else
                {
                    currentRatingFilter = Convert.ToInt32(selectedRating.Tag);
                }
            }
            else
            {
                currentRatingFilter = null;
            }
            FilterMovies(); 
        }

        // Event handler for changing the selection in the Genre list view.
        private void GenreListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currentGenreFilter = genreListView.SelectedItem as Genre;
            FilterMovies();

        }

        // Applies the current filters and navigates to the MainSearch page with the filtered titles
        private void FilterMovies()
        {
            Console.WriteLine($"Filtering: Text='{currentSearchText}', Rating={currentRatingFilter}, Genre={currentGenreFilter?.Name}");
            var query = context.Titles.AsQueryable();

            if (!string.IsNullOrEmpty(currentSearchText))
            {
                query = query.Where(t => t.PrimaryTitle.ToLower().Contains(currentSearchText));
            }

            if (currentRatingFilter.HasValue)
            {
                if (currentRatingFilter == 10)
                {
                    // Directly filter for movies rated exactly 10.
                    query = query.Where(t => t.Rating != null && t.Rating.AverageRating == 10);
                }
                else
                {
                    // For ratings less than 10, define a range [currentRating, currentRating + 2).
                    query = query.Where(t => t.Rating != null && t.Rating.AverageRating >= currentRatingFilter.Value && t.Rating.AverageRating < currentRatingFilter.Value + 2);
                }
            }

            if (currentGenreFilter != null)
            {
                query = query.Where(t => t.Genres.Any(g => g.GenreId == currentGenreFilter.GenreId));
            }

            query = query
                .Include(t => t.Rating)
                .Include(t => t.Genres)
                .Include(t => t.TitleAliases)
                .Take(200);// Optimize loading

            var titles = query.ToList();

            mainFrame.NavigationService.Navigate(new Pages.MainSearch(titles, context));
        }

        // Event handler for clicking the Exit button.
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }


    }
}