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
        public static bool isPlayingPlaylist = true;
        public static string playListPlayingId = null;

        public static List<MediaItem> listSongs = new List<MediaItem>();
        public static List<MediaItem> listVideos = new List<MediaItem>();
        private static List<Playlist> allPlayList;
        public static List<Playlist> AllPlayList
        {
            get
            {
                return allPlayList;
            }
        }
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
        public static int CurrentIndex
        {
            get
            {
                for (int i = 0; i < PlayQueue.Count; i++)
                {
                    if (PlayQueue[ListIndexPlayQueue[i]].FilePath == PlayMedia.Path)
                    {
                        return i;
                    }
                }
                return 0;
            }
        }

        public static List<int> ListRandomIndex
        {
            get
            {
                Random random = new Random();
                List<int> listRanIndex = new List<int>(ListIndexPlayQueue);
                listRanIndex.Remove(CurrentIndex);
                int n = listRanIndex.Count;
                while (n > 1)
                {
                    n--;
                    int k = random.Next(n + 1);
                    int value = listRanIndex[k];
                    listRanIndex[k] = listRanIndex[n];
                    listRanIndex[n] = value;
                }
                listRanIndex.Insert(0, CurrentIndex);
                return listRanIndex;
            }
        }

        public static void PlayThePlaylist(List<MediaItem> pl)
        {
            if (pl.Count == 0) return;
            isPlayingPlaylist = true;
            playQueue.Clear();
            playQueue = new List<MediaItem>(pl);
            PlayMedia.media = MediaHelper.PlayQueue[0];
            PlayMedia.URL = MediaHelper.PlayQueue[0].FilePath;
            PlayMedia.playSong();
        }
        public static void PlayThePlaylist(Playlist pl)
        {
            if (pl.ListMedia.Count == 0) return;
            isPlayingPlaylist = true;
            playListPlayingId = pl.PlayListID;
            playQueue.Clear();
            playQueue = new List<MediaItem>(pl.ListMedia);
            PlayMedia.URL = MediaHelper.PlayQueue[0].FilePath;
            PlayMedia.playSong();
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


        public static event EventHandler updateMediaScreen;
        public static event EventHandler UpdateMediaScreen
        {
            add { updateMediaScreen += value; }
            remove { updateMediaScreen -= value; }
        }

        public static event EventHandler updatePlaylistScreen;
        public static event EventHandler UpdatePlaylistScreen
        {
            add { updatePlaylistScreen += value; }
            remove { updatePlaylistScreen -= value; }
        }
        public static event EventHandler updateListMediaScreen;
        public static event EventHandler UpdateListMediaScreen
        {
            add { updateListMediaScreen += value; }
            remove { updateListMediaScreen -= value; }
        }
        public static void UpdateScreen(object sender, EventArgs e)
        {
            if (updateMediaScreen != null)
            {
                updateMediaScreen(sender, new EventArgs());
            }
            if (updatePlaylistScreen != null)
            {
                updatePlaylistScreen(sender, new EventArgs());
            }
            if (updateListMediaScreen != null)
            {
                updateListMediaScreen(sender, new EventArgs());
            }
        }

    }
}
