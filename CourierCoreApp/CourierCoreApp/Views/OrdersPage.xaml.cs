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
    public partial class OrdersPage : ContentPage {
        public OrdersViewModel VM { get; private set; }
        public OrdersPage() {
            InitializeComponent();
        }
        public OrdersPage(OrdersViewModel vm) {
            InitializeComponent();
            VM = vm;
            this.BindingContext = VM;
        }
    }
}