using DynamicData;
using Media.Models;
using Media.Views;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.ViewModels
{
    public class SearchScreenViewModel:ViewModelBase
    {
        private ObservableCollection<MediaItem> searchResults;
        private MediaItem selectedMedia;
        private string searchText;
        public SearchScreenViewModel()
        {
            searchResults = new ObservableCollection<MediaItem>();
            this.WhenAnyValue(x => x.SearchText)
               .Throttle(TimeSpan.FromMilliseconds(400))
               .ObserveOn(RxApp.MainThreadScheduler)
               .Subscribe(DoSearch!);
        }
        public List<MediaItem> AllMedias
        {
            get
            {
                List<MediaItem> list = new List<MediaItem>(MediaHelper.listSongs);
                list.AddRange(MediaHelper.listVideos);
                return list;
            }
        }


        public ObservableCollection<MediaItem> SearchResults { get => searchResults; set { this.RaiseAndSetIfChanged(ref searchResults, value); } } 

        public MediaItem SelectedMedia
        {
            get => selectedMedia;
            set => this.RaiseAndSetIfChanged(ref selectedMedia, value);
        }

        public string? SearchText
        {
            get => searchText;
            set => this.RaiseAndSetIfChanged(ref searchText, value);
        }

        private async void DoSearch(string s)
        {
            SearchResults.Clear();
            SearchResults = new ObservableCollection<MediaItem>(ReturnMediaListsSearchedByText(s));
        }

        private List<MediaItem> ReturnMediaListsSearchedByText(string text)
        {
            var result = new List<MediaItem>();
            if (string.IsNullOrWhiteSpace(text))
            {
                text = "";
            }
            text = text.Trim();
            bool isNotHaveDiacritics = Diacritics.CheckNoDiacriticsInText(text);

                foreach (var media in AllMedias)
                {
                    // If there's no diacritics in searchText -> Proceed to search diacritics insensitively
                    // Else search diacritics sensitively
                    string title =
                        isNotHaveDiacritics == true ?
                        Diacritics.RemoveDiacritics(media.Title) :
                        media.Title;
                    string album = isNotHaveDiacritics == true ?
                        Diacritics.RemoveDiacritics(media.Album) :
                        media.Album;
                    List<string> artists = isNotHaveDiacritics == true ?
                        Diacritics.RemoveDiacriticsForAList(media.Artists) :
                        media.Artists;

                    bool isFoundTitle = title.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) >= 0;
                    bool isFoundAlbum = album.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) >= 0;
                    bool isFoundArtists = CheckArtistExistsInList(artists, text);

                    if (isFoundTitle == true || isFoundArtists == true || isFoundAlbum == true)
                    {
                        result.Add(media);
                    }
                }
            return result;
        }


        private bool CheckArtistExistsInList(List<string> listArtists, string searchText)
        {
            foreach (var artist in listArtists)
            {
                bool isFoundArtists = artist.IndexOf(searchText, StringComparison.InvariantCultureIgnoreCase) >= 0;
                if (isFoundArtists == true) return true;
            }
            return false;
        }

    }
}
