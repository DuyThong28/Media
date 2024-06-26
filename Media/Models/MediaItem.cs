﻿using Media.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TagLib;
using Avalonia.Media;
using ReactiveUI;
using System.Reactive;
using LibVLCSharp.Shared;
using Avalonia.Media.Imaging;

namespace Media.Models
{
    public class MediaItem
    {
        private string title;
        private List<string> artists;
        private string filePath;
        private IImage image;
        private TimeSpan duration;
        private string dateAdded;
        private MediaTypes mediaType;
 
        private string playlistID = null;
        public MediaTypes MediaTypes { get { return mediaType; } }
        public string Title
        {
            set => title = value;
            get
            {
                if (title == null) return "Unknown";
                return title;
            }
        }

        public List<string> Artists
        {
            set => artists = value;
            get
            {
                if (artists == null)
                {
                    artists = new List<string>()
                    {
                        "Unknown"
                    };
                }
                return artists;
            }
        }

        public string ArtistsString
        {
            get
            {
                if (artists == null)
                {
                    return "Unknown";
                }
                string txt = "";
                for (int i = 0; i < artists.Count; i++)
                {
                    txt += artists[i].Trim();
                    if (i + 1 < artists.Count)
                    {
                        txt += ", ";
                    }
                }
                return txt;
            }
        }


        public IImage Image
        {
            set => image = value;
            get
            {
                return image;
            }
        }

        public TimeSpan Duration
        {
            set => duration = value;
            get
            {
                return duration;
            }
        }

        public string DateAdded
        {
            set => dateAdded = value;
            get
            {
                return dateAdded;
            }
        }

        public string FilePath
        {
            set => filePath = value;
            get
            {
                return filePath;
            }
        }

        public string DurationText
        {
            get
            {
                string durationText = "";
                if (duration.Minutes < 10)
                    durationText += 0;
                durationText += duration.Minutes.ToString() + ':';
                if (duration.Seconds < 10)
                    durationText += 0;
                durationText += duration.Seconds.ToString();
                return durationText;
            }
        }
        public ReactiveCommand<Unit, Unit> PlaySongCommand
        {
            get; set;
        }

        public void PlayMediaCommand()
        {
            if (PlayMedia.Path != this.filePath)
            {
                for (int i = 0; i < MediaHelper.PlayQueue.Count; i++)
                {
                    if (MediaHelper.PlayQueue[i].FilePath == this.FilePath)
                    {
                        PlayMedia.URL = this.filePath;
                        return;
                    }
                }
            }
            else
            {
                if (PlayMedia.MediaPlayer.State == VLCState.Stopped)
                {
                    PlayMedia.playSong();

                }
                else if (PlayMedia.MediaPlayer.State == VLCState.Playing)
                {
                    PlayMedia.pauseSong();
                }
                else if (PlayMedia.MediaPlayer.State == VLCState.Paused)
                {
                    PlayMedia.continueSong();
                } else if(PlayMedia.MediaPlayer.State == VLCState.Ended)
                {
                    PlayMedia.continueSong();
                }
            }
        }
        public string PlaylistID { get => playlistID; set => playlistID = value; }

        public MediaItem(string path)
        {
            using (TagLib.File taglib = TagLib.File.Create(path))
            {
                this.title = taglib.Tag.Title ?? Path.GetFileNameWithoutExtension(path);
                this.artists = taglib.Tag.Artists.Length != 0 ? taglib.Tag.Artists[0].Split(',').ToList() : null;
                this.duration = taglib.Properties.Duration != null ? taglib.Properties.Duration : new TimeSpan(0, 0, 0);
                this.dateAdded = (System.IO.File.GetCreationTime(path) ).ToString("dd/MM/yyyy");
                this.filePath = path;
                this.mediaType = taglib.Properties.MediaTypes;

                if (taglib.Tag.Pictures.Length > 0)
                {
                    byte[] bin = (byte[])(taglib.Tag.Pictures[0].Data.Data);
                    string base64String = Convert.ToBase64String(bin);
                    byte[] imageBytes = Convert.FromBase64String(base64String);
                    this.image = new Bitmap(new MemoryStream(imageBytes));
                }
                else
                {
                    try
                    {
                        var ffMpeg = new NReco.VideoConverter.FFMpegConverter();
                        string fileName = Path.Combine(Environment.CurrentDirectory, "Video Thumbnail", $"{title}.jpg");
                        Directory.CreateDirectory(Path.Combine(Environment.CurrentDirectory, "Video Thumbnail"));
                        if (!System.IO.File.Exists(fileName))
                        {
                            ffMpeg.GetVideoThumbnail(path, fileName, 5);
                        }
                        this.Image = new Bitmap(fileName);
                    }
                    catch
                    {
                        this.image = ImageHelper.LoadFromResource(new Uri("avares://Media/Assets/Icons/defaultImage.jpg"));
                    }
                }
            }
           this.PlaySongCommand = ReactiveCommand.Create(() => { PlayMediaCommand();});
           
        }

        public MediaItem()
        {

        }
    }
}
