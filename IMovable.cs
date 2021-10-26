using System;
using System.Collections.Generic;
using System.Text;

namespace RPG
{
    public enum Direction
    {
        Top,
        Right,
        Bottom,
        Left
    }

    public interface IMovable : ILocatable
    {
        Direction LooksAt { get; }

        bool Move(Direction direction)
        {
            throw new NotImplementedException();
        }
    }
}
