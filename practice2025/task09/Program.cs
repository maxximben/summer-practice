using System.Reflection;
using task07;

namespace task09;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine(args[0]);
    
        string path = args[0];

        Assembly assembly = Assembly.LoadFrom(path);

        Type[] types = assembly.GetTypes();

        Console.WriteLine("Список классов:\n");

        foreach (Type type in types) 
        {
            Console.WriteLine("-> " + type.GetCustomAttribute<DisplayNameAttribute>().DisplayName);

            var methods = type.GetMethods();
            var constructors = type.GetConstructors();
            var attributes = type.GetCustomAttributes();

            if (methods.Any()) 
            {
                Console.WriteLine("   Список методов:");
                foreach (var method in methods) 
                {
                    Console.WriteLine("   -> " + method.Name);
                    var parameters = method.GetParameters();

                    if (parameters.Any())
                    {
                        Console.Write("      Список парематров: ");
                        foreach (var parameter in parameters)
                        {
                            Console.Write(parameter.ToString()+ "; ");
                        }
                        Console.WriteLine();
                    } 
                         
                }
            } else {
                Console.WriteLine("У класса нет методов");
            }
        
            if (constructors.Any())
            {
                Console.WriteLine("   Список конструкторов");
                foreach(var constructor in constructors)
                {
                    Console.WriteLine("   -> " + constructor.Name);

                    var parsmeters = constructor.GetParameters();

                    if (parsmeters.Any())
                    {
                        Console.Write("      Список парематров: ");
                        foreach (var parameter in parsmeters)
                        {
                            Console.WriteLine(parameter.ToString() + "; ");
                        }
                    } 
                }
            } 

            if (attributes.Any())
            {
                Console.WriteLine("   Список атрибутов");
                foreach (var attribute in attributes)
                {
                    Console.WriteLine("   " + attribute.ToString());
                }
            }
        }
    }
}
