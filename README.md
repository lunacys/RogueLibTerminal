# RogueLibTerminal

A minimalistic library-terminal-emulator written in C# for creating rogue-like games.

The purpose of this project is to implement terminal emulator as close to the Windows' standard console and Linux's terminal as possible with new functionality and much better performance.

A perfect case is when a programmer can simply replace C#'s default static `Console` class with `Terminal` and see no difference:

```csharp
using Console = RogueLibTerminal.Terminal;

...

Console.WriteLine("Test"); // Still writes a line into the console
```

**RogueLibTerminal** is available on Windows, Linux and MacOS.

**RogueLibTerminal** uses SDL2# library.
