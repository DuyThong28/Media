using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Layout;
using Media.Models;
using Media.ViewModels;
using System;

namespace Media.Views
{
    public partial class ListVideoSreenControl : UserControl
    {
        public ListVideoSreenControl()
        {
            InitializeComponent();
            listBoxVideo.DoubleTapped += MediaHelper.ListBox_DoubleTapped;
        }
    }
}
