using Avalonia.Controls;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Reactive;
using Avalonia.Interactivity;
using Media.Models;
using Media.ViewModels;

namespace Media.Views
{
    public partial class LibraryScreenControl : UserControl
    {
        private bool isAddAlbumWindowOpen = false;
        public LibraryScreenControl()
        {
            InitializeComponent();
        }

        private void ListBox_DoubleTapped(object? sender, Avalonia.Input.TappedEventArgs e)
        {
            if (selectPlaylist != null)
            {
                selectPlaylist(sender, new EventArgs());
            }
        }

        private event EventHandler selectPlaylist;
        public event EventHandler SelectPlaylist
        {
            add { selectPlaylist += value; }
            remove { selectPlaylist -= value; }
        }
        private void AddAlbum_Click(object sender, RoutedEventArgs e)
        {
            if (!isAddAlbumWindowOpen)
            {
                AddAlbumWindow addAlbumWindow = new AddAlbumWindow();
                addAlbumWindow.Closed += (s, args) => isAddAlbumWindowOpen = false;
                addAlbumWindow.Show();
                isAddAlbumWindowOpen = true;
            }
        }
        private void DeleteAlbum_Click(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem menuItem && menuItem.Tag is Playlist playlistToDelete)
            {
                MediaHelper.DeletePlayList(playlistToDelete);
            }
        }
    }
}
