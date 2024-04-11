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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using PMEB_Final_Group2.Models;
using PMEB_Final_Group2.Data;

namespace PMEB_Final_Group2.Pages
{

    public partial class MainSearch : Page
    {
        // Fields to hold the titles to display and the database context.
        private List<Title> _titles;
        private ImdbContext context;

        // Constructor: Initializes the page, assigns the list of titles and the database context passed from another page or component.
        public MainSearch(List<Title> titles, ImdbContext passedContext)
        {
            InitializeComponent();
            _titles = titles;
            context = passedContext;
            LoadTitles();
        }

        // Loads the titles into the ListView on the page.
        private void LoadTitles()
        {
            TitlesListView.ItemsSource = _titles;
        }

        // Event handler for the "Add to Favorite" button click.
        private void AddToFavorite_Click(object sender, RoutedEventArgs e)
        {
            // Retrieves the TitleId from the button's CommandParameter.
            var titleId = (string)((Button)sender).CommandParameter;

            // If it is already a favorite, inform the user.
            var exists = context.Favorites.Any(f => f.TitleId == titleId);
            if (exists)
            {
                MessageBox.Show("This title is already in your favorites.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                // If not, create a new Favorite object and add it to the database.
                var favorite = new Favorite { TitleId = titleId };
                context.Favorites.Add(favorite);
                context.SaveChanges();

                MessageBox.Show("Title added to favorites successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

    }
}
