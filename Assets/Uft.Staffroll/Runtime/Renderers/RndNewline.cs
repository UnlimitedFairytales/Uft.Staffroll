#nullable enable

using UnityEngine;

namespace Uft.Staffroll.Renderers
{
    public class RndNewLine : IRenderer
    {
        public float Render(IContent content, RectTransform parent, float y, StaffrollRenderContext ctx) => y - ctx.BrHeight;
    }
}
