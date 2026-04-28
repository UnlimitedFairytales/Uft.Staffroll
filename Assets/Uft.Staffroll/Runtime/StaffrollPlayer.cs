#nullable enable

using System;
using System.Collections.Generic;
using TMPro;
using Uft.Staffroll.Elements;
using Uft.Staffroll.Renderers;
using UnityEngine;

namespace Uft.Staffroll
{
    public class StaffrollPlayer : MonoBehaviour
    {
        [SerializeField] RectTransform? _contentRoot;

        [Header("Prototype")]
        [SerializeField] RndBlock? _blockPrototype;
        [SerializeField] RndImg?   _imgPrototype;

        [Header("Layout")]
        [SerializeField] float _fontSize   = 36f;
        [SerializeField] float _lineHeight = 48f;
        [SerializeField] float _brHeight   = 24f;
        [SerializeField] float _rowWidth   = 1200f;
        [SerializeField] float _twoColumnGap = 48f;

        [Header("Font")]
        [SerializeField] TMP_FontAsset? _fontA;
        [SerializeField] TMP_FontAsset? _fontB;
        [SerializeField] TMP_FontAsset? _fontC;

        [Header("Scroll")]
        [SerializeField] float _scrollSpeed = 80f;

        public Action? onCompleted;

        readonly Dictionary<Type, IRenderer> _rendererMap = new();
        ElementParser _elementParser = default!;
        bool  _playing;
        float _totalContentHeight;
        float _firstElementHeight;
        float _lastElementHeight;

        void Awake()
        {
            if (this._contentRoot == null) throw new UnassignedReferenceException(nameof(this._contentRoot));
            if (this._blockPrototype == null) throw new UnassignedReferenceException(nameof(this._blockPrototype));
            if (this._imgPrototype == null) throw new UnassignedReferenceException(nameof(this._imgPrototype));

            this._elementParser = new ElementParser(64, 16);
            this._rendererMap.Add(typeof(BrContent), new RndNewLine());
            this._rendererMap.Add(typeof(BlocksContent), new RndBlocks(this._blockPrototype));
            this._rendererMap.Add(typeof(CaptionTwoBlocksContent), new RndCaptionTwoBlocks(this._blockPrototype));
            this._rendererMap.Add(typeof(ImgContent), this._imgPrototype);
        }

        void Update()
        {
            if (!this._playing || this._contentRoot == null) return;

            this._contentRoot.anchoredPosition += Vector2.up * (this._scrollSpeed * Time.deltaTime);
            if (this._contentRoot.anchoredPosition.y >= this._totalContentHeight + 4f)
            {
                this._playing = false;
                this.onCompleted?.Invoke();
            }
        }

        public void Play(string xmlText)
        {
            if (this._contentRoot == null) throw new UnassignedReferenceException(nameof(this._contentRoot));

            this._totalContentHeight = this.BuildUI(xmlText);

            var viewHeight = this.GetViewportHeight();
            this._contentRoot.anchoredPosition = new Vector2(0f, -viewHeight);
            this._playing = true;
        }

        /// <summary>
        /// 手動スクロールモード。先頭要素の中央が画面中央に来る位置で停止する。<br/>
        /// <see cref="Scroll"/> で上下に移動できる。
        /// </summary>
        public void Browse(string xmlText)
        {
            if (this._contentRoot == null) throw new UnassignedReferenceException(nameof(this._contentRoot));

            this._totalContentHeight = this.BuildUI(xmlText);
            this._contentRoot.anchoredPosition = new Vector2(0f, this.BrowseMinY());
        }

        /// <summary>
        /// コンテンツを delta ピクセル分スクロールする（正=上方向）。
        /// クランプ範囲:
        ///   下限 … 先頭要素の中央が画面中央に来る位置
        ///   上限 … 末尾要素の中央が画面中央に来る位置
        /// </summary>
        public void Scroll(float delta)
        {
            if (this._contentRoot == null) throw new UnassignedReferenceException(nameof(this._contentRoot));

            var current = this._contentRoot.anchoredPosition.y;
            var clamped = Mathf.Clamp(current + delta, this.BrowseMinY(), this.BrowseMaxY());
            this._contentRoot.anchoredPosition = new Vector2(0f, clamped);
        }

        // 先頭要素の中央が画面中央になる anchoredPosition.y。-(画面中央までの距離) + 先頭要素の半分
        float BrowseMinY() => -(this.GetViewportHeight() * 0.5f - this._firstElementHeight * 0.5f);

        // 末尾要素の中央が画面中央になる anchoredPosition.y。コンテンツ全体高さ - 末尾要素の半分 - 画面中央までの距離
        float BrowseMaxY() => this._totalContentHeight - this._lastElementHeight * 0.5f - this.GetViewportHeight() * 0.5f;

        float BuildUI(string xmlText)
        {
            var (head, contents) = this._elementParser.Parse(xmlText);

            var ctx = new StaffrollRenderContext
            {
                FontA      = this._fontA,
                FontB      = this._fontB,
                FontC      = this._fontC,
                FontSize   = head.FontSize   ?? this._fontSize,
                LineHeight = head.LineHeight ?? this._lineHeight,
                BrHeight   = head.BrHeight  ?? this._brHeight,
                RowWidth   = head.RowWidth  ?? this._rowWidth,
                TwoColumnGap = this._twoColumnGap,
            };
            this._scrollSpeed = head.ScrollSpeed ?? this._scrollSpeed;

            float y     = 0f;
            bool  first = true;

            foreach (var content in contents)
            {
                var t = content.GetType();
                if (!this._rendererMap.TryGetValue(t, out var renderer)) continue;

                var prevY = y;
                y = renderer.Render(content, this._contentRoot!, y, ctx);

                var elementHeight = Mathf.Abs(y - prevY);
                if (first)
                {
                    this._firstElementHeight = elementHeight;
                    first = false;
                }
                this._lastElementHeight = elementHeight;
            }

            return Mathf.Abs(y);
        }

        float GetViewportHeight()
        {
            if (this._contentRoot!.parent is RectTransform viewport)
                return viewport.rect.height;
            return Screen.height;
        }
    }
}
