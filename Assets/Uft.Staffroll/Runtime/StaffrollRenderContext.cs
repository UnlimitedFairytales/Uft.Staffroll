#nullable enable

using TMPro;

namespace Uft.Staffroll
{
    public class StaffrollRenderContext
    {
        public TMP_FontAsset? FontA { get; set; }
        public TMP_FontAsset? FontB { get; set; }
        public TMP_FontAsset? FontC { get; set; }
        public float FontSize { get; set; } = 32f;
        public float LineHeight { get; set; } = 48f;
        public float BrHeight { get; set; } = 24f;
        public float RowWidth { get; set; } = 1200f;
        public float TwoColumnGap { get; set; } = 48f;

        /// <summary>"a","b","c" キーでフォントを解決する。その他はnull。</summary>
        public TMP_FontAsset? ResolveFont(string? key) => key switch
        {
            "a" => this.FontA,
            "b" => this.FontB,
            "c" => this.FontC,
            _ => null,
        };
    }
}
