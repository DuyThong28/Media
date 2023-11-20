using Avalonia;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Platform.Storage;
using Media.ViewModels;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using TagLib;
using System.Diagnostics;
using NAudio.Wave;
using Media.Models;
using Avalonia.Markup.Xaml;
using LibVLCSharp.Shared;
using LibVLCSharp.Avalonia;

namespace Media.Views
{
    public partial class MainWindow : Window
    {
        private Media.ViewModels.VideoView _videoViewer;

        public MainWindowViewModel viewModel;
        public static MainWindow _this { get; set; }
        public MainWindow()
        {
            InitializeComponent();

            viewModel = new MainWindowViewModel();
            DataContext = viewModel;
            _videoViewer = this.Get<Media.ViewModels.VideoView> ("VideoViewer");
            _this = this;
            Opened += MainWindow_Opened;
            navBarControl.NavBarItemSelected += NavBarControl_RadioButtonChecked;
            mediaControl.ButtonClicked += MediaControl_ButtonClicked;
            videoControl.ButtonClicked += MediaControl_ButtonClicked;
            libraryScreen.SelectPlaylist += LibraryScreen_SelectPlaylist;
            MediaHelper.UpdatePlayingScreen += MediaHelper_UpdatePlayingScreen;
            MediaHelper.OpenVideoScreen += ListVideoSreenControl_OpenVideoScreen;
            SettingScreenViewModel.OpenSongFolder = ReactiveCommand.Create(() => ChooseSongPath());
            SettingScreenViewModel.OpenVideoFolder = ReactiveCommand.Create(() => ChooseVideoPath());
            HomeScreenControl.SeeAllSongs += HomeScreenControl_SeeAllSongs;
            HomeScreenControl.SeeAllVideos += HomeScreenControl_SeeAllVideos;
            PlaylistScreenViewModel.BackEvent += PlaylistScreenViewModel_BackEvent;
        }

        private void MainWindow_Opened(object? sender, System.EventArgs e)
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            if (_videoViewer != null && viewModel.MediaPlayer != null)
            {
                _videoViewer.MediaPlayer = PlayMedia.MediaPlayer;
                _videoViewer.MediaPlayer.SetHandle(_videoViewer.hndl);
            }
            videoDisplay.IsVisible = false;
            controlPanel.IsVisible = false;
            videoPlayingScreen.IsVisible = false;
        }

        private void PlaylistScreenViewModel_BackEvent(object? sender, EventArgs e)
        {
            homeScreen.IsVisible = false;
            musicScreen.IsVisible = false;
            videoScreen.IsVisible = false;
            //playingScreen.IsVisible = false;
            libraryScreen.IsVisible = true;
            playlistScreen.IsVisible = false;
            settingScreen.IsVisible = false;
            searchScreen.IsVisible = false;
        }

        private void HomeScreenControl_SeeAllVideos(object? sender, EventArgs e)
        {
            homeScreen.IsVisible = false;
            musicScreen.IsVisible = false;
            videoScreen.IsVisible = true;
            //playingScreen.IsVisible = false;
            libraryScreen.IsVisible = false;
            playlistScreen.IsVisible = false;
            settingScreen.IsVisible = false;
            searchScreen.IsVisible = false;
        }

        private void HomeScreenControl_SeeAllSongs(object? sender, EventArgs e)
        {
            homeScreen.IsVisible = false;
            musicScreen.IsVisible = true;
            videoScreen.IsVisible = false;
            //playingScreen.IsVisible = false;
            libraryScreen.IsVisible = false;
            playlistScreen.IsVisible = false;
            settingScreen.IsVisible = false;
            searchScreen.IsVisible = false;
        }

        private void ListVideoSreenControl_OpenVideoScreen(object? sender, EventArgs e)
        {
            mainScreen.IsVisible = false;
            mainPlayingScreen.IsVisible = true;
            videoPlayingScreen.IsVisible = true;
            controlPanel.IsVisible = true;
        }

        private void MediaHelper_UpdatePlayingScreen(object? sender, EventArgs e)
        {
            if (PlayMedia.IsPlayVideo == true)
            {
                videoDisplay.IsVisible = true;
                playingScreen.IsVisible = false;
            }
            else if (PlayMedia.IsPlayVideo == false)
            {
                videoDisplay.IsVisible = false;
                controlPanel.IsVisible = false;
                playingScreen.IsVisible = true;
            }
            if (PlayMedia.IsFirst == true)
            {
                mediaControl.IsEnabled = true;
            }
        }

        private void LibraryScreen_SelectPlaylist(object? sender, EventArgs e)
        {
            ListBox listPlaylist = sender as ListBox;
            Playlist selectedPlaylist;
            if (listPlaylist != null)
            {
                selectedPlaylist = listPlaylist.SelectedItem as Playlist;

            if (selectedPlaylist!=null && selectedPlaylist.ListMedia != null)
            {
                (playlistScreen.DataContext as PlaylistScreenViewModel).Playlist = selectedPlaylist;
            }
            }
            homeScreen.IsVisible = false;
            musicScreen.IsVisible = false;
            videoScreen.IsVisible = false;
            //playingScreen.IsVisible = false;
            libraryScreen.IsVisible = false;
            playlistScreen.IsVisible = true;
            settingScreen.IsVisible = false;
            searchScreen.IsVisible = false;
        }

