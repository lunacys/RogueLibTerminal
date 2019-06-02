using System;

namespace RogueLibTerminal
{
    public class OnKeyEventEventHandler
    {
        // TODO: Add keyboard modifiers (ctrl, shift, etc)
        public Keys Key { get; }

        public OnKeyEventEventHandler(Keys key)
        {
            Key = key;
        }
    }
}