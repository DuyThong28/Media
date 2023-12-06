using Avalonia.Threading;
using Media.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Reactive;

namespace Media.ViewModels
{
    public class HomeScreenViewModel : ViewModelBase
    {
        public HomeScreenViewModel()
        {
            MediaHelper.updateHomeScreen += MediaHelper_updateHomeScreen;
            AllPlayLists = MediaHelper.AllPlayList;
            AllMeidas = MediaHelper.AllMedias;
            AddMediaToPlaylistCommand = ReactiveCommand.Create<MediaItem>(AddMediaToPlaylist);
        }
        private void MediaHelper_updateHomeScreen(object? sender, EventArgs e)
        {
            AllPlayLists = MediaHelper.AllPlayList;
            AllMeidas = MediaHelper.AllMedias;
        }

        public DispatcherTimer timer;
        private List<MediaItem> listSongs;
        private List<MediaItem> listVideos;
        private List<Playlist> allPlayLists;
        private List<MediaItem> allMedias;
        private int selectedMediaIndex = 0;
        public int SelectedMediaIndex
        {
            get => selectedMediaIndex;
            set=> this.RaiseAndSetIfChanged(ref selectedMediaIndex, value);
        }
        public List<MediaItem> AllMeidas
        {
            get => allMedias;
            set=> this.RaiseAndSetIfChanged(ref allMedias, value);
        }

        public List<Playlist> AllPlayLists
        {
            get => allPlayLists;
            set => this.RaiseAndSetIfChanged(ref allPlayLists, value);
        }
        public List<MediaItem> ListSongs
        {
            get => listSongs; set
            {
                if (value.Count > 7)
                {
                    value = value.GetRange(0, 7);
                }
                this.RaiseAndSetIfChanged(ref listSongs, value);
            }
        }
        public List<MediaItem> ListVideos
        {
            get => listVideos; set
            {
                if (value.Count > 7)
                {
                    value = value.GetRange(0, 7);
                }
                this.RaiseAndSetIfChanged(ref listVideos, value);
            }
        }
        public ReactiveCommand<MediaItem, Unit> AddMediaToPlaylistCommand { get; set; }
        private void AddMediaToPlaylist(MediaItem mediaItem)
        {
            MediaItem d = mediaItem;
        }

    }
}
