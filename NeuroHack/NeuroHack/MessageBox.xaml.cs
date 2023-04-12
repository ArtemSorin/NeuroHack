using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }
    }
}