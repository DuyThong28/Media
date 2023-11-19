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
            Playlist playlist = new Playlist() { ListMedia = (MediaHelper.listVideos).Concat(MediaHelper.listSongs).ToList(), PlayListName = "NewJeans 1st EP'New Jeans", BackroundImageFileName = @"C:\Users\duyth\Downloads\New_Jeans_(EP).jpg" };
            Playlist playlist2 = new Playlist() { ListMedia = (MediaHelper.listVideos), PlayListName = "Blackpink", BackroundImageFileName = @"C:\Users\duyth\Downloads\maxresdefault.jpg" };
            Playlist playlist3 = new Playlist() { ListMedia = (MediaHelper.listSongs), PlayListName = "Itzy Dalla Dalla", BackroundImageFileName = @"C:\Users\duyth\Downloads\Itzy.jpg" };
            MediaHelper.AddPlayList(playlist);
            MediaHelper.AddPlayList(playlist2);
            MediaHelper.AddPlayList(playlist3);
            SearchScreenViewModel.AllMedias = MediaHelper.AllMedias;
            LibraryScreenViewModel.ListMedia = MediaHelper.AllPlayList;
            HomeScreenViewModel.ListSongs = MediaHelper.listSongs;
            HomeScreenViewModel.ListVideos = MediaHelper.listVideos;
            ListMediaScreenViewModel.ListSongs = MediaHelper.listSongs;
            ListVideoScreenViewModel.ListVideos = MediaHelper.listVideos;
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