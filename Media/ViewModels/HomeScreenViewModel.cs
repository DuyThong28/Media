using Media.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.ViewModels
{
    public class HomeScreenViewModel:ViewModelBase
    {
        public HomeScreenViewModel()
        {
        }

        private List<MediaItem> listSongs;
        private List<MediaItem> listVideos;
        public List<MediaItem> ListSongs { get => listSongs; set { this.RaiseAndSetIfChanged(ref listSongs, value); } } 
        public List<MediaItem> ListVideos { get => listVideos; set { this.RaiseAndSetIfChanged(ref listVideos, value); } }
    }


}
