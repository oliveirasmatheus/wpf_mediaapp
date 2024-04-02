using System;
using System.Collections.Generic;

namespace PMEB_Final_Group2.Models;

public partial class Favorite
{
    public int FavoriteId { get; set; }

    public string? TitleId { get; set; }

    public virtual Title? Title { get; set; }
}
