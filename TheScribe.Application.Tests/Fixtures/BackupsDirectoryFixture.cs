using System;
using System.IO;
using System.IO.Enumeration;

namespace TheScribe.Application.Tests.Fixtures;

public class BackupsDirectoryFixture : IDisposable
{
    public string UniqueDirForTest { get; }

    public BackupsDirectoryFixture()
    {
        string tempDir = Path.GetTempPath();
        string testsDir = Path.Combine(tempDir, "TheScribe/UnitTests/");
        UniqueDirForTest = Path.Combine(testsDir, Guid.NewGuid().ToString());
        
        Directory.CreateDirectory(UniqueDirForTest);
        PopulateTestData();
    }

    private void PopulateTestData()
    {
        string eldenRingFolder = Path.Combine(UniqueDirForTest, "Elden Ring");
        Directory.CreateDirectory(eldenRingFolder);
        // TODO: Add mock data here
    }

    public void Dispose()
    {
        Directory.Delete(UniqueDirForTest, true);
    }
}