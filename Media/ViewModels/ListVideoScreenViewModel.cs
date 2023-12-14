using Media.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Reactive;

namespace Media.ViewModels
{
    public class ListVideoScreenViewModel:ViewModelBase
    {
        public ListVideoScreenViewModel()
        {
            PlayMediaCommand = ReactiveCommand.Create(() => { MediaHelper.PlayThePlaylist(ListVideos); });
            MediaHelper.updateListVideoScreen += MediaHelper_updateListVideoScreen;
            AllPlayLists = MediaHelper.AllPlayList;
        }

        private void MediaHelper_updateListVideoScreen(object? sender, EventArgs e)
        {
            AllPlayLists = MediaHelper.AllPlayList;
        }

        private List<MediaItem> listVideos;
        public List<MediaItem> ListVideos { get => listVideos; set { this.RaiseAndSetIfChanged(ref listVideos, value); } }
        private List<Playlist> allPlayLists;

        public List<Playlist> AllPlayLists
        {
            get => allPlayLists;
            set => this.RaiseAndSetIfChanged(ref allPlayLists, value);
        }
        public ReactiveCommand<Unit, Unit> PlayMediaCommand { get; private set; }
    }
}
