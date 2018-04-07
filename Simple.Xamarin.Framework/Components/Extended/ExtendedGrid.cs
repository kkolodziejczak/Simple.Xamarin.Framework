using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Simple.Xamarin.Framework.Components
{
    public class ExtendedGrid : Grid
    {
        [Obsolete("Use of Width is forbidden.", true)]
        public new static readonly BindableProperty WidthRequest;
        [Obsolete("Use of Height is forbidden.", true)]
        public new static readonly BindableProperty HeightRequest;
        [Obsolete("Use of MinimumWidth is forbidden.", true)]
        public new static readonly BindableProperty MinimumWidthRequest;
        [Obsolete("Use of MinimumHeight is forbidden.", true)]
        public new static readonly BindableProperty MinimumHeightRequest;

        #region Common Properties

        public new static readonly BindableProperty MarginProperty =
           BindableProperty.Create(nameof(Margin), typeof(NamedThickness), typeof(ExtendedLabel), default(NamedThickness), propertyChanged: (b, o, n) =>
           {
               if (b is Grid grid)
               {
                   if (n is NamedThickness thickness)
                   {
                       grid.Margin = new Thickness(thickness.Left, thickness.Top, thickness.Right, thickness.Bottom);
                   }
               }
           });

        public new NamedThickness Margin
        {
            get => (NamedThickness)GetValue(MarginProperty);
            set => SetValue(MarginProperty, value);

        }

        public new static readonly BindableProperty PaddingProperty =
           BindableProperty.Create(nameof(Padding), typeof(NamedThickness), typeof(ExtendedLabel), default(NamedThickness), propertyChanged: (b, o, n) =>
           {
               if (b is Grid grid)
               {
                   if (n is NamedThickness thickness)
                   {
                       grid.Padding = new Thickness(thickness.Left, thickness.Top, thickness.Right, thickness.Bottom);
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
               if (b is Grid grid)
               {
                   grid.GestureRecognizers.Clear();
                   grid.GestureRecognizers.Add(new TapGestureRecognizer
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
            set => SetValue(TapCommandProperty, value);
        }

        public static readonly BindableProperty TapCommandParameterProperty =
           BindableProperty.Create(nameof(TapCommandParameter), typeof(object), typeof(ExtendedLabel), default(object), propertyChanged: (b, o, n) =>
           {
               if (b is Grid grid)
               {
                   if (grid.GestureRecognizers.FirstOrDefault() is TapGestureRecognizer gesture)
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

    }
}
