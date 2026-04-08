#nullable enable

using System.Xml.Linq;

namespace Uft.Staffroll.Elements
{
    public class ElmImg : IElement
    {
        public virtual IContent Parse(XElement el) => new ImgContent(el.GetAttrIgnoreCase("src") ?? string.Empty);
    }
}
