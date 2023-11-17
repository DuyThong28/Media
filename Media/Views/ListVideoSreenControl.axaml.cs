using Avalonia.Controls;
using Media.Models;

namespace Media.Views
{
    public partial class ListVideoSreenControl : UserControl
    {
        public ListVideoSreenControl()
        {
            InitializeComponent();
        }

        private void ListBox_DoubleTapped(object? sender, Avalonia.Input.TappedEventArgs e)
        {
            MediaItem media = (sender as ListBox).SelectedItem as MediaItem;
            media.PlayMediaCommand();
        }
    }
}
