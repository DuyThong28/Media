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
    }
}
