using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models;

public partial class Genre
{
    public int GenreId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Album> Albums { get; set; } = new List<Album>();
}
