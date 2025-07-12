using task14;

namespace task14tests;

public class task14tests
{
    [Fact]
    public void Test1()
    {
        Assert.Equal(0, DefiniteIntegral.Solve(-1, 1, x => x, 1e-4, 2), 1e-4);
    }
    
    [Fact]
    public void Test2()
    {
        Assert.Equal(0, DefiniteIntegral.Solve(-1, 1, x => Math.Sin(x), 1e-5, 8), 1e-4);
    }
    
    [Fact]
    public void Test3()
    {
        Assert.Equal(12.5, DefiniteIntegral.Solve(0, 5, x => x, 1e-6, 8), 1e-5);
    }
}