using CourierCoreApp.Helpers;
using CourierCoreApp.Models;
using CourierCoreApp.Properties;
using CourierCoreApp.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;

namespace CourierCoreApp.ViewModels {
    public class MainPageViewModel : BaseViewModel {
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
		//private Guid _UsrID;
		//public Guid UsrID {
		//	get => _UsrID;
		//	set {
		//		_UsrID = value;
		//		OnPropertyChanged(nameof(UsrID));
		//	}
		//}

		private RelayCommand _AuthCommand;
        public RelayCommand AuthCommand {
			get => _AuthCommand ??= new RelayCommand(async obj => {
				using HttpClient client = new HttpClient();
				//string baseAddress = "https://d9632ebceae8.ngrok.io";
				//string Phone = null;
				//if(PhoneNumber.Length == 11) {
				//	Phone = PhoneNumber.Substring(1,PhoneNumber.Length - 1);
				//}
				HttpResponseMessage response = await client.GetAsync(Resources.BaseAddress + "/api/TpUsers/login?login=" + Login);
				var resp = response.Content.ReadAsStringAsync().Result;
				Message = resp;
				if(resp != null) {
					//TODO: проверить что можем парсить ответ
					List<TpUsers> users = JsonConvert.DeserializeObject<List<TpUsers>>(resp);
					if(users.Any(x => x.UsrIsDisabled == false && x.UsrDelId == null)) {
						if(users.Count(x => x.UsrIsDisabled == false && x.UsrDelId == null) == 1) {
							App.Current.MainPage = new NavigationPage(new MainMenuPage(users.First(x => x.UsrIsDisabled == false && x.UsrDelId == null).UsrId.ToString()));
							//await App.Current.MainPage.Navigation.PushModalAsync(new MainMenuPage(users.First(x => x.UsrIsDisabled == false && x.UsrDelId == null).UsrId.ToString()));
							
						}
						else
							Message = "Неоднозначный логин пользователя. Вход запрещен";
					}
					else
						Message = "Пользователь с указанным логином отключен или удален";
                }
			});
		}
    }
}
