#nullable enable

using UnityEngine;
using UnityEngine.UI;

namespace Uft.Staffroll.Renderers
{
    internal static class RowFactory
    {
        /// <summary>HorizontalLayoutGroup を持つ行コンテナを生成して返す。</summary>
        /// <remarks>アンカー・ピボットは中央上</remarks>
        internal static RectTransform CreateRowContainer(RectTransform parent, float anchoredY, StaffrollRenderContext ctx)
        {
            var go = new GameObject("Row", typeof(RectTransform));
            go.transform.SetParent(parent, false);

            var rt = (RectTransform)go.transform;
            rt.anchorMin = new Vector2(0.5f, 1f);
            rt.anchorMax = new Vector2(0.5f, 1f);
            rt.pivot = new Vector2(0.5f, 1f);
            rt.anchoredPosition = new Vector2(0f, anchoredY);
            rt.sizeDelta = new Vector2(ctx.RowWidth, ctx.LineHeight);

            var hlg = go.AddComponent<HorizontalLayoutGroup>();
            hlg.childAlignment = TextAnchor.MiddleCenter;
            hlg.childControlWidth = true;
            hlg.childControlHeight = true;
            hlg.childForceExpandWidth = true;
            hlg.childForceExpandHeight = true;

            return rt;
        }
    }
}
