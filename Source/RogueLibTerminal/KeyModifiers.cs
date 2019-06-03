using System;

namespace RogueLibTerminal
{
	[Flags]
	public enum KeyModifiers
	{
		None = 0,
		LeftControl = 1,
		LeftAlt = 2,
		LeftShift = 4,
		RightControl = 8,
		RightAlt = 16,
		RightShift = 32,
		Caps = 64
	}
}
