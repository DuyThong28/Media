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
    public class LibraryScreenViewModel : ViewModelBase
    {
        public LibraryScreenViewModel()
        {
            //
            ListMedia = new ObservableCollection<Playlist>();
            //
        }
        //
        private ObservableCollection<Playlist> listMedia;

        public ObservableCollection<Playlist> ListMedia
        {
            get => listMedia;
            set => this.RaiseAndSetIfChanged(ref listMedia, value);
        }
        //
        /*
        private List<Playlist> listMedia;
        private Playlist selectedPlaylist;
        public List<Playlist> ListMedia { get => listMedia; set { this.RaiseAndSetIfChanged(ref listMedia, value); } }
    
        //public Playlist SelectedPlaylist { get => selectedPlaylist;}*/
        public ReactiveCommand<Unit, Unit> newPlayListCommand = ReactiveCommand.Create(() => { });

        private void MediaHelper_UpdateLibraryScreen(object? sender, EventArgs e)
        {
            SelectedPlaylist = MediaHelper.selectPlaylist(ListMedia);
        }
    }
}
