using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using System.Windows.Input;

namespace Media.Views
{
    public class PlaylistItemTC : TemplatedControl
    {
        public static readonly StyledProperty<string> PlaylistNameProperty =
       AvaloniaProperty.Register<PlaylistItemTC, string>(
      nameof(PlaylistName),
      defaultValue: "");
        public string PlaylistName
        {
            get { return GetValue(PlaylistNameProperty); }
            set { SetValue(PlaylistNameProperty, value); }
        }
        public static readonly StyledProperty<string> CountMediasProperty =
            AvaloniaProperty.Register<PlaylistItemTC, string>(
                nameof(CountMedias),
                defaultValue: "");

        public string CountMedias
        {
            get { return GetValue(CountMediasProperty); }
            set => SetValue(CountMediasProperty, value);
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
            AvaloniaProperty.Register<PlaylistItemTC, IImage>(
                nameof(ImageSource));
        public IImage ImageSource
        {
            get => GetValue(ImageSourceProperty);
            set => SetValue(ImageSourceProperty, value);
        }


        public static readonly StyledProperty<object> CommandParameterProperty =
         AvaloniaProperty.Register<PlaylistItemTC, object>(
             nameof(CommandParameter),
             defaultValue: null);
        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public static readonly StyledProperty<ICommand> CommandProperty =
         AvaloniaProperty.Register<PlaylistItemTC, ICommand>(
             nameof(Command));
        public ICommand Command
        {
            get { return GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
    }
}
