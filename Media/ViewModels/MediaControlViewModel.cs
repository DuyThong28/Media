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
using Avalonia.Threading;

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

        public MediaControlViewModel()
        {
            if (PlayMedia.timer != null)
            {
                PlayMedia.timer.Tick += Timer_Tick;
            }
        }
        public double TbMaxValue
        {
            get { return tbMaxValue; }
            set { this.RaiseAndSetIfChanged(ref tbMaxValue, value); }
        }

        public double TbValue
        {
            get { return tbValue; }
            set { this.RaiseAndSetIfChanged(ref tbValue, value);
                TimeSongPlay = TimeSpan.FromSeconds(TbValue).ToString(@"mm\:ss");}
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
        private void Timer_Tick(object sender, EventArgs e)
        {
            //if(_media.FilePath!= PlayMedia.Path)
            //{
                _media = PlayMedia.media;
                SongName = PlayMedia.media.Title;
                NameAuthor = PlayMedia.media.ArtistsString;
                ImageSource = PlayMedia.media.Image;
                TbMaxValue = PlayMedia.DurationSong;
                TbValue = PlayMedia.CurrentPositionSong;
                TimeSongPlay = PlayMedia.CurrentPositionstringSong;
                TimeSongEnd = PlayMedia.DurationstringSong;
            //}
            TbValue += 1;
        }
    }
}
