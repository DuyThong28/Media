using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Media.Models;
using ReactiveUI;

namespace Media.ViewModels
{
    public class PlaylistScreenViewModel:ViewModelBase
    {
        public PlaylistScreenViewModel() {
        }

        private List<MediaItem> listMedia;
        public List<MediaItem> ListMedia { get => listMedia; set { this.RaiseAndSetIfChanged(ref listMedia, value); } }
    }
}
