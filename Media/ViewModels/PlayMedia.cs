using System;
using Avalonia.Threading;
using Media.Models;
using LibVLCSharp.Shared;
using MyMedia = LibVLCSharp.Shared.Media;
using TagLib;
using Avalonia.Markup.Xaml.MarkupExtensions;

namespace Media.ViewModels
{
    public enum RepeatMode
    {
        Off = 0,
        One = 1,
        All = 2,
    }
    public static class PlayMedia
    {
        public static MediaItem media;
        public static DispatcherTimer timer = new DispatcherTimer();
        public static RepeatMode Repeat = RepeatMode.Off;
        static string path = null;
        static long currentTimePlay = 0;
        static bool checkFirst = false;
        public static bool Suffle = false;
        public static LibVLC _libVlc;
        public static MediaPlayer MediaPlayer;
        public static MyMedia _media;
        public static bool IsPlay
        {
            get; set;
        } = false;

        public static bool IsPlayVideo
        {
            get; set;
        } = false;
        public static bool IsFirst
        {
            get { return checkFirst; }
        }
        public static long CurrentTimePlay
        {
            get { return currentTimePlay; }
            set { currentTimePlay = value; }
        }

        public static float Volume
        {
            set { MediaPlayer.Volume = Convert.ToInt32(value*100); }
            get { return MediaPlayer.Volume/100; }
        }

        public static long DurationSong
        {
            get { return MediaPlayer.Length; }
        }
        public static long CurrentPositionSong
        {
            get { return MediaPlayer.Time; }
        }
        public static string DurationstringSong
        {
            get
            {
                if (MediaPlayer != null)
                {
                    double seconds = MediaPlayer.Length / 1000.0;
                    int minutes = (int)Math.Floor(seconds / 60);
                    if (minutes < 60)
                    {
                        return TimeSpan.FromMilliseconds(MediaPlayer.Length).ToString(@"mm\:ss");
                    }
                    else
                    {
                        return TimeSpan.FromMilliseconds(MediaPlayer.Length).ToString(@"hh\:mm\:ss");
                    }
                }
                return "00:00";
            }
        }
        public static string CurrentPositionstringSong
        {
            get
            {
                if (MediaPlayer != null)
                {
                    double seconds = MediaPlayer.Length / 1000.0;
                    int minutes = (int)Math.Floor(seconds / 60);
                    if (minutes < 60)
                    {
                        return TimeSpan.FromMilliseconds(MediaPlayer.Time).ToString(@"mm\:ss");
                    }
                    else
                    {
                        return TimeSpan.FromMilliseconds(MediaPlayer.Time).ToString(@"hh\:mm\:ss");
                    }
                }
                return "00:00";
            }
        }
        public static string Path
        {
            get { return path; }
            set { path = value;}
        }
        public static string URL
        {
            set
            {
                checkFirst = true;

                if (value != null)
                {
                    path = value;
                }
                if (path != null)
                {
                    //MediaPlayer.Pause();
                    media = new MediaItem(path);
                    _media = new MyMedia(_libVlc, new Uri(path));
                    IsPlay = true;
                    if (media.MediaTypes == MediaTypes.Audio)
                    {
                        IsPlayVideo = false;
                    }
                    else
                    {
                       IsPlayVideo = true;
                    }
                    MediaPlayer.Play(_media);

                    if (updateScreen != null)
                    {
                        updateScreen(null, new EventArgs());
                    }
                    timer.Start();
                    timer.Interval = new TimeSpan(0, 0, 1);
                    currentTimePlay =0;
                    _media.Dispose();
                }
            }
        }
        public static void setCurrentTimePlay()
        {
            currentTimePlay = MediaPlayer.Time;
        }
        public static void continueSong()
        {
            if (MediaPlayer.State == VLCState.Paused)
            {
                MediaPlayer.Time = currentTimePlay;
                IsPlay = true;
                MediaPlayer.Play();
                if(updateScreen != null)
                    {
                    updateScreen(null, new EventArgs());
                }
            }
        }
        public static void playSong()
        {
            IsPlay = true;
            MediaPlayer.Time = currentTimePlay;
            MediaPlayer.Play(_media);
            if (updateScreen != null)
            {
                updateScreen(null, new EventArgs());
            }
        }
        public static void dispose()
        {
            IsPlay = false;
            currentTimePlay = 0;
            MediaPlayer?.Dispose();
            _libVlc?.Dispose();
        }
        public static void pauseSong()
        {
            IsPlay = false;
            currentTimePlay = MediaPlayer.Time;
            MediaPlayer.Pause();
            if (updateScreen != null)
            {
                updateScreen(null, new EventArgs());
            }
        }

        public static void stopSong()
        {
            IsPlay = false;
            MediaPlayer.Stop();
            if (updateScreen != null)
            {
                updateScreen(null, new EventArgs());
            }
        } 
        public static void muteVolume()
        {
            MediaPlayer.Volume = 0;
        }
        public static void setCurrentPosition(int position)
        {
            if (MediaPlayer != null)
            {
                MediaPlayer.Time = position;
                CurrentTimePlay = position;
            }
        }

        private static event EventHandler updateScreen;
        public static event EventHandler UpdateScreen
        {
           add { updateScreen += value; }
            remove { updateScreen -= value; }
        }

       

        
    }
}
