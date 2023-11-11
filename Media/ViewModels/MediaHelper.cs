using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.Primitives;
using DynamicData;
using Media.Models;
using Media.Views;
using ReactiveUI;
using TagLib;

namespace Media.ViewModels
{
    public static class MediaHelper
    {
        private static string musicPathFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
        private static string videoPathFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
        private static List<MediaItem> playQueue = new List<MediaItem>();
        private static List<int> listIndexDefalt = new List<int>();
        public static bool isPlayingPlaylist = false;
        public static string playListPlayingId = null;

        public static List<MediaItem> listSongs = new List<MediaItem>();
        public static List<MediaItem> listVideos = new List<MediaItem>();
        public static List<MediaItem> PlayQueue
        {
            set => playQueue = value;
            get => playQueue;
        }
        public static ReactiveCommand<Unit,Unit> newCommand { get; set; }

        public static List<int> ListIndexPlayQueue
        {
            get
            {
                listIndexDefalt.Clear();
                for (int i = 0; i < PlayQueue.Count; i++)
                {
                    listIndexDefalt.Add(i);
                }
                return listIndexDefalt;
            }
        }

        public static string MusicPathFolder
        {
            set
            {
                musicPathFolder = value;
            }
            get
            { return musicPathFolder; }
        }
        public static string VideoPathFolder
        {
            set
            {
                videoPathFolder = value;
            }
            get { return videoPathFolder; }
        }

        public static void FetchListMedia(MediaTypes mediaTypes)
        {
            string searchPattern;
            List<MediaItem> listMedia = new List<MediaItem>();
            string path;

            switch (mediaTypes)
            {
                case MediaTypes.Audio:
                    searchPattern = "*.mp3";
                    path = musicPathFolder;
                    listMedia = listSongs = new List<MediaItem>();
                    break;
                case MediaTypes.Video:
                    searchPattern = "*.mp4";
                    path = videoPathFolder;
                    listMedia = listVideos = new List<MediaItem>();
                    break;
                default:
                    throw new Exception("MediaTypes is invalid");
            }

            string[] filePaths = { };

            try
            {
                filePaths = System.IO.Directory.GetFiles(
                    path,
                    searchPattern,
                    System.IO.SearchOption.AllDirectories);
            }
            catch
            {
                
            }

            foreach (string filePath in filePaths)
            {
                listMedia.Add(item: new MediaItem(filePath));
            }
        }


    }
}
