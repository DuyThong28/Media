using Avalonia.Media;
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
    public class PlayingScreenViewModel:ViewModelBase
    {
        public MediaItem _media = null;
        private IImage imageSource;
        private string songName;
        private string nameAuthor;
        private bool isPlay;
        private List<MediaItem> listMedia;
        public PlayingScreenViewModel()
        {
            if (PlayMedia.timer != null)
            {
                MediaHelper.updateMediaScreen += UpdateScreen;
            }
        }
        public IImage ImageSource
        {
            get => imageSource;
            set => this.RaiseAndSetIfChanged(ref imageSource, value);
        }
        public string SongName
        {
            get => songName;
            set => this.RaiseAndSetIfChanged(ref songName, value);
        }
        public string NameAuthor
        {
            get => nameAuthor;
            set => this.RaiseAndSetIfChanged(ref nameAuthor, value);
        }
        public List<MediaItem> ListMedia { get => listMedia; set { this.RaiseAndSetIfChanged(ref listMedia, value); } }
        public ReactiveCommand<Unit, Unit> PlayMediaCommand { get; set; } = ReactiveCommand.Create(() => { PlayMedia.media.PlayMediaCommand(); });
        public bool IsPlay
        {
            get => isPlay;
            set => this.RaiseAndSetIfChanged(ref isPlay, value);
        }

        private void UpdateScreen(object sender, EventArgs e)
        {
            _media = PlayMedia.media;
            SongName = PlayMedia.media.Title;
            NameAuthor = PlayMedia.media.ArtistsString;
            ImageSource = PlayMedia.media.Image;
            IsPlay = PlayMedia.IsPlay;
            ListMedia = MediaHelper.PlayQueue;
        }

    }
}
