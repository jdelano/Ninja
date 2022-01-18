using System;
namespace Ninja
{
    public class Shuriken : IThrowable
    {
        private int times = 0;
        private (int x, int y) location;
        public (int x, int y) Location
        {
            get => location;
            set
            {
                if (value.x >= 0 && value.x < 79 && value.y >= 0 && value.y < 23)
                {
                    location = (value.x, value.y);
                }
                else
                {
                    Visible = false;
                }
            }
        }
        public Direction Direction { get; set; }
        public bool Visible { get; set; }

        public Shuriken()
        {
            Visible = true;
        }

        public void Advance()
        {
            switch (Direction)
            {
                case Direction.North:
                    Location = (Location.x, Location.y - 1);
                    break;
                case Direction.South:
                    Location = (Location.x, Location.y + 1);
                    break;
                case Direction.East:
                    Location = (Location.x + 1, Location.y);
                    break;
                case Direction.West:
                    Location = (Location.x - 1, Location.y);
                    break;
            }
        }

        public void Display()
        {
            if (Visible)
            {
                if (times % 2 == 0)
                {
                    Console.Write("+");
                }
                else
                {
                    Console.Write("x");
                }
                times++;
            }
        }
    }
}
