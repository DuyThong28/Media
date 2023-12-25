using Avalonia.Controls;
using Avalonia.Platform.Storage;
using Media.ViewModels;
using ReactiveUI;
using System;
using TagLib;
using Media.Models;
using System.Collections.Generic;
using System.IO;
using MsBox.Avalonia.Enums;
using MsBox.Avalonia;
using System.Diagnostics;
using System.IO.Compression;
using System.Net;
using System.Reflection;
using Avalonia;

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
        public PlayingScreenControl playingScreen;
        public Grid mainScreen;
        public NavBarControl navBarControl { get; set; }
        public static MainWindow _this { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Init();
            Opened += OnOpened;
           
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
            ColumnDefinition column1 = new ColumnDefinition() { Width = new GridLength(250, GridUnitType.Pixel) };
            ColumnDefinition column2 = new ColumnDefinition();
            grid.ColumnDefinitions.Add(column1);
            grid.ColumnDefinitions.Add(column2);

            Grid.SetColumn(navBarControl, 0);
            grid.Children.Add(navBarControl);
            Grid.SetColumn(screen, 1);
            grid.Children.Add(screen);
            mainScreen = grid;
        }

        private void MainWindow_Opened(object? sender, System.EventArgs e)
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            if (_videoViewer != null && PlayMedia.MediaPlayer != null)
            {
                _videoViewer.MediaPlayer = PlayMedia.MediaPlayer;
                _videoViewer.MediaPlayer.SetHandle(_videoViewer.hndl);
            }
            videoDisplay.IsVisible = false;
            controlPanel.IsVisible = false;
            VideoViewer.IsVisible = false;
        }

        private void PlaylistScreenViewModel_BackEvent(object? sender, EventArgs e)
        {
            screen.Content = libraryScreen;
        }

        private void HomeScreenControl_SeeAllVideos(object? sender, EventArgs e)
        {
            screen.Content = videoScreen;
            navBarControl.lbControl.SelectedIndex = 3;
        }


        private void HomeScreenControl_SeeAllSongs(object? sender, EventArgs e)
        {
            screen.Content = musicScreen;
            navBarControl.lbControl.SelectedIndex = 2;
        }

        private void ListVideoSreenControl_OpenVideoScreen(object? sender, EventArgs e)
        {
            videoDisplay.IsVisible = true;
            controlPanel.IsVisible = true;
            VideoViewer.IsVisible = true;
        }

        private void MediaHelper_UpdatePlayingScreen(object? sender, EventArgs e)
        {
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
        }

        private void MediaControl_ButtonClicked(object? sender, EventArgs e)
        {
            mediaControl.UpdateRepeatBtn(sender, new Avalonia.Interactivity.RoutedEventArgs());
            mediaControl.UpdateSuftbtn(sender, new Avalonia.Interactivity.RoutedEventArgs());
            videoControl.UpdateSuftbtn(sender, new Avalonia.Interactivity.RoutedEventArgs());
            videoControl.UpdateRepeatBtn(sender, new Avalonia.Interactivity.RoutedEventArgs());
            if (mainViewScreen.Content == mainScreen)
            {    
                if(PlayMedia.IsPlayVideo == false)
                {
                    mainViewScreen.Content = playingScreen;
                    MediaHelper.UpdateScreen(sender,e);
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

            if (viewModel.HomeScreenViewModel.SelectedMediaIndex != -1)
            {
                viewModel.HomeScreenViewModel.SelectedMediaIndex = 1;
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
                    break;
                case "musicBtn":
                    screen.Content = musicScreen;
                    break;
                case "videoBtn":
                    screen.Content = videoScreen;
                    break;
                case "searchBtn":
                    (searchScreen.DataContext as SearchScreenViewModel).SearchText = "";
                    screen.Content = searchScreen;
                    break;
                case "libraryBtn":
                    screen.Content = libraryScreen;
                    break;
                case "settingBtn":
                    screen.Content = settingScreen;
                    break;
                default:
                    screen.Content = homeScreen;
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
                string folderPathFormat = folderPath.Remove(0, 7);
                if (!Directory.Exists(folderPathFormat))
                {
                    folderPathFormat = folderPath.Remove(0, 8);
                }
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
                string folderPathFormat = folderPath.Remove(0, 7);
                if (!Directory.Exists(folderPathFormat))
                {
                    folderPathFormat = folderPath.Remove(0, 8);
                }
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
            (videoScreen.DataContext as ListVideoScreenViewModel).ListVideos = MediaHelper.ListVideos;
            (musicScreen.DataContext as ListMediaScreenViewModel).ListSongs = MediaHelper.ListSongs;
            (homeScreen.DataContext as HomeScreenViewModel).ListSongs = MediaHelper.ListSongs;
            (homeScreen.DataContext as HomeScreenViewModel).ListVideos = MediaHelper.ListVideos;
            (homeScreen.DataContext as HomeScreenViewModel).AllMeidas = MediaHelper.AllMedias;
            (searchScreen.DataContext as SearchScreenViewModel).AllMedias = MediaHelper.AllMedias;
            Random rand = new Random();
            viewModel.HomeScreenViewModel.SelectedMediaIndex = rand.Next(0, viewModel.HomeScreenViewModel.AllMeidas.Count);
        }

        private async void OnOpened(object? sender, EventArgs e)
        {
            WebClient webClient = new WebClient();
            var client = new WebClient();
            string currentVersion = MediaHelper.Version;
            try
            {
                string nextVersion = webClient.DownloadString("https://www.dropbox.com/scl/fi/a5tsavuttkfqb64qukigo/Update.txt?rlkey=lg6dqscl6g2ryi9lrdghmyxnm&dl=1");
                if (!nextVersion.Contains(currentVersion))
                {
                    var result = await MessageBoxManager.GetMessageBoxStandard("Update", "New update available! Do you want to install it?", ButtonEnum.YesNo).ShowWindowDialogAsync(this);

                    if (result == ButtonResult.Yes)
                    {
                        if (System.IO.File.Exists(@".\Media Setup.exe"))
                        {
                            System.IO.File.Delete(@".\Media Setup.exe");
                        }
                        client.DownloadFile("https://www.dropbox.com/scl/fi/4eg6ubpzl7ig5e9r6acbz/Media-SetUp.zip?rlkey=g70jjl2modwa3flci7dnm8626&dl=1", @"Media Setup.zip");
                        string zipPath = @".\Media Setup.zip";
                        string extractPath = @".\";
                        ZipFile.ExtractToDirectory(zipPath, extractPath);
                        string[] setupFiles = System.IO.Directory.GetFiles(extractPath, "Media Setup.exe", System.IO.SearchOption.AllDirectories);
                        if (setupFiles.Length > 0)
                        {
                            ProcessStartInfo startInfo = new ProcessStartInfo();
                            startInfo.FileName = setupFiles[0];
                            Process.Start(startInfo);
                        }
                        MediaHelper.Version = nextVersion;
                    }
                }
            }
            catch
            {

            }
        }
    }
}
