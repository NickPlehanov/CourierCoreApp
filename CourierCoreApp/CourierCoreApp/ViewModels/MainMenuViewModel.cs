using CourierCoreApp.Helpers;
using CourierCoreApp.Views;
using System;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace CourierCoreApp.ViewModels {
    public class MainMenuViewModel : BaseViewModel {
        public MainMenuViewModel(string _usrID) {
            UsrID = _usrID;
        }
        public MainMenuViewModel() {
            //UsrID = UsrID;
        }
        private RelayCommand _PersonalAreaEnterCommand;
        public RelayCommand PersonalAreaEnterCommand {
            get => _PersonalAreaEnterCommand ??= new RelayCommand(async obj => {
                //App.Current.MainPage = new NavigationPage(new PersonalPage(UsrID));
                PersonalAreaViewModel vm = new PersonalAreaViewModel(UsrID);
                App.Current.MainPage = new PersonalPage(vm);
            });
        }
        private RelayCommand _OrdersEnterCommand;
        public RelayCommand OrdersEnterCommand {
            get => _OrdersEnterCommand ??= new RelayCommand(async obj => {
                OrdersViewModel vm = new OrdersViewModel(UsrID);
                App.Current.MainPage = new NavigationPage(new OrdersPage(vm));
            });
        }
        private RelayCommand _ExitCommand;
        public RelayCommand ExitCommand {
            get => _ExitCommand ??= new RelayCommand(async obj => {
                App.Current.MainPage = new NavigationPage(new MainPage());
            });
        }
        private string _UsrID;
        public string UsrID {
            get => _UsrID;
            set {
                _UsrID = value;
                OnPropertyChanged(nameof(UsrID));
            }
        }
    }
}
