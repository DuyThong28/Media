using Avalonia.Controls;
using Media.Models;
using Media.ViewModels;
using System;

namespace Media.Views
{
    public partial class PlayingScreenControl : UserControl
    {
        public PlayingScreenControl()
        {
            InitializeComponent();
            listBoxMedia.DoubleTapped += MediaHelper.ListBox_DoubleTapped;
        }
    }
}
