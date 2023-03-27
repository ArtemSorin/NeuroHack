using MusicApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MusicApp.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            musicList = GetMusics();
            recentMusic = musicList.Where(x => x.IsRecent == true).FirstOrDefault();
        }

        ObservableCollection<Music> musicList;
        public ObservableCollection<Music> MusicList
        {
            get { return musicList; }
            set
            {
                musicList = value;
                OnPropertyChanged();
            }
        }

        private Music recentMusic;
        public Music RecentMusic
        {
            get { return recentMusic; }
            set
            {
                recentMusic = value;
                OnPropertyChanged();
            }
        }

        private Music selectedMusic;
        public Music SelectedMusic
        {
            get { return selectedMusic; }
            set
            {
                selectedMusic = value;
                OnPropertyChanged();
            }
        }

        public ICommand SelectionCommand => new Command(PlayMusic);

        private async void PlayMusic()
        {
            if (selectedMusic != null)
            {
                var viewModel = new PlayerViewModel(selectedMusic, musicList);
                var playerPage = new NeuroHack.PlayerPage { BindingContext = viewModel };

                var navigation = Application.Current.MainPage as NavigationPage;
                await navigation.PushAsync(playerPage, true);
            }
        }

        private ObservableCollection<Music> GetMusics()
        {
            return new ObservableCollection<Music>
            {
                new Music { Title = "Music play", Artist = "hh", Url = "https://vgmsite.com/soundtracks/genshin-impact-gamerip/oabwzmjqpm/001.mp3", IsRecent = true },
                new Music { Title = "autor autor", Artist = "hh", Url = "https://www.ocf.berkeley.edu/~acowen/Verified_Normed/aRiJNS8Oz6E_130.mp3" },
                new Music { Title = "m3", Artist = "hh", Url = "https://www.ocf.berkeley.edu/~acowen/Verified_Normed/nHRqTSgKJGg_2193.mp3" },
                new Music { Title = "m4", Artist = "hh", Url = "https://www.ocf.berkeley.edu/~acowen/Verified_Normed/QEjgPh4SEmU_11.mp3" },
                new Music { Title = "m5", Artist = "hh", Url = "https://www.ocf.berkeley.edu/~acowen/Verified_Normed/t4H_Zoh7G5A_202.mp3" },
                new Music { Title = "m6", Artist = "hh", Url = "https://www.ocf.berkeley.edu/~acowen/Verified_Normed/mbgWjujwvwo_10.mp3" }
            };
        }
    }
}

