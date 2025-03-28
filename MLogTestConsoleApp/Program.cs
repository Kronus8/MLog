﻿using System;
using System.IO;
using MLog;

namespace MLogTestConsoleApp;

class Program
{
    static void Main(string[] args)
    {
        var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        var subFolderPath = Path.Combine(path, "MLogTestFolder");
        var mLog = new MLogBuilder().WithFilePath(subFolderPath).WithFileName("CustomMLogFileName").ToDebug().ToConsole().Build();
        
        mLog.Log("Hello World!");
        mLog.Info("Hello World!");
        mLog.Error("Hello World!");
        mLog.Trace("Hello World!");
        
        // File.Create(subFolderPath + "\\CustomMLogFileName.log").Close();
        // File.SetCreationTimeUtc(subFolderPath + "\\CustomMLogFileName.log", new DateTime(2025, 02, 13));
        // File.AppendAllText(subFolderPath + "\\CustomMLogFileName.log", "Test");
    }
}