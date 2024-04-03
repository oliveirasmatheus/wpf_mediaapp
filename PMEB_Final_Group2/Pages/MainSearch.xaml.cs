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

namespace PMEB_Final_Group2.Pages
{

    public partial class MainSearch : Page
    {

        private List<Title> _titles;

        public MainSearch(List<Title> titles)
        {
            InitializeComponent();
            _titles = titles;
            LoadTitles();
        }

        private void LoadTitles()
        {
            TitlesListView.ItemsSource = _titles;
        }

    }
}
