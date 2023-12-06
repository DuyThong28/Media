using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using Avalonia.Controls;
using Avalonia.Interactivity;
using DynamicData;
using Media.Models;
using Media.Views;
using ReactiveUI;
using TagLib;
using MsBox.Avalonia;
using System.Threading.Tasks;

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
        private static List<MediaItem> listSongs = new List<MediaItem>();
        private static List<MediaItem> listVideos = new List<MediaItem>();
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
            set { allPlayList = value; OnAllPlayListChanged(); }
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
                    (listRanIndex[n], listRanIndex[k]) = (listRanIndex[k], listRanIndex[n]);
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
            MediaHelper.PlayQueue[0].IsPlay = true;
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
            MediaHelper.PlayQueue[0].IsPlay = true;
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
        public static List<MediaItem> ListSongs { get => listSongs; set { listSongs = value; OnListSongsChanded(); } }

        public static List<MediaItem> ListVideos { get => listVideos; set { listVideos = value; OnListVideosChanded(); } }

        public static async Task FetchListMedia(MediaTypes mediaTypes)
        {
            string searchPattern;
            List<MediaItem> listMedia = new List<MediaItem>();
            string path;

            switch (mediaTypes)
            {
                case MediaTypes.Audio:
                    searchPattern = "*.mp3";
                    path = musicPathFolder;
                    listMedia = ListSongs = new List<MediaItem>();
                    break;
                case MediaTypes.Video:
                    searchPattern = "*.mp4";
                    path = videoPathFolder;
                    listMedia = ListVideos = new List<MediaItem>();
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
                await MessageBoxManager.GetMessageBoxStandard("Lỗi", "Lỗi đường dẫn " + path + " không tồn tại").ShowWindowDialogAsync(MainWindow.GetInstance());
            }

            foreach (string filePath in filePaths)
            {
                listMedia.Add(item: new MediaItem(filePath));
            }
            AllMedias.Clear();
            AllMedias.AddRange(ListSongs);
            AllMedias.AddRange(ListVideos);
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
        
        public static event EventHandler updateHomeScreen;
        public static event EventHandler UpdateHomeScreen
        {
            add { updateHomeScreen += value; }
            remove { updateHomeScreen -= value; }
        } 
        
        public static event EventHandler updateListMediaScreen;
        public static event EventHandler UpdateListMediaScreen
        {
            add { updateListMediaScreen += value; }
            remove { updateListMediaScreen -= value; }
        }

        public static event EventHandler updateListVideoScreen;
        public static event EventHandler UpdateListVideoScreen
        {
            add { updateListVideoScreen += value; }
            remove { updateListVideoScreen -= value; }
        }


        public static event EventHandler openVideoScreen;
        public static event EventHandler OpenVideoScreen
        {
            add { openVideoScreen += value; }
            remove { openVideoScreen -= value; }
        }

        public static event EventHandler turnPlayingScreen;
        public static event EventHandler TurnPlayingScreen
        {
            add => turnPlayingScreen += value;
            remove { turnPlayingScreen -= value; }
        }
        public static void UpdateScreen(object sender, EventArgs e)
        {
   
            if (updatePlaylistScreen != null)
            {
                updatePlaylistScreen(sender, new EventArgs());
            }
            if (updateLibraryScreen != null)
            {
                updateLibraryScreen(sender, new EventArgs());
            }
            if (updatePlayingScreen != null)
            {
                updatePlayingScreen(sender, new EventArgs());
            }
            if (turnPlayingScreen != null)
            {
                turnPlayingScreen(sender, new EventArgs());
            }
        }

        public static void UpdateMainScreen(object sender, EventArgs e)
        {
            if (updateListVideoScreen != null)
            {
                updateListVideoScreen(sender, new EventArgs());
            }
            if(updateHomeScreen!= null)
            {
                updateHomeScreen(sender, new EventArgs());
            }
            if(updateListMediaScreen!= null)
            {
                updateListMediaScreen(sender, new EventArgs());
            }
            if (updatePlaylistScreen != null)
            {
                updatePlaylistScreen(sender, new EventArgs());
            }

        }


        public static void UpdateMediaControl(object sender, EventArgs e)
        {
            if (updateMediaScreen != null)
            {
                updateMediaScreen(sender, new EventArgs());
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
            if (MediaHelper.isPlayingPlaylist == true && MediaHelper.playListPlayingId == playlist.PlayListID)
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
        
        public static event EventHandler AllPlayListChanged;

        public static void OnAllPlayListChanged()
        {
            AllPlayListChanged?.Invoke(null, EventArgs.Empty);
            UpdateMainScreen(null, new EventArgs());
        }
        public static event EventHandler ListSongsChanged;
        public static void OnListSongsChanded()
        {
            ListSongsChanged?.Invoke(null, EventArgs.Empty);
        }
        public static event EventHandler ListVideosChanged;
        public static void OnListVideosChanded()
        {
            ListVideosChanged?.Invoke(null, EventArgs.Empty);
        }

        public static void AddMediaQueue_Click(object sender, RoutedEventArgs e)
        {
            //bool isplay = false;
            if (sender is MenuItem menuItem)
            {
                if (menuItem.DataContext is MediaItem mediaItem)
                {
                    for (int i = 0; i < PlayQueue.Count; i++)
                    {
                        if (PlayQueue[i].FilePath == mediaItem.FilePath)
                        {
                            PlayQueue.Remove(PlayQueue[i]);
                        }
                    }
                    PlayQueue.Add(mediaItem);

                    List<MediaItem> tempQueue = new List<MediaItem>(playQueue);
                    PlayQueue.Clear();
                    PlayQueue = new List<MediaItem>();
                    PlayQueue.AddRange(tempQueue);
                }
            }

            UpdateScreen(sender, e);
        }

        public static void PlayNextInQueue(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem menuItem)
            {
                if (menuItem.DataContext is MediaItem mediaItem)
                {
                    if (PlayQueue.Count==0)
                    {
                        PlayQueue.Add(mediaItem);
                    }
                    else if (mediaItem.FilePath == PlayMedia.media.FilePath)
                        return;
                    else
                    {
                        for (int i = 0; i < PlayQueue.Count; i++)
                        {
                            if (mediaItem.FilePath == playQueue[i].FilePath)
                            {
                                playQueue.RemoveAt(i);
                                break;
                            }
                        }
                        for (int i = 0; i < PlayQueue.Count; i++)
                        {
                            if (PlayMedia.media.FilePath == playQueue[i].FilePath)
                            {
                                PlayQueue.Insert(i + 1, mediaItem);
                                break;
                            }
                        }
                    }
                    List<MediaItem> tempQueue = new List<MediaItem>(playQueue);
                    PlayQueue.Clear();
                    PlayQueue = new List<MediaItem>();
                    PlayQueue.AddRange(tempQueue);
                    UpdateScreen(sender, e);
                }
            }
        }

        public static void MenuItem_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            IList<object> list = menuItem.CommandParameter as IList<object>;
            if (list != null)
            {
                MediaItem mediaItem = list[1] as MediaItem;
                if (!mediaItem.IsPlay)
                {
                    PlaylistScreenViewModel playlistscreen = list[0] as PlaylistScreenViewModel;
                    playlistscreen.Playlist.DeleteMedia(mediaItem);
                    playlistscreen.ListMedia.Remove(mediaItem);
                    playlistscreen.ListMedia = new List<MediaItem>(playlistscreen.ListMedia);
                }
            }
        }
        public static void MenuItem_Click_1(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            IList<object> list = menuItem.CommandParameter as IList<object>;
            if (list != null)
            {
                MediaItem mediaItem = list[1] as MediaItem;
                if (!mediaItem.IsPlay)
                {
                    PlayingScreenViewModel playingscreen = list[0] as PlayingScreenViewModel;
                    playingscreen.ListMedia.Remove(mediaItem);
                    playingscreen.ListMedia = new List<MediaItem>(playingscreen.ListMedia);
                    PlayQueue = playingscreen.ListMedia;
                }
            }
        }
        public static IEnumerable<IGrouping<char, Playlist>> SortListAToZ(List<Playlist> list)
        {
            IEnumerable<IGrouping<char, Playlist>> res = from playlist in list
                                                         orderby playlist.PlayListName ascending
                                                          group playlist by playlist.PlayListName[0];
            return res;
        }
        public static IEnumerable<IGrouping<string, Playlist>> SortListDateAdded(List<Playlist> list)
        {
            IEnumerable<IGrouping<string, Playlist>> res = from playlist in list
                                                            orderby playlist.DateCreated ascending
                                                            group playlist by playlist.DateCreated;
            return res;
        }

        public static void RenameAlbum_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = MainWindow.GetInstance();
            if (sender is MenuItem menuItem && menuItem.Tag is Playlist playlistToRename)
            {
                RenameAlbum.Playlist = playlistToRename;
                AddAlbumWindow addAlbumWindow = new AddAlbumWindow();
                addAlbumWindow.ShowDialog(mainWindow);
            }
            else if (sender is PlaylistScreenViewModel)
            {
                playlistToRename = (sender as PlaylistScreenViewModel).Playlist;
                RenameAlbum.Playlist = playlistToRename;
                AddAlbumWindow addAlbumWindow = new AddAlbumWindow();
                addAlbumWindow.ShowDialog(mainWindow);
                RenameAlbum.Playlistscreen = sender as PlaylistScreenViewModel;                        
            }
        }
    }
}
