using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Reflection;

namespace task11;


public interface ICalculator
{
    int Add(int a, int b);
    int Minus(int a, int b);
    int Mul(int a, int b);
    int Div(int a, int b);
}

public class CalculatorGenerator
{
    public static ICalculator CreateCalc(string code)
    {
        var references = new MetadataReference[]
        {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(ICalculator).Assembly.Location),
        };

        var compile = CSharpCompilation.Create("Calc", new[] { CSharpSyntaxTree.ParseText(code) }, references,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        var memoryStream = new MemoryStream();
        compile.Emit(memoryStream);
        memoryStream.Seek(0, SeekOrigin.Begin);
        var assembly = Assembly.Load(memoryStream.ToArray());
        
        var calculatorType = assembly.GetType("Calculator");
        if (calculatorType is null)
        {
            throw new ArgumentNullException();
        }
        
        var calc = Activator.CreateInstance(calculatorType);
        if (calc is null)
        {
            throw new ArgumentNullException();
        }
        
        return (ICalculator) calc;
    }
}
