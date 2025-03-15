namespace MLog
{
    public class MLogger
    {
        public string FilePath { get; set; }
        public string FileName { get; set; } = "MLog";
        public bool WriteToDebug { get; set; } = false;
        public bool WriteToConsole { get; set; } = false;
        private const string FileExtension = ".log";
        
        private readonly Logger _loggerInstance;

        public MLogger()
        {
            _loggerInstance = new Logger(this);
        }

        private string GetFileName() => $"{FilePath}\\{FileName}{FileExtension}";

        public void Log(string message) => _loggerInstance.Log(GetFileName(), message);

        public void Info(string message) => _loggerInstance.Log(GetFileName(), message, LogLevel.Info);
        
        public void Error(string message) => _loggerInstance.Log(GetFileName(), message, LogLevel.Error);
        
        public void Trace(string message) => _loggerInstance.Log(GetFileName(), message, LogLevel.Trace);
    }
}