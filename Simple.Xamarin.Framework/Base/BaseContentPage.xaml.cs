using Simple.Xamarin.Framework.core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Simple.Xamarin.Framework
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BaseContentPage : ContentPage
    {
        public BaseContentPage()
        {
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            if (BindingContext is BasePageViewModel vm)
            {
                return vm.OnBackButtonPressed();
            }
            // else pop back
            return base.OnBackButtonPressed();
        }
    }
}