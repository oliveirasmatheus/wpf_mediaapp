using Microsoft.EntityFrameworkCore;
using PMEB_Final_Group2.Data;
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

    public partial class Favorites : Page
    {
        private ImdbContext context;

        public Favorites()
        {
            InitializeComponent();
            context = new ImdbContext();
            LoadFavoriteMovies();
        }

        private void LoadFavoriteMovies()
        {
            var favoriteMovies = (from favorite in context.Favorites
                                  join title in context.Titles.Include(t => t.TitleAliases)
                                                              .Include(t => t.Genres)
                                                              .Include(t => t.Rating)
                                                              .Include("Names") // For Directors
                                                              
                                  on favorite.TitleId equals title.TitleId
                                  let firstAlias = title.TitleAliases.FirstOrDefault()
                                  let directors = title.Names.Select(n => n.PrimaryName) // Adjust based on the actual relationship
                                  let writers = title.Names1.Select(n => n.PrimaryName) // Adjust based on the actual relationship
                                  select new
                                  {
                                      TitleId = title.TitleId,
                                      Title = title.PrimaryTitle,
                                      OriTitle = title.OriginalTitle,
                                      StartYear = title.StartYear,
                                      isAdult = title.IsAdult == true ? "Only for Adult" : "Good For Every One",
                                      Runtime = title.RuntimeMinutes,
                                      Rating = title.Rating != null ? title.Rating.AverageRating : null,
                                      VoteNum = title.Rating != null ? title.Rating.NumVotes.ToString() : null,
                                      Genres = string.Join(", ", title.Genres.Select(g => g.Name)),
                                      Region = firstAlias != null ? firstAlias.Region : null,
                                      Language = firstAlias != null ? firstAlias.Language : null,
                                      Directors = string.Join(", ", directors), // Comma-separated list of directors
                                      
                                  }).ToList();

            FavoritesListView.ItemsSource = favoriteMovies;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Button deleteButton = (Button)sender;
            var movie = deleteButton.DataContext as dynamic; // Safe casting to dynamic

            var movieId = movie?.TitleId as string; // Ensures movieId is null if TitleId isn't a string or is absent

            if (!string.IsNullOrWhiteSpace(movieId))
            {
                try
                {
                    var favoriteToRemove = context.Favorites.FirstOrDefault(f => f.TitleId == movieId);
                    if (favoriteToRemove != null)
                    {
                        context.Favorites.Remove(favoriteToRemove);
                        context.SaveChanges(); // Attempt to save changes to the database

                        // Refresh the list to reflect the deletion
                        LoadFavoriteMovies();

                        // Show success message
                        MessageBox.Show("Movie successfully deleted from favorites.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception details or handle them as needed
                    Console.WriteLine(ex.Message); // Placeholder for actual logging

                    // Inform the user that an error occurred
                    MessageBox.Show("An error occurred while trying to delete the movie from favorites. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

    }
}
