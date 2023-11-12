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

namespace Media.ViewModels
{
    public static class PlayMedia
    {

        static string path = null;
        static double currentTimePlay = 0.0;
        static bool checkFirst = false;
        public static bool Suffle = false;
        public static WaveOutEvent waveOutEvent = new WaveOutEvent();
        public static AudioFileReader audioFileReader;
        //public static string mp3FilePath = @"C:\Users\duyth\OneDrive\Máy tính\Media\ChanAi-OrangeKhoi-6225088.mp3";
        public static bool IsFirst
        {
            get { return checkFirst; }
        }
        public static void setCurrentTimePlay()
        {
           currentTimePlay = audioFileReader.CurrentTime.TotalSeconds;
        }
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
                if (waveOutEvent.PlaybackState == PlaybackState.Playing)
                    return audioFileReader.TotalTime.ToString();
                return "00:00";
            }
        }
        public static string CurrentPositionstringSong
        {
            get
            {
                if (waveOutEvent.PlaybackState == PlaybackState.Playing || waveOutEvent.PlaybackState == PlaybackState.Paused)
                    return audioFileReader.CurrentTime.ToString();
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
                    audioFileReader = new AudioFileReader(path);
                    waveOutEvent.Init(audioFileReader);
                    currentTimePlay = 0.0;
                    waveOutEvent.Stop();
                }
            }
        }
        public static float Volume
        {
            set {   waveOutEvent.Volume = value; }
        }
        public static void continueSong()
        {
            if (waveOutEvent.PlaybackState == PlaybackState.Paused)
            {
                audioFileReader.CurrentTime = audioFileReader.CurrentTime;
                //audioFileReader.CurrentTime = TimeSpan.FromSeconds(currentTimePlay);
                waveOutEvent.Play();
            }
        }
        public static void playSong()
        {
            audioFileReader.CurrentTime = TimeSpan.FromSeconds(currentTimePlay);
            waveOutEvent.Play();
        }
        public static void stopSong()
        {
            currentTimePlay = 0.0;
            waveOutEvent.Stop();
        }
        public static void pauseSong()
        {
            waveOutEvent.Pause();
        }
        public static void muteVolume()
        {
            waveOutEvent.Volume = 0;
        }
        public static void setCurrentPosition(int mousePosition, int progressBarWidth)
        {
            //try
            //{
            //    if (waveOutEvent. != null)
            //        player.Ctlcontrols.currentPosition = player.currentMedia.duration * mousePosition / progressBarWidth;

            //}
            //catch (Exception ex)
            //{
            //    Debug.Print(ex.ToString());
            //}
        }
    }
}
