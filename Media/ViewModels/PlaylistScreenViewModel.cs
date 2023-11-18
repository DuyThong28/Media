using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using Media.Models;
using ReactiveUI;

namespace Media.ViewModels
{
    public class PlaylistScreenViewModel : ViewModelBase
    {
        private List<MediaItem> listMedia;
        private string playListName = "Danh sách phát không có tiêu đề";
        private string countMedia = "0";
        private Playlist playlist;
        private bool isPlay;
        public ReactiveCommand<Unit, Unit> PlayMediaCommand { get; private set; }
        public PlaylistScreenViewModel()
        {
         PlayMediaCommand = ReactiveCommand.Create(() => { PlayPlaylist(); });
            MediaHelper.UpdatePlaylistScreen += UpdateMediaControl;
        }

        public List<MediaItem> ListMedia { get => listMedia; set { this.RaiseAndSetIfChanged(ref listMedia, value);} }
        public string PlayListName
        {
            get => playListName;
            set => this.RaiseAndSetIfChanged(ref  playListName, value);
        }
        public bool IsPlay
        {
            get => isPlay;
            set => this.RaiseAndSetIfChanged(ref isPlay, value);
        }
        public Playlist Playlist { get => playlist; set { this.RaiseAndSetIfChanged(ref playlist, value); UpdateScreen(); } }
        public string CountMedia { 
            get {
                return countMedia+ " mục";
            }
            set=> this.RaiseAndSetIfChanged(ref countMedia, value); 
        }
        private void UpdateMediaControl(object sender, EventArgs e)
        {
            IsPlay = PlayMedia.IsPlay;
        }
        private void UpdateScreen()
        {
            ListMedia = Playlist.ListMedia;
            PlayListName = Playlist.PlayListName;
            CountMedia = Playlist.ListMedia.Count.ToString();
        }

        private void PlayPlaylist()
        {
            if(MediaHelper.playListPlayingId == playlist.PlayListID)
            {
                if (PlayMedia.media != null)
                {
                    PlayMedia.media.PlayMediaCommand();
                }
            } else
            {
                MediaHelper.PlayThePlaylist(Playlist);
            }
        }
    }
}
