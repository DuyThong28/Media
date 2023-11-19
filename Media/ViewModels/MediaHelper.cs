using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using DynamicData;
using LibVLCSharp.Shared;
using Media.Models;
using Media.Views;
using ReactiveUI;
using TagLib;

namespace Media.ViewModels
{
    static class MediaHelper
    {
        private static PlaylistDatabase database = new PlaylistDatabase();
        private static string musicPathFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
        private static string videoPathFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
        private static List<MediaItem> playQueue = new List<MediaItem>();
        private static List<int> listIndexDefalt = new List<int>();
        public static bool isPlayingPlaylist = false;
        public static string playListPlayingId = null;
        private static List<Playlist> allPlayList = database.QueryAllPlaylists();
        public static List<MediaItem> listSongs = new List<MediaItem>();
        public static List<MediaItem> listVideos = new List<MediaItem>();
        public static List<MediaItem> allMedias = new List<MediaItem>();

        public static List<MediaItem> AllMedias
        {
            get => allMedias;
            set => allMedias = value;
        }
        public static List<Playlist> AllPlayList
        {
            get
            {
                return allPlayList;
            }
        }
        public static void UpdatePlaylist(Playlist playlist)
        {
            int index = MediaHelper.AllPlayList.FindIndex(pl => pl.PlayListID == playlist.PlayListID);
            if (index > 0)
            {
                allPlayList[index] = playlist;
                Database.UpdatePlaylist(playlist);
            }
        }
        public static void AddPlayList(Playlist playlist)
        {
            if (allPlayList.Exists(x => x.PlayListID == playlist.PlayListID))
            {
                throw new Exception("Playlist trùng ID");
            }
            allPlayList.Add(playlist);
            database.InsertPlaylist(playlist);
            OnAllPlayListChanged();
        }

        public static void DeletePlayList(Playlist playlist)
        {
            int index = allPlayList.FindIndex(x => x.PlayListID == playlist.PlayListID);
            if (index >= 0)
            {
                database.DeletePlaylist(allPlayList[index].PlayListID);
                allPlayList.RemoveAt(index);
            }
            OnAllPlayListChanged();
        }
        public static void DeleteMediaFromPlaylist(string mediaPath, string playlistID)
        {
            int index = allPlayList.FindIndex(x => x.PlayListID == playlistID);
            if (index >= 0)
            {
                int indexOfMedia = allPlayList[index].ListMedia.FindIndex(x => x.FilePath == mediaPath);
                allPlayList[index].ListMedia.RemoveAt(indexOfMedia);
            }
            database.DeleteMediaInAPlaylist(mediaPath, playlistID);
        }
        public static List<MediaItem> PlayQueue
        {
            set => playQueue = value;
            get => playQueue;
        }
        public static ReactiveCommand<Unit, Unit> newCommand { get; set; }

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
            isPlayingPlaylist = false;
            playListPlayingId = null;
            playQueue.Clear();
            playQueue = new List<MediaItem>(pl);
            PlayMedia.media = MediaHelper.PlayQueue[0];
            PlayMedia.URL = MediaHelper.PlayQueue[0].FilePath;
            if (PlayMedia.media.MediaTypes != TagLib.MediaTypes.Audio)
            {
                if (openVideoScreen != null)
                {
                    openVideoScreen(PlayMedia.media, new EventArgs());
                }
            }
        }
        public static void PlayThePlaylist(Playlist pl)
        {
            if (pl.ListMedia.Count == 0) return;
            isPlayingPlaylist = true;
            playListPlayingId = pl.PlayListID;
            playQueue.Clear();
            playQueue = new List<MediaItem>(pl.ListMedia);
            PlayMedia.URL = MediaHelper.PlayQueue[0].FilePath;
            if (PlayMedia.media.MediaTypes != TagLib.MediaTypes.Audio)
            {
                if (openVideoScreen != null)
                {
                    openVideoScreen(PlayMedia.media, new EventArgs());
                }
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

        internal static PlaylistDatabase Database { get => database; set => database = value; }

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
            AllMedias.Clear();
            AllMedias = new List<MediaItem>(MediaHelper.listSongs);
            AllMedias.AddRange(MediaHelper.listVideos);
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

        public static event EventHandler updatePlayingScreen;
        public static event EventHandler UpdatePlayingScreen
        {
            add { updatePlayingScreen += value; }
            remove { updatePlayingScreen -= value; }
        }  
        
        public static event EventHandler updateLibraryScreen;
        public static event EventHandler UpdateLibraryScreen
        {
            add { updateLibraryScreen += value; }
            remove { updateLibraryScreen -= value; }
        }

        public static event EventHandler openVideoScreen;
        public static event EventHandler OpenVideoScreen
        {
            add { openVideoScreen += value; }
            remove { openVideoScreen -= value; }
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
            if (updatePlayingScreen != null)
            {
                updatePlayingScreen(sender, new EventArgs());
            }
            if (updateLibraryScreen != null)
            {
                updateLibraryScreen(sender, new EventArgs());
            }
        }


        public static void ListBox_DoubleTapped(object? sender, Avalonia.Input.TappedEventArgs e)
        {
            MediaItem media = (sender as ListBox).SelectedItem as MediaItem;
            if (media != null)
            {
                if (media.MediaTypes != TagLib.MediaTypes.Audio)
                {
                    if (openVideoScreen != null)
                    {
                        openVideoScreen(sender, new EventArgs());
                    }
                }
                media.PlayMediaCommand();
            }
        }

        public static MediaItem selectPlayingItem(List<MediaItem> listMedia)
        {
            MediaItem selectedItem = null;
            if (listMedia != null)
            {
                selectedItem = listMedia.Cast<MediaItem>().FirstOrDefault(item => item.FilePath == PlayMedia.Path);

            }
            return selectedItem;
        }
        
        public static MediaItem selectPlaylistItem(Playlist playlist)
        {
            MediaItem selectedItem = null;
            if (MediaHelper.isPlayingPlaylist==true&&MediaHelper.playListPlayingId==playlist.PlayListID)
            {
                if (playlist.ListMedia != null)
                {
                    selectedItem = playlist.ListMedia.Cast<MediaItem>().FirstOrDefault(item => item.FilePath == PlayMedia.Path);

                }
            }
            return selectedItem;
        }

        public static Playlist selectPlaylist(List<Playlist> list)
        {
            Playlist selectedItem = null;
            if (list != null)
            {
               selectedItem = list.Cast<Playlist>().FirstOrDefault(item => item.PlayListID == MediaHelper.playListPlayingId);
            }
            return selectedItem;
        }
        //
        public static event EventHandler AllPlayListChanged;

        private static void OnAllPlayListChanged()
        {
            AllPlayListChanged?.Invoke(null, EventArgs.Empty);
        }

        //
    }
}
