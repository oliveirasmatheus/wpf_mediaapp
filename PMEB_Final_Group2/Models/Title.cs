using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMEB_Final_Group2.Models
{
    public partial class Title
    {
        public string GenresString => string.Join(", ", Genres.Select(g => g.Name));
    }
}
