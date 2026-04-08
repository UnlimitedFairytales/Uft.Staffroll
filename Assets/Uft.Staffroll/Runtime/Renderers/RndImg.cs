#nullable enable

using System;
using UnityEngine;
using UnityEngine.UI;

namespace Uft.Staffroll.Renderers
{
    /// <summary>
    /// &lt;img src="..."&gt;<br/>
    /// Resources から Sprite を読み込んで表示する。
    /// </summary>
    public class RndImg : MonoBehaviour, IRenderer
    {
        [SerializeField] Image? _image;

        public float Render(IContent content, RectTransform parent, float y, StaffrollRenderContext ctx)
        {
            var src          = ((ImgContent)content).Src;
            var instantiated = Instantiate(this, parent);
            var height       = instantiated.SetSprite(src, y);
            return y - height;
        }

        /// <remarks>アンカー・ピボットは中央上</remarks>
        /// <returns>実際に占有した高さ</returns>
        protected virtual float SetSprite(string src, float anchoredY)
        {
            var sprite = Resources.Load<Sprite>(src);
            if (sprite == null) throw new Exception($"Sprite not found in Resources: '{src}'");
            if (this._image == null) throw new UnassignedReferenceException(nameof(this._image));

            this._image.sprite = sprite;
            this._image.SetNativeSize();

            var rt = (RectTransform)this.transform;
            rt.anchorMin = new Vector2(0.5f, 1f);
            rt.anchorMax = new Vector2(0.5f, 1f);
            rt.pivot = new Vector2(0.5f, 1f);
            rt.anchoredPosition = new Vector2(0f, anchoredY);

            return rt.sizeDelta.y;
        }
    }
}
