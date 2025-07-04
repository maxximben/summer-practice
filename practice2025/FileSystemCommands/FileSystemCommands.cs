using CommandLib;
using System.IO;

namespace FileSystemCommands;

public class DirectorySizeCommand : ICommand 
{
    private string DirectoryName;
    public long Size;

    public DirectorySizeCommand(string name)
    {
        DirectoryName = name;
    }
    static long GetDirectorySize(string path)
    {
        if (!Directory.Exists(path))
        {
            return 0;
        }
           
        long size = 0;

        string[] files = Directory.GetFiles(path);
        foreach (string file in files)
        {
            FileInfo info = new FileInfo(file);
            size += info.Length;   
        }

        string[] subDirs = Directory.GetDirectories(path);
        foreach (string dir in subDirs)
        {
            size += GetDirectorySize(dir);
        }

        return size;
    }

    public void Execute() 
    {
        Size = GetDirectorySize(DirectoryName);
    }
}

public class FindFilesCommand : ICommand
{
    private string DirectoryName;
    private string Mask;

    public string[] files = [];

    public FindFilesCommand(string DirectoryName, string Mask)
    {
        this.DirectoryName = DirectoryName;
        this.Mask = Mask;
    }

    public void Execute()
    {
        if (!Directory.Exists(DirectoryName))
        {
            return;
        }

        files = Directory.GetFiles(DirectoryName, Mask);
    }
}

