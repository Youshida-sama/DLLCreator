using System;
using System.Collections.Generic;
using System.IO;
using System.Security.AccessControl;

namespace DLLCreator
{
    public class ContentWorker
    {
        public FileInfo file;
        public string[] fileData;
        public List<object[]> FoundedRows;
        public bool HasChanges;
        public ContentWorker(FileInfo file)
        {
            this.file = file;
            HasChanges = false;
            File.SetAttributes(file.FullName, FileAttributes.Normal);
            fileData = File.ReadAllLines(file.FullName);
            FoundedRows = new List<object[]>();
        }
        public void Replace(Dictionary<string, string> keys) 
        {
            Logger.WriteLine("ContentWorker", "File: " + file.Name, ConsoleColor.Red);
            int rowId = 0;
            foreach (string dataRow in fileData)
            {
                bool isChanged = false;
                foreach (var value in keys)
                {
                    if (dataRow.Contains(value.Key))
                    {
                        FoundedRows.Add(new object[3]{rowId, value.Key, value.Value});
                        isChanged = true;
                    }
                }
                //if (isChanged)
                //    Logger.WriteLine(dataRow, ConsoleColor.DarkCyan);
                //else
                //    Logger.WriteLine(dataRow, ConsoleColor.White);
                rowId++;
            }
            Logger.WriteLine("ContentWorker", "Founded texts: " + FoundedRows.Count, ConsoleColor.Green);
            if (FoundedRows.Count != 0)
            {
                HasChanges = true;
                Logger.WriteLine("ContentWorker", "Replaced: ", ConsoleColor.Green);
                foreach (object[] row in FoundedRows) 
                {
                    string newData = fileData[(int)row[0]].Replace((string)row[1], (string)row[2]);
                    Logger.Write("(ContentWorker)" + (int)row[0] + ": '", ConsoleColor.Green);
                    Logger.Write(fileData[(int)row[0]].Replace("  ", ""), ConsoleColor.Yellow);
                    Logger.Write("' to '", ConsoleColor.Green);
                    Logger.Write(newData.Replace("  ", ""), ConsoleColor.Yellow);
                    Logger.WriteLine("'", ConsoleColor.Green);
                    fileData[(int)row[0]] = newData;
                    
                }
                Console.WriteLine();

            }
        }
        public void SaveFile()
        {
            File.WriteAllLines(file.FullName, fileData);
        }
    }
}
