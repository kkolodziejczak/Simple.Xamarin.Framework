using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Simple.Xamarin.Framework
{

    public class SXF
    {
        public static bool Initialized { get; private set; }
        public static double DefaultFontSize { get; private set; } = 14;
        public static double DefaultUnitSize { get; private set; } = 20;

        public static double GetFontSize(ExetendedNamedSize size) => GetSize(size, true);
        public static double GetUnitSize(ExetendedNamedSize size) => GetSize(size, false);

        public static Color IconTextColor { get; set; } = Color.White;
        public static Color MainColor { get; set; } = Color.Green;

        public static double ProgressBarPageBackgroundOpacity { get; set; } = 0.7;
        public static double ProgressBarThickness { get; set; } = 25;

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
            double baseSpacing = Device.GetNamedSize(NamedSize.Large, typeof(Label)) - Device.GetNamedSize(NamedSize.Medium, typeof(Label));
            var unitSpacing = font ? baseSpacing : DefaultUnitSize/2;
            switch (size)
            {
                case ExetendedNamedSize.Default:
                    return baseunit;
                case ExetendedNamedSize.XSmall:
                    return baseunit - unitSpacing * 2;
                case ExetendedNamedSize.xSmall:
                    return baseunit - unitSpacing * 1.5;
                case ExetendedNamedSize.Small:
                    return baseunit - unitSpacing;
                case ExetendedNamedSize.Normal:
                    return baseunit;
                case ExetendedNamedSize.Large:
                    return baseunit + unitSpacing;
                case ExetendedNamedSize.xLarge:
                    return baseunit + unitSpacing * 1.5;
                case ExetendedNamedSize.XLarge:
                    return baseunit + unitSpacing * 2;
                case ExetendedNamedSize.xxLarge:
                    return baseunit + unitSpacing * 2.5;
                case ExetendedNamedSize.XXLarge:
                    return baseunit + unitSpacing * 3;
                case ExetendedNamedSize.Zero:
                    return 0;
                default:
                    return baseunit;
            }
        }
    }
}
