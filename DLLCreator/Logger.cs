using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLLCreator
{
    public static class Logger
    {
        public static void WriteLine(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void WriteLine(string parent, string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write("(");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(parent);
            Console.ForegroundColor = color;
            Console.WriteLine(")" + text);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void Write(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = ConsoleColor.White;
        }

    }
}
