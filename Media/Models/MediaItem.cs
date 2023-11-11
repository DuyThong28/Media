using Media.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagLib;
using Avalonia.Controls;
using Avalonia.Media;
using System.Resources;
using System.Drawing;

namespace Media.Models
{
    public class MediaItem
    {
        private string title;
        private List<string> artists;
        private string album;
        private string filePath;
        private IImage image;
        private TimeSpan duration;
        private DateTime dateAdded;
        private MediaTypes mediaType;
        private TagLib.File others;
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

        public string Album
        {
            set => album = value;
            get
            {
                if (album == null) return "Unknown";
                return album;
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

        public DateTime DateAdded
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
        public TagLib.File Others => others;

        public MediaItem(string path)
        {
            using (TagLib.File taglib = TagLib.File.Create(path))
            {
                this.title = taglib.Tag.Title ?? Path.GetFileNameWithoutExtension(path);
                this.artists = taglib.Tag.Artists.Length != 0 ? taglib.Tag.Artists[0].Split(',').ToList() : null;
                this.duration = taglib.Properties.Duration != null ? taglib.Properties.Duration : new TimeSpan(0, 0, 0);
                this.dateAdded = System.IO.File.GetCreationTime(path);
                this.filePath = path;
                this.mediaType = taglib.Properties.MediaTypes;
                this.album = taglib.Tag.Album;
                this.others = taglib;

                if (taglib.Tag.Pictures.Length > 0)
                {
                    byte[] bin = (byte[])(taglib.Tag.Pictures[0].Data.Data);
                    var image = System.Drawing.Image.FromStream(new MemoryStream(bin));
                    this.image = ImageHelper.ConvertToAvaloniaBitmap(image);
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
                            ffMpeg.GetVideoThumbnail(path, fileName, 1);
                        }
                        this.Image = ImageHelper.ConvertToAvaloniaBitmap(new Bitmap(fileName));
                    }
                    catch
                    {
                        this.image = ImageHelper.LoadFromResource(new Uri("avares://Media/Assets/Icons/defaultImage.jpg"));
                    }
                }
                taglib.Save();
            }
        }

        public MediaItem()
        {

        }
    }
}
