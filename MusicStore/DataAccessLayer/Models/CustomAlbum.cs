using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models;

public partial class CustomAlbum
{
    public int CustomAlbumId { get; set; }

    public int? UserId { get; set; }

    public string AlbumName { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Song> Songs { get; set; } = new List<Song>();

    public virtual User? User { get; set; }
}
