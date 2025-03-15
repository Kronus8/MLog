# MLog

MLog is an extremely simple class library that writes text to files and the standard console built for .NET Standard 2.0. I initially wrote this to use in another project which required .NET Standard 2.0. I wanted to seperate the code out. In theory it could be used for any use case where you just want to log to files and possibly the console at the same time. It uses a Fluent API to be built.

```c#
var mLog = new MLogBuilder().WithFilePath(filePath).WithFileName("CustomMLogFileName").Build();
```

Alternatively if you want to write out to debug or console: 

```c#
var mLog = new MLogBuilder().WithFilePath(filePath).WithFileName("CustomMLogFileName").ToDebug().ToConsole().Build();
```

If no file name is specified all the log files will be called "MLog".

I hope to continue working on it as time goes on!
