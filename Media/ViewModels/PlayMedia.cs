using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using System.Reactive;
using NAudio.Wave;
using System.Diagnostics;
using Avalonia.Threading;
using Instances;
using Media.Models;

namespace Media.ViewModels
{
    public static class PlayMedia
    {
        public static MediaItem media;
        public static DispatcherTimer timer = new DispatcherTimer();
        static string path = null;
        static double currentTimePlay = 0.0;
        static bool checkFirst = false;
        public static bool Suffle = false;
        public static WaveOutEvent waveOutEvent = new WaveOutEvent();
        public static AudioFileReader audioFileReader;
        //public static bool IsFirst
        //{
        //    get { return checkFirst; }
        //}
        public static double CurrentTimePlay
        {
            get { return currentTimePlay; }
            set { currentTimePlay = value; }
        }

        public static double DurationSong
        {
            get { return audioFileReader.TotalTime.TotalSeconds; }
        }
        public static double CurrentPositionSong
        {
            get { return audioFileReader.CurrentTime.TotalSeconds; }
        }
        public static string DurationstringSong
        {
            get
            {
                if (audioFileReader!=null)
                    return audioFileReader.TotalTime.ToString(@"mm\:ss");
                return "00:00";
            }
        }
        public static string CurrentPositionstringSong
        {
            get
            {
                if (audioFileReader != null)
                    return audioFileReader.CurrentTime.ToString(@"mm\:ss");
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
                //checkFirst = true;

                if (value != null)
                {
                    path = value;
                }
                if (path != null)
                {
                    waveOutEvent.Dispose();
                    audioFileReader = new AudioFileReader(path);
                    waveOutEvent.Init(audioFileReader);
                    timer.Interval = new TimeSpan(0, 0, 1);
                    currentTimePlay = 0.0;

                }
            }
        }
        public static float Volume
        {
            set {   waveOutEvent.Volume = value; }
        }
        public static void setCurrentTimePlay()
        {
            currentTimePlay = audioFileReader.CurrentTime.TotalSeconds;
        }
        public static void continueSong()
        {
            if (waveOutEvent.PlaybackState == PlaybackState.Paused)
            {
                audioFileReader.CurrentTime = TimeSpan.FromSeconds(currentTimePlay);
                waveOutEvent.Play();
                timer.Start();
            }
        }
        public static void playSong()
        {
            audioFileReader.CurrentTime = TimeSpan.FromSeconds(currentTimePlay);
            waveOutEvent.Play();
            timer.Start();
        }
        public static void stopSong()
        {
            currentTimePlay = 0.0;
            waveOutEvent.Dispose();
            timer.Stop();
        }
        public static void pauseSong()
        {
            currentTimePlay = audioFileReader.CurrentTime.TotalSeconds;
            waveOutEvent.Pause();
            timer.Stop();
        }
        public static void muteVolume()
        {
            waveOutEvent.Volume = 0;
        }
        public static void setCurrentPosition(double position)
        {
            if (waveOutEvent != null)
            {
                audioFileReader.CurrentTime = TimeSpan.FromSeconds(position);
            }
        }
    }
}
