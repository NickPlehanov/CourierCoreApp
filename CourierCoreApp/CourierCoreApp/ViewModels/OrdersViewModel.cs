using Android.Content;
using CourierCoreApp.Helpers;
using CourierCoreApp.Models;
using CourierCoreApp.Properties;
using CourierCoreApp.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

namespace CourierCoreApp.ViewModels {
    public class OrdersViewModel : BaseViewModel {
        public OrdersViewModel(string usr_ID) {
            UsrID = usr_ID;
            IndicatorVisible = false;
            Opacity = 1;
        }
        public OrdersViewModel() {

        }

        private ObservableCollection<TpOrders> _Orders = new ObservableCollection<TpOrders>();
        public ObservableCollection<TpOrders> Orders {
            get => _Orders;
            set {
                _Orders = value;
                OnPropertyChanged(nameof(Orders));
            }
        }
        private ObservableCollection<Orders_OrderItem_MenuItems> _OrderItems = new ObservableCollection<Orders_OrderItem_MenuItems>();
        public ObservableCollection<Orders_OrderItem_MenuItems> OrderItems {
            get => _OrderItems;
            set {
                _OrderItems = value;
                OnPropertyChanged(nameof(OrderItems));
            }
        }
        private ObservableCollection<Guests_GuestDeliveries_Clients> _Guests = new ObservableCollection<Guests_GuestDeliveries_Clients>();
        public ObservableCollection<Guests_GuestDeliveries_Clients> Guests {
            get => _Guests;
            set {
                _Guests = value;
                OnPropertyChanged(nameof(Guests));
            }
        }
        private decimal _sum;
        public decimal Sum {
            get => _sum;
            set {
                _sum = value;
                OnPropertyChanged(nameof(Sum));
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
        private Guests_GuestDeliveries_Clients _SelectedGuest;
        public Guests_GuestDeliveries_Clients SelectedGuest {
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
                if(SelectedGuest != null) {
                    Uri uri = new Uri("tel:" + SelectedGuest.ClntPhones);
                    await Launcher.OpenAsync(uri);
                }
                //if(SelectedGuest != null) {
                //    using HttpClient client_orits = new HttpClient();
                //    HttpResponseMessage response_orits = await client_orits.GetAsync(Resources.BaseAddress + "/api/TpOrders/OrdrInfo?gest_ID=" + SelectedGuest.GestID);
                //    var resp_orits = response_orits.Content.ReadAsStringAsync().Result;
                //    List<Orders_OrderItem_MenuItems> orits = new List<Orders_OrderItem_MenuItems>();
                //    try {
                //        orits = JsonConvert.DeserializeObject<List<Orders_OrderItem_MenuItems>>(resp_orits);
                //    }
                //    catch {
                //        await Application.Current.MainPage.DisplayAlert("Ошибка","Не удалось получить корректные данные, проверьте подключение к интернету, в случае повторной ошибки, обратитесь к администратору","ОК");
                //    }
                //    OrderItems.Clear();
                //    string msg = "";
                //    decimal sum = 0;
                //    foreach(Orders_OrderItem_MenuItems item in orits) {
                //        OrderItems.Add(item);
                //        msg += item.MitmName + " в кол-ве: " + item.OritCount.ToString() + " (" + item.OritVolume.ToString("0.00") + ") по цене: " + item.OritPrice.ToString("0.00") + Environment.NewLine;
                //        sum += item.OritCount * item.OritVolume * item.OritPrice;
                //    }
                //    await Application.Current.MainPage.DisplayAlert("Спецификация заказа",msg + Environment.NewLine + "Общая сумма заказа: " + sum.ToString("0.00"),"ОК");
                //}
            });
        }
        private RelayCommand _BackPressedCommand;
        public RelayCommand BackPressedCommand {
            get => _BackPressedCommand ??= new RelayCommand(async obj => {
                MainMenuViewModel vm = new MainMenuViewModel(UsrID);
                App.Current.MainPage = new MainMenuPage(vm);
            });
        }
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
        private RelayCommand _GuestCloseCommand;
        public RelayCommand GuestCloseCommand {
            get => _GuestCloseCommand ??= new RelayCommand(async obj => {
                if(obj != null) {
                    bool answer =await Application.Current.MainPage.DisplayAlert("Внимание","Данный заказ будет закрыт. Вы действительно хотите продолжить?","Ок","Отмена");
                    if(answer) {
                        using HttpClient client = new HttpClient();
                        HttpResponseMessage response = await client.GetAsync(Resources.BaseAddress + "/api/TpGuestDeliveries/id?id=" + obj);
                        var resp = response.Content.ReadAsStringAsync().Result;
                        TpGuestDeliveries gestdlv=null;
                        try {
                            gestdlv = JsonConvert.DeserializeObject<TpGuestDeliveries>(resp);
                        }
                        catch {
                            await Application.Current.MainPage.DisplayAlert("Ошибка","Не удалось получить корректные данные, проверьте подключение к интернету, в случае повторной ошибки, обратитесь к администратору","ОК");
                        }
                        if(gestdlv != null) {
                            gestdlv.GsdlvDlvrstId = 0;
                            using(HttpClient clientPut = new HttpClient()) {
                                var httpContent = new StringContent(JsonConvert.SerializeObject(gestdlv),Encoding.UTF8,"application/json");
                                //form.Add(new StreamContent(ph.File.GetStream()),String.Format("file"),String.Format(ObjectNumber + "_" + ph._Types.PhotoTypeName + ".jpeg"));
                                HttpResponseMessage responsePut = await clientPut.PutAsync(Resources.BaseAddress + "/api/TpGuestDeliveries",httpContent);
                            }
                            //TODO: Проверять ответ, который пришел от сервера
                        }
                    }
                }
            });
        }
        private RelayCommand _RefreshCommand;
        public RelayCommand RefreshCommand {
            get => _RefreshCommand ??= new RelayCommand(async obj => {
                IndicatorVisible = true;
                Opacity = 0.1;
                using HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(Resources.BaseAddress + "/api/TpGuests/gestByUsrID?courierID=" + UsrID + "&date=" + DateTime.Now);
                var resp = response.Content.ReadAsStringAsync().Result;
                List<Guests_GuestDeliveries_Clients> gests = new List<Guests_GuestDeliveries_Clients>();
                try {
                    gests = JsonConvert.DeserializeObject<List<Guests_GuestDeliveries_Clients>>(resp);
                }
                catch {
                    await Application.Current.MainPage.DisplayAlert("Ошибка","Не удалось получить корректные данные, проверьте подключение к интернету, в случае повторной ошибки, обратитесь к администратору","ОК");
                }
                Guests.Clear();
                //TODO: Преобравать clntPhones из XML
                foreach(Guests_GuestDeliveries_Clients item in gests) {
                    //List<Checks_Payments> _payments = new List<Checks_Payments>();
                    using HttpClient client_payments = new HttpClient();
                    HttpResponseMessage response_payments = await client.GetAsync(Resources.BaseAddress + "/api/TpChecks/chkInfo?gestID=" + item.GestID);
                    var resp_payments = response_payments.Content.ReadAsStringAsync().Result;
                    List<Checks_Payments> payments = new List<Checks_Payments>();
                    try {
                        payments = JsonConvert.DeserializeObject<List<Checks_Payments>>(resp_payments);
                    }
                    catch {
                        //await Application.Current.MainPage.DisplayAlert("Ошибка","Не удалось получить корректные данные, проверьте подключение к интернету, в случае повторной ошибки, обратитесь к администратору","ОК");
                    }

                    using HttpClient client_orits = new HttpClient();
                    HttpResponseMessage response_orits = await client_orits.GetAsync(Resources.BaseAddress + "/api/TpOrders/OrdrInfo?gest_ID=" + item.GestID);
                    var resp_orits = response_orits.Content.ReadAsStringAsync().Result;
                    List<Orders_OrderItem_MenuItems> orits = new List<Orders_OrderItem_MenuItems>();
                    try {
                        orits = JsonConvert.DeserializeObject<List<Orders_OrderItem_MenuItems>>(resp_orits);
                    }
                    catch {
                        /*await Application.Current.MainPage.DisplayAlert("Ошибка","Не удалось получить корректные данные, проверьте подключение к интернету, в случае повторной ошибки, обратитесь к администратору","ОК");*/
                    }
                    OrderItems.Clear();
                    Sum = 0;
                    foreach(Orders_OrderItem_MenuItems _item in orits) {
                        OrderItems.Add(_item);
                        Sum += _item.OritCount * _item.OritPrice;
                    }

                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(item.ClntPhones);
                    if(payments.Count>0)
                    Guests.Add(new Guests_GuestDeliveries_Clients() {
                            GestID = item.GestID,
                            GestDateOpen = (DateTime)item.GestDateOpen,
                            GestDateClose = item.GestDateClose,
                            GestComment = item.GestComment,
                            GestName = item.GestName,
                            ClntID = item.ClntID,
                            ClntName = item.ClntName,
                            //ClntPhones = item.ClntPhones,
                            ClntPhones = !string.IsNullOrEmpty(item.ClntPhones) ? doc.DocumentElement.SelectSingleNode("/Phones/Phone").InnerText: null,
                            PaySum = payments[0].ChpySum,
                            PayType = payments[0].PytpName,
                            PayCheck = true
                        });
                    else
                        Guests.Add(new Guests_GuestDeliveries_Clients() {
                            GestID = item.GestID,
                            GestDateOpen = (DateTime)item.GestDateOpen,
                            GestDateClose = item.GestDateClose,
                            GestComment = item.GestComment,
                            GestName = item.GestName,
                            ClntID = item.ClntID,
                            ClntName = item.ClntName,
                            ClntPhones = item.ClntPhones,
                            PayCheck = false
                        });
                    //_orits.Clear();
                    //ordrs.Clear();
                    //Guests.Add(item);
                }
                IndicatorVisible = false;
                Opacity = 1;
            });
        }
    }
}
