using System.Reflection;
using CommandLib;

namespace CommandRunner;

class CommandRunner
{
    static void Main(string[] args)
    {
        string dllPath = @"C:\Users\Maksim\Desktop\summer-practice\practice2025\FileSystemCommands\bin\Debug\net9.0\FileSystemCommands.dll"; 
        Assembly assembly = Assembly.LoadFrom(dllPath);

        Type[] types = assembly.GetTypes();
        foreach (Type type in types)
        {
            if (typeof(ICommand).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
            {
                if (type.Name == "DirectorySizeCommand")
                {
                    string directoryPath = @"C:\Users\Maksim\Desktop\summer-practice\";
                    object instance = Activator.CreateInstance(type, directoryPath);

                    MethodInfo executeMethod = type.GetMethod("Execute");
                    executeMethod.Invoke(instance, null);

                    FieldInfo sizeField = type.GetField("Size");
                    long size = (long)sizeField.GetValue(instance);
                    Console.WriteLine($"Размер директории {directoryPath}: {size} байт");
                }

                if (type.Name == "FindFilesCommand")
                {
                    string directoryPath = @"C:\Users\Maksim\Desktop\"; 
                    string mask = "*.txt"; 
                    object instance = Activator.CreateInstance(type, directoryPath, mask);

                    MethodInfo executeMethod = type.GetMethod("Execute");
                    executeMethod.Invoke(instance, null);

                    FieldInfo filesField = type.GetField("files");
                    string[] files = (string[])filesField.GetValue(instance);
                    Console.WriteLine($"Найдено файлов по маске {mask}:");
                    foreach (string file in files)
                    {
                        Console.WriteLine(file);
                    }
                }
            }
        }
    }
}
