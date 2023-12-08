using Avalonia.Controls;
using Avalonia.Interactivity;
using Media.ViewModels;

namespace Media.Views
{
    public partial class PlaylistScreenControl : UserControl
    {
        public PlaylistScreenControl()
        {
            InitializeComponent();
            listMedia.Tapped += MediaHelper.ListBox_DoubleTapped;
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
           MediaHelper.MenuItem_Click(sender, e);
        }
        private void AddMediaQueue_Click(object sender, RoutedEventArgs e)
        {
            MediaHelper.AddMediaQueue_Click(sender, e);
        }
        private void PlayNext(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            MediaHelper.PlayNextInQueue(sender, e);
        }

        private void Button_Click_1(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var playlistDataContext = ((sender as Button).DataContext as PlaylistScreenViewModel);
            MediaHelper.RenameAlbum_Click(playlistDataContext, e);
        }
    }
}
