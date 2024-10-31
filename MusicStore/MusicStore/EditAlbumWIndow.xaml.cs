using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MusicStore
{

    public partial class EditAlbumWIndow : Window
    {
        private readonly DatabaseService _databaseService;


        public EditAlbumWIndow()
        {
            InitializeComponent();
            _databaseService = new DatabaseService();
            LoadAlbums();
            btnArtists.Click += btnArtists_Click;
            btnExit.Click += BtnExit_Click;

        }

        private void LoadAlbums()
        {
            var albums = _databaseService.GetContext().Albums
                .Include(a => a.Artist)
                .Include(a => a.Genre)
                .ToList();
            dgMain.ItemsSource = albums;
        }
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var album = (sender as Button)?.DataContext as Album;
            if (album != null)
            {
                MessageBox.Show($"Sửa album: {album.Title}");
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var album = (sender as Button)?.DataContext as Album;
            if (album != null)
            {
                var result = MessageBox.Show($"Xác nhận xóa album?", "Xác nhận", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    _databaseService.GetContext().Albums.Remove(album);
                    _databaseService.GetContext().SaveChanges();
                    LoadAlbums();
                }
            }
        }

        private void BtnDetail_Click(object sender, RoutedEventArgs e)
        {
            var album = (sender as Button)?.DataContext as Album;
            if (album != null)
            {
                MessageBox.Show($"Chi tiết album: {album.Title}");
            }
        }

        private void btnArtists_Click(object sender, RoutedEventArgs e)
        {
            EditArtistWindow editArtistWindow = new EditArtistWindow();
            this.Hide();
            editArtistWindow.Closed += (s, args) => this.Show();
            editArtistWindow.Show();
        }
        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();

        }
    }
}
