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
                tw.Init();
            }

            //Console.Destroy();
        }
    }
}
