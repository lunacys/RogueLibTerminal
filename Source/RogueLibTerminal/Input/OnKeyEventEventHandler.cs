namespace RogueLibTerminal.Input
{
    public class OnKeyEventEventHandler
    {
		/// <summary>
        /// Gets current key
        /// </summary>
        public Keys Key { get; }
        /// <summary>
        /// Gets active key modifiers
        /// </summary>
		public KeyModifiers Modifiers { get; }

		/// <summary>
        /// Gets whether either left or right Control key is down
        /// </summary>
		public bool IsControlDown => Modifiers.HasFlag(KeyModifiers.LeftControl) || Modifiers.HasFlag(KeyModifiers.RightControl);
		/// <summary>
        /// Gets whether either left or right Alt key is down
        /// </summary>
		public bool IsAltDown => Modifiers.HasFlag(KeyModifiers.LeftAlt) || Modifiers.HasFlag(KeyModifiers.RightAlt);
		/// <summary>
        /// Gets whether either left or right Shift key is down
        /// </summary>
		public bool IsShiftDown => Modifiers.HasFlag(KeyModifiers.LeftShift) || Modifiers.HasFlag(KeyModifiers.RightShift);

        public OnKeyEventEventHandler(Keys key, KeyModifiers modifiers)
        {
            Key = key;
            Modifiers = modifiers;
        }
    }
}