#nullable enable

using TMPro;
using UnityEngine;

namespace Uft.Staffroll.Renderers
{
    /// <summary>
    /// &lt;div class="caption-2blocks"&gt;<br/>
    /// 左キャプションor空 + 右アイテム。<br/>
    /// キャプションは1行目のみ表示し、2行目以降は空セルを配置する。
    /// </summary>
    public class RndCaptionTwoBlocks : IRenderer
    {
        readonly RndBlock _blockPrototype;

        public RndCaptionTwoBlocks(RndBlock blockPrototype)
        {
            this._blockPrototype = blockPrototype;
        }

        public float Render(IContent content, RectTransform parent, float y, StaffrollRenderContext ctx)
        {
            var casted = (CaptionTwoBlocksContent)content;
            if (casted.Items.Count == 0) return y; // NOTE: 描画なし

            for (int i = 0; i < casted.Items.Count; i++)
            {
                var row = RowFactory.CreateRowContainer(parent, y, ctx, ctx.TwoColumnGap);

                // 左列: 1行目のみキャプション、以降は空セル
                var caption = Object.Instantiate(this._blockPrototype, row);
                caption.SetText(i == 0 ? casted.Caption : string.Empty, ctx.ResolveFont(casted.FontKey), TextAlignmentOptions.Right, ctx.FontSize);

                // 右列: アイテム
                var text = casted.Items[i];
                var block = Object.Instantiate(this._blockPrototype, row);
                block.SetText(text, ctx.ResolveFont(casted.FontKey), TextAlignmentOptions.Left, ctx.FontSize);

                y -= ctx.LineHeight;
            }

            return y;
        }
    }
}
