using System;
using System.IO;

namespace MLog
{
    public static class Log
    {
        // TODO: Remove line break when the log file is empty.
        private static string BuildString(string text, LogLevel level)
        {
            var builtString = $"{Environment.NewLine}[MLOG][{level.ToString().ToUpper()}][{DateTime.Now.ToString()}]: {text}";
            return builtString;
        }
        
        public static void Info(string path, string message)
        {
            var textToWrite = BuildString(message, LogLevel.Info);
            WriteToFile(path, textToWrite);
        }
        
        public static void Error(string path, string message)
        {
            var textToWrite = BuildString(message, LogLevel.Error);
            WriteToFile(path, textToWrite);
        }
        
        public static void Trace(string path, string message)
        {
            var textToWrite = BuildString(message, LogLevel.Trace);
            WriteToFile(path, textToWrite);
        }

        private static void WriteToFile(string path, string message)
        {
            var fileExists = File.Exists(path);

            if (!fileExists)
            {
                File.Create(path).Close();
                fileExists = true;
            }
            
            var fileInfo = new FileInfo(path);
            var creationTimeUtc = fileInfo.CreationTimeUtc;

            if (creationTimeUtc.Day == DateTime.UtcNow.Day)
            {
                File.AppendAllText(path, message);
                return;
            }
            
            var oldPathName = fileInfo.FullName.Replace(".txt", string.Empty);
            var newPath = $"{oldPathName}{creationTimeUtc:yyyyMMdd}.log";
            File.Move(path, newPath);
            File.AppendAllText(path, message);
        }
    }
}