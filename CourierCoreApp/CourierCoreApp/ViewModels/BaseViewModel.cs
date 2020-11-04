using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CourierCoreApp.ViewModels {
    public class BaseViewModel : INotifyPropertyChanged {

		public event PropertyChangedEventHandler PropertyChanged;

		protected void NotifyPropertyChanged(string propertyName) {
			if(this.PropertyChanged != null) {
				this.PropertyChanged(this,new PropertyChangedEventArgs(propertyName));
			}
		}
		public void OnPropertyChanged([CallerMemberName] string prop = "") {
			PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(prop));
		}
	}
}
