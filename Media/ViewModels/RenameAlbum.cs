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

        private static Playlist playlist = null;
    }
}
