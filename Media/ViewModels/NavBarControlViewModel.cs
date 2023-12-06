using Avalonia.Controls;
using ReactiveUI;

namespace Media.ViewModels
{
    public class NavBarControlViewModel:ViewModelBase
    {
        public NavBarControlViewModel()
        {

        }

        private ListBoxItem selectedNavBarItem;
        public ListBoxItem SelectedNavBarItem
        {
            get { return selectedNavBarItem; }
            set => this.RaiseAndSetIfChanged(ref selectedNavBarItem, value);
        }
    }

}
