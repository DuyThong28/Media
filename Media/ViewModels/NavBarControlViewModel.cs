using Avalonia.Controls;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Media.ViewModels
{
    public class NavBarControlViewModel:ViewModelBase
    {
        private ListBoxItem selectedNavBarItem;
        public ListBoxItem SelectedNavBarItem
        {
            get { return selectedNavBarItem; }
            set => this.RaiseAndSetIfChanged(ref selectedNavBarItem, value);
        }
    }

}
