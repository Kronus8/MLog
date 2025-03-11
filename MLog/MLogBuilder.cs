using System.IO;

namespace MLog
{
    public class MLogBuilder
    {
        private MLog _mLog = new MLog();

        public MLogBuilder WithFilePath(string filePath)
        {
            _mLog.FilePath = filePath;
            return this;
        }

        public MLogBuilder WithFileName(string fileName)
        {
            _mLog.FileName = fileName;
            return this;
        }

        public MLog Build()
        {
            Directory.CreateDirectory(_mLog.FilePath);
            return _mLog;
        }
    }
}