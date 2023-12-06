using ReactiveUI;
using System.Reactive;


namespace Media.ViewModels
{
    public class SettingScreenViewModel : ViewModelBase
    {
        public SettingScreenViewModel()
        {
        }
        public  ReactiveCommand<Unit, Unit> OpenSongFolder { get; set; }
        public  ReactiveCommand<Unit, Unit> OpenVideoFolder { get; set; }
    }

}
