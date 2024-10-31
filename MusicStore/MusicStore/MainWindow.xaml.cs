using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using MusicStore;

namespace MusicStore
{

    public partial class MainWindow : Window
    {
        private readonly DatabaseService _databaseService;

        public MainWindow()
        {
            InitializeComponent();
            _databaseService = new DatabaseService();
            btnAlbums.Click += BtnAlbums_Click;
            btnArtists.Click += btnArtists_Click;
            btnExit.Click += BtnExit_Click;
        }

        private void BtnAlbums_Click(object sender, RoutedEventArgs e)
        {
            EditAlbumWIndow editAlbumWindow = new EditAlbumWIndow();
            this.Hide();  
            editAlbumWindow.Closed += (s, args) => this.Show();  
            editAlbumWindow.Show();
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