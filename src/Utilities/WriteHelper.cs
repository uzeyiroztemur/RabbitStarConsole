using System;

namespace RabbitStarConsole.Utilities
{
    public class WriteHelper
    {
        public static void Write(ConsoleColor backgroundColor, string text)
        {
            Console.BackgroundColor = backgroundColor;
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine();
            Console.WriteLine(text);
            Console.ResetColor();
            Console.WriteLine();
        }
    }
}