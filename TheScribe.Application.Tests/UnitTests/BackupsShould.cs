using System;
using FluentAssertions;
using TheScribe.Application.Tests.Fixtures;
using Xunit;

namespace TheScribe.Application.Tests.UnitTests;

public class BackupsShould : IClassFixture<BackupsDirectoryFixture>, IClassFixture<LoggingFixture>
{
    private BackupsDirectoryFixture _backupsDirectoryFixture;
    
    public BackupsShould(BackupsDirectoryFixture backupsDirectoryFixture)
    {
        _backupsDirectoryFixture = backupsDirectoryFixture;
    }
    
    [Fact]
    public void GetBackupListForGame_ReturnValidResults()
    {
        Console.WriteLine(_backupsDirectoryFixture.UniqueDirForTest);
        // TODO: Actually write a unit test.
        true.Should().BeTrue();
    }
}