using System;
using System.IO;
using MLog;

namespace MLogTestConsoleApp;

class Program
{
    static void Main(string[] args)
    {
        var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        var subFolderPath = Path.Combine(path, "MLogTestFolder");
        var mLog = new MLogBuilder().WithFilePath(subFolderPath).WithFileName("CustomMLogFileName").Build();
        
        mLog.Log("Hello World!");
        mLog.Info("Hello World!");
        mLog.Error("Hello World!");
        mLog.Trace("Hello World!");
    }
}