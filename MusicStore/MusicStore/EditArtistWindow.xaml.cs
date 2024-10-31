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

    public partial class EditArtistWindow : Window
    {
        private readonly DatabaseService _databaseService;

        public EditArtistWindow()
        {
            InitializeComponent();
            _databaseService = new DatabaseService();
            LoadArtists();
            btnAlbums.Click += BtnAlbums_Click;
            btnExit.Click += BtnExit_Click;

        }

        private void LoadArtists()
        {
            try
            {
                var artists = _databaseService.GetContext().Artists
                    .Include(a => a.Albums)
                    .ToList();
                dgMain.ItemsSource = artists;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var artist = (sender as Button)?.DataContext as Artist;
            if (artist != null)
            {
                MessageBox.Show($"Sửa nghệ sĩ: {artist.Name}");
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var artist = (sender as Button)?.DataContext as Artist;
            if (artist != null)
            {
                var result = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa nghệ sĩ '{artist.Name}'?",
                    "Xác nhận xóa",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        _databaseService.GetContext().Artists.Remove(artist);
                        _databaseService.GetContext().SaveChanges();
                        LoadArtists();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi xóa: {ex.Message}", "Lỗi",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void BtnAlbums_Click(object sender, RoutedEventArgs e)
        {
            EditAlbumWIndow editAlbumWindow = new EditAlbumWIndow();
            this.Hide();
            editAlbumWindow.Show();
        }
        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();

        }
    }
}
