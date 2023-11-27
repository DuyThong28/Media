using Media.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.ViewModels
{
    static class RenameAlbum
    {
        public static Playlist Playlist { get => playlist; set => playlist = value; }
        public static PlaylistScreenViewModel Playlistscreen { get => playlistscreen; set => playlistscreen = value; }

        private static Playlist playlist = null;
        private static PlaylistScreenViewModel playlistscreen;
       
    }
}
