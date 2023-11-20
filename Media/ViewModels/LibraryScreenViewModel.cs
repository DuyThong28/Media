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
            ListMedia = new List<Playlist>();
            PlayPlayListCommand = ReactiveCommand.Create<Playlist>(PlayPlaylist);
            MediaHelper.UpdateLibraryScreen += MediaHelper_UpdateLibraryScreen; 
            //
        }
        //
        private List<Playlist> listMedia;
        //

        private Playlist selectedPlaylist;
        public List<Playlist> ListMedia { get => listMedia; set { this.RaiseAndSetIfChanged(ref listMedia, value); } }
        private void MediaHelper_UpdateLibraryScreen(object? sender, EventArgs e)
        {
            SelectedPlaylist = MediaHelper.selectPlaylist(ListMedia);
        }

        public Playlist SelectedPlaylist { get => selectedPlaylist; set => this.RaiseAndSetIfChanged(ref selectedPlaylist, value); }
        public ReactiveCommand<Playlist, Unit> PlayPlayListCommand { get; set; }
        private void PlayPlaylist(Playlist playlist)
        {
            SelectedPlaylist = playlist;
            if (MediaHelper.playListPlayingId == SelectedPlaylist.PlayListID)
            {
                if (PlayMedia.media != null)
                {
                    PlayMedia.media.PlayMediaCommand();
                }
            }
            else
            {
                MediaHelper.PlayThePlaylist(SelectedPlaylist);
            }
        }

    }
}
