using Avalonia.Controls;
using Media.ViewModels;

namespace Media.Views
{
    public partial class SearchScreenControl : UserControl
    {
        public SearchScreenControl()
        {
            InitializeComponent();
            listBoxMedia.Tapped += ListBoxMedia_Tapped;
        }

        private void ListBoxMedia_Tapped(object? sender, Avalonia.Input.TappedEventArgs e)
        {
            MediaHelper.ListBox_DoubleTapped(sender, e);
            listBoxMedia.SelectedIndex = -1;
        }
    }
}
