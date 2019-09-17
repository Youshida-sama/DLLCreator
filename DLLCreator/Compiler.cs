using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Build.Evaluation;
using Microsoft.Build.Execution;
using Microsoft.Build.Logging;
using System.Threading;

namespace DLLCreator
{
    public static class Compiler
    {
        public static string filedir;
        public static string filenameOrig;
        public static string dll;
        public static string Log;
        public static void Initialize()
        {
            filenameOrig = AppDomain.CurrentDomain.BaseDirectory + "ForCompiling";
            filedir = AppDomain.CurrentDomain.BaseDirectory + "ForCompiling" + DateTime.Now.Day + DateTime.Now.Month;
            dll = "ForCompiling" + DateTime.Now.Day + DateTime.Now.Month;
        }
        public static void CreateDuplicate(string dllName = null)
        {
            
            if (dllName != null) 
            {
                filedir = AppDomain.CurrentDomain.BaseDirectory + dllName;
                dll = dllName;
            }


            Logger.WriteLine("FileWorker", "Writing in '" + filedir + "'", ConsoleColor.White);
            if (Directory.Exists(filedir))
            {
                Logger.WriteLine("FileWorker", "Folder is Exist.", ConsoleColor.White);
                Logger.WriteLine("FileWorker", "Deleting...", ConsoleColor.White);
                FileWorker.DeleteDirectory(filedir);
            }
            Logger.WriteLine("FileWorker", "Copying Folder...", ConsoleColor.White);
            FileWorker.DirectoryCopy(filenameOrig, filedir, true);
            Logger.WriteLine("FileWorker", "Successfully created copy.", ConsoleColor.White);

        }

        public static void ReplaceObjects()
        {
            Logger.WriteLine("ReplaceObjects", "Write TestFileName for new DLL.", ConsoleColor.White);
            string testFileName = Console.ReadLine();
            FileWorker.Files = new List<FileInfo>();
            FileWorker.LoadHierarchy(filedir);
            FileWorker.Files = FileWorker.Files.Where(w => !w.FullName.Contains(filedir + @"\bin\") && !w.FullName.Contains(filedir + @"\obj\")).ToList();
            foreach (FileInfo file in FileWorker.Files)
            {
                ContentWorker content = new ContentWorker(file);
                Dictionary<string, string> words = new Dictionary<string, string>();
                words.Add("%filename%", testFileName);
                words.Add("%dllname%", dll);
                words.Add("%25dllname%25", dll);
                content.Replace(words);
                content.SaveFile();
            }
        }
        public static BuildResult CompileProject()
        {
            Log = "";
            string projectFilePath = filedir + @"\ForCompiling.csproj";
            ProjectCollection pc = new ProjectCollection();
            Dictionary<string, string> globalProperty = new Dictionary<string, string>();
            globalProperty.Add("nodeReuse", "false");
            BuildParameters bp = new BuildParameters(pc);
            bp.Loggers = new List<Microsoft.Build.Framework.ILogger>()
            {
                new FileLogger() {Parameters = @"logfile=buildresult.txt"}
            };
            Logger.WriteLine("CompileProject", "ProjectFilePath: " + projectFilePath, ConsoleColor.White);
            BuildRequestData buildRequest = new BuildRequestData(projectFilePath, globalProperty, "4.0", new string[] { "Clean", "Build" }, null);
            BuildResult buildResult = BuildManager.DefaultBuildManager.Build(bp, buildRequest);
            BuildManager.DefaultBuildManager.Dispose();
            Logger.WriteLine("CompileProject", "Culture: " + bp.Culture, ConsoleColor.White);
            Logger.WriteLine("CompileProject", "BuildThreadPriority: " + bp.BuildThreadPriority, ConsoleColor.White);

            pc = null;
            bp = null;
            buildRequest = null;

            if (buildResult.OverallResult == BuildResultCode.Success)
            {
                Logger.WriteLine("CompileProject", "Builded successful.", ConsoleColor.Green);
            }
            else
            {
                Log = File.ReadAllText("buildresult.txt");
                Logger.WriteLine(Log, ConsoleColor.DarkRed);
                Logger.WriteLine("CompileProject", "Not builded, check log.", ConsoleColor.Red);
            }
            return buildResult;
        }
    }
}
