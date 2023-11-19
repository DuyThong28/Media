using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Interactivity;
using System.IO;
using System.Drawing;
using Media.Models;
using Media.ViewModels;

namespace Media;

public partial class AddAlbumWindow : Window
{
    public AddAlbumWindow()
    {
        InitializeComponent();
        this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
    }
    private string imagePath;
    private async void SelectImage_Click(object sender, RoutedEventArgs e)
    {
        OpenFileDialog dialog = new OpenFileDialog();
        dialog.Title = "Chọn Ảnh Bìa";
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
                    bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    ms.Position = 0;
                    bmp = new Avalonia.Media.Imaging.Bitmap(ms);
                }
                ImageButton.Content = new Avalonia.Controls.Image { Source = bmp };
                imagePath = result[0];
            }
        }
    }
    private void AddAlbum_Click(object sender, RoutedEventArgs e)
    {
        string albumName = AlbumNameTextBox.Text;
        string imagePath = this.imagePath;
        Playlist playlist = new Playlist(null, albumName, imagePath, null);
        MediaHelper.AddPlayList(playlist);
        this.Close();
    }
    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }

}