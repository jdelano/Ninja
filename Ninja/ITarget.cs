using System;
namespace Ninja
{
    public interface ITarget
    {
        (int x, int y) Location { get; set; }
        bool IsHitting(IThrowable throwable);
        void Display();
    }
}
