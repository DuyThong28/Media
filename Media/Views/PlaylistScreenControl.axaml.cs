using Avalonia.Controls;
using Avalonia.Media;
using Media.Models;
using Media.ViewModels;
using System.Collections.Generic;
using System.Numerics;

namespace Media.Views
{
    public partial class PlaylistScreenControl : UserControl
    {
        public PlaylistScreenControl()
        {
            InitializeComponent();
            listMedia.DoubleTapped += MediaHelper.ListBox_DoubleTapped;
        }

        private void ScrollViewer_ScrollChanged(object? sender, Avalonia.Controls.ScrollChangedEventArgs e)
        {
            double Y = (sender as ScrollViewer).Offset.Y;
            if (Y > 320)
            {
                playBtn.IsVisible = true;
            } else
            {
                playBtn.IsVisible = false;
            }
        }

        private void MenuItem_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            IList<object> list = menuItem.CommandParameter as IList<object>;
            if (list != null)
            {
                MediaItem mediaItem = list[1] as MediaItem;
                if (!mediaItem.IsPlay)
                {
                    PlaylistScreenViewModel playlistscreen = list[0] as PlaylistScreenViewModel;
                    playlistscreen.Playlist.DeleteMedia(mediaItem);
                    playlistscreen.ListMedia.Remove(mediaItem);
                }
            }
        }
    }
}
