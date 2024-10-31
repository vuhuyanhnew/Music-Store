using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;
using MusicStore;


namespace MusicStore
{
    public class DatabaseService
    {
        private readonly IConfiguration _configuration;
        private readonly MusicStoreContext _context;

        public DatabaseService()
        {
            _configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();

            var optionsBuilder = new DbContextOptionsBuilder<MusicStoreContext>();
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));

            _context = new MusicStoreContext(optionsBuilder.Options);
        }

        public MusicStoreContext GetContext()
        {
            return _context;
        }

        public List<Album> GetAllAlbums()
        {
            return _context.Albums
                .Include(a => a.Artist)
                .Include(a => a.Genre)
                .ToList();
        }

        public List<Artist> GetAllArtists()
        {
            return _context.Artists.ToList();
        }

    }
}
