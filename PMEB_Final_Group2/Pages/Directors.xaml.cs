using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using PMEB_Final_Group2.Data;
using PMEB_Final_Group2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
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
    /// Directors.xaml 的交互逻辑
    /// </summary>
    public partial class Directors : Page
    {

        private ImdbContext context;
        private CollectionViewSource directorViewSource;

        public Directors()
        {
            InitializeComponent();
            context = new ImdbContext();
            directorViewSource = (CollectionViewSource) FindResource(nameof(directorViewSource));

            LoadDirectors();

        }

        private void LoadDirectors()
        {
            var directors = from name in context.Names
                            join principal in context.Principals
                            on name.NameId equals principal.NameId
                            where principal.JobCategory == "director"
                            select new
                            {
                                personName = name.PrimaryName
                            }
                            into directorDetails
                            group directorDetails by directorDetails.personName.Substring(0, 1) into directorGroup
                            select new
                            {
                                IndexKey = directorGroup.Key,
                                DirCount = $"({directorGroup.Count()})",
                                DirectorsGroup = directorGroup.Distinct().ToList(),
                            };

            // Execute query to retrieve principals of type "director" and tie it to page element
            directorsListView.ItemsSource = directors.ToList();
        }
    }
}

//SQL For all the data needed for page
//SELECT DISTINCT n.primaryName, p.job_category, tits.primaryTitle, rats.averageRating, gen.Name
//FROM Names n
//JOIN Principals p
//ON n.nameID = p.nameID
//JOIN Directors dir
//ON dir.nameID = n.nameID
//JOIN Titles tits
//ON tits.titleID = dir.titleID
//JOIN Ratings rats
//ON rats.titleID = tits.titleID
//JOIN Title_Genres tit_gen
//ON tit_gen.titleId = tits.titleID
//JOIN Genres gen
//ON tit_gen.genreID = gen.GenreID
//WHERE p.job_category = 'director'