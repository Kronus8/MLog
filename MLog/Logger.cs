using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace MLog
{
    public class Logger
    {
        private static MLogger _mLoggerInstance;
        
        private static string BuildString(string text, LogLevel? level)
        {
            return level == null
                ? $"{Environment.NewLine}[MLOG][{LogLevel.Trace.ToString().ToUpper()}][{DateTime.Now}]: {text}"
                : $"{Environment.NewLine}[MLOG][{level.ToString().ToUpper()}][{DateTime.Now}]: {text}";
        }

        public Logger(MLogger loggerInstance)
        {
            _mLoggerInstance = loggerInstance ?? throw new ArgumentNullException(nameof(loggerInstance));
        }
        
        public void Log(string path, string message, LogLevel? level = null)
        {
            var textToWrite = level == null ? BuildString(message, null) : BuildString(message, level);
            WriteToLogFile(path, textToWrite);

            if (_mLoggerInstance.WriteToDebug)
            {
                Debug.WriteLine(textToWrite);
            }

            if (_mLoggerInstance.WriteToConsole)
            {
                Console.WriteLine(textToWrite);
            }
        }
        
        private static bool LogFileExists(string path) => File.Exists(path);

        private static void GenerateLogFile(string path) => File.Create(path).Close();
        
        private static FileInfo GetLogFileInfo(string path) => new FileInfo(path);
        
        private static void WriteToFile(string path, string text) => File.AppendAllText(path, text);

        private void WriteToLogFile(string path, string text)
        {
            var fileExists = LogFileExists(path);

            if (!fileExists)
            {
                GenerateLogFile(path);
                fileExists = true;
            }

            var fileInfo = GetLogFileInfo(path);
            
            if (fileInfo.Length == 0)
            {
                text = text.Replace(Environment.NewLine, string.Empty);
            }

            if (fileInfo.CreationTimeUtc.ToString("yyyy-MM-dd") == DateTime.UtcNow.ToString("yyyy-MM-dd"))
            {
                WriteToFile(path, text);
                return;
            }

            RenameLogFile(path);
            text = text.Replace(Environment.NewLine, string.Empty);
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