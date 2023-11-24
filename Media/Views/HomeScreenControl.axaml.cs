using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Interactivity;
using Avalonia.Threading;
using Media.Models;
using Media.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

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
            MediaHelper.AddMediaQueue_Click(sender, e);
        }
        private void MenuItem_Click_2(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            IList<object> list = menuItem.CommandParameter as IList<object>;
            if (list!=null)
            {
                MediaItem mediaItem = list[0] as MediaItem;
                Playlist playlist = list[1] as Playlist;
                playlist.AddMedia(mediaItem);              
            }
        }

        private void ListBox_SelectionChanged(object? sender, Avalonia.Controls.SelectionChangedEventArgs e)
        {
            ListBox listBox = sender as ListBox;
            DoSomethingAfterTimeoutAsync(listBox);
        }

        private async Task DoSomethingAfterTimeoutAsync(ListBox listBox)
        {
            await Task.Delay(TimeSpan.FromSeconds(5));
            Random random = new Random();
            if (listBox.SelectedIndex != -1)
            {
                listBox.SelectedIndex = random.Next(0, listBox.Items.Count);
            }
        }

        private void PlayNext(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            MediaHelper.PlayNextInQueue(sender, e);
        }
    }
}
