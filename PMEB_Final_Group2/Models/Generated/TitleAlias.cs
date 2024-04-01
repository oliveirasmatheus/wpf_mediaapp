using System;
using System.Collections.Generic;

namespace PMEB_Final_Group2.Models;

public partial class TitleAlias
{
    public string TitleId { get; set; } = null!;

    public int Ordering { get; set; }

    public string Title { get; set; } = null!;

    public string? Region { get; set; }

    public string? Language { get; set; }

    public bool? IsOriginalTitle { get; set; }

    public virtual Title TitleNavigation { get; set; } = null!;
}
