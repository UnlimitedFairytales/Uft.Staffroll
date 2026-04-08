#nullable enable

using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Uft.Staffroll.Elements
{
    public class ElementParser
    {
        readonly int _contentsCapacity;
        readonly Dictionary<string, IElement> _elementMap;

        public ElementParser(int contentsCapacity = 16, int divChildrenCapacity = 16)
        {
            this._contentsCapacity = contentsCapacity;
            this._elementMap = new(StringComparer.OrdinalIgnoreCase)
            {
                { "div", new ElmDiv(divChildrenCapacity) },
                { "br", new ElmBr() },
                { "img", new ElmImg() },
            };
        }

        public List<IContent> Parse(string xmlText)
        {
            var contents = new List<IContent>(this._contentsCapacity);

            var root = XDocument.Parse(xmlText).Root;
            if (root == null) return contents;

            foreach (var el in root.Elements())
            {
                var name = el.Name.LocalName;
                if (!this._elementMap.TryGetValue(name, out var element)) continue;
                var content = element.Parse(el);
                contents.Add(content);
            }
            return contents;
        }
    }
}
