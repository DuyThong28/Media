using Avalonia.Controls;
using Media.Models;
using Media.ViewModels;

namespace Media.Views
{
    public partial class SearchScreenControl : UserControl
    {
        public SearchScreenControl()
        {
            InitializeComponent();
            listBoxMedia.DoubleTapped += MediaHelper.ListBox_DoubleTapped;
        }
    }
}
