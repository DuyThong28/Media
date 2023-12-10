using Avalonia.Controls;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Reactive;
using Avalonia.Interactivity;
using Media.Models;
using Media.ViewModels;
using System.Linq;
using System.Collections.Generic;

namespace Media.Views
{
    public partial class LibraryScreenControl : UserControl
    {
        public LibraryScreenControl()
        {
            InitializeComponent();
            lbLibrary.Tapped += LbLibrary_Tapped;
        }


        private void LbLibrary_Tapped(object? sender, Avalonia.Input.TappedEventArgs e)
        {
            (sender as ListBox).SelectedItem = MediaHelper.selectPlaylist((sender as ListBox).ItemsSource as List<Playlist>);
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
            RenameAlbum.Playlist = null;
            var mainWindow = MainWindow.GetInstance();
            AddAlbumWindow addAlbumWindow = new AddAlbumWindow();
            addAlbumWindow.ShowDialog(mainWindow);
        }
        private void DeleteAlbum_Click(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem menuItem && menuItem.Tag is Playlist playlistToDelete)
            {
                MediaHelper.DeletePlayList(playlistToDelete);
            }
        }
        private void RenameAlbum_Click(object sender, RoutedEventArgs e)
        {
            MediaHelper.RenameAlbum_Click(sender, e);
        }

        private void ComboBox_SelectionChanged(object? sender, Avalonia.Controls.SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (comboBox != null && comboBox.SelectedItem != null)
            {
                ComboBoxItem selectedItem = comboBox.SelectedItem as ComboBoxItem;
                if (selectedItem != null)
                {
                    TextBlock textBlock = selectedItem.Content as TextBlock;
                    if (textBlock != null)
                    {
                        string selectedText = textBlock.Text;
                        switch (selectedText)
                        {
                            case "Sort A-Z":
                                IEnumerable<IGrouping<char, Playlist>> resAToZ = MediaHelper.SortListAToZ(MediaHelper.AllPlayList);
                                List<Playlist> sortedListAToZ = resAToZ.SelectMany(group => group).ToList();
                                MediaHelper.AllPlayList = sortedListAToZ;
                                break;                        
                            case "Sort by Date":
                                IEnumerable<IGrouping<string, Playlist>> resDate = MediaHelper.SortListDateAdded(MediaHelper.AllPlayList);
                                List<Playlist> sortedListDate = resDate.SelectMany(group => group).ToList();
                                MediaHelper.AllPlayList = sortedListDate;
                                break;
                        }
                    }
                }
            }
        }
    }
}
