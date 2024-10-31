using DataAccessLayer.Models;
using Microsoft.Data.SqlClient;
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
            var context = _databaseService.GetContext();
            var albums = context.Albums
                .Include(a => a.Artist)
                .Include(a => a.Genre)
                .AsNoTracking()  
                .ToList();
            dgMain.ItemsSource = albums;
        }
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var album = (sender as Button)?.DataContext as Album;
            if (album != null)
            {
                UpdateAlbum(album);
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
            //editArtistWindow.Closed += (s, args) => this.Show();
            editArtistWindow.Show();
        }
        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();

        }
        private void UpdateAlbum(Album album)
        {
            try
            {
                using (var context = _databaseService.GetContext())
                {
                    var albumToUpdate = context.Albums.Find(album.AlbumId);
                    if (albumToUpdate != null)
                    {
                        albumToUpdate.Title = album.Title;
                        albumToUpdate.Price = album.Price;
                        context.SaveChanges();  

                        MessageBox.Show("Cập nhật thành công!", "Thông báo",
                            MessageBoxButton.OK, MessageBoxImage.Information);

                        LoadAlbums(); 
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật: {ex.Message}", "Lỗi",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
