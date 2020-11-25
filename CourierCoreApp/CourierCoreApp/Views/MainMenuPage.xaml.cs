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
        public MainMenuViewModel ViewModel { get; private set; }
        public MainMenuPage() {
            InitializeComponent();
            //this.BindingContext = new MainMenuViewModel(_usrID);
        }
        public MainMenuPage(MainMenuViewModel vm) {
            InitializeComponent();
            ViewModel = vm;
            this.BindingContext = ViewModel;
        }
        protected override bool OnBackButtonPressed() {
            //var vm = (ViewModel)BindingContext;
            if(ViewModel.ExitCommand.CanExecute(null))  // You can add parameters if any
              {
                ViewModel.ExitCommand.Execute(null); // You can add parameters if any
                return true;
            }
            else
                return false;
        }
    }
}