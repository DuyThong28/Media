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
            Playlist playlist = new Playlist();
            MediaItem item = new MediaItem(@"C:\Users\lenovo\Downloads\TungQuen-WrenEvansitsnk-12038297.mp3");
            MediaItem item1 = new MediaItem(@"C:\Users\lenovo\Downloads\ThangDien-PhuongLyJustaTee-5774043.mp3");
            MediaItem item2 = new MediaItem(@"C:\Users\lenovo\Downloads\Id072019-WN-10597501.mp3");
            playlist.AddMedia(item);
            playlist.AddMedia(item1);
            playlist.AddMedia(item2);
            MediaHelper.AddPlayList(playlist);
            MediaHelper.DeleteMediaFromPlaylist(item.FilePath, playlist.PlayListID);
            LibraryScreenViewModel.ListMedia = new List<Playlist> { playlist, new Playlist() { ListMedia = MediaHelper.listSongs } };
            HomeScreenViewModel.ListSongs = MediaHelper.listSongs;
            HomeScreenViewModel.ListVideos = MediaHelper.listVideos;
            ListMediaScreenViewModel.ListSongs = MediaHelper.listSongs;
            ListVideoScreenViewModel.ListVideos = MediaHelper.listVideos;
            //MediaControlViewModel.getPathOfSong(new MediaItem(@"C:\Users\lenovo\Music\Tuy-Am-Xesi-Masew-Nhat-Nguyen.mp3"));
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