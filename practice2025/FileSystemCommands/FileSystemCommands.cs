using CommandLib;

namespace FileSystemCommands;

public class DirectorySizeCommand : ICommand 
{
    private string DirectoryName;
    public long Size;

    public DirectorySizeCommand(string name)
    {
        DirectoryName = name;
    }

    public void Execute() 
    {
        if (!Directory.Exists(DirectoryName)) 
        { 
            Size = 0;
            return;
        }

        foreach (var file in Directory.GetFiles(DirectoryName)) 
        { 
            Size += file.Length;
        }
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

