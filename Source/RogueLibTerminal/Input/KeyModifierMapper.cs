using System;
using SDL2;

namespace RogueLibTerminal.Input
{
	internal static class KeyModifierMapper
	{
		public static KeyModifiers MapSdlMods(SDL.SDL_Keymod mods)
		{
			if (mods == SDL.SDL_Keymod.KMOD_NONE)
				return KeyModifiers.None;

			KeyModifiers result = KeyModifiers.None;

			if ((mods & SDL.SDL_Keymod.KMOD_LCTRL) != SDL.SDL_Keymod.KMOD_NONE)
				result |= KeyModifiers.LeftControl;
			if ((mods & SDL.SDL_Keymod.KMOD_LALT) != SDL.SDL_Keymod.KMOD_NONE)
				result |= KeyModifiers.LeftAlt;
			if ((mods & SDL.SDL_Keymod.KMOD_LSHIFT) != SDL.SDL_Keymod.KMOD_NONE)
				result |= KeyModifiers.LeftShift;
			if ((mods & SDL.SDL_Keymod.KMOD_RCTRL) != SDL.SDL_Keymod.KMOD_NONE)
				result |= KeyModifiers.RightControl;
			if ((mods & SDL.SDL_Keymod.KMOD_RALT) != SDL.SDL_Keymod.KMOD_NONE)
				result |= KeyModifiers.RightAlt;
			if ((mods & SDL.SDL_Keymod.KMOD_RSHIFT) != SDL.SDL_Keymod.KMOD_NONE)
				result |= KeyModifiers.RightShift;
			if ((mods & SDL.SDL_Keymod.KMOD_CAPS) != SDL.SDL_Keymod.KMOD_NONE)
				result |= KeyModifiers.Caps;

            return result;
		}
	}
}
