﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMEB_Final_Group2.Models
{
    public partial class Title
    {
        public string GenresString => string.Join(", ", Genres.Select(g => g.Name));
        public string IsAdultOrNot => IsAdult == true ? "For Adult" : "For Everyone";
        public string AliasRegion => TitleAliases.FirstOrDefault()?.Region ?? "Unknown";
        public string AliasLanguage => TitleAliases.FirstOrDefault()?.Language ?? "Unknown";

        public string RuntimeFormatted { get
            {
                return $"Runtime: {this.RuntimeMinutes} min" ?? "Runtime: unlisted";
            }
        }

        public string YearFormatted { get
            {
                return $"Released: {this.StartYear}";
            }
        }
    }
}
