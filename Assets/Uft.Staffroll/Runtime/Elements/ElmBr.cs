#nullable enable

using System.Xml.Linq;

namespace Uft.Staffroll.Elements
{
    public class ElmBr : IElement
    {
        public virtual IContent Parse(XElement el) => new BrContent();
    }
}
