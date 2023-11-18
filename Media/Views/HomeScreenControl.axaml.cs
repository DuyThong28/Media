using Avalonia.Controls;
using Media.Models;
using Media.ViewModels;
using System;

namespace Media.Views
{
    public partial class HomeScreenControl : UserControl
    {
        public HomeScreenControl()
        {
            InitializeComponent();
            listBoxVideo.DoubleTapped += MediaHelper.ListBox_DoubleTapped;
            listMusic.DoubleTapped += MediaHelper.ListBox_DoubleTapped;
        }
    }
}
