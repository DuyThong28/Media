using Avalonia.Media;
using Media.Models;
using ReactiveUI;
using System;
using System.IO;

namespace Media.ViewModels
{
    public class AddAlbumWindowViewModel : ViewModelBase
    {
        private Playlist playlist = null;
        public IImage BackGroundImage
        {
            get
            {
                if (playlist == null)
                {
                    string fileName = Path.Combine(Environment.CurrentDirectory, "Default Image", "defaultImage.jpg");
                    if (File.Exists(fileName))
                        
                        return new Avalonia.Media.Imaging.Bitmap(fileName);
                    else
                        return null;
                }
                else
                {
                    return playlist.BackGroundImage;
                }               
            }
        }
        public Playlist Playlist { get => playlist; set { this.RaiseAndSetIfChanged(ref playlist, value); } }
        public AddAlbumWindowViewModel(Playlist playlist = null)
        {
            this.playlist = playlist;
        }
    }
}
