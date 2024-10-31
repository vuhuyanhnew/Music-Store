using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Models;

public partial class MusicStoreContext : DbContext
{
    public MusicStoreContext()
    {
    }

    public MusicStoreContext(DbContextOptions<MusicStoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Album> Albums { get; set; }

    public virtual DbSet<Artist> Artists { get; set; }

    public virtual DbSet<CustomAlbum> CustomAlbums { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Song> Songs { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-2NJRVE0;Database=MusicStore;User Id=sa;Password=123456;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Album>(entity =>
        {
            entity.HasKey(e => e.AlbumId).HasName("PK__Albums__97B4BE3720FCA498");

            entity.Property(e => e.AlbumUrl).HasMaxLength(255);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Title).HasMaxLength(200);

            entity.HasOne(d => d.Artist).WithMany(p => p.Albums)
                .HasForeignKey(d => d.ArtistId)
                .HasConstraintName("FK__Albums__ArtistId__4222D4EF");

            entity.HasOne(d => d.Genre).WithMany(p => p.Albums)
                .HasForeignKey(d => d.GenreId)
                .HasConstraintName("FK__Albums__GenreId__412EB0B6");
        });

        modelBuilder.Entity<Artist>(entity =>
        {
            entity.HasKey(e => e.ArtistId).HasName("PK__Artists__25706B50D5C99008");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<CustomAlbum>(entity =>
        {
            entity.HasKey(e => e.CustomAlbumId).HasName("PK__CustomAl__1052CF1DF36371B3");

            entity.Property(e => e.AlbumName).HasMaxLength(100);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.User).WithMany(p => p.CustomAlbums)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__CustomAlb__UserI__45F365D3");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.GenreId).HasName("PK__Genres__0385057ED7218475");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__C3905BCF3BE57001");

            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.Country).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Orders__UserId__4D94879B");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailId).HasName("PK__OrderDet__D3B9D36C7BFD55C7");

            entity.Property(e => e.UnitPrice).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Album).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.AlbumId)
                .HasConstraintName("FK__OrderDeta__Album__5165187F");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__OrderDeta__Order__5070F446");
        });

        modelBuilder.Entity<Song>(entity =>
        {
            entity.HasKey(e => e.SongId).HasName("PK__Songs__12E3D6975F8DDC75");

            entity.Property(e => e.Title).HasMaxLength(100);

            entity.HasOne(d => d.Album).WithMany(p => p.Songs)
                .HasForeignKey(d => d.AlbumId)
                .HasConstraintName("FK__Songs__AlbumId__49C3F6B7");

            entity.HasOne(d => d.Artist).WithMany(p => p.Songs)
                .HasForeignKey(d => d.ArtistId)
                .HasConstraintName("FK__Songs__ArtistId__48CFD27E");

            entity.HasOne(d => d.CustomAlbum).WithMany(p => p.Songs)
                .HasForeignKey(d => d.CustomAlbumId)
                .HasConstraintName("FK__Songs__CustomAlb__4AB81AF0");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4CF245E73B");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E4743766B5").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534729158B7").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.LastLogin).HasColumnType("datetime");
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
