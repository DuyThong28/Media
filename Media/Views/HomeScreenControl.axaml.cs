using Avalonia.Controls;
using Avalonia.Interactivity;
using Media.Models;
using Media.ViewModels;
using System;

namespace Media.Views
{
    public partial class HomeScreenControl : UserControl
    {
        public HomeScreenControl()
        {
            InitializeComponent();
            listBoxVideo.DoubleTapped += MediaHelper.ListBox_DoubleTapped;
            listMusic.DoubleTapped += MediaHelper.ListBox_DoubleTapped;
        }

        private static event EventHandler seeAllSongs;
        public static event EventHandler SeeAllSongs

        {
            add { seeAllSongs += value; }
            remove { seeAllSongs -= value;}
        }

        private static event EventHandler seeAllVideos;
        public static event EventHandler SeeAllVideos
        {
            add { seeAllVideos += value; }
            remove { seeAllVideos -= value;}
        }

        private void Button_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (seeAllVideos != null)
                seeAllVideos(sender, new EventArgs());
        }

        private void Button_Click_1(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
           if (seeAllSongs != null)
                seeAllSongs(sender, new EventArgs());
        }
        private void AddMediaQueue_Click(object sender, RoutedEventArgs e)
        {
            bool isplay = false;
            if (sender is MenuItem menuItem)
            {             
                if (menuItem.DataContext is MediaItem mediaItem)
                {
                    if (!mediaItem.MediaAdded)
                    {
                        mediaItem.MediaAdded = true;                                             
                        for (int i = 0; i < MediaHelper.PlayQueue.Count; i++)
                        {
                            if (MediaHelper.PlayQueue[i].IsPlay)
                            {
                                MediaHelper.PlayQueue.Insert(i + 1, mediaItem);
                                break;
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < MediaHelper.PlayQueue.Count; i++)
                        {
                            if (mediaItem.FilePath == MediaHelper.PlayQueue[i].FilePath)
                            {
                                isplay = MediaHelper.PlayQueue[i].IsPlay;
                                if (!isplay)
                                    MediaHelper.PlayQueue.Remove(MediaHelper.PlayQueue[i]);
                                break;
                            }
                        }
                        if (!isplay)
                        {
                            for (int i = 0; i < MediaHelper.PlayQueue.Count; i++)
                            {
                                if (MediaHelper.PlayQueue[i].IsPlay)
                                {
                                    MediaHelper.PlayQueue.Insert(i + 1, mediaItem);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
