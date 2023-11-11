using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.ViewModels
{
    public class PlayingScreenViewModel:ViewModelBase
    {
        public PlayingScreenViewModel()
        {
        }

        private ObservableCollection<string> listMedia;
        public ObservableCollection<string> ListMedia { get => listMedia; set { this.RaiseAndSetIfChanged(ref listMedia, value); } }
    }
}
