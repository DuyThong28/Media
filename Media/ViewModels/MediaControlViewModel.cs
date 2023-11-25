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
//using Microsoft.CodeAnalysis.CSharp.Syntax;
using Avalonia.Threading;
using System.Reactive;
using NAudio.Wave;
using System.Runtime.InteropServices;
using LibVLCSharp.Shared;

namespace Media.ViewModels
{
    public class MediaControlViewModel:ViewModelBase
    {
        public MediaItem _media = null;
        private long tbMaxValue = 0;
        private long tbValue = 0;
        private float tbVolume = 1;
        private string timeSongEnd = "00:00";
        private string timeSongPlay = "00:00";
        private IImage imageSource = ImageHelper.LoadFromResource(new Uri("avares://Media/Assets/Icons/defaultImage.jpg"));
        private string songName = "Tên bài hát";
        private string nameAuthor = "Tên ca sĩ";
        private List<int> listIndexPlay;

        public MediaControlViewModel()
        {
            PlayNextCommand = ReactiveCommand.Create(() => { PlayNext(); });
            PlayMediaCommand = ReactiveCommand.Create(() => { Play(); });
            PlayPrevCommand = ReactiveCommand.Create(() => { PlayPrev(); });
            RepeatCommand = ReactiveCommand.Create(()=> { Repeat(); });
            SuffCommand = ReactiveCommand.Create(() => { Suff(); });
            if (PlayMedia.timer != null)
            {
                MediaHelper.updateMediaScreen += UpdateMediaControl;
            }
        }
        public long TbMaxValue
        {
            get { return tbMaxValue; }
            set { this.RaiseAndSetIfChanged(ref tbMaxValue, value); }
        }

        public long TbValue
        {
            get { return tbValue; }
            set { this.RaiseAndSetIfChanged(ref tbValue, value);
                    double seconds = value / 1000.0;
                    int minutes = (int)Math.Floor(seconds / 60);
                    if (minutes < 60)
                    {
                        TimeSongPlay = TimeSpan.FromMilliseconds(value).ToString(@"mm\:ss");
                    }
                    else
                    {
                        TimeSongPlay = TimeSpan.FromMilliseconds(value).ToString(@"hh\:mm\:ss");
                    }
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

        private bool isPlay = false;
        public bool IsPlay
        {
            get => isPlay;
            set => this.RaiseAndSetIfChanged(ref isPlay, value);
        }
        public float TbVolume
        {
            get { return tbVolume; }
            set { this.RaiseAndSetIfChanged(ref tbVolume, value);
                PlayMedia.Volume = tbVolume;
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
            if (PlayMedia.MediaPlayer.State == VLCState.Playing)
            {
                if (_media != PlayMedia.media)
                {
                    _media = PlayMedia.media;
                    SongName = PlayMedia.media.Title;
                    NameAuthor = PlayMedia.media.ArtistsString;
                    ImageSource = PlayMedia.media.Image;
                    TbMaxValue = PlayMedia.DurationSong;
                    TimeSongEnd = PlayMedia.DurationstringSong;
                }
                TbValue = PlayMedia.CurrentPositionSong;
                //TimeSongPlay = PlayMedia.CurrentPositionstringSong;
            }
            else if (PlayMedia.MediaPlayer.State == VLCState.Ended)
            {
                if (PlayMedia.Repeat == RepeatMode.One)
                {
                    PlayMedia.setCurrentPosition(0);
                    PlayMedia.playSong();
                } else
                {
                    PlayNext();
                }
            }

        }

        private void Play()
        {
            if (PlayMedia.media!=null)
            {
                PlayMedia.media.PlayMediaCommand();
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
                    }
                    else if (i == MediaHelper.PlayQueue.Count - 1 && PlayMedia.Repeat == RepeatMode.All)
                    {
                        PlayMedia.URL = MediaHelper.PlayQueue[0].FilePath;
                        PlayMedia.media = MediaHelper.PlayQueue[0];
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
