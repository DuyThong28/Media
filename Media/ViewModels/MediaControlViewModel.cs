using Avalonia.Media.Imaging;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using Media.Models;
using Avalonia.Media;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Media.ViewModels
{
    public class MediaControlViewModel:ViewModelBase
    {
        public MediaItem _media = null;
        private double tbMaxValue; 
        private double tbValue;
        private string timeSongEnd;
        private string timeSongPlay;
        private IImage imageSource;
        private string songName;
        private string nameAuthor;
        public double TbMaxValue
        {
            get { return tbMaxValue; }
            set { this.RaiseAndSetIfChanged(ref tbMaxValue, value); }
        }

        public double TbValue
        {
            get { return tbValue; }
            set { this.RaiseAndSetIfChanged(ref tbValue, value); }
        }
        public string TimeSongEnd
        {
            get { return timeSongEnd; }
            set { this.RaiseAndSetIfChanged(ref timeSongEnd, value); }
        }
        
        public string TimeSongPlay
        {
            get { return timeSongPlay; }
            set { this.RaiseAndSetIfChanged(ref timeSongPlay, value); }
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
            set=> this.RaiseAndSetIfChanged(ref nameAuthor, value);
        }


        public void getPathOfSong(MediaItem media)
        {
            if (media != null) _media = media;
            SongName = media.Title;
            NameAuthor = media.ArtistsString;
            ImageSource = media.Image;
            TbMaxValue = (double)media.Duration.TotalSeconds;
            TbValue = 0;
            TimeSongPlay = "00:00";
            TimeSongEnd = media.DurationText;

            if (media.FilePath != null) PlayMedia.URL = media.FilePath;
            PlayMedia.setCurrentTimePlay();
            PlayMedia.playSong();
        }

        public void Slider_ValueChanged(object? sender, Avalonia.Controls.Primitives.RangeBaseValueChangedEventArgs e)
        {

        }

    }
}
