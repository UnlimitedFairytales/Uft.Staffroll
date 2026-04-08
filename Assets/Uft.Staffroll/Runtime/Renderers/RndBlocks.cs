#nullable enable

using UnityEngine;

namespace Uft.Staffroll.Renderers
{
    /// <summary>
    /// &lt;div class="Nblocks"&gt;<br/>
    /// Nカラムレイアウト。<br/>
    /// 項目をN個ずつ1行にまとめ、端数は空セルで補完する。
    /// </summary>
    public class RndBlocks : IRenderer
    {
        readonly RndBlock _blockPrototype;

        public RndBlocks(RndBlock blockPrototype)
        {
            this._blockPrototype = blockPrototype;
        }

        public float Render(IContent content, RectTransform parent, float y, StaffrollRenderContext ctx)
        {
            var casted = (BlocksContent)content;
            if (casted.Items.Count == 0) return y; // NOTE: 描画なし

            var cols = casted.Cols;
            for (int i = 0; i < casted.Items.Count; i += cols)
            {
                var row = RowFactory.CreateRowContainer(parent, y, ctx);

                for (int col = 0; col < cols; col++)
                {
                    var text = (i + col) < casted.Items.Count ? casted.Items[i + col] : string.Empty;
                    var block = Object.Instantiate(this._blockPrototype, row);
                    block.SetText(text, ctx.ResolveFont(casted.FontKey));
                }

                y -= ctx.LineHeight;
            }

            return y;
        }
    }
}
