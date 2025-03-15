namespace MLogUnitTests;
using MLog;

public class Tests
{
    private MLogger _logger;
    private const string FolderName = "MLogUnitTestFolder";
    private const string FileName = "MLogUnitTest";
    private string _fullPath = "";
    
    [SetUp]
    public void Setup()
    {
        _fullPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), FolderName);
        _logger = new MLogBuilder().WithFilePath(_fullPath).WithFileName(FileName).ToDebug().Build();
    }

    [TearDown]
    public void TearDown()
    {
        Directory.Delete(_fullPath, true);
    }

    [Test]
    public void FolderAndLogFileGenerates()
    {
        _logger.Log("Folder and Log File is generated!");
        var filePath = Path.Combine(_fullPath, FileName) + ".log";
        
        if (!File.Exists(filePath))
        {
            Assert.Fail("Folder and File does not exist.");
        }
        
        Assert.Pass();
    }

    [Test]
    public void OldFileIsRenamedAndNewFileIsGenerated()
    {
        var file = Path.Combine(_fullPath, FileName) + ".log";
        var dateToSet = new DateTime(2000, 01, 01);
        File.Create(file).Close();
        File.SetCreationTimeUtc(file, dateToSet);
        File.AppendAllText(file, "Test");
        
        if (!File.Exists(file))
        {
            Assert.Fail("File does not exist.");
        }
    
        _logger.Log("Old file should be renamed and this file will be the new one.");
        var oldFile = Path.Combine(_fullPath, FileName) + dateToSet.ToString("yyyyMMdd") + ".log";

        if (!File.Exists(oldFile))
        {
            Assert.Fail("File does not exist.");
        }
        
        Assert.Pass();
    }
}