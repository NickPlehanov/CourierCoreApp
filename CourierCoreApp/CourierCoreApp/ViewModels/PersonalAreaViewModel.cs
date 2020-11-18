using CourierCoreApp.Helpers;
using CourierCoreApp.Models;
using CourierCoreApp.Properties;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;

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
                        if(openShifts.Count > 0)
                            await Application.Current.MainPage.DisplayAlert("Информация","Смена окрыта","ОК");
                        else
                            await Application.Current.MainPage.DisplayAlert("Ошибка","Не удалось открыть смену","ОК");
                    }
                }
                else
                    //TODO: показать информацию об открытой смене и предложить закрыть старую и открыть новую?!
                    await Application.Current.MainPage.DisplayAlert("Ошибка","В системе уже есть открытая смена","ОК");
            });
        }
        private RelayCommand _CloseShiftCommand;
        public RelayCommand CloseShiftCommand {
            get => _CloseShiftCommand ??= new RelayCommand(async obj => {
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
                    if (tps.Count>0)
                        await Application.Current.MainPage.DisplayAlert("Информация","Смена закрыта","ОК");
                    else
                        await Application.Current.MainPage.DisplayAlert("Ошибка","Не удалось закрыть смену","ОК");
                }
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
