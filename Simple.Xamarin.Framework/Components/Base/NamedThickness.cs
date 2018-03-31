using Xamarin.Forms;

namespace Simple.Xamarin.Framework
{
    /// <summary>
    /// <see cref="NamedThickness"/> is Thickness that takes <see cref="ExetendedNamedSize"/> and doubles to combine unit system with double values
    /// </summary>
    [TypeConverter(typeof(ExtendedThicknessConverter))]
    public struct NamedThickness
    {
        public NamedThickness(ExetendedNamedSize uniformSize) : this(uniformSize, uniformSize, uniformSize, uniformSize) { }
        public NamedThickness(ExetendedNamedSize horizontalSize, ExetendedNamedSize verticalSize) : this(horizontalSize, verticalSize, horizontalSize, verticalSize) { }
        public NamedThickness(ExetendedNamedSize left, ExetendedNamedSize top, ExetendedNamedSize right, ExetendedNamedSize bottom)
        {
            Top = SXF.GetUnitSize(top);
            Left = SXF.GetUnitSize(left);
            Right = SXF.GetUnitSize(right);
            Bottom = SXF.GetUnitSize(bottom);
        }

        /// <summary>
        /// The thickness of the left side of a rectangle.
        /// </summary>
        public double Left { get; set; }

        /// <summary>
        /// The thickness of the top of a rectangle.
        /// </summary>
        public double Top { get; set; }
        /// <summary>
        /// The thickness of the right side of a rectangle.
        /// </summary>
        public double Right { get; set; }
        /// <summary>
        /// The thickness of the bottom of a rectangle.
        /// </summary>
        public double Bottom { get; set; }
    }
}
