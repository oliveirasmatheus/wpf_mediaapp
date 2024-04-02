using PMEB_Final_Group2.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
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
        public MainWindow()
        {
            InitializeComponent();
            //Testing 
            ImdbContext context = new ImdbContext();


            var topTitles = context.Titles
                                   .OrderBy(t => t.TitleId)
                                   .Take(10)
                                   .ToList();

            var titlesText = string.Join(Environment.NewLine, topTitles.Select(t => t.TitleId));
            TitlesTextBox.Text = titlesText;
            //Testing
        }
    }
}