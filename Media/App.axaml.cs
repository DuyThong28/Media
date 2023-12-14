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
            ShowUpdate();
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
        private async void ShowUpdate()
        {
            WebClient webClient = new WebClient();
            var client = new WebClient();
            if (!webClient.DownloadString("https://www.dropbox.com/scl/fi/a5tsavuttkfqb64qukigo/Update.txt?rlkey=lg6dqscl6g2ryi9lrdghmyxnm&dl=1").Contains("1.0.0"))
            {
                var result = await MessageBoxManager.GetMessageBoxStandard("Update", "New update available! Do you want to install it?", ButtonEnum.YesNo).ShowAsync();

                if (result == ButtonResult.Yes)
                {
                    try
                    {
                        if (System.IO.File.Exists(@".\Media Setup.exe"))
                        {
                            System.IO.File.Delete(@".\Media Setup.exe");
                        }
                        client.DownloadFile("https://www.dropbox.com/scl/fi/4eg6ubpzl7ig5e9r6acbz/Media-SetUp.zip?rlkey=g70jjl2modwa3flci7dnm8626&dl=1", @"Media Setup.zip");
                        string zipPath = @".\Media Setup.zip";
                        string extractPath = @".\";
                        ZipFile.ExtractToDirectory(zipPath, extractPath);
                        string[] setupFiles = System.IO.Directory.GetFiles(extractPath, "Media Setup.exe", System.IO.SearchOption.AllDirectories);
                        if (setupFiles.Length > 0)
                        {
                            ProcessStartInfo startInfo = new ProcessStartInfo();
                            startInfo.FileName = setupFiles[0];
                            Process.Start(startInfo);
                        }
                    }
                    catch
                    {

                    }
                }
            }
        }

        void OnExit(object sender, ControlledApplicationLifetimeExitEventArgs e)
        {          
            PlayMedia.dispose();
        }
    }
}