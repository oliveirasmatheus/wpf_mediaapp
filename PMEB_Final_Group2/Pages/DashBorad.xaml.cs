using Microsoft.EntityFrameworkCore;
using PMEB_Final_Group2.Data;
using PMEB_Final_Group2.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PMEB_Final_Group2.Pages
{
    /// <summary>
    /// DashBorad.xaml
    /// </summary>
    public partial class DashBorad : Page
    {

        private readonly ImdbContext context = new ImdbContext();

        public DashBorad()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            // Get list of favorite genres
            var favoriteGenres = context.Favorites
                                        .SelectMany(f => f.Title.Genres)
                                        .Select(g => g.Name)
                                        .Distinct()
                                        .ToList();

            // Load suggested movies based on favorite genres
            var suggestedMoviesQuery = (from title in context.Titles
                                        join rating in context.Ratings on title.TitleId equals rating.TitleId
                                        where title.Genres.Any(g => favoriteGenres.Contains(g.Name))
                                        orderby rating.NumVotes descending
                                        select new
                                        {
                                            Title = title.OriginalTitle,
                                            TitleId = title.TitleId,
                                            AverageRating = rating.AverageRating != null ? $"★{rating.AverageRating}/10" : "Rate not available",
                                            Genres = string.Join(", ", title.Genres.Select(genre => genre.Name)) ?? "Unknown Genre",
                                            RuntimeMinutes = title.RuntimeMinutes ?? 0
                                        }).Take(6);

            // Load new movies ordered by startYear
            var newMoviesQuery = (from title in context.Titles
                                  join rating in context.Ratings on title.TitleId equals rating.TitleId into ratingGroup
                                  from ratingInfo in ratingGroup.DefaultIfEmpty()
                                  orderby title.StartYear descending
                                  select new
                                  {
                                      Title = title.OriginalTitle,
                                      TitleId = title.TitleId,
                                      AverageRating = ratingInfo != null ? $"★{ratingInfo.AverageRating}/10" : "Rate not available",
                                      Genres = string.Join(", ", title.Genres.Select(genre => genre.Name)) ?? "Unknown Genre",
                                      RuntimeMinutes = title.RuntimeMinutes ?? 0
                                  }).Take(10);

            // Set the item sources for suggested movies and new movies
            listSuggestedMovies.ItemsSource = suggestedMoviesQuery.ToList();
            listNewMovies.ItemsSource = newMoviesQuery.ToList();
        }

        private void AddToFavorites_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null && button.Tag != null && button.Tag is string titleId)
            {
                // Add the movie to favorites using the titleId
                AddToFavorite(titleId);
            }
        }

        private void AddToFavorite(string titleId)
        {
            try
            {
                // Check if the movie is already in favorites
                var existingFavorite = context.Favorites.FirstOrDefault(f => f.TitleId == titleId);
                if (existingFavorite != null)
                {
                    MessageBox.Show("Movie already exists in favorites!");
                    return;
                }

                // Add the movie to favorites
                var favorite = new Favorite { TitleId = titleId };
                context.Favorites.Add(favorite);
                context.SaveChanges();

                MessageBox.Show("Movie added to favorites successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }


    }
}
