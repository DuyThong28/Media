using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Media;
using Media.Models;
using ReactiveUI;

namespace Media.ViewModels
{
    public class PlaylistScreenViewModel : ViewModelBase
    {
        private List<MediaItem> listMedia;
        private string playListName = "Không có tiêu đề";
        private string countMedia = "0";
        private Playlist playlist;
        private bool isPlay;
        private MediaItem selectedItem;
        private string mainColor = "#4E97D1";
        private IImage backGroundImage;

        public IImage BackGroundImage
        {
            get => backGroundImage;
            set => this.RaiseAndSetIfChanged(ref backGroundImage, value);
        }
        public string MainColor
        {
            get => mainColor;
            set=> this.RaiseAndSetIfChanged(ref mainColor, value);  
        }

        public ReactiveCommand<Unit, Unit> PlayMediaCommand { get; private set; }
        public ReactiveCommand<Unit, Unit> BackCommand { get; private set; }
        public PlaylistScreenViewModel()
        {
            PlayMediaCommand = ReactiveCommand.Create(() => { PlayPlaylist(); });
            BackCommand = ReactiveCommand.Create(() => { if (backEvent != null)
                    backEvent(this, new EventArgs());
            });
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

        public MediaItem SelectedItem { get => selectedItem; set => this.RaiseAndSetIfChanged(ref selectedItem,value); }

        private void UpdateMediaControl(object sender, EventArgs e)
        {
            if (playlist != null)
            {
                if (MediaHelper.isPlayingPlaylist == true && MediaHelper.playListPlayingId == playlist.PlayListID)
                {
                    IsPlay = PlayMedia.IsPlay;
                }
                else
                {
                    IsPlay = false;
                }
                if(selectedItem != MediaHelper.selectPlaylistItem(playlist)) { 
                    SelectedItem = MediaHelper.selectPlaylistItem(playlist);
                }
            }
        }
        private void UpdateScreen()
        {
            //ListMedia = Playlist.ListMedia;
            ListMedia = MediaHelper.listSongs;
            PlayListName = Playlist.PlayListName;
            CountMedia = Playlist.ListMedia.Count.ToString();
            BackGroundImage = Playlist.BackGroundImage;
            if (playlist.BackroundImageFileName != null)
            {
                MainColor = ImageHelper.GetDominantColor(Playlist.BackroundImageFileName).ToString();
            }
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

        private static event EventHandler backEvent;
        public static event EventHandler BackEvent
        {
            add { backEvent += value; }
            remove { backEvent -= value; }
        }
    }
}
