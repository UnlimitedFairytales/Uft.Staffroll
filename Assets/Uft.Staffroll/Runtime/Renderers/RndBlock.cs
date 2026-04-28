#nullable enable

using TMPro;
using UnityEngine;

namespace Uft.Staffroll.Renderers
{
    /// <summary>
    /// ブロックレイアウト用の1セル。<br/>
    /// Blocks / CaptionTwoBlocks で共用するプロトタイプ。<br/>
    /// セルの幅・見た目は Prefab 側（LayoutElement 等）で制御する。
    /// </summary>
    public class RndBlock : MonoBehaviour
    {
        [SerializeField] TMP_Text? _text;

        public void SetText(string text, TMP_FontAsset? font = null, TextAlignmentOptions? alignment = null, float fontSize = 0f)
        {
            if (this._text == null) throw new UnassignedReferenceException(nameof(this._text));

            this._text.text = text;
            if (font != null) this._text.font = font;
            if (alignment.HasValue) this._text.alignment = alignment.Value;
            if (fontSize > 0f) this._text.fontSize = fontSize;
        }
    }
}
