using System;
using System.Collections.Generic;

namespace KasetMore.Data.Models;

public partial class Category
{
    public string CategoryName { get; set; } = null!;

    public string? CategoryDesc { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }
}
