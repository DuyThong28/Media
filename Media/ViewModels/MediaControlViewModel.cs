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
using System.Reactive;
using NAudio.Wave;
using System.Runtime.InteropServices;

namespace Media.ViewModels
{
    public class MediaControlViewModel:ViewModelBase
    {
        public MediaItem _media = null;
        private double tbMaxValue;
        private double tbValue = 0.0;
        private float tbVolume = 1;
        private string timeSongEnd;
        private string timeSongPlay;
        private IImage imageSource;
        private string songName;
        private string nameAuthor;
        private List<int> listIndexPlay;

        public MediaControlViewModel()
        {
            PlayNextCommand = ReactiveCommand.Create(() => { PlayNext();});
            PlayMediaCommand = ReactiveCommand.Create(()=> {  PlayMedia.media.PlayMediaCommand(); });
            PlayPrevCommand = ReactiveCommand.Create(() => { PlayPrev(); });
            RepeatCommand = ReactiveCommand.Create(()=> { Repeat(); });
            SuffCommand = ReactiveCommand.Create(() => { Suff(); });
            if (PlayMedia.timer != null)
            {
                MediaHelper.updateMediaScreen += UpdateMediaControl;
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
                TimeSongPlay = TimeSpan.FromSeconds(TbValue).ToString(@"mm\:ss");
            }
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

        private bool isPlay;
        public bool IsPlay
        {
            get => isPlay;
            set => this.RaiseAndSetIfChanged(ref isPlay, value);
        }
        public float TbVolume
        {
            get { return tbVolume; }
            set { this.RaiseAndSetIfChanged(ref tbVolume, value);
            }
        }

        public ReactiveCommand<Unit, Unit> PlayMediaCommand { get; set; }
        public ReactiveCommand<Unit, Unit> PlayNextCommand { get; set; }
        public ReactiveCommand<Unit, Unit> PlayPrevCommand { get; set; }
        public ReactiveCommand<Unit,Unit> RepeatCommand { get; set; } 
        public ReactiveCommand<Unit,Unit> SuffCommand { get; set; } 
        
        private void UpdateMediaControl(object sender, EventArgs e)
        {
            listIndexPlay = new List<int>(MediaHelper.ListIndexPlayQueue);
            IsPlay = PlayMedia.IsPlay;
            if (PlayMedia.waveOutEvent.PlaybackState == PlaybackState.Playing)
            {
                _media = PlayMedia.media;
                SongName = PlayMedia.media.Title;
                NameAuthor = PlayMedia.media.ArtistsString;
                ImageSource = PlayMedia.media.Image;
                TbMaxValue = PlayMedia.DurationSong;
                TbValue = PlayMedia.CurrentPositionSong;
                TimeSongPlay = PlayMedia.CurrentPositionstringSong;
                TimeSongEnd = PlayMedia.DurationstringSong;
                PlayMedia.Volume = tbVolume;
            }
            else if (PlayMedia.waveOutEvent.PlaybackState == PlaybackState.Stopped)
            {
                if (PlayMedia.Repeat == RepeatMode.One)
                {
                    PlayMedia.setCurrentPosition(0);
                    PlayMedia.playSong();
                } else if(PlayMedia.Repeat == RepeatMode.All)
                {
                    PlayNext();
                }
            }
        }

        private void PlayNext()
        {
            if (PlayMedia.IsFirst == false) return;
            for (int i = 0; i < MediaHelper.PlayQueue.Count; i++)
            {
                if (MediaHelper.PlayQueue[listIndexPlay[i]].FilePath == PlayMedia.Path)
                {
                    if (i != MediaHelper.PlayQueue.Count - 1)
                    {
                        PlayMedia.URL = MediaHelper.PlayQueue[i+1].FilePath;
                        PlayMedia.media = MediaHelper.PlayQueue[i + 1];
                        PlayMedia.playSong();
                    }
                    else if (i == MediaHelper.PlayQueue.Count - 1 && PlayMedia.Repeat == RepeatMode.All)
                    {
                        PlayMedia.URL = MediaHelper.PlayQueue[0].FilePath;
                        PlayMedia.media = MediaHelper.PlayQueue[0];
                        PlayMedia.playSong();
                    }
                    return;
                }
            }
        }
        private void PlayPrev()
        {
            if (PlayMedia.IsFirst == false) return;
            for (int i = 0; i < MediaHelper.PlayQueue.Count; i++)
            {
                if (MediaHelper.PlayQueue[listIndexPlay[i]].FilePath == PlayMedia.Path)
                {
                    if (i != 0)
                    {
                        PlayMedia.URL = MediaHelper.PlayQueue[i - 1].FilePath;
                        PlayMedia.media = MediaHelper.PlayQueue[i - 1];
                        PlayMedia.playSong();
                    }
                    return;
                }
            }
        }

        private void Repeat()
        {
            if (PlayMedia.Repeat == RepeatMode.Off)
            {
                PlayMedia.Repeat = RepeatMode.All;
            }
            else if (PlayMedia.Repeat == RepeatMode.All)
            {
                PlayMedia.Repeat = RepeatMode.One;
            }
            else if (PlayMedia.Repeat == RepeatMode.One)
            {
                PlayMedia.Repeat = RepeatMode.Off;
            }
        }

        private void Suff()
        {
            if (PlayMedia.Suffle)
            {
                PlayMedia.Suffle = false;
                listIndexPlay = new List<int>(MediaHelper.ListIndexPlayQueue);
            }
            else
            {
                PlayMedia.Suffle = true;
                listIndexPlay = new List<int>(MediaHelper.ListRandomIndex);
            }
        }

    }
}
