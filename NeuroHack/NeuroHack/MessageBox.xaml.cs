using MusicApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NeuroHack
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MessageBox : Popup
    {
        public MessageBox()
        {
            InitializeComponent();
            double[] mas = new double[10];
            FilterButton.Clicked += (sender, e) => BuildingPlayList(out mas);
        }
        private void BuildingPlayList(out double[] mas)
        {
            mas = new double[10];

            mas[0] = SliderAmusing.LowerValue;
            mas[1] = SliderAmusing.UpperValue;

            mas[2] = SliderBeatyful.LowerValue;
            mas[3] = SliderBeatyful.UpperValue;

            mas[4] = SliderDreamy.LowerValue;
            mas[5] = SliderDreamy.UpperValue;

            mas[6] = SliderAnnoying.LowerValue;
            mas[7] = SliderAnnoying.UpperValue;

            var viewModel = new MainViewModel();
            var playerPage = new MainPage { BindingContext = viewModel };

            Navigation.PushAsync(playerPage);

            Dismiss("");
        }
    }
}