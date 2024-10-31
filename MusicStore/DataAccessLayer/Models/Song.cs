using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models;

public partial class Song
{
    public int SongId { get; set; }

    public string Title { get; set; } = null!;

    public int? ArtistId { get; set; }

    public int? AlbumId { get; set; }

    public int? CustomAlbumId { get; set; }

    public int? Duration { get; set; }

    public DateOnly? ReleaseDate { get; set; }

    public int? TrackNumber { get; set; }

    public virtual Album? Album { get; set; }

    public virtual Artist? Artist { get; set; }

    public virtual CustomAlbum? CustomAlbum { get; set; }
}
