namespace task14;

public class DefiniteIntegral
{
    public static double Calculate(double a, double b, Func<double, double> function, double step)
    {
        if (b < a)
        {
            return -Calculate(b, a, function, step);
        }

        double result = 0;
        double current = a;
        while (current < b)
        {
            double next = Math.Min(current + step, b);
            result += (function(current) + function(next)) / 2 * (next - current);
            current = next;
        }
        return result;
    }

    public static double Solve(double a, double b, Func<double, double> function, double step, int threadsNumber)
    {
        double sum = 0;
        Thread[] threads = new Thread[threadsNumber];
        Barrier barrier = new Barrier(threadsNumber + 1);
        object locker = new object();

        double intervalLength = (b - a) / threadsNumber;


        for (int i = 0; i < threadsNumber; i++)
        {
            double start = a + i * intervalLength;
            double end = a + (i + 1) * intervalLength;
            threads[i] = new Thread(() =>
            {
                double localSum = Calculate(start, end, function, step);
                
                lock (locker)
                {
                    sum += localSum;
                }

                barrier.SignalAndWait();
            });
            threads[i].Start();
        }

        barrier.SignalAndWait();
        barrier.Dispose();
        
        return sum;
    }
}