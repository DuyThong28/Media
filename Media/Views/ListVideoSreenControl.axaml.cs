using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Layout;
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
            listBoxVideo.DoubleTapped += MediaHelper.ListBox_DoubleTapped;
        }

        private void MenuItem_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            MediaHelper.AddMediaQueue_Click(sender, e);
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
                                IEnumerable<IGrouping<char, MediaItem>> resAToZ = Playlist.SortListAToZ(MediaHelper.ListVideos);
                                List<MediaItem> sortedListAToZ = resAToZ.SelectMany(group => group).ToList();
                                MediaHelper.ListVideos = sortedListAToZ;
                                break;
                            case "Sort by Author":
                                IEnumerable<IGrouping<string, MediaItem>> resArtists = Playlist.SortListArtists(MediaHelper.ListVideos);
                                List<MediaItem> sortedListArtists = resArtists.SelectMany(group => group).ToList();
                                MediaHelper.ListVideos = sortedListArtists;
                                break;
                            case "Sort by Date":
                                IEnumerable<IGrouping<string, MediaItem>> resDate = Playlist.SortListDateAdded(MediaHelper.ListVideos);
                                List<MediaItem> sortedListDate = resDate.SelectMany(group => group).ToList();
                                MediaHelper.ListVideos = sortedListDate;
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
    }
}
