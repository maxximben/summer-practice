using System.Reflection;

namespace task07;


[DisplayName("Пример класса")]
[Version(1, 0)]
public class SampleClass
{
    [DisplayName("Числовое свойство")]
    public int Number { get; set; }
    [DisplayName("Тестовый метод")]
    public void TestMethod() {}
}

public class SampleClass2
{
    public int Number { get; set; }
    public void TestMethod() {}
}

public class DisplayNameAttribute : Attribute
{
    public string DisplayName { get; set; }

    public DisplayNameAttribute(string name)
    {
        DisplayName = name;
    }
}

public class VersionAttribute : Attribute
{
    public int Major { get; set; }
    public int Minor { get; set; }

    public VersionAttribute(int major, int minor)
    {
        Major = major;
        Minor = minor;
    }
}

public static class ReflectionHelper
{
    public static void PrintTypeInfo(Type type)
    {
        var displayName = type.GetCustomAttribute<DisplayNameAttribute>();
        var version = type.GetCustomAttribute<VersionAttribute>();
        var methods = from method in type.GetMethods() where method.GetCustomAttribute<DisplayNameAttribute>() != null select method;
        var properties = from property in type.GetProperties() where property.GetCustomAttribute<DisplayNameAttribute>() != null select property;

        foreach (var method in methods)
        {
            Console.WriteLine(method);
        }

        foreach (var property in properties)
        {
            Console.WriteLine(property);
        }
    }
}