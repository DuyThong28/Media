using Avalonia.Controls;
using Media.Models;
using Media.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Media.Views
{
    public partial class ListVideoSreenControl : UserControl
    {
        public ListVideoSreenControl()
        {
            InitializeComponent();
            listBoxVideo.Tapped += ListBoxVideo_Tapped;
        }

        private void ListBoxVideo_Tapped(object? sender, Avalonia.Input.TappedEventArgs e)
        {
            MediaHelper.ListBox_DoubleTapped(sender, e);
            listBoxVideo.SelectedIndex = -1;
        }

        private void MenuItem_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            MediaHelper.AddMediaQueue_Click(sender, e);
        }

        private void ComboBox_SelectionChanged(object? sender, Avalonia.Controls.SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (comboBox != null && comboBox.SelectedItem != null && listBoxVideo != null)
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
                                IEnumerable<IGrouping<char, MediaItem>> resAToZ = Playlist.SortListAToZ(MediaHelper.ListVideos);
                                List<MediaItem> sortedListAToZ = resAToZ.SelectMany(group => group).ToList();
                                    listBoxVideo.ItemsSource = sortedListAToZ;
                                break;
                            case "Sort by Author":
                                IEnumerable<IGrouping<string, MediaItem>> resArtists = Playlist.SortListArtists(MediaHelper.ListVideos);
                                List<MediaItem> sortedListArtists = resArtists.SelectMany(group => group).ToList();
                                listBoxVideo.ItemsSource = sortedListArtists;
                                break;
                            case "Sort by Date":
                                IEnumerable<IGrouping<string, MediaItem>> resDate = Playlist.SortListDateAdded(MediaHelper.ListVideos);
                                List<MediaItem> sortedListDate = resDate.SelectMany(group => group).ToList();
                                listBoxVideo.ItemsSource = sortedListDate;
                                break;
                        }
                    }
                }
            }
        }

        private void PlayNext(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            MediaHelper.PlayNextInQueue(sender, e);
        }

        private void MenuItem_Click_1(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            IList<object> list = menuItem.CommandParameter as IList<object>;
            if (list != null)
            {
                MediaItem mediaItem = list[0] as MediaItem;
                Playlist playlist = list[1] as Playlist;
                playlist.AddMedia(mediaItem);
            }
        }
    }
}
