using Avalonia.Controls;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Reactive;

namespace Media.Views
{
    public partial class LibraryScreenControl : UserControl
    {
        public LibraryScreenControl()
        {
            InitializeComponent();
        }

        private void ListBox_DoubleTapped(object? sender, Avalonia.Input.TappedEventArgs e)
        {
            if (selectPlaylist!=null)
            {
                selectPlaylist(sender, new EventArgs());
            }
        }

        private event EventHandler selectPlaylist;
        public event EventHandler SelectPlaylist
        {
            add { selectPlaylist += value; }
            remove { selectPlaylist -= value; }
        }
    }
}
