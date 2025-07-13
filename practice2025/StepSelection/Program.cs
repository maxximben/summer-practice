using System.Diagnostics;
using task14;

namespace StepSelection;

class Program
{
    static void Main(string[] args)
    {
        double[] steps = { 1e-1, 1e-2, 1e-3, 1e-4, 1e-5, 1e-6 };
        double rightAnswer = 0.5128123249929941;
        double a = 0;
        double b = 200;
        Console.WriteLine($"sin(x), [{a}, {b}]");
        Console.WriteLine("Правильный ответ: " + rightAnswer);
        
        for (int i = 0; i < steps.Length; i++)
        {
            double step = steps[i];
            
            double res = 0;
            Stopwatch stopwatch = new Stopwatch();
            Console.WriteLine();
            stopwatch.Start();
            res = DefiniteIntegral.Calculate(a, b, Math.Sin, step);
            stopwatch.Stop();
            Console.WriteLine("Шаг: " + step);
            Console.WriteLine("Время (tick): " + stopwatch.ElapsedTicks);
            Console.WriteLine("Результат: " + res);
            Console.WriteLine("Разница с правильным ответом: " + Math.Abs(rightAnswer - res).ToString("F20"));
            
            stopwatch.Reset();
        }
    }
}