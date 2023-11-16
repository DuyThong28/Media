using Avalonia;
using Avalonia.Controls;
using Media.ViewModels;
using System;
using System.Collections.Generic;

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
            if (buttonClicked != null)
            {
                buttonClicked(sender, e);
            }
        }

        private  event EventHandler buttonClicked;
        public  event EventHandler ButtonClicked
        {
            add { buttonClicked += value; }
            remove { buttonClicked -= value; }
        }

        public void Slider_PointerCaptureLost(object? sender, Avalonia.Input.PointerCaptureLostEventArgs e)
        {
            PlayMedia.setCurrentPosition(Convert.ToInt32(mediaTrackBar.Value));
        }

        private void Slider_ValueChanged(object? sender, Avalonia.Controls.Primitives.RangeBaseValueChangedEventArgs e)
        {
            if (slVolume.Value == 0)
            {
                btn_Volume_img.Classes.Remove("unMute");
                btn_Volume_img.Classes.Add("mute");
            }
            else
            {
                btn_Volume_img.Classes.Remove("mute");
                btn_Volume_img.Classes.Add("unMute");
            }
        }

        private void Button_Click_1(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
                if (PlayMedia.Repeat == RepeatMode.Off)
                {
                btn_repeat_img.Classes.Add("repeatOn");
                btn_repeat_img.Classes.Remove("repeat");
                btn_repeat_img.Classes.Remove("repeatOne");
            }
                else if (PlayMedia.Repeat == RepeatMode.All)
                {
                btn_repeat_img.Classes.Remove("repeatOn");
                btn_repeat_img.Classes.Remove("repeat");
                btn_repeat_img.Classes.Add("repeatOne");
                }
                else if (PlayMedia.Repeat == RepeatMode.One)
                {
                btn_repeat_img.Classes.Remove("repeatOn");
                btn_repeat_img.Classes.Remove("repeatOne");
                btn_repeat_img.Classes.Add("repeat");

            }
        }

        private void Button_Click_2(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (PlayMedia.Suffle)
            {
                btn_suff_img.Classes.Add("notSuff");
                btn_suff_img.Classes.Remove("suff");
            }
            else
            {
                btn_suff_img.Classes.Remove("notSuff");
                btn_suff_img.Classes.Add("suff");
            }
        }

        private void Button_Click_3(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
        }
    }
}
