#nullable enable

using System.Xml.Linq;

namespace Uft.Staffroll.Elements
{
    public interface IElement
    {
        IContent Parse(XElement el);
    }
}
