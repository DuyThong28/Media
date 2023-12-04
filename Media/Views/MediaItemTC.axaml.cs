using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using System.Windows.Input;

namespace Media.Views
{
    public class MediaItemTC : TemplatedControl
    {
        public static readonly StyledProperty<string> MediaNameProperty =
         AvaloniaProperty.Register<MediaItemTC, string>(
        nameof(MediaName),
        defaultValue: "");
        public string MediaName
        {
            get { return GetValue(MediaNameProperty); }
            set { SetValue(MediaNameProperty, value); }
        }
        public static readonly StyledProperty<string> AuthorNameProperty =
            AvaloniaProperty.Register<MediaItemTC, string>(
                nameof(AuthorName),
                defaultValue: "");

        public string AuthorName
        {
            get { return GetValue(AuthorNameProperty); }
            set => SetValue(AuthorNameProperty, value);
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
            AvaloniaProperty.Register<MediaItemTC, IImage>(
                nameof(ImageSource));
        public IImage ImageSource
        {
            get => GetValue(ImageSourceProperty);
            set => SetValue(ImageSourceProperty, value);
        }


        public static readonly StyledProperty<object> CommandParameterProperty =
         AvaloniaProperty.Register<MediaItemTC, object>(
             nameof(CommandParameter),
             defaultValue: null);
        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public static readonly StyledProperty<ICommand> CommandProperty =
         AvaloniaProperty.Register<MediaItemTC, ICommand>(
             nameof(Command));
        public ICommand Command
        {
            get { return GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly StyledProperty<string> DurationTextProperty =
    AvaloniaProperty.Register<MusicRowTC, string>(
        nameof(DurationText),
        defaultValue: "00:00");

        public string DurationText
        {
            get { return GetValue(DurationTextProperty); }
            set => SetValue(DurationTextProperty, value);
        }
    }
}
