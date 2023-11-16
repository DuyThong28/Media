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
    public class LibraryScreenViewModel:ViewModelBase
    {
        public LibraryScreenViewModel()
        {
            
        }
        private List<Playlist> listMedia;
        private Playlist selectedPlaylist;
        public List<Playlist> ListMedia { get => listMedia; set { this.RaiseAndSetIfChanged(ref listMedia, value); } }
        public Playlist SelectedPlaylist { get => selectedPlaylist;}

    }
}
