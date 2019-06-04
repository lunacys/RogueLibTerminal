# Terminal API

Here you can find all the methods, properties and event Terminal has and standard Console class of C# analog.

## Methods

| Status | `Terminal` Method | `Console` Analog | Description |
| --- | --- | --- | --- |
| Planned | `void Beep()` | `void Beep()` | Plays the sound of a beep through the console speaker |
| In progress | `void Clear()` | `void Clear()` | Clears the console buffer |
| In progress | `void MoveBufferArea(int srcLeft, int srcTop, int srcWidth, int srcHeight, dstLeft, dstTop)` | `void MoveBufferArea(int srcLeft, int srcTop, int srcWidth, int srcHeight, dstLeft, dstTop)` | Copies a specified source area of the screen buffer to a specified destination area |
| Planned | `System.IO.Stream OpenStandardError()` \ `System.IO.Stream OpenStandardError(int bufferSize)`  | `System.IO.Stream OpenStandardError()` \ `System.IO.Stream OpenStandardError(int bufferSize)` | 1. Acquires the standard error stream \ 2. Acquires the standard error stream, which is set to a specified buffer size |
| Planned | `System.IO.Stream OpenStandardInput();` \ `System.IO.Stream OpenStandardInput(int bufferSize);` | `System.IO.Stream OpenStandardInput();` \ `System.IO.Stream OpenStandardInput(int bufferSize);` | Acquires the standard input stream |
| Planned | `System.IO.Stream OpenStandardOutput();` \ `System.IO.Stream OpenStandardOutput(int bufferSize);` | `System.IO.Stream OpenStandardOutput();` \ `System.IO.Stream OpenStandardOutput(int bufferSize);` | Acquires the standard output stream |
| In progress | `int Read()` | `int Read()` | Reads the next character from the standard input stream |
| In progress | `Keys ReadKey()` | `ConsoleKeyInfo ReadKey()` | Obtains the next character or function key pressed by the user. `Keys` and `ConsoleKeyInfo` enums are interchangeable |
| In progress | `string ReadLine()` | `string ReadLine()` | Reads the next line of characters from the standard input stream. Line ends with new line symbol (`\n` or `\r\n`) |
| In progress | `void ResetColor()` | `void ResetColor()` | Sets the foreground and background console colors to their defaults |
| In progress | `void SetBufferSize(int width, int height)` | `void SetBufferSize(int width, int height)` | Sets the height and width of the screen buffer area to the specified values |
| In progress | `void SetCursorPosition(int left, int top)` \ `void SetCursorPosition(Point leftTop)` | `void SetCursorPosition(int left, int top)` | Sets the position of the cursor |
| Planned | `void SetError(System.IO.TextWriter newError)` | `void SetError(System.IO.TextWriter newError)` | Sets the Error property to the specified TextWriter object |
| Planned | `void SetIn(System.IO.TextReader newIn)` | `void SetIn(System.IO.TextReader newIn)` | Sets the In property to the specified TextReader object |
| Planned | `void SetOut(System.IO.TextWriter newOut)` | `void SetOut(System.IO.TextWriter newOut)` | Sets the Out property to the specified TextWriter object. |
| In progress | `void SetWindowPosition(int left, int top)` \ `void SetWindowPosition(Point position)`| `void SetWindowPosition(int left, int top)` | Sets the position of the console window relative to the screen buffer |
| In progress | `void SetWindowSize(int width, int height)` \ `void SetWindowSize(Point size)` | `void SetWindowSize(int width, int height)` | Sets the height and width of the console window to the specified values |
