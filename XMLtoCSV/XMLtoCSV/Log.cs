using System;

namespace XMLtoCSV
{
    public class Log
    {
        public static void WriteLineColour(string message, ConsoleColor colour)
        {
            Console.ForegroundColor = colour;
            Console.WriteLine(message);

            Console.ForegroundColor = ConsoleColor.Black;
        }
    }
}
