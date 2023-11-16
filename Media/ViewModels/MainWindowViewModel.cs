using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;
using Media.Models;
using Media.Views;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Windows.Input;
using TagLib;

namespace Media.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public static MainWindowViewModel Instance { get; set; }
        public MainWindowViewModel()
        {
            MediaHelper.FetchListMedia(MediaTypes.Audio);
            MediaHelper.FetchListMedia(MediaTypes.Video);
            LibraryScreenViewModel.ListMedia = new List<Playlist> { new Playlist() { ListMedia = MediaHelper.listSongs },
                new Playlist() { ListMedia = MediaHelper.listSongs },
                new Playlist(){ ListMedia = new List<MediaItem>()},
                new Playlist(){ ListMedia = MediaHelper.listSongs },
                new Playlist(){ ListMedia = MediaHelper.listSongs },
                new Playlist(){ ListMedia = MediaHelper.listSongs },
                new Playlist(){ ListMedia = MediaHelper.listSongs },
                new Playlist(){ ListMedia = MediaHelper.listSongs },
                new Playlist(){ ListMedia = MediaHelper.listSongs },
                new Playlist(){ ListMedia = MediaHelper.listSongs },
                new Playlist(){ ListMedia = MediaHelper.listSongs },
                new Playlist(){ ListMedia = MediaHelper.listSongs },
                new Playlist(){ ListMedia = MediaHelper.listSongs },
                new Playlist(){ ListMedia = MediaHelper.listSongs },
                new Playlist(){ ListMedia = MediaHelper.listSongs },
                new Playlist(){ ListMedia = MediaHelper.listSongs } };
            HomeScreenViewModel.ListSongs = MediaHelper.listSongs;
            HomeScreenViewModel.ListVideos = MediaHelper.listVideos;
            ListMediaScreenViewModel.ListSongs = MediaHelper.listSongs;
            ListVideoScreenViewModel.ListVideos = MediaHelper.listVideos;
            MediaControlViewModel.getPathOfSong(new MediaItem(@"C:\Users\lenovo\Music\Tuy-Am-Xesi-Masew-Nhat-Nguyen.mp3"));
            //PlayMedia.Path = @"C:\Users\duyth\OneDrive\Máy tính\Media\ChanAi-OrangeKhoi-6225088.mp3";
            //PlayMedia.playSong();
        }

        public MediaControlViewModel MediaControlViewModel { get; set; } = new MediaControlViewModel();
        public HomeScreenViewModel HomeScreenViewModel { get; set; } = new HomeScreenViewModel();
        public ListMediaScreenViewModel ListMediaScreenViewModel { get; set; } = new ListMediaScreenViewModel();
        public ListVideoScreenViewModel ListVideoScreenViewModel { get; set; } = new ListVideoScreenViewModel();     
        public PlaylistScreenViewModel PlaylistScreenViewModel { get; set; } = new PlaylistScreenViewModel();
        public PlayingScreenViewModel PlayingScreenViewModel { get; set; } = new PlayingScreenViewModel();
        public LibraryScreenViewModel LibraryScreenViewModel { get; set; } = new LibraryScreenViewModel();
        public SettingScreenViewModel SettingScreenViewModel { get; set; } = new SettingScreenViewModel();
        public SearchScreenViewModel SearchScreenViewModel { get; set; } = new SearchScreenViewModel();
        public NavBarControlViewModel NavBarControlViewModel { get; set; } = new NavBarControlViewModel();
    }
}