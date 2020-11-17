using CourierCoreApp.Helpers;
using CourierCoreApp.Properties;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace CourierCoreApp.ViewModels {
    public class PersonalAreaViewModel : BaseViewModel {
        public PersonalAreaViewModel(string usr_ID) {
            UsrID = usr_ID;
        }
        private string _UsrID;
        public string UsrID {
            get => _UsrID;
            set {
                _UsrID = value;
                OnPropertyChanged(nameof(UsrID));
            }
        }
        private string _ShiftInfo;
        public string ShiftInfo {
            get => _ShiftInfo;
            set {
                _ShiftInfo = value;
                OnPropertyChanged(nameof(ShiftInfo));
            }
        }
        private RelayCommand _OpenShiftCommand;
        public RelayCommand OpenShiftCommand {
            get => _OpenShiftCommand ??= new RelayCommand(async obj => {
                using HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(Resources.BaseAddress + "/api/TpUserLocationPresences/usr_ID?usr_ID=" + UsrID);
                var resp = response.Content.ReadAsStringAsync().Result;
            });
        }
        private RelayCommand _CloseShiftCommand;
        public RelayCommand CloseShiftCommand {
            get => _CloseShiftCommand ??= new RelayCommand(async obj => {

            });
        }
        //private async string GetShiftInfo(Guid usrID) {
        //    using HttpClient client = new HttpClient();
        //    string baseAddress = "https://d9632ebceae8.ngrok.io";
        //    HttpResponseMessage response = await client.GetAsync(baseAddress + "/api/TpUsers/login?login=" + Login);
        //    var resp = response.Content.ReadAsStringAsync().Result;
        //}
    }
}
