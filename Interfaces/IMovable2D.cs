namespace RPG
{
    public enum Direction
    {
        Top,
        Right,
        Bottom,
        Left
    }

    public interface IMovable2D : ILocatable2D
    {
        Direction LookingAt { get; }
        Point PreviousPosition { get; }

        bool Move(Direction direction);
    }
}
