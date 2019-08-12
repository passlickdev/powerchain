using System;

namespace powerchain
{
    class Util
    {

        /// <summary>
        /// Writes a string with a custom color to the console
        /// </summary>
        /// <param name="text">Text to be written</param>
        /// <param name="color">Foreground color of console</param>
        public static void consoleWrite(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }

    }
}

// (c) Passlick Development 2019. All rights reserved.