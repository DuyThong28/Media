using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

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
            set {backroundImageFileName = value;}
            get => backroundImageFileName;
        }

        public Image BackGroundImage =>  Image.FromFile(backroundImageFileName);

 
        public DateTime DateCreated
        {
            set => dateCreated = value;
            get { return dateCreated; }
        }


        public Playlist(string id = null, string name = "Unnamed", string backroundImageFileName = null, List<MediaItem> listMedia = null)
        {
            if (id == null) playListID = Guid.NewGuid().ToString("N");
            else playListID = id;
            playListName = name;
            this.backroundImageFileName = backroundImageFileName;
            dateCreated = DateTime.Now;
            if (listMedia != null)
                this.ListMedia = listMedia;
        }
    }
}
