using System;
using System.Collections.Generic;

namespace KasetMore.Data.Models;

public partial class ProductImage
{
    public int AttatchmentId { get; set; }

    public string? Image { get; set; }

    public int ProductId { get; set; }

    public virtual Product Product { get; set; } = null!;
}
