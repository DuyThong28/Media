using Avalonia.Controls;
using Avalonia.Interactivity;
using System.IO;
using Media.Models;
using Media.ViewModels;
using Avalonia.Media.Imaging;

namespace Media.Views;

public partial class AddAlbumWindow : Window
{
    public AddAlbumWindow()
    {
        InitializeComponent();
        this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        this.DataContext = new AddAlbumWindowViewModel(RenameAlbum.Playlist);
    }
    private string imagePath;

    [System.Obsolete]
    private async void SelectImage_Click(object sender, RoutedEventArgs e)
    {
        OpenFileDialog dialog = new OpenFileDialog();
        dialog.Title = "Choose image";
        dialog.Filters.Add(new FileDialogFilter() { Name = "Ảnh", Extensions = { "png", "jpg", "jpeg", "gif", "bmp" } });
        
        string[] result = await dialog.ShowAsync(this);
        if (result != null && result.Length > 0)
        {
            using (FileStream stream = File.OpenRead(result[0]))
            {
                Bitmap bitmap = new Bitmap(stream);
                Avalonia.Media.Imaging.Bitmap bmp = null;
                using (MemoryStream ms = new MemoryStream())
                {
                    bitmap.Save(ms);
                    ms.Position = 0;
                    bmp = new Avalonia.Media.Imaging.Bitmap(ms);
                }
                image.Source =  bmp;
                imagePath = result[0];
            }
        }
    }
    private void AddAlbum_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is AddAlbumWindowViewModel addAlbumWindowViewModel)
        {
            if (addAlbumWindowViewModel.Playlist != null) 
            {
                string albumName = AlbumNameTextBox.Text;
                if (albumName == null || string.IsNullOrWhiteSpace(albumName))  
                    albumName = "Unnamed";
                string imagePath;
                if (this.imagePath != null)
                    imagePath = this.imagePath;
                else
                    imagePath = addAlbumWindowViewModel.Playlist.BackroundImageFileName;                   
                Playlist playlist = new Playlist(null, albumName, imagePath, null);
                playlist.ListMedia = RenameAlbum.Playlist.ListMedia;
                MediaHelper.AllPlayList.Remove(RenameAlbum.Playlist);
                MediaHelper.Database.DeletePlaylist(RenameAlbum.Playlist.PlayListID);
                RenameAlbum.Playlist = playlist;
                MediaHelper.AddPlayList(playlist);
                if (RenameAlbum.Playlistscreen != null)
                    RenameAlbum.Playlistscreen.Playlist = RenameAlbum.Playlist;
            }
            else
            {
                string albumName = AlbumNameTextBox.Text;
                if (albumName == null || string.IsNullOrWhiteSpace(albumName))
                    albumName = "Unnamed";
                string imagePath = this.imagePath;
                Playlist playlist = new Playlist(null, albumName, imagePath, null);
                MediaHelper.AddPlayList(playlist);
            }

           
        }
        this.Close();
    }
    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }

}