namespace RogueLibTerminal
{
    public class OnKeyEventEventHandler
    {
        public Keys Key { get; }
		public KeyModifiers Modifiers { get; }

		public bool IsControlDown => Modifiers.HasFlag(KeyModifiers.LeftControl) || Modifiers.HasFlag(KeyModifiers.RightControl);
		public bool IsAltDown => Modifiers.HasFlag(KeyModifiers.LeftAlt) || Modifiers.HasFlag(KeyModifiers.RightAlt);
		public bool IsShiftDown => Modifiers.HasFlag(KeyModifiers.LeftShift) || Modifiers.HasFlag(KeyModifiers.RightShift);

        public OnKeyEventEventHandler(Keys key, KeyModifiers modifiers)
        {
            Key = key;
            Modifiers = modifiers;
        }
    }
}