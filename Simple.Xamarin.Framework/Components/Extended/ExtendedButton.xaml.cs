using System.Linq;

using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Simple.Xamarin.Framework
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExtendedButton : Frame
    {

        #region Common Properties

        public new static readonly BindableProperty MarginProperty =
           BindableProperty.Create(nameof(Margin), typeof(NamedThickness), typeof(ExtendedLabel), default(NamedThickness), propertyChanged: (b, o, n) =>
           {
               if (b is Frame layout)
               {
                   if (n is NamedThickness thickness)
                   {
                       layout.Margin = new Thickness(thickness.Left, thickness.Top, thickness.Right, thickness.Bottom);
                   }
               }
           });

        public new NamedThickness Margin
        {
            get => (NamedThickness)GetValue(MarginProperty);
            set => SetValue(MarginProperty, value);

        }

        public new static readonly BindableProperty PaddingProperty =
           BindableProperty.Create(nameof(Padding), typeof(NamedThickness), typeof(ExtendedLabel), new NamedThickness(ExetendedNamedSize.Normal, ExetendedNamedSize.Small), propertyChanged: (b, o, n) =>
           {
               if (b is Frame layout)
               {
                   if (n is NamedThickness thickness)
                   {
                       layout.Padding = new Thickness(thickness.Left, thickness.Top, thickness.Right, thickness.Bottom);
                   }
               }
           });

        public new NamedThickness Padding
        {
            get => (NamedThickness)GetValue(MarginProperty);
            set => SetValue(MarginProperty, value);
        }

        public static readonly BindableProperty TapCommandProperty =
           BindableProperty.Create(nameof(TapCommand), typeof(ICommand), typeof(ExtendedLabel), default(ICommand), propertyChanged: (b, o, n) =>
           {
               if (b is Frame layout)
               {
                   layout.GestureRecognizers.Clear();
                   layout.GestureRecognizers.Add(new TapGestureRecognizer
                   {
                       Command = n as ICommand,
                   });
               }
           });

        /// <summary>
        /// Command that will be will execute when user Tap on this label
        /// </summary>
        public ICommand TapCommand
        {
            get => (ICommand)GetValue(TapCommandProperty);
            set
            {
                SetValue(TapCommandProperty, value);
                if(value is ICommand)
                {
                    value.CanExecuteChanged += Command_CanExecuteChanged;
                }
            }
        }

        public static readonly BindableProperty TapCommandParameterProperty =
           BindableProperty.Create(nameof(TapCommandParameter), typeof(object), typeof(ExtendedLabel), default(object), propertyChanged: (b, o, n) =>
           {
               if (b is Frame layout)
               {
                   if (layout.GestureRecognizers.FirstOrDefault() is TapGestureRecognizer gesture)
                       gesture.CommandParameter = n;
               }
           });

        /// <summary>
        /// Command parameter that will be passed to Command executing
        /// </summary>
        public object TapCommandParameter
        {
            get => GetValue(TapCommandParameterProperty);
            set => SetValue(TapCommandParameterProperty, value);
        }

        #endregion

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create("Text", typeof(string), typeof(ProgressBar), default(string));

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public Color LocalBackgroundColor { get; protected set; }

        public ExtendedButton()
        {
            InitializeComponent();
            BindingContext = this;
            LocalBackgroundColor = BackgroundColor;
            this.HorizontalOptions = LayoutOptions.StartAndExpand;
            this.VerticalOptions = LayoutOptions.StartAndExpand;
        }

        ~ExtendedButton()
        {
            TapCommand.CanExecuteChanged -= Command_CanExecuteChanged;
        }

        private void Command_CanExecuteChanged(object sender, System.EventArgs e)
        {
            this.IsEnabled = TapCommand.CanExecute(this);
        }

    }
}
