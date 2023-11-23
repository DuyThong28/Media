using Avalonia.Controls;
using Avalonia.Platform.Storage;
using Media.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using TagLib;


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
