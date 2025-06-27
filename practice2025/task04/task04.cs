namespace task04
{
    public interface ISpaceship
    {
        void MoveForward();      // Движение вперед
        void Rotate(int angle);  // Поворот на угол (градусы)
        void Fire();             // Выстрел ракетой
        int Speed { get; }       // Скорость корабля
        int FirePower { get; }   // Мощность выстрела
    }

    public class Cruiser : ISpaceship
    {
        public int Speed { get; set; } = 50;
        public int FirePower { get; set; } = 100;

        public double X { get; set; } = 0;
        public double Y { get; set; } = 0;

        public double Angle { get; set; } = 0;

        public int Rockets = 25;

        public void MoveForward() 
        { 
            X += Speed * Math.Cos(Angle * Math.PI / 180);
            Y += Speed * Math.Sin(Angle * Math.PI / 180);
        }

        public void Rotate(int angle)
        {
            Angle += angle;
        }

        public void Fire()
        {
            Rockets--;
        }
    }

    public class Fighter : ISpaceship 
    {
        public int Speed { get; set; } = 100;
        public int FirePower { get; set; } = 50;

        public double X { get; set; } = 0;
        public double Y { get; set; } = 0;

        public double Angle { get; set; } = 0;

        public int Rockets = 10;

        public void MoveForward()
        {
            X += Speed * Math.Cos(Angle * Math.PI / 180);
            Y += Speed * Math.Sin(Angle * Math.PI / 180);
        }

        public void Rotate(int angle)
        {
            Angle += angle;
        }

        public void Fire()
        {
            Rockets--;
        }
    }
}
