using task17;

namespace task17tests;


public class ServerThreadTests
{
    private ServerThread serverThread;

    public ServerThreadTests()
    {
        serverThread = new ServerThread();
    }

    [Fact]
    public void HardStop_ExecutedInThread_StopsThread()
    {
        serverThread.Start();
        Assert.True(serverThread.GetIsRunning(), "Поток должен быть запущен");

        ICommand hardStop = new HardStop(serverThread);
        serverThread.AddCommand(hardStop);

        Thread.Sleep(100);
        Assert.False(serverThread.GetIsRunning(), "Поток должен быть остановлен после HardStop");
    }
    
    [Fact]
    public void SoftStop_ExecutedInThread_StopsThread()
    {
        serverThread.Start();
        Assert.True(serverThread.GetIsRunning(), "Поток должен быть запущен");

        ICommand softStop = new SoftStop(serverThread);
        serverThread.AddCommand(softStop);

        Thread.Sleep(100); 
        Assert.False(serverThread.GetIsRunning(), "Поток должен быть остановлен после SoftStop");
        Assert.True(serverThread.GetSoftStop(), "Флаг softStop должен быть установлен");
    }

    [Fact]
    public void HardStop_FromAnotherThread_TriggersExceptionHandler()
    {
        var server = new ServerThread();
        var hardStop = new HardStop(server);

        using var sw = new StringWriter();
        Console.SetOut(sw);
        
        hardStop.Execute();

        var output = sw.ToString();
        Assert.Contains("HardStop", output);
        Assert.Contains("успешно выполняется только в потоке", output);
    }

    [Fact]
    public void SoftStop_FromAnotherThread_TriggersExceptionHandler()
    {
        var server = new ServerThread();
        var softStop = new SoftStop(server);

        using var sw = new StringWriter();
        Console.SetOut(sw);

        softStop.Execute();

        var output = sw.ToString();
        Assert.Contains("SoftStop", output);
        Assert.Contains("успешно выполняется только в потоке", output);
    }
}
