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
        public LibraryScreenControl libraryScreen;
        public HomeScreenControl homeScreen;
        public ListVideoSreenControl videoScreen;
        public ListMediaScreenControl musicScreen;
        public SettingScreenControl settingScreen;
        public SearchScreenControl searchScreen;
        public PlaylistScreenControl playlistScreen;
        public MainWindowViewModel viewModel;
        public ContentControl screen;
        //public MediaControl mediaControl;
        public PlayingScreenControl playingScreen;
        public Grid mainScreen;
        public NavBarControl navBarControl { get; set; }
        public static MainWindow _this { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Init();

            
        }

        public void Init()
        {
            viewModel = new MainWindowViewModel();
            DataContext = viewModel;
            _videoViewer = this.Get<Media.ViewModels.VideoView>("VideoViewer");
            viewModel.SettingScreenViewModel.OpenSongFolder = ReactiveCommand.Create(() => ChooseSongPath());
            viewModel.SettingScreenViewModel.OpenVideoFolder = ReactiveCommand.Create(() => ChooseVideoPath());
            libraryScreen = new LibraryScreenControl() { DataContext = viewModel.LibraryScreenViewModel };
            videoScreen = new ListVideoSreenControl() { DataContext = viewModel.ListVideoScreenViewModel };
            homeScreen = new HomeScreenControl() { DataContext = viewModel.HomeScreenViewModel };
            musicScreen = new ListMediaScreenControl() { DataContext = viewModel.ListMediaScreenViewModel };
            settingScreen = new SettingScreenControl() { DataContext = viewModel.SettingScreenViewModel };
            searchScreen = new SearchScreenControl() { DataContext = viewModel.SearchScreenViewModel };
            navBarControl = new NavBarControl() { DataContext = viewModel.NavBarControlViewModel };
            playingScreen = new PlayingScreenControl() { DataContext = viewModel.PlayingScreenViewModel };
            screen = new ContentControl() { Content = homeScreen };
            _this = this;
            DefineMainScreenGrid();
            Opened += MainWindow_Opened;
            navBarControl.NavBarItemSelected += NavBarControl_RadioButtonChecked;
            mediaControl.ButtonClicked += MediaControl_ButtonClicked;
            videoControl.ButtonClicked += MediaControl_ButtonClicked;
            libraryScreen.SelectPlaylist += LibraryScreen_SelectPlaylist;
            //MediaHelper.UpdatePlayingScreen += MediaHelper_UpdatePlayingScreen;
            MediaHelper.OpenVideoScreen += ListVideoSreenControl_OpenVideoScreen;
            MediaHelper.TurnPlayingScreen += MediaHelper_TurnPlayingScreen;
        
            HomeScreenControl.SeeAllSongs += HomeScreenControl_SeeAllSongs;
            HomeScreenControl.SeeAllVideos += HomeScreenControl_SeeAllVideos;
            PlaylistScreenViewModel.BackEvent += PlaylistScreenViewModel_BackEvent;
            mainViewScreen.Content = mainScreen;
        }

        private void MediaHelper_TurnPlayingScreen(object? sender, EventArgs e)
        {
           
                if (PlayMedia.IsPlayVideo)
                {
                if (mainViewScreen.Content == playingScreen)
                {
                    videoDisplay.IsVisible = true;
                    controlPanel.IsVisible = true;
                    VideoViewer.IsVisible = true;
                }
                } else
                {
                    videoDisplay.IsVisible = false;
                    controlPanel.IsVisible = false;
                    VideoViewer.IsVisible = false;
                }
        }

        public void DefineMainScreenGrid()
        {
            Grid grid = new Grid();
            // Define the column definitions
            ColumnDefinition column1 = new ColumnDefinition() { Width = new GridLength(250, GridUnitType.Pixel) };
            ColumnDefinition column2 = new ColumnDefinition();
            grid.ColumnDefinitions.Add(column1);
            grid.ColumnDefinitions.Add(column2);

            // Add child controls to the grid
            Grid.SetColumn(navBarControl, 0);
            grid.Children.Add(navBarControl);
            //mainScreen
            Grid.SetColumn(screen, 1);
            grid.Children.Add(screen);
            mainScreen = grid;
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
            VideoViewer.IsVisible = false;
            //controlPanel.IsVisible = false;
            //videoPlayingScreen.IsVisible = false;
        }

        private void PlaylistScreenViewModel_BackEvent(object? sender, EventArgs e)
        {
            //homeScreen.IsVisible = false;
            //musicScreen.IsVisible = false;
            //videoScreen.IsVisible = false;
            ////playingScreen.IsVisible = false;
            //libraryScreen.IsVisible = true;
            //playlistScreen.IsVisible = false;
            //settingScreen.IsVisible = false;
            //searchScreen.IsVisible = false;
            screen.Content = libraryScreen;
        }

        private void HomeScreenControl_SeeAllVideos(object? sender, EventArgs e)
        {
            //homeScreen.IsVisible = false;
            //musicScreen.IsVisible = false;
            //videoScreen.IsVisible = true;
            ////playingScreen.IsVisible = false;
            //libraryScreen.IsVisible = false;
            //playlistScreen.IsVisible = false;
            //settingScreen.IsVisible = false;
            //searchScreen.IsVisible = false;
            screen.Content = videoScreen;
        }


        private void HomeScreenControl_SeeAllSongs(object? sender, EventArgs e)
        {
            //homeScreen.IsVisible = false;
            //musicScreen.IsVisible = true;
            //videoScreen.IsVisible = false;
            ////playingScreen.IsVisible = false;
            //libraryScreen.IsVisible = false;
            //playlistScreen.IsVisible = false;
            //settingScreen.IsVisible = false;
            //searchScreen.IsVisible = false;
            screen.Content = musicScreen;
        }

        private void ListVideoSreenControl_OpenVideoScreen(object? sender, EventArgs e)
        {
            //mainScreen.IsVisible = false;
            //mainPlayingScreen.IsVisible = true;
            //videoPlayingScreen.IsVisible = true;
            //controlPanel.IsVisible = true;
            videoDisplay.IsVisible = true;
            controlPanel.IsVisible = true;
            VideoViewer.IsVisible = true;
        }

        private void MediaHelper_UpdatePlayingScreen(object? sender, EventArgs e)
        {
            //if (PlayMedia.IsPlayVideo == true)
            //{
            //    videoDisplay.IsVisible = true;
            //    playingScreen.IsVisible = false;
            //}
            //else if (PlayMedia.IsPlayVideo == false)
            //{
            //    videoDisplay.IsVisible = false;
            //    controlPanel.IsVisible = false;
            //    playingScreen.IsVisible = true;
            //}
            //if (PlayMedia.IsFirst == true)
            //{
            //    mediaControl.IsEnabled = true;
            //}
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
                    playlistScreen = new PlaylistScreenControl() { DataContext = viewModel.PlaylistScreenViewModel };
                (playlistScreen.DataContext as PlaylistScreenViewModel).Playlist = selectedPlaylist;
                    MediaHelper.UpdateScreen(sender, e);
            }
            }
            screen.Content = playlistScreen;
            //homeScreen.IsVisible = false;
            //musicScreen.IsVisible = false;
            //videoScreen.IsVisible = false;
            ////playingScreen.IsVisible = false;
            //libraryScreen.IsVisible = false;
            //playlistScreen.IsVisible = true;
            //settingScreen.IsVisible = false;
            //searchScreen.IsVisible = false;
        }

        private void MediaControl_ButtonClicked(object? sender, EventArgs e)
        {
            //mainScreen.IsVisible = !mainScreen.IsVisible;
            //mainPlayingScreen.IsVisible = !mainPlayingScreen.IsVisible;
            //videoPlayingScreen.IsVisible = !videoPlayingScreen.IsVisible;
            //if(PlayMedia.IsPlayVideo==true) {
            //controlPanel.IsVisible = !controlPanel.IsVisible;
            //}
            viewModel.HomeScreenViewModel.SelectedMediaIndex = viewModel.HomeScreenViewModel.SelectedMediaIndex == 1 ? 0 : 1;
            if (mainViewScreen.Content == mainScreen)
            {    
                if(PlayMedia.IsPlayVideo == false)
                {
                    mainViewScreen.Content = playingScreen;
                    videoDisplay.IsVisible = false;
                    controlPanel.IsVisible = false;
                    VideoViewer.IsVisible = false;
                }
                else
                {
                    videoDisplay.IsVisible = !videoDisplay.IsVisible;
                    controlPanel.IsVisible = !controlPanel.IsVisible;
                    VideoViewer.IsVisible = !VideoViewer.IsVisible;
                }
            } else
            {
                mainViewScreen.Content = mainScreen;
            }
        }
        private void NavBarControl_RadioButtonChecked(object? sender, EventArgs e)
        {

            ListBox listBox = sender as ListBox;
            ListBoxItem listBoxItem = (ListBoxItem)listBox.SelectedItem;

            switch (listBoxItem.Name)
            {
                case "homeBtn":
                    screen.Content = homeScreen;
                    viewModel.HomeScreenViewModel.SelectedMediaIndex = viewModel.HomeScreenViewModel.SelectedMediaIndex == 1 ? 0:1;
                    //homeScreen.IsVisible = true;
                    //musicScreen.IsVisible = false;
                    //videoScreen.IsVisible = false;
                    ////playingScreen.IsVisible = false;
                    //libraryScreen.IsVisible = false;
                    //playlistScreen.IsVisible = false;
                    //settingScreen.IsVisible = false;
                    //searchScreen.IsVisible = false;
                    break;
                case "musicBtn":
                    screen.Content = musicScreen;
                    viewModel.HomeScreenViewModel.SelectedMediaIndex = -1;
                    //homeScreen.IsVisible = false;
                    //musicScreen.IsVisible = true;
                    //videoScreen.IsVisible = false;
                    ////playingScreen.IsVisible = false;
                    //libraryScreen.IsVisible = false;
                    //playlistScreen.IsVisible = false;
                    //settingScreen.IsVisible = false;
                    //searchScreen.IsVisible = false;
                    break;
                case "videoBtn":
                    screen.Content = videoScreen;
                    viewModel.HomeScreenViewModel.SelectedMediaIndex = -1;
                    //homeScreen.IsVisible = false;
                    //musicScreen.IsVisible = false;
                    //videoScreen.IsVisible = true;
                    ////playingScreen.IsVisible = false;
                    //libraryScreen.IsVisible = false;
                    //playlistScreen.IsVisible = false;
                    //settingScreen.IsVisible = false;
                    //searchScreen.IsVisible = false;
                    break;
                case "searchBtn":
                    screen.Content = searchScreen;
                    viewModel.HomeScreenViewModel.SelectedMediaIndex = -1;
                    //homeScreen.IsVisible = false;
                    //musicScreen.IsVisible = false;
                    //videoScreen.IsVisible = false;
                    ////playingScreen.IsVisible = false;
                    //libraryScreen.IsVisible = false;
                    //playlistScreen.IsVisible = false;
                    //settingScreen.IsVisible = false;
                    //searchScreen.IsVisible = true;
                    break;
                case "libraryBtn":
                    screen.Content = libraryScreen;
                    viewModel.HomeScreenViewModel.SelectedMediaIndex = -1;
                    //homeScreen.IsVisible = false;
                    //musicScreen.IsVisible = false;
                    //videoScreen.IsVisible = false;
                    ////playingScreen.IsVisible = false;
                    //libraryScreen.IsVisible = true;
                    //playlistScreen.IsVisible = false;
                    //settingScreen.IsVisible = false;
                    //searchScreen.IsVisible = false;
                    break;
                case "settingBtn":
                    screen.Content = settingScreen;
                    viewModel.HomeScreenViewModel.SelectedMediaIndex = -1;
                    //homeScreen.IsVisible = false;
                    //musicScreen.IsVisible = false;
                    //videoScreen.IsVisible = false;
                    ////playingScreen.IsVisible = false;
                    //libraryScreen.IsVisible = false;
                    //playlistScreen.IsVisible = false;
                    //settingScreen.IsVisible = true;
                    //searchScreen.IsVisible = false;
                    break;
                default:
                    screen.Content = homeScreen;
                    viewModel.HomeScreenViewModel.SelectedMediaIndex = viewModel.HomeScreenViewModel.SelectedMediaIndex == 1 ? 0 : 1;
                    //homeScreen.IsVisible = true;
                    //musicScreen.IsVisible = false;
                    //videoScreen.IsVisible = false;
                    ////playingScreen.IsVisible = false;
                    //libraryScreen.IsVisible = false;
                    //playlistScreen.IsVisible = false;
                    //settingScreen.IsVisible = false;
                    //searchScreen.IsVisible = false;
                    break;
            }
        }

        public async void ChooseSongPath()
        {
            var topLevel = TopLevel.GetTopLevel(this);

            var files = await topLevel.StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions
            {
                Title = "Select music folder",
                AllowMultiple = false,
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
            (musicScreen.DataContext as ListMediaScreenViewModel).ListSongs = MediaHelper.ListSongs;
            (homeScreen.DataContext as HomeScreenViewModel).ListSongs = MediaHelper.ListSongs;
            (homeScreen.DataContext as HomeScreenViewModel).ListVideos = MediaHelper.ListVideos;
            (videoScreen.DataContext as ListVideoScreenViewModel).ListVideos = MediaHelper.ListVideos;
            (searchScreen.DataContext as SearchScreenViewModel).AllMedias = MediaHelper.AllMedias;

        }
    }
}
