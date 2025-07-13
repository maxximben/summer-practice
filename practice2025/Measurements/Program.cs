using System.Diagnostics;
using task14;

namespace Measurements;

class Program
{
    static void Main(string[] args)
    {
        double[] steps = { 1e-1, 1e-2, 1e-3, 1e-4, 1e-5, 1e-6 };
        
        for (int j = 0; j < steps.Length; j++)
        {
            Stopwatch stopwatch = new Stopwatch();
                 
            double a = -100;
            double b = 100;
            double step = steps[j];
        
            int[] threads = {1, 2, 4, 6, 8, 10, 12, 14, 16};
            double[] xs = new double[threads.Length];
            int numTrials = 5;
        
            for (int i = 0; i < threads.Length; i++)
            {
                double totalTicks = 0;
                for (int trial = 0; trial < numTrials; trial++)
                {
                    if (threads[i] == 1)
                    {
                        stopwatch.Start();
                        DefiniteIntegral.Calculate(a, b, Math.Sin, step);
                        stopwatch.Stop();
                    }
                    else
                    {
                        stopwatch.Start();
                        DefiniteIntegral.Solve(a, b, Math.Sin, step, threads[i]);
                        stopwatch.Stop();
                    }
                    
                    totalTicks += stopwatch.ElapsedTicks;
                    stopwatch.Reset();
                }
                xs[i] = totalTicks / numTrials; 
            }
        
            string title = $"Шаг {step}";
        
            var plt = new ScottPlot.Plot();
            plt.Title($"Шаг {step}");
            plt.XLabel("Время вычисления функции Solve (tick)");
            plt.YLabel("Количество потоков");
            plt.Add.Scatter(xs, threads);
            plt.SavePng($"C:/Users/Maksim/Desktop/{title}.png", 800, 600);
        }
    }
}