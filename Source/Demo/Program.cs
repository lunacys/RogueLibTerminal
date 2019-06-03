using System;
using RogueLibTerminal;

//using Console = RogueLibTerminal.Terminal;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            using (TerminalWindow tw = new TerminalWindow())
            {
	            tw.KeyDown += (sender, handler) =>
	            {
		            Console.WriteLine($"Pressed button: {handler.Key}, Mods: {handler.Modifiers}");
	            };
                tw.Init();
            }

            //Console.Destroy();
        }
    }
}