        private void MediaControl_ButtonClicked(object? sender, EventArgs e)
        {
            mainScreen.IsVisible = !mainScreen.IsVisible;
            mainPlayingScreen.IsVisible = !mainPlayingScreen.IsVisible;
            videoPlayingScreen.IsVisible = !videoPlayingScreen.IsVisible;
            if(PlayMedia.IsPlayVideo==true) {
            controlPanel.IsVisible = !controlPanel.IsVisible;
            }
        }
        private void NavBarControl_RadioButtonChecked(object? sender, EventArgs e)
        {

            ListBox listBox = sender as ListBox;
            ListBoxItem listBoxItem = (ListBoxItem)listBox.SelectedItem;

            switch (listBoxItem.Name)
            {
                case "homeBtn":
                    homeScreen.IsVisible = true;
                    musicScreen.IsVisible = false;
                    videoScreen.IsVisible = false;
                    //playingScreen.IsVisible = false;
                    libraryScreen.IsVisible = false;
                    playlistScreen.IsVisible = false;
                    settingScreen.IsVisible = false;
                    searchScreen.IsVisible = false;
                    break;
                case "musicBtn":
                    homeScreen.IsVisible = false;
                    musicScreen.IsVisible = true;
                    videoScreen.IsVisible = false;
                    //playingScreen.IsVisible = false;
                    libraryScreen.IsVisible = false;
                    playlistScreen.IsVisible = false;
                    settingScreen.IsVisible = false;
                    searchScreen.IsVisible = false;
                    break;
                case "videoBtn":
                    homeScreen.IsVisible = false;
                    musicScreen.IsVisible = false;
                    videoScreen.IsVisible = true;
                    //playingScreen.IsVisible = false;
                    libraryScreen.IsVisible = false;
                    playlistScreen.IsVisible = false;
                    settingScreen.IsVisible = false;
                    searchScreen.IsVisible = false;
                    break;
                case "searchBtn":
                    homeScreen.IsVisible = false;
                    musicScreen.IsVisible = false;
                    videoScreen.IsVisible = false;
                    //playingScreen.IsVisible = false;
                    libraryScreen.IsVisible = false;
                    playlistScreen.IsVisible = false;
                    settingScreen.IsVisible = false;
                    searchScreen.IsVisible = true;
                    break;
                case "libraryBtn":
                    homeScreen.IsVisible = false;
                    musicScreen.IsVisible = false;
                    videoScreen.IsVisible = false;
                    //playingScreen.IsVisible = false;
                    libraryScreen.IsVisible = true;
                    playlistScreen.IsVisible = false;
                    settingScreen.IsVisible = false;
                    searchScreen.IsVisible = false;
                    break;
                case "settingBtn":
                    homeScreen.IsVisible = false;
                    musicScreen.IsVisible = false;
                    videoScreen.IsVisible = false;
                    //playingScreen.IsVisible = false;
                    libraryScreen.IsVisible = false;
                    playlistScreen.IsVisible = false;
                    settingScreen.IsVisible = true;
                    searchScreen.IsVisible = false;
                    break;
                default:
                    homeScreen.IsVisible = true;
                    musicScreen.IsVisible = false;
                    videoScreen.IsVisible = false;
                    //playingScreen.IsVisible = false;
                    libraryScreen.IsVisible = false;
                    playlistScreen.IsVisible = false;
                    settingScreen.IsVisible = false;
                    searchScreen.IsVisible = false;
                    break;
            }
        }

        public async void ChooseSongPath()
        {
            var topLevel = TopLevel.GetTopLevel(this);

            var files = await topLevel.StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions
            {
                Title = "Select music folder",
                AllowMultiple = false
            });

            if (files.Count >= 1)
            {
                string folderPath = files[0].Path.ToString();
                string folderPathFormat = folderPath.Remove(0, 8);
                MediaHelper.MusicPathFolder = folderPathFormat;
                MediaHelper.FetchListMedia(MediaTypes.Audio);
                UpdateMedia();
            }

        }
        public async void ChooseVideoPath()
        {
            var topLevel = TopLevel.GetTopLevel(this);

            var files = await topLevel.StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions
            {
                Title = "Select video folder",
                AllowMultiple = false
            });

            if (files.Count >= 1)
            {
                string folderPath = files[0].Path.ToString();
                string folderPathFormat = folderPath.Remove(0, 8);
                MediaHelper.VideoPathFolder = folderPathFormat;
                MediaHelper.FetchListMedia(MediaTypes.Video);
                UpdateMedia();
            }

        }

        public static MainWindow GetInstance()
        {
                return _this;
        }

        public void UpdateMedia()
        {
            (musicScreen.DataContext as ListMediaScreenViewModel).ListSongs = MediaHelper.listSongs;
            (homeScreen.DataContext as HomeScreenViewModel).ListSongs = MediaHelper.listSongs;
            (homeScreen.DataContext as HomeScreenViewModel).ListVideos = MediaHelper.listVideos;
            (videoScreen.DataContext as ListVideoScreenViewModel).ListVideos = MediaHelper.listVideos;
            (searchScreen.DataContext as SearchScreenViewModel).AllMedias = MediaHelper.AllMedias;

        }
    }
}
