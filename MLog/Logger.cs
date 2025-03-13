using System;
using System.IO;

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
            var fileLength = fileInfo.Length;
            
            if (fileLength == 0)
                message = message.Replace(Environment.NewLine, string.Empty);

            if (creationTimeUtc.Day == DateTime.UtcNow.Day)
            {
                File.AppendAllText(path, message);
                return;
            }
            
            var oldPathName = fileInfo.FullName.Replace(".log", string.Empty);
            var newPath = $"{oldPathName}{creationTimeUtc:yyyyMMdd}.log";
            File.Move(path, newPath);
            File.Create(path).Close();
            File.SetCreationTimeUtc(path, DateTime.UtcNow);
            File.AppendAllText(path, message);
        }
    }
}