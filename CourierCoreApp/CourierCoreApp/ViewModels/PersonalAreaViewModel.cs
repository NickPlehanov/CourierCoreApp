using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace CourierCoreApp.ViewModels {
    class PersonalAreaViewModel : BaseViewModel {
        private string _ShiftInfo;
        public string ShiftInfo {
            get => _ShiftInfo;
            set {
                _ShiftInfo = value;
                OnPropertyChanged(nameof(ShiftInfo));
            }
        }

        //private async string GetShiftInfo(Guid usrID) {
        //    using HttpClient client = new HttpClient();
        //    string baseAddress = "https://d9632ebceae8.ngrok.io";
        //    HttpResponseMessage response = await client.GetAsync(baseAddress + "/api/TpUsers/login?login=" + Login);
        //    var resp = response.Content.ReadAsStringAsync().Result;
        //}
    }
}
