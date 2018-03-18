using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Simple.Xamarin.Framework.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NavigationBar : ContentView
    {
        public NavigationBar()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create("Title", typeof(string), typeof(NavigationBar), default(string));

        /// <summary>
        /// NaviBar title
        /// </summary>
        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static readonly BindableProperty LeftButtonTitleProperty =
            BindableProperty.Create("LeftButtonTitle", typeof(string), typeof(NavigationBar), default(string));

        /// <summary>
        /// Left Button title
        /// </summary>
        public string LeftButtonTitle
        {
            get => (string)GetValue(LeftButtonTitleProperty);
            set => SetValue(LeftButtonTitleProperty, value);
        }

        public static readonly BindableProperty LeftButtonCommandProperty =
            BindableProperty.Create("LeftButtonCommand", typeof(ICommand), typeof(NavigationBar), default(ICommand));

        /// <summary>
        /// Command that will fire after Left button is pressed
        /// </summary>
        public ICommand LeftButtonCommand
        {
            get => (ICommand)GetValue(LeftButtonCommandProperty);
            set => SetValue(LeftButtonCommandProperty, value);
        }

        public static readonly BindableProperty RightButtonTitleProperty =
            BindableProperty.Create("RightButtonTitle", typeof(string), typeof(NavigationBar), default(string));

        /// <summary>
        /// Right Button title
        /// </summary>
        public string RightButtonTitle
        {
            get => (string)GetValue(RightButtonTitleProperty);
            set => SetValue(RightButtonTitleProperty, value);
        }

        public static readonly BindableProperty RightButtonCommandProperty =
            BindableProperty.Create("RightButtonCommand", typeof(ICommand), typeof(NavigationBar), default(ICommand));

        /// <summary>
        /// Command that will fire after Right button is pressed
        /// </summary>
        public ICommand RightButtonCommand
        {
            get => (ICommand)GetValue(RightButtonCommandProperty);
            set => SetValue(RightButtonCommandProperty, value);
        }

    }
}