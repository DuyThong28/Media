using Avalonia.Media;
using Media.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Reactive;


namespace Media.ViewModels
{
    public class PlayingScreenViewModel:ViewModelBase
    {
        public MediaItem _media = null;
        private IImage imageSource = ImageHelper.LoadFromResource(new Uri("avares://Media/Assets/Icons/defaultImage.jpg"));
        private string songName;
        private string nameAuthor;
        private bool isPlay;
        private List<MediaItem> listMedia;      
        private string backgroundColor;
        
        public PlayingScreenViewModel()
        {
            if (PlayMedia.timer != null)
            {
                MediaHelper.updatePlayingScreen += UpdateScreen;
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
            SelectedItem = MediaHelper.selectPlayingItem(ListMedia);
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
