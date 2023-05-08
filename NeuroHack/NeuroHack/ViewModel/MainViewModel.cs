using MusicApp.Model;
using MySqlConnector;
using NeuroHack;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace MusicApp.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            double[] mas = new double[10];
            musicList = GetMusics(mas);
            recentMusic = musicList.Where(x => x.IsRecent == true).FirstOrDefault();
        }
        public MainViewModel(double[] mas)
        {
            musicList = GetMusics(mas);
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

        public ICommand MusicSettings => new Command(MusicParams);

        private async void MusicParams()
        {
            var popupPage = new MessageBox() {  };

            double[] a = (double[])await App.Current.MainPage.Navigation.ShowPopupAsync(new MessageBox());

            var viewModel = new MainViewModel(a);
            var mainPage = new MainPage { BindingContext = viewModel };

            var navigation = Application.Current.MainPage as NavigationPage;
            await navigation.PushAsync(mainPage, true);
        }

        private async void PlayMusic()
        {
            if (selectedMusic != null)
            {
                var viewModel = new PlayerViewModel(selectedMusic, musicList);
                var playerPage = new PlayerPage { BindingContext = viewModel };

                var navigation = Application.Current.MainPage as NavigationPage;
                await navigation.PushAsync(playerPage, true);
            }
        }

        private ObservableCollection<Music> GetMusics(double[] mas)
        {
            ObservableCollection<Music> result = new ObservableCollection<Music>();

            result.Add(new Music { Title = "Dragonspine Battle Theme", Artist = "Genshin Impact OST", Url = "https://storage1.lightaudio.ru/3992bd9c/35ec4dd1/Dragonspine%20Battle%20Theme%20%E2%80%94%20Genshin%20Impact%20OST.m3u8", IsRecent = true });

            var connection = new MySqlConnection("Server='31.31.196.209';Database='u1962034_project.neurohacking';User Id='u1962034_project';Password='bitoWL84';");
            connection.Open();

            Random r = new Random();
            int rInt = r.Next(0, 100);

            var command = connection.CreateCommand();
            command.CommandText = "SELECT * " +
                $"FROM music WHERE Beatyful = {mas[0]}";

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                result.Add(new Music
                {
                    Title = reader.GetString("Song"),
                    Artist = reader.GetString("Band"),
                    Url = reader.GetString("Link"),  
                });
            }
            
            return result;
        }
    }
}

