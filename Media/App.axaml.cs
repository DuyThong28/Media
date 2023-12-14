using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Media.ViewModels;
using Media.Views;
using MsBox.Avalonia.Enums;
using MsBox.Avalonia;
using System.Diagnostics;
using System.IO.Compression;
using System.Net;

namespace Media
{

    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }


        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {

                    DataContext = new MainWindowViewModel()
                };

                desktop.Exit += OnExit;
            }

            base.OnFrameworkInitializationCompleted();
        }
       

        void OnExit(object sender, ControlledApplicationLifetimeExitEventArgs e)
        {          
            PlayMedia.dispose();
        }
    }
}