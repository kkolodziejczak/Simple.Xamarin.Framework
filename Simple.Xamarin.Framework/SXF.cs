using System.Collections.Generic;
using Xamarin.Forms;

namespace Simple.Xamarin.Framework
{

    public class SXF
    {
        private static double _baseSpacing = Device.GetNamedSize(NamedSize.Large, typeof(Label)) - Device.GetNamedSize(NamedSize.Medium, typeof(Label));
        public static bool Initialized { get; private set; }
        public static double DefaultFontSize { get; private set; } = 14;
        public static double DefaultUnitSize { get; private set; } = 20;

        public static double GetFontSize(ExetendedNamedSize size) => GetSize(size, true);
        public static double GetUnitSize(ExetendedNamedSize size) => GetSize(size, false);

        public static Color IconTextColor { get; set; } = Color.White;
        public static Color MainColor { get; set; } = Color.Green;

        public static double HalfUnit => OneUnit / 2;
        public static double QuarterUnit => OneUnit / 4;
        public static double OneUnit => DefaultUnitSize;
        public static double DoubleUnit => OneUnit * 2;
        public static double TripleUnit => OneUnit * 3;
        public static double QuadrupleUnit => OneUnit * 4;

        public SXF SetDefaultFontSize(double size)
        {
            DefaultFontSize = size;
            return this;
        }

        public SXF SetDefaultUnitSize(double size)
        {
            DefaultUnitSize = size;
            return this;
        }

        public SXF SetIconTextColor(Color color)
        {
            IconTextColor = color;
            return this;
        }

        public SXF SetMainColor(Color color)
        {
            MainColor = color;
            return this;
        }

        public void Initialize()
        {
            Initialized = true;
        }

        private static double GetSize(ExetendedNamedSize size, bool font)
        {
            var baseunit = font ? DefaultFontSize : DefaultUnitSize;
            switch (size)
            {
                case ExetendedNamedSize.Default:
                    return baseunit;
                case ExetendedNamedSize.XSmall:
                    return baseunit * 0.25;
                case ExetendedNamedSize.xSmall:
                    return baseunit * 0.5;
                case ExetendedNamedSize.Small:
                    return baseunit * 0.75;
                case ExetendedNamedSize.Normal:
                    return baseunit;
                case ExetendedNamedSize.Large:
                    return baseunit + baseunit * 0.25;
                case ExetendedNamedSize.xLarge:
                    return baseunit + baseunit * 0.5;
                case ExetendedNamedSize.XLarge:
                    return baseunit + baseunit * 0.75;
                case ExetendedNamedSize.xxLarge:
                    return baseunit * 2;
                case ExetendedNamedSize.XXLarge:
                    return baseunit + baseunit * 1.25;
                case ExetendedNamedSize.Zero:
                    return 0;
                default:
                    return baseunit;
            }
        }
    }
}
