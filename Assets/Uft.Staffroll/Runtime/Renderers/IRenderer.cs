#nullable enable

using UnityEngine;

namespace Uft.Staffroll.Renderers
{
    public interface IRenderer
    {
        /// <summary><see cref="IContent"/>に対する描画の決定。</summary>
        /// <param name="y">描画開始位置</param>
        /// <returns>次のContentの推奨描画開始位置（＝この描画の高さを引いた値）</returns>
        float Render(IContent content, RectTransform parent, float y, StaffrollRenderContext ctx);
    }
}
