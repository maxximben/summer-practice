using System.Collections.Concurrent;

namespace task17;

public interface ICommand
{
    void Execute();
}

public class ServerThread
{
    public Thread thread { get; }
    private bool isRunning;
    public bool GetIsRunning() => isRunning;
    private BlockingCollection<ICommand> commands = new BlockingCollection<ICommand>();
    private bool softStop;
    public bool GetSoftStop() => softStop; 

    public ServerThread()
    {
        thread = new Thread(Run);
    }

    public void HardStop() => isRunning = false;
    public void SoftStop() => softStop = true;
    

    private void Run()
    {
        while (isRunning && !softStop)
        {
            ICommand command = commands.Take();
            try
            {
                command.Execute();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        isRunning = false;
    }

    public void Start()
    {
        if (isRunning)
        {
            return;
        }

        isRunning = true;
        thread.Start();
    }

    public void AddCommand(ICommand command)
    {
        if (!isRunning)
        {
            ExceptionHandler.HandleException(command, new Exception("Поток не запущен"));
        }
        
        commands.Add(command);
    }
}

public class HardStop : ICommand
{
    private ServerThread serverThread;

    public HardStop(ServerThread serverThread)
    {
        this.serverThread = serverThread;
    }
    
    public void Execute()
    {
        if (Thread.CurrentThread != serverThread.thread)
        {
            ExceptionHandler.HandleException(this, new Exception("HardStop успешно выполняется только в потоке, который она должна остановить"));
        }
        
        serverThread.HardStop();
    }
}

public class SoftStop : ICommand
{
    private ServerThread serverThread;

    public SoftStop(ServerThread serverThread)
    {
        this.serverThread = serverThread;
    }
    
    public void Execute()
    {
        if (Thread.CurrentThread != serverThread.thread)
        {
            ExceptionHandler.HandleException(this, new Exception("SoftStop успешно выполняется только в потоке, который она должна остановить"));
        }
        
        serverThread.SoftStop();
    }
}

public class ExceptionHandler
{
    public static void HandleException(ICommand command, Exception exception)
    {
        Console.WriteLine(command.GetType());
        Console.WriteLine(exception.Message);
    }
}
