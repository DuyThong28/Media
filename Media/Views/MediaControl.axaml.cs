using Avalonia.Controls;
using Avalonia.Interactivity;
using Media.ViewModels;
using System;

namespace Media.Views
{
    public partial class MediaControl : UserControl
    {
        WindowState prevWindowState { get; set; }
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

        public void UpdateRepeatBtn(object? sender, RoutedEventArgs e)
        {
            if (PlayMedia.Repeat == RepeatMode.Off)
            {
                btn_repeat_img.Classes.Remove("repeatOn");
                btn_repeat_img.Classes.Remove("repeatOne");
                btn_repeat_img.Classes.Add("repeat");
                
            }
            else if (PlayMedia.Repeat == RepeatMode.All)
            {
                btn_repeat_img.Classes.Add("repeatOn");
                btn_repeat_img.Classes.Remove("repeat");
                btn_repeat_img.Classes.Remove("repeatOne");
            }
            else if (PlayMedia.Repeat == RepeatMode.One)
            {
                btn_repeat_img.Classes.Remove("repeatOn");
                btn_repeat_img.Classes.Remove("repeat");
                btn_repeat_img.Classes.Add("repeatOne");

            }
        }

        public void UpdateSuftbtn(object ? sender, RoutedEventArgs e)
        {
            if (PlayMedia.Suffle)
            {
                btn_suff_img.Classes.Remove("notSuff");
                btn_suff_img.Classes.Add("suff");
            }
            else
            {
                btn_suff_img.Classes.Add("notSuff");
                btn_suff_img.Classes.Remove("suff");
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

        HotKeyManager k;
        private void Button_Click_4(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            Window topLevel = MainWindow.GetInstance();

            if (topLevel != null)
            {
                if (topLevel.WindowState != WindowState.FullScreen)
                {
                    prevWindowState = topLevel.WindowState;
                    topLevel.WindowState = WindowState.FullScreen;
                }
                else
                {
                    topLevel.WindowState = prevWindowState;
                }
            }

        }

       
    }
}
