using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Threading;
using LibVLCSharp.Shared;
using Media.Models;
using Media.ViewModels;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Media.Views
{
    public partial class HomeScreenControl : UserControl
    {
        public HomeScreenControl()
        {
            InitializeComponent();
            listBoxVideo.Tapped += ListBoxVideo_Tapped1;
            listMusic.Tapped += ListBoxVideo_Tapped1;
            listBoxVideo.PointerExited += ListBoxVideo_PointerExited;
            listMusic.PointerExited += ListBoxVideo_PointerExited;
            string x = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        private void ListBoxVideo_PointerExited(object? sender, PointerEventArgs e)
        {
            (sender as ListBox).SelectedIndex = -1;
        }

        private void ListBoxVideo_Tapped1(object? sender, Avalonia.Input.TappedEventArgs e)
        {
            MediaHelper.Play_New_Queue(sender, e);
            listBoxVideo.SelectedIndex = -1;
            listMusic.SelectedIndex = -1;
        } 

        private void Play_Menu_Click(object? sender, RoutedEventArgs e)
        {
            MediaHelper.Play_New_Queue(sender,  null);
            listBoxVideo.SelectedIndex = -1;
            listMusic.SelectedIndex = -1;
        }
        
        private void Item_Tapped1(object? sender, Avalonia.Input.TappedEventArgs e)
        {
            MediaHelper.Play_New_Queue(sender, null);
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
            
            Random random = new Random();
            int currentIndex = listBox.SelectedIndex;
            int randomNumber = random.Next(0, listBox.Items.Count);
            do
            {
                randomNumber = random.Next(0, listBox.Items.Count);
            } while (randomNumber == currentIndex);
            if (listBox.SelectedIndex != -1)
            {
                await Task.Delay(TimeSpan.FromSeconds(4));
                listBox.SelectedIndex = randomNumber;
            }
        }

        private void PlayNext(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            MediaHelper.PlayNextInQueue(sender, e);
        }
    }
}
