using Avalonia.Controls;
using Media.Models;
using Media.ViewModels;
using System;
using System.Collections.Generic;

namespace Media.Views
{
    public partial class PlayingScreenControl : UserControl
    {
        public PlayingScreenControl()
        {
            InitializeComponent();
            listBoxMedia.DoubleTapped += MediaHelper.ListBox_DoubleTapped;
        }

        private void PlayNext(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            MediaHelper.PlayNextInQueue(sender, e);
        }

        private void MenuItem_Click_1(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            MediaHelper.MenuItem_Click_1(sender, e);
        }
    }
}
