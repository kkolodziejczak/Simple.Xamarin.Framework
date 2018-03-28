using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Simple.Xamarin.Framework.Converters
{

    [ContentProperty("Value")]
    [AcceptEmptyServiceProvider]
    public class PxToDP : IMarkupExtension
    {
        public double Density;
        public double Value { get; set; }
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return Value * Density;
        }
    }
}
