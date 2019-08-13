using System;
using System.Linq;

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


        /// <summary>
        /// Generates a random string with length 'length'
        /// </summary>
        /// <param name="length">Length of random string</param>
        /// <returns></returns>
        public static string randomString(int length = 16)
        {

            Random random = new Random();

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());

        }

    }
}

// (c) Passlick Development 2019. All rights reserved.