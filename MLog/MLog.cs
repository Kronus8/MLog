namespace MLog
{
    public class MLog
    {
        public string FilePath { get; set; }
        public string FileName { get; set; } = "MLog";
        private const string FileExtension = ".log";

        private string GetFileName()
        {
            return $"{FilePath}\\{FileName}{FileExtension}";
        }

        public string GetFileExtension()
        {
            return FileExtension;
        }

        public void Info(string message)
        {
            Log.Info(GetFileName(), message);
        }
        
        public void Error(string message)
        {
            Log.Error(GetFileName(), message);
        }
        
        public void Trace(string message)
        {
            Log.Trace(GetFileName(), message);
        }
    }
}