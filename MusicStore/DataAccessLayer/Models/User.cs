using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public bool IsAdmin { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? LastLogin { get; set; }

    public virtual ICollection<CustomAlbum> CustomAlbums { get; set; } = new List<CustomAlbum>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
