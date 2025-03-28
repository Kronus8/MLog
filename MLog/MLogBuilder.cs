using System.IO;

namespace MLog
{
    public class MLogBuilder
    {
        private readonly MLogger _mLogger = new MLogger();

        public MLogBuilder WithFilePath(string filePath)
        {
            _mLogger.FilePath = filePath;
            return this;
        }

        public MLogBuilder WithFileName(string fileName)
        {
            _mLogger.FileName = fileName;
            return this;
        }

        public MLogBuilder ToDebug()
        {
            _mLogger.WriteToDebug = true;
            return this;
        }
        
        public MLogBuilder ToConsole()
        {
            _mLogger.WriteToConsole = true;
            return this;
        }

        public MLogger Build()
        {
            Directory.CreateDirectory(_mLogger.FilePath);
            return _mLogger;
        }
    }
}