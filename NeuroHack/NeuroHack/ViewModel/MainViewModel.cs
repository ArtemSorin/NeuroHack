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

        public ICommand MusicSettings => new Command(MusicParams);

        private void MusicParams()
        {
            var popup = new MessageBox();

            App.Current.MainPage.Navigation.ShowPopup(popup);
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

        private ObservableCollection<Music> GetMusics()
        {
            ObservableCollection<Music> result = new ObservableCollection<Music>();

            result.Add(new Music { Title = "m1", Artist = "hh", Url = "https://vgmsite.com/soundtracks/genshin-impact-gamerip/oabwzmjqpm/001.mp3", IsRecent = true });
            
            var connection = new MySqlConnection("Server='31.31.196.209';Database='u1962034_project.neurohacking';User Id='u1962034_project';Password='bitoWL84';");
            connection.Open();

            Random r = new Random();
            int rInt = r.Next(0, 100);

            var command = connection.CreateCommand();
            command.CommandText = "SELECT * " +
                $"FROM music WHERE Beatyful = {rInt} OR Amusing = {rInt} OR Dreamy = {rInt} OR Annoying = {rInt}";

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

