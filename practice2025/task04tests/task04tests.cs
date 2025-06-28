using Xunit;
using task04;

public class SpaceshipTests
{
    [Fact]
    public void Cruiser_ShouldHaveCorrectStats()
    {
        ISpaceship cruiser = new Cruiser();
        Assert.Equal(50, cruiser.Speed);
        Assert.Equal(100, cruiser.FirePower);
    }

    [Fact]
    public void Fighter_ShouldBeFasterThanCruiser()
    {
        var fighter = new Fighter();
        var cruiser = new Cruiser();
        Assert.True(fighter.Speed > cruiser.Speed);
    }

    [Fact]
    public void Cruiser_ShouldHaveCorrectInitialRockets()
    {
        var cruiser = new Cruiser();
        Assert.Equal(25, cruiser.Rockets);
    }

    [Fact]
    public void Fighter_ShouldHaveCorrectInitialRockets()
    {
        var fighter = new Fighter();
        Assert.Equal(10, fighter.Rockets);
    }

    [Fact]
    public void Cruiser_MoveForward_ShouldUpdatePosition()
    {
        var cruiser = new Cruiser();
        cruiser.MoveForward();
        Assert.Equal(50 * Math.Cos(0), cruiser.X, 2);
        Assert.Equal(50 * Math.Sin(0), cruiser.Y, 2);
    }

    [Fact]
    public void Fighter_MoveForward_ShouldUpdatePosition()
    {
        var fighter = new Fighter();
        fighter.MoveForward();
        Assert.Equal(100 * Math.Cos(0), fighter.X, 2);
        Assert.Equal(100 * Math.Sin(0), fighter.Y, 2);
    }

    [Fact]
    public void Cruiser_Rotate_ShouldUpdateAngle()
    {
        var cruiser = new Cruiser();
        cruiser.Rotate(90);
        Assert.Equal(90, cruiser.Angle);
    }

    [Fact]
    public void Fighter_Rotate_ShouldUpdateAngle()
    {
        var fighter = new Fighter();
        fighter.Rotate(45);
        Assert.Equal(45, fighter.Angle);
    }

    [Fact]
    public void Cruiser_Fire_ShouldDecreaseRockets()
    {
        var cruiser = new Cruiser();
        var initialRockets = cruiser.Rockets;
        cruiser.Fire();
        Assert.Equal(initialRockets - 1, cruiser.Rockets);
    }

    [Fact]
    public void Fighter_Fire_ShouldDecreaseRockets()
    {
        var fighter = new Fighter();
        var initialRockets = fighter.Rockets;
        fighter.Fire();
        Assert.Equal(initialRockets - 1, fighter.Rockets);
    }

    [Fact]
    public void Cruiser_MoveForward_WithAngle_ShouldUpdatePositionCorrectly()
    {
        var cruiser = new Cruiser();
        cruiser.Rotate(90);
        cruiser.MoveForward();
        Assert.Equal(50 * Math.Cos(Math.PI / 2), cruiser.X, 2);
        Assert.Equal(50 * Math.Sin(Math.PI / 2), cruiser.Y, 2);
    }

    [Fact]
    public void Fighter_MoveForward_WithAngle_ShouldUpdatePositionCorrectly()
    {
        var fighter = new Fighter();
        fighter.Rotate(90);
        fighter.MoveForward();
        Assert.Equal(100 * Math.Cos(Math.PI / 2), fighter.X, 2);
        Assert.Equal(100 * Math.Sin(Math.PI / 2), fighter.Y, 2);
    }
}