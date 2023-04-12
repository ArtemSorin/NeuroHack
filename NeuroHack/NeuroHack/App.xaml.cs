using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NeuroHack
{
    public partial class App : Application
    {
        public App ()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Njg1OTAzQDMyMzAyZTMyMmUzMGtEVStWSGpjMWVBY1FIOEVudjFLVVkyb3VBL3BzSC9mSG55ZER4Wm80ZXc9");

            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart ()
        {
        }

        protected override void OnSleep ()
        {
        }

        protected override void OnResume ()
        {
        }
    }
}

