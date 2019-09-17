using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLLCreator
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Title = "Compiller";
            Logger.WriteLine("Main", "Started execution.", ConsoleColor.White);
            Logger.WriteLine("Main", "Write Folder for new DLL.", ConsoleColor.White);
            string dllName = Console.ReadLine();
            Compiler.Initialize();
            Compiler.CreateDuplicate(dllName);
            Compiler.ReplaceObjects();
            Compiler.CompileProject();
            Logger.WriteLine("Main", "Testing new DLL...", ConsoleColor.White);
            Tester.RunMethod(Compiler.filedir + @"\bin\debug\" + dllName + ".dll", "Run");
            Logger.WriteLine("Main", "Completed execution. Press any button to exit...", ConsoleColor.White);
            Console.ReadKey();
        }
    }
}
