using CourierCoreApp.Helpers;
using CourierCoreApp.Models;
using CourierCoreApp.Properties;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;

namespace CourierCoreApp.ViewModels {
    public class OrdersViewModel :BaseViewModel {
        public OrdersViewModel(string usr_ID) {
            UsrID = usr_ID;
        }
        
        private ObservableCollection<TpOrders> _Orders = new ObservableCollection<TpOrders>();
        public ObservableCollection<TpOrders> Orders {
            get => _Orders;
            set {
                _Orders = value;
                OnPropertyChanged(nameof(Orders));
            }
        }
        private ObservableCollection<TpOrderItems> _OrderItems = new ObservableCollection<TpOrderItems>();
        public ObservableCollection<TpOrderItems> OrderItems {
            get => _OrderItems;
            set {
                _OrderItems = value;
                OnPropertyChanged(nameof(OrderItems));
            }
        }
        private ObservableCollection<TpGuests> _Guests = new ObservableCollection<TpGuests>();
        public ObservableCollection<TpGuests> Guests {
            get => _Guests;
            set {
                _Guests = value;
                OnPropertyChanged(nameof(Guests));
            }
        }

        private TpOrders _SelectedOrder;
        public TpOrders SelectedOrder {
            get => _SelectedOrder;
            set {
                _SelectedOrder = value;
                OnPropertyChanged(nameof(SelectedOrder));
            }
        }        
        private TpGuests _SelectedGuest;
        public TpGuests SelectedGuest {
            get => _SelectedGuest;
            set {
                _SelectedGuest = value;
                OnPropertyChanged(nameof(SelectedGuest));
            }
        }

        private string _UsrID;
        public string UsrID {
            get => _UsrID;
            set {
                _UsrID = value;
                OnPropertyChanged(nameof(UsrID));
            }
        }

        private RelayCommand _GuestInfoCommand;
        public RelayCommand GuestInfoCommand {
            get => _GuestInfoCommand ??= new RelayCommand(async obj => {
                using HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(Resources.BaseAddress + "/api/TpUserLocationPresences/usr_ID?usr_ID=" + UsrID);
                var resp = response.Content.ReadAsStringAsync().Result;
                List<TpUserLocationPresence> openShifts = new List<TpUserLocationPresence>();
                try {
                    openShifts = JsonConvert.DeserializeObject<List<TpUserLocationPresence>>(resp);
                }
                catch {
                    await Application.Current.MainPage.DisplayAlert("Ошибка","Не удалось получить корректные данные, проверьте подключение к интернету, в случае повторной ошибки, обратитесь к администратору","ОК");
                }
            });
        }
    }
}
