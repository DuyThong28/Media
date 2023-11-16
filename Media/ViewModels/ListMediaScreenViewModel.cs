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
            MediaHelper.UpdateListMediaScreen += MediaHelper_UpdateListMediaScreen;
        }

        private void MediaHelper_UpdateListMediaScreen(object? sender, EventArgs e)
        {

        }

        private List<MediaItem> listSongs;
        public List<MediaItem> ListSongs { get => listSongs; set { this.RaiseAndSetIfChanged(ref listSongs, value); } }
        public ReactiveCommand<Unit, Unit> PlayMediaCommand { get; private set; }

    }
}
