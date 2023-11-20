using Media.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace Media.ViewModels
{
    public class ListMediaScreenViewModel:ViewModelBase
    {
        public ListMediaScreenViewModel() {
            PlayMediaCommand = ReactiveCommand.Create(() => { MediaHelper.PlayThePlaylist(listSongs); });
            MediaHelper.updateListMediaScreen += MediaHelper_updateListMediaScreen;
            AllPlayLists = MediaHelper.AllPlayList;
        }

        private void MediaHelper_updateListMediaScreen(object? sender, EventArgs e)
        {
            AllPlayLists = MediaHelper.AllPlayList;
        }

        private List<MediaItem> listSongs;
        public List<MediaItem> ListSongs { get => listSongs; set { this.RaiseAndSetIfChanged(ref listSongs, value); } }
        private List<Playlist> allPlayLists;

        public List<Playlist> AllPlayLists
        {
            get => allPlayLists;
            set => this.RaiseAndSetIfChanged(ref allPlayLists, value);
        }
        public ReactiveCommand<Unit, Unit> PlayMediaCommand { get; private set; }

    }
}
