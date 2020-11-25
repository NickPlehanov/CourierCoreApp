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
        public MainPageViewModel() {
			Opacity = 1;
			IndicatorVisible = false;
		}
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
		private bool _IndicatorVisible;
		public bool IndicatorVisible {
			get => _IndicatorVisible;
            set {
				_IndicatorVisible = value;
				OnPropertyChanged(nameof(IndicatorVisible));
            }
        }
		private double _Opacity;
		public double Opacity {
			get => _Opacity;
            set {
				_Opacity = value;
				OnPropertyChanged(nameof(Opacity));
            }
        }

		private RelayCommand _AuthCommand;
        public RelayCommand AuthCommand {
			get => _AuthCommand ??= new RelayCommand(async obj => {
				Opacity = 0.1;
				IndicatorVisible = true;
				using HttpClient client = new HttpClient();				
				HttpResponseMessage response = await client.GetAsync(Resources.BaseAddress + "/api/TpUsers/login?login=" + Login);
				var resp = response.Content.ReadAsStringAsync().Result;
				Message = resp;
				if(resp != null) {
					//TODO: проверить что можем парсить ответ
					List<TpUsers> users = JsonConvert.DeserializeObject<List<TpUsers>>(resp);
					if(users.Any(x => x.UsrIsDisabled == false && x.UsrDelId == null)) {
						if(users.Count(x => x.UsrIsDisabled == false && x.UsrDelId == null) == 1) {
							//App.Current.MainPage = new NavigationPage(new MainMenuPage(users.First(x => x.UsrIsDisabled == false && x.UsrDelId == null).UsrId.ToString()));
							//await App.Current.MainPage.Navigation.PushModalAsync(new MainMenuPage(users.First(x => x.UsrIsDisabled == false && x.UsrDelId == null).UsrId.ToString()));
							MainMenuViewModel vm = new MainMenuViewModel(users.First(x => x.UsrIsDisabled == false && x.UsrDelId == null).UsrId.ToString());
							App.Current.MainPage = new MainMenuPage(vm);
							Opacity = 1;
							IndicatorVisible = false;
						}
						else
							Message = "Неоднозначный логин пользователя. Вход запрещен";
					}
					else
						Message = "Пользователь с указанным логином отключен или удален";
                }
				Opacity = 1;
				IndicatorVisible = false;
			});
		}
    }
}
