using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Media;
using System;
using System.Windows.Input;
using TagLib.Flac;
using Media.Models;
using Media.Views;
using Media.Models;
using ReactiveUI;
using Media.ViewModels;

namespace Media.Views
{
    public class MusicRowTC : UserControl
    {
        public static readonly StyledProperty<string> MediaNameProperty =
         AvaloniaProperty.Register<MusicRowTC, string>(
        nameof(MediaName),
        defaultValue: "");
        public string MediaName
        {
            get { return GetValue(MediaNameProperty); }
            set { SetValue(MediaNameProperty, value); }
        }
        
        public static readonly StyledProperty<string> AuthorNameProperty =
            AvaloniaProperty.Register<MusicRowTC, string>(
                nameof(AuthorName),
                defaultValue: "");

        public string AuthorName
        {
            get { return GetValue(AuthorNameProperty); }
            set => SetValue(AuthorNameProperty, value);
        }

        public static readonly StyledProperty<string> DurationTextProperty =
            AvaloniaProperty.Register<MusicRowTC, string>(
                nameof(DurationText),
                defaultValue: "");

        public string DurationText
        {
            get { return GetValue(DurationTextProperty); }
            set => SetValue(DurationTextProperty, value);
        } 
        
        public static readonly StyledProperty<string> DateAddedProperty =
            AvaloniaProperty.Register<MusicRowTC, string>(
                nameof(DateAdded),
                defaultValue: "");

        public string DateAdded
        {
            get { return GetValue(DateAddedProperty); }
            set => SetValue(DateAddedProperty, value);
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
             nameof(Command), defaultValue: ReactiveCommand.Create(() => { PlayMedia.playSong(); }));

        public ICommand Command
        {
            get { return GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
    }
}
