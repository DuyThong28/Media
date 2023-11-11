using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Media;
using System;
using System.Windows.Input;

namespace Media.Views
{
    public class SettingItemTC : TemplatedControl
    {
        public static readonly StyledProperty<string> ButtonContentProperty =
            AvaloniaProperty.Register<MusicRowTC, string>(
                nameof(ButtonContent),
                defaultValue: "");

        public string ButtonContent
        {
            get { return GetValue(ButtonContentProperty); }
            set => SetValue(ButtonContentProperty, value);
        }

        public static readonly StyledProperty<string> SettingContentProperty =
            AvaloniaProperty.Register<MusicRowTC, string>(
                nameof(SettingContent),
                defaultValue: "");

        public string SettingContent
        {
            get { return GetValue(SettingContentProperty); }
            set => SetValue(SettingContentProperty, value);
        }

        public static readonly StyledProperty<IImage> ImageSourceProperty =
            AvaloniaProperty.Register<MusicRowTC, IImage>(
                nameof(ImageSource));
        public IImage ImageSource
        {
            get => GetValue(ImageSourceProperty);
            set => SetValue(ImageSourceProperty, value);
        }


        public static readonly StyledProperty<object> CommandParameterProperty =
         AvaloniaProperty.Register<MusicRowTC, object>(
             nameof(CommandParameter),
             defaultValue: null);
        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public static readonly StyledProperty<ICommand> CommandProperty =
         AvaloniaProperty.Register<MusicRowTC, ICommand>(
             nameof(Command));
        public ICommand Command
        {
            get { return GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
    }
}

