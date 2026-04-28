#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Uft.Staffroll
{
    public static class Extensions
    {
        /// <summary>IgnoreCaseで属性を取得する。属性が存在しない場合はnullを返す。</summary>
        public static string? GetAttrIgnoreCase(this XElement el, string name)
        {
            foreach (var attr in el.Attributes())
            {
                if (string.Equals(attr.Name.LocalName, name, StringComparison.OrdinalIgnoreCase))
                    return attr.Value;
            }
            return null;
        }

        /// <summary>IgnoreCaseで子要素を1つ取得する。存在しない場合はnullを返す。</summary>
        public static XElement? GetElementIgnoreCase(this XElement el, string name)
        {
            foreach (var child in el.Elements())
            {
                if (string.Equals(child.Name.LocalName, name, StringComparison.OrdinalIgnoreCase))
                    return child;
            }
            return null;
        }

        /// <summary>子ノードをXML文字列として結合して返す。TMProのrichTextタグ（&lt;b&gt;等）をそのまま保持する。CDATAセクションは中身のみ展開する。</summary>
        public static string InnerXml(this XElement el) =>
            string.Concat(el.Nodes().Select(n => n is XCData cdata ? cdata.Value : n.ToString()));

        /// <summary>IgnoreCaseで子要素を取得する。</summary>
        public static IEnumerable<XElement> GetElementsIgnoreCase(this XElement el, string name)
        {
            foreach (var child in el.Elements())
            {
                if (string.Equals(child.Name.LocalName, name, StringComparison.OrdinalIgnoreCase))
                    yield return child;
            }
        }
    }

    public static class StringExtensions
    {
        /// <summary>IgnoreCaseで文字列を比較する。</summary>
        public static bool EqualsIgnoreCase(this string? s, string? other) =>
            string.Equals(s, other, StringComparison.OrdinalIgnoreCase);
    }
}
