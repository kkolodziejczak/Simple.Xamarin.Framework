using Simple.Xamarin.Framework.core;

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

        public static readonly BindableProperty BottomToolBarContentProperty = BindableProperty.Create(
            "BottomToolBarContent", typeof(View), typeof(BaseContentPage), default(View));

        /// <summary>
        /// Content to display at bottom ToolBar
        /// </summary>
        public View BottomToolBarContent
        {
            get => (View)GetValue(BottomToolBarContentProperty);
            set => SetValue(BottomToolBarContentProperty, value);
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

        public static readonly BindableProperty UpperToolBarContentProperty = BindableProperty.Create(
            "UpperToolBarContent", typeof(View), typeof(BaseContentPage), default(View));

        /// <summary>
        /// Content to display at upper ToolBar
        /// </summary>
        public View UpperToolBarContent
        {
            get => (View)GetValue(UpperToolBarContentProperty);
            set => SetValue(UpperToolBarContentProperty, value);
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