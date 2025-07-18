using task11;

namespace task11tests;

public class task11tests
{
    private static string code = @"

    using task11;
    public class Calculator : ICalculator
    {
        public int Add(int a, int b) => a + b;
        public int Minus(int a, int b) => a - b;
        public int Mul(int a, int b) => a * b;
        public int Div(int a, int b) => a / b;
    }";
    
    [Fact]
    public void AddReturnsCorrectValue()
    {
        var calc = CalculatorGenerator.CreateCalc(code);
        Assert.Equal(5, calc.Add(2, 3));
    }

    [Fact]
    public void MinusReturnsCorrectValue()
    {
        var calc = CalculatorGenerator.CreateCalc(code);
        Assert.Equal(5, calc.Minus(7, 2));
    }

    [Fact]
    public void MulReturnsCorrectValue()
    {
        var calc = CalculatorGenerator.CreateCalc(code);
        Assert.Equal(12, calc.Mul(3, 4));
    }

    [Fact]
    public void DivReturnsCorrectValue()
    {
        var calc = CalculatorGenerator.CreateCalc(code);
        Assert.Equal(4, calc.Div(12, 3));
    }
}

