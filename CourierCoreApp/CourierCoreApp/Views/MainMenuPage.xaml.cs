using CourierCoreApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CourierCoreApp.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMenuPage : ContentPage {
        public MainMenuPage(string _usrID) {
            InitializeComponent();
            this.BindingContext = new MainMenuViewModel(_usrID);
        }
    }
}