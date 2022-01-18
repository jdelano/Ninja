using System;
namespace Ninja
{
    public class Box : ITarget
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

        public void Display()
        {
            Console.Write("[]");
        }

        public bool IsHitting(IThrowable throwable)
        {
            return (throwable.Location.y == Location.y &&
                (throwable.Location.x == Location.x ||
                throwable.Location.x == Location.x + 1));
        }
    }
}
