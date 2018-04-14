using System;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Simple.Xamarin.Framework
{

    public class ExtendedLabel : StackLayout
    {
        [Obsolete("Use of Width is forbidden.", true)]
        public new static readonly BindableProperty WidthRequestProperty;
        [Obsolete("Use of Height is forbidden.", true)]
        public new static readonly BindableProperty HeightRequestProperty;
        [Obsolete("Use of MinimumWidth is forbidden.", true)]
        public new static readonly BindableProperty MinimumWidthRequestProperty;
        [Obsolete("Use of MinimumHeight is forbidden.", true)]
        public new static readonly BindableProperty MinimumHeightRequestProperty;
        [Obsolete("Use of MinimumHeight is forbidden.", true)]
        public new static readonly BindableProperty SpacingProperty;

        public ExtendedLabel()
        {
            Spacing = 0;
            Padding = Margin = new NamedThickness(ExetendedNamedSize.Zero);
             
            HorizontalOptions = LayoutOptions.StartAndExpand;
            VerticalOptions = LayoutOptions.FillAndExpand;
            Children.Add(new Label
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            });
        }

        #region Common Properties

        public new static readonly BindableProperty MarginProperty =
           BindableProperty.Create(nameof(Margin), typeof(NamedThickness), typeof(ExtendedLabel), default(NamedThickness), propertyChanged: (b, o, n) =>
           {
               if (b is StackLayout stackLayout)
               {
                   if (n is NamedThickness thickness)
                   {
                       stackLayout.Margin = new Thickness(thickness.Left, thickness.Top, thickness.Right, thickness.Bottom);
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
               if (b is StackLayout stackLayout)
               {
                   if (n is NamedThickness thickness)
                   {
                       stackLayout.Padding = new Thickness(thickness.Left, thickness.Top, thickness.Right, thickness.Bottom);
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
               if (b is StackLayout stackLayout)
               {
                   stackLayout.GestureRecognizers.Clear();
                   stackLayout.GestureRecognizers.Add(new TapGestureRecognizer
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
               if (b is StackLayout stackLayout)
               {
                   if (stackLayout.GestureRecognizers.FirstOrDefault() is TapGestureRecognizer gesture)
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

        #region Label Properties

        public static readonly BindableProperty HorizontalTextAlignmentProperty =
            BindableProperty.Create(nameof(HorizontalTextAlignment), typeof(TextAlignment), typeof(ExtendedLabel), default(TextAlignment), propertyChanged: (b, o, n) =>
            {
                var label = GetLabel(b);
                if (n is TextAlignment textAlignment)
                {
                    label.HorizontalTextAlignment = textAlignment;
                }
            });

        public TextAlignment HorizontalTextAlignment
        {
            get => (TextAlignment)GetValue(HorizontalTextAlignmentProperty);
            set => SetValue(HorizontalTextAlignmentProperty, value);
        }

        public static readonly BindableProperty VerticalTextAlignmentProperty =
            BindableProperty.Create(nameof(VerticalTextAlignment), typeof(TextAlignment), typeof(ExtendedLabel), default(TextAlignment), propertyChanged: (b, o, n) =>
            {
                var label = GetLabel(b);
                if (n is TextAlignment textAlignment)
                {
                    label.VerticalTextAlignment = textAlignment;
                }
            });

        public TextAlignment VerticalTextAlignment
        {
            get => (TextAlignment)GetValue(VerticalTextAlignmentProperty);
            set => SetValue(VerticalTextAlignmentProperty, value);
        }

        public static readonly BindableProperty FontSizeProperty =
          BindableProperty.Create(nameof(FontSize), typeof(ExetendedNamedSize), typeof(ExtendedLabel), ExetendedNamedSize.Default, propertyChanged: (b, o, n) =>
          {
              var label = GetLabel(b);
              if (n is ExetendedNamedSize size)
              {
                  label.FontSize = SXF.GetFontSize(size);
              }
          });

        public ExetendedNamedSize FontSize
        {
            get => (ExetendedNamedSize)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        public static readonly BindableProperty TextProperty =
           BindableProperty.Create(nameof(Text), typeof(string), typeof(ExtendedLabel), default(string), propertyChanged: (b, o, n) =>
           {
               var label = GetLabel(b);
               if (n is string text)
               {
                   label.Text = text;
               }
           });

        /// <summary>
        /// Text of <see cref="ExtendedLabel"/>
        /// </summary>
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly BindableProperty TextColorProperty =
           BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(ExtendedLabel), default(Color), propertyChanged: (b, o, n) =>
           {
               var label = GetLabel(b);
               if (n is Color color)
               {
                   label.TextColor = color;
               }
           });

        /// <summary>
        /// Color of <see cref="ExtendedLabel"/>
        /// </summary>
        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        public static readonly BindableProperty FontAttributesProperty =
           BindableProperty.Create(nameof(FontAttributes), typeof(FontAttributes), typeof(ExtendedLabel), default(FontAttributes), propertyChanged: (b, o, n) =>
           {
               var label = GetLabel(b);
               if (n is FontAttributes attributes)
                   label.FontAttributes = attributes;
           });

        /// <summary>
        /// A string specifying style information like Italic and Bold.
        /// </summary>
        public FontAttributes FontAttributes
        {
            get => (FontAttributes)GetValue(FontAttributesProperty);
            set => SetValue(FontAttributesProperty, value);
        }

        public static readonly BindableProperty FontFamilyProperty =
           BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(ExtendedLabel), default(string), propertyChanged: (b, o, n) =>
           {
               var label = GetLabel(b);
               if (n is string family)
                   label.FontFamily = family;
           });

        /// <summary>
        /// The <see cref="string"/> font name.
        /// </summary>
        public string FontFamily
        {
            get => (string)GetValue(FontFamilyProperty);
            set => SetValue(FontFamilyProperty, value);
        }

        #endregion

        #region Helpers

        private static Label GetLabel(BindableObject bindable)
        {
            var control = bindable as StackLayout;
            if (control.Children.FirstOrDefault() is Label label)
                return label;
            return null;
        }

        #endregion

    }
}
