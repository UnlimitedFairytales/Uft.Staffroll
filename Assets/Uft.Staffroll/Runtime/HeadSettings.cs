#nullable enable

using System.Globalization;
using System.Xml.Linq;

namespace Uft.Staffroll
{
    public class HeadSettings
    {
        public float? FontSize { get; set; }
        public float? LineHeight { get; set; }
        public float? BrHeight { get; set; }
        public float? RowWidth { get; set; }
        public float? TwoColumnGap { get; set; }
        public float? ScrollSpeed { get; set; }

        public void Parse(XElement headEl)
        {
            var layoutEl = headEl.GetElementIgnoreCase("layout");
            if (layoutEl != null)
            {
                this.FontSize = ParseFloat(layoutEl.GetAttrIgnoreCase("font-size"));
                this.LineHeight = ParseFloat(layoutEl.GetAttrIgnoreCase("line-height"));
                this.BrHeight = ParseFloat(layoutEl.GetAttrIgnoreCase("br-height"));
                this.RowWidth = ParseFloat(layoutEl.GetAttrIgnoreCase("row-width"));
                this.TwoColumnGap = ParseFloat(layoutEl.GetAttrIgnoreCase("two-column-gap"));
            }

            var scrollEl = headEl.GetElementIgnoreCase("scroll");
            if (scrollEl != null)
                this.ScrollSpeed = ParseFloat(scrollEl.GetAttrIgnoreCase("speed"));
        }

        static float? ParseFloat(string? value)
        {
            if (value == null) return null;
            if (float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out var result))
                return result;
            return null;
        }
    }
}
