using CourierCoreApp.Helpers;
using CourierCoreApp.Models;
using CourierCoreApp.Properties;
using CourierCoreApp.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;

namespace CourierCoreApp.ViewModels {
    public class PersonalAreaViewModel : BaseViewModel {
        public PersonalAreaViewModel(string usr_ID) {
            UsrID = usr_ID;
            IndicatorVisible = false;
            Opacity = 1;
        }
        private string _UsrID;
        public string UsrID {
            get => _UsrID;
            set {
                _UsrID = value;
                OnPropertyChanged(nameof(UsrID));
            }
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
        private RelayCommand _BackPressedCommand;
        public RelayCommand BackPressedCommand {
            get => _BackPressedCommand ??= new RelayCommand(async obj => {
                MainMenuViewModel vm = new MainMenuViewModel(UsrID);
                App.Current.MainPage = new MainMenuPage(vm);
            });
        }
        private string _ShiftInfo;
        public string ShiftInfo {
            get => _ShiftInfo;
            set {
                _ShiftInfo = value;
                OnPropertyChanged(nameof(ShiftInfo));
            }
        }
        private DateTime _Start;
        public DateTime Start {
            get => _Start;
            set {
                if(value == DateTime.Parse("01.01.2010 00:00:00"))
                    _Start = DateTime.Parse(DateTime.Now.ToString("dd-MM-yyyy"));
                else
                    _Start = value;
                OnPropertyChanged(nameof(Start));
            }
        }
        private DateTime _End;
        public DateTime End {
            get => _End;
            set {
                if(value == DateTime.Parse("01.01.2010 00:00:00"))
                    _End = DateTime.Parse(DateTime.Now.ToString("dd-MM-yyyy"));
                else
                    _End = value;
                OnPropertyChanged(nameof(End));
            }
        }
        private ObservableCollection<TpUserLocationPresence> _ShiftsInfo = new ObservableCollection<TpUserLocationPresence>();
        public ObservableCollection<TpUserLocationPresence> ShiftsInfo {
            get => _ShiftsInfo;
            set {
                _ShiftsInfo = value;
                OnPropertyChanged(nameof(ShiftsInfo));
            }
        }
        private RelayCommand _OpenShiftCommand;
        public RelayCommand OpenShiftCommand {
            get => _OpenShiftCommand ??= new RelayCommand(async obj => {
                IndicatorVisible = true;
                Opacity = 0.1;
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
                if(openShifts.Count == 0) {
                    //TODO: вынести в настройки для определения местоположения, или предоставить пользователю выбор
                    response = await client.GetAsync(Resources.BaseAddress + "/api/TpLocations/description?description=ул. Сахьяновой 21/1");
                    resp = response.Content.ReadAsStringAsync().Result;
                    List<TpLocations> loc = new List<TpLocations>();
                    try {
                        loc = JsonConvert.DeserializeObject<List<TpLocations>>(resp);
                    }
                    catch {
                        await Application.Current.MainPage.DisplayAlert("Ошибка","Не удалось получить корректные данные, проверьте подключение к интернету, в случае повторной ошибки, обратитесь к администратору","ОК");
                    }
                    if(loc != null) {
                        TpUserLocationPresence currentShift = new TpUserLocationPresence() {
                            UslpId = Guid.NewGuid(),
                            UslpDateBegin = DateTime.Now,
                            UslpDateEnd = null,
                            UslpUsrId = Guid.Parse(UsrID),
                            UslpLocId = loc.FirstOrDefault().LocId,
                            UslpUsrIdBegin = null,
                            UslpUsrIdEnd = null
                        };
                        HttpContent content = new StringContent(JsonConvert.SerializeObject(currentShift).ToString(),Encoding.UTF8,"application/json");
                        response = await client.PostAsync(Resources.BaseAddress + "/api/TpUserLocationPresences",content);
                        resp = response.Content.ReadAsStringAsync().Result;
                        openShifts.Clear();
                        try {
                            openShifts.Add(JsonConvert.DeserializeObject<TpUserLocationPresence>(resp));
                        }
                        catch(Exception ex) {
                            await Application.Current.MainPage.DisplayAlert("Ошибка",ex.Message,"ОК"); 
                        }
                        if(openShifts.Count > 0) {
                            await Application.Current.MainPage.DisplayAlert("Информация","Смена окрыта","ОК");
                            ShiftInfo = "Смена открыта: " + DateTime.Now.ToString();
                        }
                        else
                            await Application.Current.MainPage.DisplayAlert("Ошибка","Не удалось открыть смену","ОК");
                    }
                }
                else
                    //TODO: показать информацию об открытой смене и предложить закрыть старую и открыть новую?!
                    await Application.Current.MainPage.DisplayAlert("Ошибка","В системе уже есть открытая смена","ОК");
                IndicatorVisible = false;
                Opacity = 1;
            });
        }
        private RelayCommand _CloseShiftCommand;
        public RelayCommand CloseShiftCommand {
            get => _CloseShiftCommand ??= new RelayCommand(async obj => {
                IndicatorVisible = true;
                Opacity = 0.1;
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
                //TODO: проверять, что есть несколько открытых смен
                if(openShifts.Count != 0) {
                    TpUserLocationPresence currentShift = new TpUserLocationPresence() {
                        UslpId = openShifts.FirstOrDefault().UslpId,
                        UslpDateBegin = openShifts.FirstOrDefault().UslpDateBegin,
                        UslpDateEnd = DateTime.Now,
                        UslpUsrId = Guid.Parse(UsrID),
                        UslpLocId = openShifts.FirstOrDefault().UslpLocId,
                        UslpUsrIdBegin = openShifts.FirstOrDefault().UslpUsrIdBegin,
                        UslpUsrIdEnd = openShifts.FirstOrDefault().UslpUsrIdEnd
                    };
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(currentShift).ToString(),Encoding.UTF8,"application/json");
                    response = await client.PutAsync(Resources.BaseAddress + "/api/TpUserLocationPresences",content);
                    List<TpUserLocationPresence> tps = new List<TpUserLocationPresence>();
                    resp = response.Content.ReadAsStringAsync().Result;
                    try {
                        tps = JsonConvert.DeserializeObject<List<TpUserLocationPresence>>(resp);
                    }
                    catch {
                        await Application.Current.MainPage.DisplayAlert("Ошибка","Не удалось получить корректные данные, проверьте подключение к интернету, в случае повторной ошибки, обратитесь к администратору","ОК");
                    }
                    if(tps.Count > 0) {
                        await Application.Current.MainPage.DisplayAlert("Информация","Смена закрыта","ОК");
                        ShiftInfo = "Смена закрыта: " + DateTime.Now.ToString();
                    }
                    else
                        await Application.Current.MainPage.DisplayAlert("Ошибка","Не удалось закрыть смену","ОК");
                }
                IndicatorVisible = false;
                Opacity = 1;
            });
        }
        private RelayCommand _GetLastShiftInfo;
        public RelayCommand GetShiftInfoCommand {
            get => _GetLastShiftInfo ??= new RelayCommand(async obj => {
                Opacity = 0.1;
                IndicatorVisible = true;
                ShiftsInfo.Clear();
                ShiftInfo = null;
                using HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(Resources.BaseAddress + "/api/TpUserLocationPresences?usr_ID="+UsrID+"&start="+Start+"&end="+End+"");
                var resp = response.Content.ReadAsStringAsync().Result;
                List<TpUserLocationPresence> Shifts = new List<TpUserLocationPresence>();
                try {
                    Shifts = JsonConvert.DeserializeObject<List<TpUserLocationPresence>>(resp);
                }
                catch {
                    await Application.Current.MainPage.DisplayAlert("Ошибка","Не удалось получить корректные данные, проверьте подключение к интернету, в случае повторной ошибки, обратитесь к администратору","ОК");
                }
                if(Shifts.Count > 0) {
                    foreach(TpUserLocationPresence item in Shifts) {
                        ShiftsInfo.Add(new TpUserLocationPresence() {
                            UslpDateBegin = item.UslpDateBegin,
                            UslpDateEnd = item.UslpDateEnd,
                            CountHours = item.CountHours
                        });
                    }
                }
                else
                    ShiftInfo = "За выбранный период данных не найдено";
                Opacity = 1;
                IndicatorVisible = false;
            });
        }
    }
}
