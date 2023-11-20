using Media.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace Media.ViewModels
{
    public class ListVideoScreenViewModel:ViewModelBase
    {
        public ListVideoScreenViewModel()
        {
            PlayMediaCommand = ReactiveCommand.Create(() => { MediaHelper.PlayThePlaylist(ListVideos); });
        }
        private List<MediaItem> listVideos;
        public List<MediaItem> ListVideos { get => listVideos; set { this.RaiseAndSetIfChanged(ref listVideos, value); } }
        public ReactiveCommand<Unit, Unit> PlayMediaCommand { get; private set; }
    }
}
