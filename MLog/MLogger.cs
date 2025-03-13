namespace MLog
{
    public class MLogger
    {
        public string FilePath { get; set; }
        public string FileName { get; set; } = "MLog";
        private const string FileExtension = ".log";

        private string GetFileName() => $"{FilePath}\\{FileName}{FileExtension}";

        public void Log(string message) => Logger.Log(GetFileName(), message);

        public void Info(string message) => Logger.Log(GetFileName(), message, LogLevel.Info);
        
        public void Error(string message) => Logger.Log(GetFileName(), message, LogLevel.Error);
        
        public void Trace(string message) => Logger.Log(GetFileName(), message, LogLevel.Trace);
    }
}