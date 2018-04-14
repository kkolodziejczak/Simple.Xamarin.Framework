using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Simple.Xamarin.Framework
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProgressBar : ContentView
	{
		public ProgressBar ()
		{
			InitializeComponent ();
		}

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create("Text", typeof(string), typeof(ProgressBar), default(string));

//        , propertyChanged: (b, o, n) =>
//            {
//                if (b is ContentView content)
//                {
//                    if (content.Content is Grid layout)
//                    {
//                        if (layout.Children[1] is StackLayout stackLayout)
//                        {
//                            if (stackLayout.Children[1] is ExtendedLabel label)
//                            {
//                                label.Text = n as string;
//                            }
//}
//                    }
//                }
//            }

        /// <summary>
        /// Text that will be displayed above ActivityIndicator
        /// </summary>
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly BindableProperty ProgressProperty =
            BindableProperty.Create("Progress", typeof(double), typeof(ProgressBar), default(double), propertyChanged: (b, o, n) =>
            {
                if (b is ContentView content)
                {
                    if (content.Content is Grid layout)
                    {
                        if (layout.Children[1] is StackLayout stackLayout)
                        {
                            if (stackLayout.Children[0] is Grid progressGrid)
                            {
                                var parent = progressGrid.Children[1].Parent as Grid;
                                progressGrid.Children[1].WidthRequest = parent.Width * (double)n;
                            }
                        }
                    }
                }
            });

        public double Progress
        {
            get => (double)GetValue(ProgressProperty);
            set => SetValue(ProgressProperty, value);
        }
    }
}