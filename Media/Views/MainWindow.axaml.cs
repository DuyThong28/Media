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

namespace Media.Views
{
    public partial class MainWindow : Window
    {
        private Process ffmpegProcess;
        public MainWindow()
        {
            InitializeComponent();
            navBarControl.NavBarItemSelected += NavBarControl_RadioButtonChecked;
            mediaControl.ButtonClicked += MediaControl_ButtonClicked;
            SettingScreenViewModel.OpenSongFolder = ReactiveCommand.Create(() => ChooseSongPath());
            SettingScreenViewModel.OpenVideoFolder = ReactiveCommand.Create(() => ChooseVideoPath());
            ffmpegProcess = new Process();
            PlayAudio(@"C:\Users\duyth\OneDrive\Máy tính\Media\ChanAi-OrangeKhoi-6225088.mp3");
        }

        private void PlayAudio(string filePath)
        {
            // Example FFmpeg command to play audio:
            // ffmpeg -i input.mp3 -f wav - | ffplay -

            var startInfo = new ProcessStartInfo
            {
                FileName = "ffmpeg",
                Arguments = $"-i {filePath} -f wav - | ffplay -",
                RedirectStandardInput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            ffmpegProcess.StartInfo = startInfo;
            ffmpegProcess.Start();
        }

        private void StopAudio()
        {
            // Kill the FFmpeg process
            ffmpegProcess.Kill();
        }
        private void MediaControl_ButtonClicked(object? sender, EventArgs e)
        {
            playingScreen.IsVisible = !playingScreen.IsVisible;
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
                    playingScreen.IsVisible = false;
                    libraryScreen.IsVisible = false;
                    playlistScreen.IsVisible = false;
                    settingScreen.IsVisible = false;
                    searchScreen.IsVisible = false;
                    break;
                case "musicBtn":
                    homeScreen.IsVisible = false;
                    musicScreen.IsVisible = true;
                    videoScreen.IsVisible = false;
                    playingScreen.IsVisible = false;
                    libraryScreen.IsVisible = false;
                    playlistScreen.IsVisible = false;
                    settingScreen.IsVisible = false;
                    searchScreen.IsVisible = false;
                    break;
                case "videoBtn":
                    homeScreen.IsVisible = false;
                    musicScreen.IsVisible = false;
                    videoScreen.IsVisible = true;
                    playingScreen.IsVisible = false;
                    libraryScreen.IsVisible = false;
                    playlistScreen.IsVisible = false;
                    settingScreen.IsVisible = false;
                    searchScreen.IsVisible = false;
                    break;
                case "searchBtn":
                    homeScreen.IsVisible = false;
                    musicScreen.IsVisible = false;
                    videoScreen.IsVisible = false;
                    playingScreen.IsVisible = false;
                    libraryScreen.IsVisible = false;
                    playlistScreen.IsVisible = false;
                    settingScreen.IsVisible = false;
                    searchScreen.IsVisible = true;
                    break;
                case "libraryBtn":
                    homeScreen.IsVisible = false;
                    musicScreen.IsVisible = false;
                    videoScreen.IsVisible = false;
                    playingScreen.IsVisible = false;
                    libraryScreen.IsVisible = true;
                    playlistScreen.IsVisible = false;
                    settingScreen.IsVisible = false;
                    searchScreen.IsVisible = false;
                    break;
                case "settingBtn":
                    homeScreen.IsVisible = false;
                    musicScreen.IsVisible = false;
                    videoScreen.IsVisible = false;
                    playingScreen.IsVisible = false;
                    libraryScreen.IsVisible = false;
                    playlistScreen.IsVisible = false;
                    settingScreen.IsVisible = true;
                    searchScreen.IsVisible = false;
                    break;
                default:
                    homeScreen.IsVisible = true;
                    musicScreen.IsVisible = false;
                    videoScreen.IsVisible = false;
                    playingScreen.IsVisible = false;
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
                (musicScreen.DataContext as ListMediaScreenViewModel).ListSongs = MediaHelper.listSongs;
                (homeScreen.DataContext as HomeScreenViewModel).ListSongs = MediaHelper.listSongs;
                (homeScreen.DataContext as HomeScreenViewModel).ListVideos = MediaHelper.listVideos;
                (videoScreen.DataContext as ListVideoScreenViewModel).ListVideos = MediaHelper.listVideos;
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
                MediaHelper.MusicPathFolder = folderPathFormat;
                MediaHelper.FetchListMedia(MediaTypes.Video);
                (musicScreen.DataContext as ListMediaScreenViewModel).ListSongs = MediaHelper.listSongs;
                (homeScreen.DataContext as HomeScreenViewModel).ListSongs = MediaHelper.listSongs;
                (homeScreen.DataContext as HomeScreenViewModel).ListVideos = MediaHelper.listVideos;
                (videoScreen.DataContext as ListVideoScreenViewModel).ListVideos = MediaHelper.listVideos;
            }

        }
    }
}