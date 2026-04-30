#nullable enable

using System.Collections.Generic;
using System.Xml.Linq;

namespace Uft.Staffroll.Elements
{
    public class ElmDiv : IElement
    {
        protected int _childrenCapacity;

        public ElmDiv(int childrenCapacity = 16)
        {
            this._childrenCapacity = childrenCapacity;
        }

        public virtual IContent Parse(XElement el)
        {
            var attrClass = el.GetAttrIgnoreCase("class");
            var attrFont  = el.GetAttrIgnoreCase("font");

            if (attrClass.EqualsIgnoreCase("2blocks"))
                return new BlocksContent(2, this.ParseChildren(el), attrFont);

            if (attrClass.EqualsIgnoreCase("3blocks"))
                return new BlocksContent(3, this.ParseChildren(el), attrFont);

            if (attrClass.EqualsIgnoreCase("caption-2blocks"))
            {
                var caption = string.Empty;
                var items = new List<string>(this._childrenCapacity);
                foreach (var child in el.GetElementsIgnoreCase("div"))
                {
                    if (child.GetAttrIgnoreCase("class").EqualsIgnoreCase("caption"))
                        caption = child.InnerXml();
                    else
                        items.Add(child.InnerXml());
                }
                return new CaptionTwoBlocksContent(caption, items, attrFont);
            }

            return new BlocksContent(1, new List<string> { el.InnerXml() }, attrFont);
        }

        protected virtual List<string> ParseChildren(XElement el)
        {
            var list = new List<string>(this._childrenCapacity);
            foreach (var child in el.GetElementsIgnoreCase("div"))
                list.Add(child.InnerXml());
            return list;
        }
    }
}
