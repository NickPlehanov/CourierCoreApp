using CourierCoreApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CourierCoreApp.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PersonalPage : ContentPage {
        public PersonalAreaViewModel ViewModel { get; private set; }
        public PersonalPage() {
            InitializeComponent();
        }
        public PersonalPage(PersonalAreaViewModel vm) {
            InitializeComponent();
            ViewModel = vm;
            this.BindingContext = vm;
        }
        protected override bool OnBackButtonPressed() {
            //var vm = (ViewModel)BindingContext;
            if(ViewModel.BackPressedCommand.CanExecute(null))  // You can add parameters if any
              {
                ViewModel.BackPressedCommand.Execute(null); // You can add parameters if any
                return true;
            }
            else
                return false;
        }
    }
}