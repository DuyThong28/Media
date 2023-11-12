using Avalonia.Controls;
using Media.Models;
using Media.ViewModels;
using ReactiveUI;

namespace Media.Views
{
    public partial class ListMediaScreenControl : UserControl
    {
        public ListMediaScreenControl()
        {

            InitializeComponent();
        }

        private void ListBox_SelectionChanged(object? sender, Avalonia.Controls.SelectionChangedEventArgs e)
        {
            ListBox newListBoxItem = (sender as ListBox);
            ListBoxItem media = newListBoxItem.SelectedItem as ListBoxItem;
        }

    }
}
