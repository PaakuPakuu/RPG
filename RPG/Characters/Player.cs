using System;

namespace RPG
{
    public class Player : Combatant, IMovable2D, IDrawable
    {
        private readonly char[] _sprites = new char[] { '⇑', '⇒', '⇓', '⇐' };
        private readonly Point[] _moveVectors = new Point[]
            {
                new Point(0, -1),
                new Point(1, 0),
                new Point(0, 1),
                new Point(-1, 0)
            };

        public Direction LookingAt { get; private set; }
        public Point PreviousPosition { get; private set; }
        public Point Position { get; }
        public string Sprite { get => _sprites[(int)LookingAt].ToString(); }

        public enum PlayerAction
        {
            MoveNorth,
            MoveEast,
            MoveSouth,
            MoveWest,
            OpenInventory,
            TriggerAction,
            Pause
        }

        public Player(string name) : base(name)
        {
            PreviousPosition = new Point();
            Position = Game.CurrentMap.SpawnPosition;
        }

        private bool IsIntoMap(Point point) => point.X >= 0 && point.X < Game.CurrentMap.Width && point.Y >= 0 && point.Y < Game.CurrentMap.Height;

        public PlayerAction WaitForAction()
        {
            bool validInput = false;

            while (!validInput)
            {
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.Z:
                        return PlayerAction.MoveNorth;
                    case ConsoleKey.D:
                        return PlayerAction.MoveEast;
                    case ConsoleKey.S:
                        return PlayerAction.MoveSouth;
                    case ConsoleKey.Q:
                        return PlayerAction.MoveWest;
                    case ConsoleKey.I:
                        return PlayerAction.OpenInventory;
                    case ConsoleKey.E:
                        return PlayerAction.TriggerAction;
                    case ConsoleKey.Escape:
                        return PlayerAction.Pause;
                    default: break;
                }
            }

            return PlayerAction.Pause;
        }

        public bool Move(Direction direction)
        {
            bool hasRotated = (direction != LookingAt);
            bool hasMoved = false;
            Point nextPosition = Position + _moveVectors[(int)direction];

            LookingAt = direction;
            PreviousPosition.X = Position.X;
            PreviousPosition.Y = Position.Y;

            if (Game.CurrentMap.IsWalkable(nextPosition) && IsIntoMap(nextPosition))
            {
                Position.X = nextPosition.X;
                Position.Y = nextPosition.Y;
                hasMoved = true;
            }

            return hasRotated || hasMoved;
        }

        public void Draw()
        {
            DisplayTools.WriteInBufferAt(
                Game.CurrentMap.MapDisplay[PreviousPosition.Y][PreviousPosition.X].ToString(),
                PreviousPosition.X + Game.CurrentMap.PositionInBuffer.X,
                PreviousPosition.Y + Game.CurrentMap.PositionInBuffer.Y
                );

            DisplayTools.WriteInBufferAt(Sprite, Position.X + Game.CurrentMap.PositionInBuffer.X, Position.Y + Game.CurrentMap.PositionInBuffer.Y);
        }
    }
}
