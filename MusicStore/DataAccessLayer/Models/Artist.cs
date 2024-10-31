﻿using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models;

public partial class Artist
{
    public int ArtistId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Album> Albums { get; set; } = new List<Album>();

    public virtual ICollection<Song> Songs { get; set; } = new List<Song>();
}
