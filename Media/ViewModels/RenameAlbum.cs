using Media.Models;

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
