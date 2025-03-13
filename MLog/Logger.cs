using System;
using System.IO;
using System.Linq;

namespace MLog
{
    public static class Logger
    {
        private static string BuildString(string text, LogLevel? level)
        {
            return level == null
                ? $"{Environment.NewLine}[MLOG][{LogLevel.Trace.ToString().ToUpper()}][{DateTime.Now}]: {text}"
                : $"{Environment.NewLine}[MLOG][{level.ToString().ToUpper()}][{DateTime.Now}]: {text}";
        }
        
        public static void Log(string path, string message, LogLevel? level = null)
        {
            var textToWrite = level == null ? BuildString(message, null) : BuildString(message, level);
            WriteToLogFile(path, textToWrite);
        }
        
        private static bool LogFileExists(string path) => File.Exists(path);

        private static void GenerateLogFile(string path) => File.Create(path).Close();
        
        private static FileInfo GetLogFileInfo(string path) => new FileInfo(path);
        
        private static void WriteToFile(string path, string text) => File.AppendAllText(path, text);

        private static void WriteToLogFile(string path, string text)
        {
            var fileExists = LogFileExists(path);

            if (!fileExists)
            {
                GenerateLogFile(path);
                fileExists = true;
            }

            var fileInfo = GetLogFileInfo(path);

            if (fileInfo.CreationTimeUtc.ToString("yyyy-MM-dd") == DateTime.UtcNow.ToString("yyyy-MM-dd"))
            {
                WriteToFile(path, text);
                return;
            }

            RenameLogFile(path);
            WriteToFile(path, text);
        }

        private static void RenameLogFile(string path)
        {
            var fileInfo = GetLogFileInfo(path);
            var oldPathName = fileInfo.FullName.Replace(".log", string.Empty);
            var newPath = $"{oldPathName}{fileInfo.CreationTimeUtc:yyyyMMdd}.log";
            File.Move(path, newPath);
            File.Create(path).Close();
            File.SetCreationTimeUtc(path, DateTime.UtcNow);
        }
    }
}