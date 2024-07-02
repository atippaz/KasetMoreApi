using System;
using System.Collections.Generic;

namespace KasetMore.Data.Models;

public partial class User
{
    public string Email { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? ProfilePicture { get; set; }

    public string UserType { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string? DisplayName { get; set; }

    public string Password { get; set; } = null!;

    public string IsVerified { get; set; } = null!;

    public string? LaserCode { get; set; }

    public string? IdNumber { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public DateTime? CreateDate { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
