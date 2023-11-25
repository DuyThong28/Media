using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;
using LibVLCSharp.Shared;
using Media.Models;
using Media.Views;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Windows.Input;
using TagLib;


namespace Media.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public static MainWindowViewModel Instance { get; set; }

        public readonly LibVLC _libVlc = new LibVLC(enableDebugLogs: true);
        public MediaPlayer MediaPlayer { get; set; } = new MediaPlayer(new LibVLC());
        public LibVLCSharp.Shared.Media media;

        public MainWindowViewModel()
        {
            PlayMedia._libVlc = _libVlc;
            PlayMedia.MediaPlayer = MediaPlayer;
            MediaHelper.FetchListMedia(MediaTypes.Audio);
            MediaHelper.FetchListMedia(MediaTypes.Video);
            PlayMedia.UpdateScreen += MediaHelper.UpdateScreen;
            PlayMedia.timer.Tick += MediaHelper.UpdateMediaControl;
            var ListMedia = new List<Playlist>(MediaHelper.AllPlayList);
            LibraryScreenViewModel.ListMedia = ListMedia;
            HomeScreenViewModel.ListSongs = MediaHelper.ListSongs;
            HomeScreenViewModel.ListVideos = MediaHelper.ListVideos;
            ListMediaScreenViewModel.ListSongs = MediaHelper.ListSongs;
            ListVideoScreenViewModel.ListVideos = MediaHelper.ListVideos;
            SearchScreenViewModel.AllMedias = MediaHelper.AllMedias;
            PlayMedia._libVlc = _libVlc;
            PlayMedia.MediaPlayer = MediaPlayer;
            //
            MediaHelper.AllPlayListChanged += MediaHelper_AllPlayListChanged;
            MediaHelper.ListSongsChanged += MediaHelper_ListSongsChanged;
            MediaHelper.ListVideosChanged += MediaHelper_ListVideosChanged;
            //
        }

        private void MediaHelper_ListVideosChanged(object? sender, EventArgs e)
        {
            ListVideoScreenViewModel.ListVideos = MediaHelper.ListVideos;
        }

        private void MediaHelper_ListSongsChanged(object? sender, EventArgs e)
        {
            ListMediaScreenViewModel.ListSongs = MediaHelper.ListSongs;
        }

        //
        private void MediaHelper_AllPlayListChanged(object sender, EventArgs e)
        {
            var ListMedia = new List<Playlist>(MediaHelper.AllPlayList);
            LibraryScreenViewModel.ListMedia = ListMedia;
        }
        //
        //public AddAlbumWindowViewModel AddAlbumWindowViewModel { get; set; } = new AddAlbumWindowViewModel();
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