using Avalonia.Controls;
using Avalonia.Input;
using Media.ViewModels;

namespace Media.Views
{
    public partial class SearchScreenControl : UserControl
    {
        public SearchScreenControl()
        {
            InitializeComponent();
            listBoxMedia.Tapped += ListBoxMedia_Tapped;
            listBoxMedia.PointerCaptureLost += ListBoxMedia_PointerCaptureLost;
        }

        private void ListBoxMedia_PointerCaptureLost(object? sender, PointerCaptureLostEventArgs e)
        {
            (sender as ListBox).SelectedIndex = -1;

        }


        private void ListBoxMedia_Tapped(object? sender, Avalonia.Input.TappedEventArgs e)
        {
            MediaHelper.ListBox_DoubleTapped(sender, e);
            listBoxMedia.SelectedIndex = -1;
        }
    }
}
