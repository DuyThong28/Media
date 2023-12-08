using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Media.ViewModels;
using Media.Views;
using Ao.Lang.AvaloniaUI;
using System.Globalization;
using Ao.Lang.Runtime;
using Ao.Lang;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

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