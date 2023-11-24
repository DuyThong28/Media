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
            listMusic.DoubleTapped += MediaHelper.ListBox_DoubleTapped;
        }

        private void MenuItem_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            MediaHelper.AddMediaQueue_Click(sender, e);
        }
        private void PlayNext(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            MediaHelper.PlayNextInQueue(sender, e);
        }
    }
}
