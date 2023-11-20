﻿using Media.Models;
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
            PlayPlayListCommand = ReactiveCommand.Create<Playlist>(PlayPlaylist);
            MediaHelper.UpdateLibraryScreen += MediaHelper_UpdateLibraryScreen;
            //
        }
        private void LibraryScreenViewModel_PlayPlayListEvent(object? sender, EventArgs e)
        {
            SelectedPlaylist = sender as Playlist;
        }
        private Playlist selectedPlaylist;
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

        private void MediaHelper_UpdateLibraryScreen(object? sender, EventArgs e)
        {
            SelectedPlaylist = MediaHelper.selectPlaylist(ListMedia);
        }
    }
}
