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
    public class LibraryScreenViewModel:ViewModelBase
    {
        public LibraryScreenViewModel()
        {

        }
        private List<MediaItem> listMedia;
        public List<MediaItem> ListMedia { get => listMedia; set { this.RaiseAndSetIfChanged(ref listMedia, value); } }
    }
}
