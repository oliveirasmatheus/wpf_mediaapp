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

        private List<Title> _titles;
        private ImdbContext context;

        public MainSearch(List<Title> titles, ImdbContext passedContext)
        {
            InitializeComponent();
            _titles = titles;
            context = passedContext;
            LoadTitles();
        }

        private void LoadTitles()
        {
            TitlesListView.ItemsSource = _titles;
        }

        private void AddToFavorite_Click(object sender, RoutedEventArgs e)
        {
            var titleId = (string)((Button)sender).CommandParameter;

            // Check if the title is already a favorite
            var exists = context.Favorites.Any(f => f.TitleId == titleId);
            if (exists)
            {
                MessageBox.Show("This title is already in your favorites.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                // Add to favorites
                var favorite = new Favorite { TitleId = titleId };
                context.Favorites.Add(favorite);
                context.SaveChanges();
                MessageBox.Show("Title added to favorites successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

    }
}
