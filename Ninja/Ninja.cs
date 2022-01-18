using System;
namespace Ninja
{
    public class Ninja
    {
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
            }
        }

        public Direction Direction { get; set; }

        public void Display()
        {
            Console.Write("N");
        }

        public void Move()
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
    }
}
