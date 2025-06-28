using System.Reflection;

namespace task05;

public class ClassAnalyzer
{
    private Type _type;
    
    public ClassAnalyzer(Type type)
    {
        _type = type;
    }

    public IEnumerable<string> GetPublicMethods()
    {
        var methods = from method in _type.GetMethods() where method.IsPublic select method.Name;
        return methods;
    }

    public IEnumerable<string> GetMethodParams(string methodname)
    {
        var parameters = from parameter in _type.GetMethod(methodname).GetParameters() select parameter.Name; 
        return parameters.ToList();
    }

    public IEnumerable<string> GetAllFields()
    {
        var fields = from field in _type.GetRuntimeFields() select field.Name;
        return fields;
    }
    
    public IEnumerable<string> GetProperties()
    {
        var properties = from p in _type.GetProperties() select p.Name;
        return properties;
    }

    public bool HasAttribute<T>() where T : Attribute
    {
        return _type.GetCustomAttributes(typeof(T), true).Any();
    }
}
    

