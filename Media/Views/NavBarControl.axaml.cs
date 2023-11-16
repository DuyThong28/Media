using Avalonia.Controls;
using Media.ViewModels;
using ReactiveUI;
using System;

namespace Media.Views
{
    public partial class NavBarControl : UserControl
    {
        public NavBarControl()
        {
            InitializeComponent();
            
        }

        private event EventHandler navBarItemSelected;
        public event EventHandler NavBarItemSelected
        {
            add { navBarItemSelected += value; }
            remove { navBarItemSelected -= value; }
        }

        private void ListBox_SelectionChanged(object? sender, Avalonia.Controls.SelectionChangedEventArgs e)
        {
            if (navBarItemSelected != null)
            {
                navBarItemSelected(sender, new EventArgs());
            }
        }

    }
}
