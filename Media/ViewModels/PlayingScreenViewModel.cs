using Avalonia.Controls;
using Avalonia.Media;
using LibVLCSharp.Shared;
using Media.Models;
using NAudio.CoreAudioApi;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using MyMedia = LibVLCSharp.Shared.Media;
using Color = Avalonia.Media.Color;


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
        private string backgroundColor;
        public PlayingScreenViewModel()
        {
            if (PlayMedia.timer != null)
            {
                MediaHelper.updateMediaScreen += UpdateScreen;
            }
            PlayMediaCommand = ReactiveCommand.Create(() => { Play(); });
        }

        public string BackgroundColor
        {
            get { return backgroundColor; } set { this.RaiseAndSetIfChanged(ref backgroundColor, value);}
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
        public ReactiveCommand<Unit, Unit> PlayMediaCommand { get; set; }
        public bool IsPlay
        {
            get => isPlay;
            set => this.RaiseAndSetIfChanged(ref isPlay, value);
        }
        private void Play()
        {
            if (PlayMedia.media != null)
            {
                PlayMedia.media.PlayMediaCommand();
            }
        }

        private void UpdateScreen(object sender, EventArgs e)
        {
            SelectedItem = MediaHelper.selectPlayingItem(ListMedia);
            if(_media!= PlayMedia.media)
            {
                _media = PlayMedia.media;
                SongName = PlayMedia.media.Title;
                NameAuthor = PlayMedia.media.ArtistsString;
                ImageSource = PlayMedia.media.Image;
            }
            IsPlay = PlayMedia.IsPlay;
            if(ListMedia!=MediaHelper.PlayQueue)
            ListMedia = MediaHelper.PlayQueue;
            
        }
        
      
        private MediaItem selectedItem;
        public MediaItem SelectedItem
        {
            get { return selectedItem; }
            set
            {
                this.RaiseAndSetIfChanged(ref selectedItem, value);
            }
        }
       
    }
}
