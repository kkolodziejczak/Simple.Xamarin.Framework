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
            BindingContext = this;
        }

        public static readonly BindableProperty NavigationBarBackgroundColorProperty =
            BindableProperty.Create("NavigationBarBackgroundColor", typeof(Color), typeof(BaseContentPage), default(Color));

        /// <summary>
        /// NavigationBar Background color
        /// </summary>
        public Color NavigationBarBackgroundColor
        {
            get => (Color)GetValue(NavigationBarBackgroundColorProperty);
            set => SetValue(NavigationBarBackgroundColorProperty, value);
        }

        public static readonly BindableProperty BottomToolBarBackgroundColorProperty =
            BindableProperty.Create("BottomToolBarBackgroundColor", typeof(Color), typeof(BaseContentPage), default(Color));

        /// <summary>
        /// Bottom ToolBar Background color
        /// </summary>
        public Color BottomToolBarBackgroundColor
        {
            get => (Color)GetValue(BottomToolBarBackgroundColorProperty);
            set => SetValue(BottomToolBarBackgroundColorProperty, value);
        }

        public static readonly BindableProperty UpperToolBarBackgroundColorProperty =
            BindableProperty.Create("UpperToolBarBackgroundColor", typeof(Color), typeof(BaseContentPage), default(Color));

        /// <summary>
        /// Upper ToolBar Background color
        /// </summary>
        public Color UpperToolBarBackgroundColor
        {
            get => (Color)GetValue(UpperToolBarBackgroundColorProperty);
            set => SetValue(UpperToolBarBackgroundColorProperty, value);
        }

        public static readonly BindableProperty UpperToolBarItemTemplateProperty = BindableProperty.Create(
            "UpperToolBarItemTemplate", typeof(DataTemplate), typeof(BaseContentPage), default(DataTemplate));

        public DataTemplate UpperToolBarItemTemplate
        {
            get => (DataTemplate)GetValue(UpperToolBarItemTemplateProperty);
            set => SetValue(UpperToolBarItemTemplateProperty, value);
        }

        public static readonly BindableProperty BottomToolBarItemTemplateProperty = BindableProperty.Create(
            "BottomToolBarItemTemplate", typeof(DataTemplate), typeof(BaseContentPage), default(DataTemplate));

        public DataTemplate BottomToolBarItemTemplate
        {
            get { return (DataTemplate)GetValue(BottomToolBarItemTemplateProperty); }
            set { SetValue(BottomToolBarItemTemplateProperty, value); }
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