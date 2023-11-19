using Avalonia.Controls;
using Media.Models;
using Media.ViewModels;
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
    }
}
