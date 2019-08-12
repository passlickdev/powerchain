using System;

namespace powerchain
{
    class Util
    {
        public static void consoleWrite(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }

    }
}
