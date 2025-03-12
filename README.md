# MLog

MLog is a very simple class library that writes text to files built for .NET Standard 2.0. I initially wrote this to use in my other project Freeroam:V which is a mod for FiveM (part of the Cfx Platform) as I wanted to seperate the code out. In theory it could be used for any use case where you just want to log to files. It uses a Fluent API to be built.

var mLog = new MLogBuilder().WithFilePath(filePath).WithFileName("CustomMLogFileName").Build();

If no file name is specified all the log files will be called "MLog"

I hope to continue working on it as time goes on!
