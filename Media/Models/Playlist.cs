using Media.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Avalonia.Media;
using Path = System.IO.Path;

namespace Media.Models
{
    public class Playlist
    {
        private string playListID;
        private string playListName;
        private List<MediaItem> listMedia = new List<MediaItem>();
        private string backroundImageFileName = null;
        private string dateCreated;
        private static readonly string ImageBackgroundFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Media Player\\Play List Image";

        public string PlayListID => playListID;
        public string PlayListName
        {
            get => playListName;
            set => playListName = value;
        }
        public string BackroundImageFileName
        {
            set { backroundImageFileName = value; }
            get => backroundImageFileName;
        }

        public IImage BackGroundImage
        {
            get { if (File.Exists(backroundImageFileName))
                    return new Avalonia.Media.Imaging.Bitmap(backroundImageFileName);
                else
                    return null;
            }
        } 

        public string DateCreated
        {
            set => dateCreated = value;
            get { return dateCreated; }
        }

        public List<MediaItem> ListMedia { get => listMedia; set => listMedia = value; }

        public Playlist(string id = null, string name = "Unnamed", string backroundImageFileName = null, List<MediaItem> listMedia = null, string dateCreated = null)
        {
            string fileName = Path.Combine(Environment.CurrentDirectory, "Default Image", "defaultImage.jpg");
            Directory.CreateDirectory(Path.Combine(Environment.CurrentDirectory, "Default Image"));
            string curDir = Environment.CurrentDirectory.Trim();
            string newPath = Path.GetFullPath(Path.Combine( curDir, @"..\..\..\"));
            string imagepath = Path.GetFullPath((newPath + @"\Assets\Icons\defaultImage.jpg"));
            if (!File.Exists(fileName))
            {
                File.Copy(imagepath, fileName);
            }    
            if (backroundImageFileName == null)
            {
                backroundImageFileName = fileName;
            }
            if (id == null) playListID = Guid.NewGuid().ToString("N");
            else playListID = id;
            playListName = name;
            this.backroundImageFileName = backroundImageFileName;
            if (dateCreated == null)
                this.dateCreated = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            else
                this.dateCreated = dateCreated;
            if (listMedia != null)
                this.ListMedia = listMedia;
        }
        public static IEnumerable<IGrouping<char, MediaItem>> SortListAToZ(List<MediaItem> list)
        {
            IEnumerable<IGrouping<char, MediaItem>> res = from song in list
                                                          orderby song.Title ascending
                                                          group song by song.Title[0];
            return res;
        }

        public static IEnumerable<IGrouping<string, MediaItem>> SortListDateAdded(List<MediaItem> list)
        {
            IEnumerable<IGrouping<string, MediaItem>> res = from song in list
                                                            orderby song.DateAdded ascending
                                                            group song by song.DateAdded;
            return res;
        }

        public static IEnumerable<IGrouping<string, MediaItem>> SortListAlbum(List<MediaItem> list)
        {
            IEnumerable<IGrouping<string, MediaItem>> res = from song in list
                                                            orderby song.Album ascending
                                                            group song by song.Album;
            return res;
        }

        public static IEnumerable<IGrouping<string, MediaItem>> SortListArtists(List<MediaItem> list)
        {
            IEnumerable<IGrouping<string, MediaItem>> res = from song in list
                                                            orderby song.ArtistsString ascending
                                                            group song by song.ArtistsString;
            return res;
        }
        public void AddMedia(MediaItem media)
        {
            if (ListMedia.Exists(x => x.FilePath == media.FilePath))
            {
                return;
            }
            media.PlaylistID = PlayListID;
            ListMedia.Add(media);
            MediaHelper.Database.InsertMediaIntoPlaylistMedias(this, media);
            MediaHelper.OnAllPlayListChanged();
        }
        public void AddRangeMedia(List<MediaItem> medias)
        {
            foreach (MediaItem media in medias)
            {
                AddMedia(media);
            }
        }
        public void DeleteMedia(MediaItem media)
        {          
            media.PlaylistID = PlayListID;
            ListMedia.Remove(media);
            MediaHelper.Database.DeleteMediaInAPlaylist(media.FilePath, this.PlayListID);
            MediaHelper.OnAllPlayListChanged();
        }       
    }
}
