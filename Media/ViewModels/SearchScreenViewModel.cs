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
    public class SearchScreenViewModel:ViewModelBase
    {
        private List<MediaItem> searchResults;
        public List<MediaItem> SearchResults { get => searchResults; set { this.RaiseAndSetIfChanged(ref searchResults, value); } }

        private MediaItem selectedMedia;
        public MediaItem SelectedMedia
        {
            get => selectedMedia;
            set => this.RaiseAndSetIfChanged(ref selectedMedia, value);
        }
    }
}
