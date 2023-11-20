﻿using Media.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Media;
using System.Runtime.CompilerServices;

namespace Media.Models
{
    public class Playlist
    {
        private string playListID;
        private string playListName;
        private List<MediaItem> listMedia = new List<MediaItem>();
        private string backroundImageFileName = null;
        private DateTime dateCreated;
        private static readonly string ImageBackgroundFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Media Player\\Play List Image";
        public List<MediaItem> ListMedia { get => listMedia; set => listMedia = value; }

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

        public IImage BackGroundImage => ImageHelper.ConvertToAvaloniaBitmap(Image.FromFile(backroundImageFileName));

        public DateTime DateCreated
        {
            set => dateCreated = value;
            get { return dateCreated; }
        }


        public Playlist(string id = null, string name = "Unnamed", string backroundImageFileName = null, List<MediaItem> listMedia = null)
        {
            if (backroundImageFileName == null) backroundImageFileName = @"C:\Users\lenovo\source\repos\Media\Media\Assets\Icons\defaultImage.jpg";
            if (id == null) playListID = Guid.NewGuid().ToString("N");
            else playListID = id;
            playListName = name;
            this.backroundImageFileName = backroundImageFileName;
            dateCreated = DateTime.Now;
            if (listMedia != null)
                this.ListMedia = listMedia;
        }
        public static IEnumerable<IGrouping<char, MediaItem>> SortListAToZ(List<MediaItem> list)
        {
            IEnumerable<IGrouping<char, MediaItem>> res = from song in list
                                                          orderby song.Title ascending
                                                          group song by song.Title[0];
            return res.Reverse();
        }

        public static IEnumerable<IGrouping<string, MediaItem>> SortListDateAdded(List<MediaItem> list)
        {
            IEnumerable<IGrouping<string, MediaItem>> res = from song in list
                                                            orderby song.DateAdded ascending
                                                            group song by song.DateAdded.ToString("dd/MM/yyyy");
            return res.Reverse();
        }

        public static IEnumerable<IGrouping<string, MediaItem>> SortListAlbum(List<MediaItem> list)
        {
            IEnumerable<IGrouping<string, MediaItem>> res = from song in list
                                                            orderby song.Album ascending
                                                            group song by song.Album;
            return res.Reverse();
        }

        public static IEnumerable<IGrouping<string, MediaItem>> SortListArtists(List<MediaItem> list)
        {
            IEnumerable<IGrouping<string, MediaItem>> res = from song in list
                                                            orderby song.ArtistsString ascending
                                                            group song by song.ArtistsString;
            return res.Reverse();
        }
        public void AddMedia(MediaItem media)
        {
            if (listMedia.Exists(x => x.FilePath == media.FilePath))
            {
                return;
            }
            media.PlaylistID = PlayListID;
            listMedia.Add(media);
            MediaHelper.Database.InsertMediaIntoPlaylistMedias(this, media);
        }
        public void AddRangeMedia(List<MediaItem> medias)
        {
            foreach (MediaItem media in medias)
            {
                AddMedia(media);
            }
        }

    }
}
