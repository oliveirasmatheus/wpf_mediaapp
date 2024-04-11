using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using PMEB_Final_Group2.Data;
using PMEB_Final_Group2.Models;

using System.Windows.Controls;
using System.Windows.Data;

namespace PMEB_Final_Group2.Pages
{

    public partial class Directors : Page
    {

        private ImdbContext context;
        private CollectionViewSource directorViewSource;

        public Directors()
        {
            InitializeComponent();
            context = new ImdbContext();
            directorViewSource = (CollectionViewSource)FindResource(nameof(directorViewSource));

            LoadDirectors();

        }

        private void LoadDirectors()
        {
            var directors = (from name in context.Names
                             join principal in context.Principals on name.NameId equals principal.NameId
                             join title in context.Titles on principal.TitleId equals title.TitleId
                             where principal.JobCategory == "director"
                             group new { name, title, title.Genres } by name.PrimaryName
                            into directorGroup
                             select new
                             {
                                 DirectorName = directorGroup.Key,
                                 AllTitles = directorGroup.Select(x => x.title)
                             }).Take(200);

            directorsListView.ItemsSource = directors.ToList();
        }

        private void SearchDirectorsButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var directorsSearchQuery = from name in context.Names
                            join principal in context.Principals on name.NameId equals principal.NameId
                            join title in context.Titles on principal.TitleId equals title.TitleId
                            where principal.JobCategory == "director"
                            where name.PrimaryName.Contains(txtDirectorsSearch.Text)
                            group new { name, title, title.Genres } by name.PrimaryName
                            into directorGroup
                            select new
                            {
                                DirectorName = directorGroup.Key,
                                AllTitles = directorGroup.Select(x => x.title)
                            };

            directorsListView.ItemsSource = directorsSearchQuery.ToList();
        }
    }
}
