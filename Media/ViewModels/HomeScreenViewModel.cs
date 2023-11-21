using Media.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Media.ViewModels
{
    public class HomeScreenViewModel : ViewModelBase
    {
        public HomeScreenViewModel()
        {
            MediaHelper.updateHomeScreen += MediaHelper_updateHomeScreen;
            AllPlayLists = MediaHelper.AllPlayList;
            AddMediaToPlaylistCommand = ReactiveCommand.Create<MediaItem>(AddMediaToPlaylist);
        }

        private void MediaHelper_updateHomeScreen(object? sender, EventArgs e)
        {
            AllPlayLists = MediaHelper.AllPlayList;
        }

        private List<MediaItem> listSongs;
        private List<MediaItem> listVideos;
        private List<Playlist> allPlayLists;

        public List<Playlist> AllPlayLists
        {
            get => allPlayLists;
            set => this.RaiseAndSetIfChanged(ref allPlayLists, value);
        }
        public List<MediaItem> ListSongs
        {
            get => listSongs; set
            {
                if (value.Count > 5)
                {
                    value = value.GetRange(0, 5);
                }
                this.RaiseAndSetIfChanged(ref listSongs, value);
            }
        }
        public List<MediaItem> ListVideos
        {
            get => listVideos; set
            {
                if (value.Count > 5)
                {
                    value = value.GetRange(0, 5);
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
