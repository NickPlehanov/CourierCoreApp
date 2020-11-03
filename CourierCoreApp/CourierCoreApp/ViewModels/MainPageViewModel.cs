using CourierCoreApp.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace CourierCoreApp.ViewModels {
    class MainPageViewModel : BaseViewModel {
        private string _Message;
        public string Message {
            get => _Message;
            set {
                _Message = value;
                OnPropertyChanged(nameof(Message));
            }
        }

		private string _Login;
		public string Login {
			get => _Login;
            set {
				_Login = value;
				OnPropertyChanged(nameof(Login));
            }
        }

        private RelayCommand _AuthCommand;
        public RelayCommand AuthCommand {
			get => _AuthCommand ??= new RelayCommand(async obj => {
				using HttpClient client = new HttpClient();
				string baseAddress = "https://6a9bbf99574b.ngrok.io";
				//string Phone = null;
				//if(PhoneNumber.Length == 11) {
				//	Phone = PhoneNumber.Substring(1,PhoneNumber.Length - 1);
				//}
				HttpResponseMessage response = await client.GetAsync(baseAddress + "/api/TpUsers/login?login=" + Login);
				var resp = response.Content.ReadAsStringAsync().Result;
				Message = resp;
				//List<NewMounterExtensionBase> mounters = JsonConvert.DeserializeObject<List<NewMounterExtensionBase>>(resp);

				//response = await client.GetAsync(baseAddress + "/api/NewServicemanExtensionBases/" + Phone);
				//resp = response.Content.ReadAsStringAsync().Result;
				//List<NewServicemanExtensionBase> servicemans = JsonConvert.DeserializeObject<List<NewServicemanExtensionBase>>(resp);
				//if(mounters != null || servicemans != null) {
				//	if(mounters.Count > 0 || servicemans.Count > 0) {
				//		//await PushAsync(new NavigationPage(new MainMenuPage()));
				//		var _MainMenuPage = new MainMenuPage();
				//		if(mounters.Count > 0)
				//			_MainMenuPage.BindingContext = mounters;
				//		if(servicemans.Count > 0)
				//			_MainMenuPage.BindingContext = servicemans;
				//		App.Current.MainPage = new NavigationPage(new MainMenuPage());
				//	}
				//}
			});
		}
    }
}
