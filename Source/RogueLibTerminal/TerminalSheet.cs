namespace RogueLibTerminal
{
    public class TerminalSheet
    {
        public int SymbolWidth { get; set; }
        public int SymbolHeight { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        private Symbol[] _symbols;

        public TerminalSheet(int width, int height, int symbolWidth, int symbolHeight)
        {
            Width = width;
            Height = height;
            SymbolWidth = symbolWidth;
            SymbolHeight = symbolHeight;

            _symbols = new Symbol[Width * Height];
        }
    }
}