using Avalonia.Controls;
using System;

namespace Media.Views
{
    public partial class MediaControl : UserControl
    {
        public MediaControl()
        {
            InitializeComponent();
        }

        private void Button_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if(buttonClicked!=null)
            {
                buttonClicked(sender, e);
            }
        }

        private event EventHandler buttonClicked;
        public event EventHandler ButtonClicked
        {
            add { buttonClicked += value; }
            remove { buttonClicked -= value; }
        }
    }
}
