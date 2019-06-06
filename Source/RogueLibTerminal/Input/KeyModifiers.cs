using System;

namespace RogueLibTerminal.Input
{
	[Flags]
	public enum KeyModifiers
	{
		/// <summary>
        /// No key modifiers
        /// </summary>
		None = 0,
		/// <summary>
        /// Left Control key is down
        /// </summary>
		LeftControl = 1,
		/// <summary>
        /// Left Alt key is down
        /// </summary>
		LeftAlt = 2,
		/// <summary>
        /// Left Shift key is down
        /// </summary>
		LeftShift = 4,
		/// <summary>
        /// Right Control key is down
        /// </summary>
		RightControl = 8,
		/// <summary>
        /// Right Alt key is down
        /// </summary>
		RightAlt = 16,
		/// <summary>
        /// Right Shift key is down
        /// </summary>
		RightShift = 32,
		/// <summary>
        /// Caps Lock is on
        /// </summary>
		Caps = 64
	}
}
