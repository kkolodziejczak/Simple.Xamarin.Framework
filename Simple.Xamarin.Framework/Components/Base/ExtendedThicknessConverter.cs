using System;
using Xamarin.Forms;

namespace Simple.Xamarin.Framework
{
    public class ExtendedThicknessConverter : TypeConverter
    {
        public override bool CanConvertFrom(Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override object ConvertFromInvariantString(string value)
        {
            var namedSizes = value.Replace(" ", "").Split(',');
            try
            {
                if (namedSizes.Length == 1)
                {
                    Enum.TryParse(value, out ExetendedNamedSize size);
                    return new NamedThickness(size);
                }
                if (namedSizes.Length == 2)
                {
                    var horizontal = Enum.Parse(typeof(ExetendedNamedSize), namedSizes[0]);
                    var vertical = Enum.Parse(typeof(ExetendedNamedSize), namedSizes[1]);
                    return new NamedThickness((ExetendedNamedSize)horizontal, (ExetendedNamedSize)vertical);
                }
                if (namedSizes.Length != 4)
                {
                    throw new ArgumentException("[Simple.Xamarin.Framework]\nExtendedThickness can only have 1, 2, 4 thicknesses!");
                }
                Enum.TryParse(namedSizes[0], out ExetendedNamedSize left);
                Enum.TryParse(namedSizes[1], out ExetendedNamedSize top);
                Enum.TryParse(namedSizes[2], out ExetendedNamedSize right);
                Enum.TryParse(namedSizes[3], out ExetendedNamedSize bottom);
                return new NamedThickness(left, top, right, bottom);
            }
            catch (ArgumentException)
            {
                throw new NotSupportedException($"[Simple.Xamarin.Framework]\nValue: \"{value}\" contains values that are not allowed in Thickness! You can find allowed values in enum called {nameof(ExetendedNamedSize)}.");
            }
        }
    }
}
