using FileSystemCommands;
using System;
using System.IO;
using Xunit;

namespace task08tests
{
    public class FileSystemCommandsTests
    {
        [Fact]
        public void DirectorySizeCommand_ShouldCalculateSize()
        {
            var testDir = Path.Combine(Path.GetTempPath(), "TestDir1");
            Directory.CreateDirectory(testDir);
            File.WriteAllText(Path.Combine(testDir, "test1.txt"), "Hello");
            File.WriteAllText(Path.Combine(testDir, "test2.txt"), "World"); 
            var command = new DirectorySizeCommand(testDir);
            command.Execute();
            Assert.Equal(10, command.Size); 
            Directory.Delete(testDir, true);
        }

        [Fact]
        public void DirectorySizeCommand_NonExistentDirectory_ShouldReturnZero()
        {
            var nonExistentDir = Path.Combine(Path.GetTempPath(), "TestDir2");
            var command = new DirectorySizeCommand(nonExistentDir);
            command.Execute();
            Assert.Equal(0, command.Size); 
        }

        [Fact]
        public void FindFilesCommand_ShouldFindMatchingFiles()
        {
            var testDir = Path.Combine(Path.GetTempPath(), "TestDir3");
            Directory.CreateDirectory(testDir);
            File.WriteAllText(Path.Combine(testDir, "file1.txt"), "Text");
            File.WriteAllText(Path.Combine(testDir, "file2.log"), "Log");
            var command = new FindFilesCommand(testDir, "*.txt");
            command.Execute();
            Assert.Single(command.files); 
            Assert.Contains("file1.txt", command.files[0]); 
            Directory.Delete(testDir, true);
        }

        [Fact]
        public void FindFilesCommand_NonExistentDirectory_ShouldReturnEmptyArray()
        {
            var nonExistentDir = Path.Combine(Path.GetTempPath(), "TestDir4");
            var command = new FindFilesCommand(nonExistentDir, "*.txt");
            command.Execute();
            Assert.Empty(command.files); 
        }

        [Fact]
        public void FindFilesCommand_NoMatchingFiles_ShouldReturnEmptyArray()
        {
            var testDir = Path.Combine(Path.GetTempPath(), "TestDir5");
            Directory.CreateDirectory(testDir);
            File.WriteAllText(Path.Combine(testDir, "file1.log"), "Log"); 
            var command = new FindFilesCommand(testDir, "*.txt");
            command.Execute();
            Assert.Empty(command.files); 
            Directory.Delete(testDir, true);
        }
    }
}