# RogueLibTerminal

A minimalistic library-terminal-emulator written in C# for creating rogue-like games.

## Description

The purpose of this project is to implement terminal emulator as close to the Windows' standard console and Linux's terminal as possible with new functionality and much better performance.

The perfect case is when a programmer can simply replace C#'s default static `Console` class with `Terminal` and see no differences:

```csharp
using Console = RogueLibTerminal.Terminal;

...

Console.WriteLine("Test"); // Still writes a line into the console
```

In addition to the replacements/analogs for standard Console's methods, there are a lot of new useful methods. For example, there is `WriteAt()` method which writes a string or a character to the specified buffer location.

**RogueLibTerminal** is available on Windows, Linux and MacOS. The cross-platformability is provided by SDL2# library which is a wrapper for `SDL2`, `SDL2_image`, `SDL2_ttf` and `SDL2_mixer` libraries.

## Library usage examples

Although this library is written primarily for creating rogue-like games, this can be used for creating games of any genre, the library provides all the necessary APIs for it, fully wrapping SDL2# library and hiding all the low-level stuff.

## Building

In order to build RogueLibTerminal you need to install .NET Standard 2.0 SDK or higher.

Demo project uses .NET Core 2.2.

## API

Terminal API can be found [here](TerminalAPI.md).
