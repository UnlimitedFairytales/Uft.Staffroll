#nullable enable

using System.Collections.Generic;

namespace Uft.Staffroll
{
    public interface IContent { }

    public class BlocksContent : IContent
    {
        public int Cols { get; }
        public IReadOnlyList<string> Items { get; }
        public string? FontKey { get; }
        public BlocksContent(int cols, IReadOnlyList<string> items, string? fontKey)
        {
            this.Cols = cols;
            this.Items = items;
            this.FontKey = fontKey;
        }
    }

    public class CaptionTwoBlocksContent : IContent
    {
        public string Caption { get; }
        public IReadOnlyList<string> Items { get; }
        public string? FontKey { get; }
        public CaptionTwoBlocksContent(string caption, IReadOnlyList<string> items, string? fontKey)
        {
            this.Caption = caption;
            this.Items = items;
            this.FontKey = fontKey;
        }
    }

    public class BrContent : IContent { }

    public class ImgContent : IContent
    {
        public string Src { get; }
        public ImgContent(string src) { this.Src = src; }
    }
}
