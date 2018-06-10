using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SXF
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActivityIndicator : ContentView
    {
        public ActivityIndicator()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create("Text", typeof(string), typeof(ActivityIndicator), default(string));

        /// <summary>
        /// Text that will be displayed above ActivityIndicator
        /// </summary>
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

    }
}