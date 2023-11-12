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
            
        }
        private List<MediaItem> listSongs;
        public List<MediaItem> ListSongs { get => listSongs; set { this.RaiseAndSetIfChanged(ref listSongs, value); } }
        public static ReactiveCommand<Unit, Unit> newCommand { get; set; }
        private MediaItem selectedSong;
        public MediaItem SelectedSong { get => selectedSong;
            set => this.RaiseAndSetIfChanged(ref selectedSong, value);
        }

    }
}
