using System;
namespace Ninja
{
    public enum Direction
    {
        North, East, South, West
    }

    public interface IThrowable
    {
        Direction Direction { get; set; }
        (int x, int y) Location { get; set; }
        void Advance();
        void Display();
    }
}
