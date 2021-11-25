using System;

namespace RPG
{
    public class Player : Combatant, IMovable2D, IDrawable
    {
        private readonly char[] _sprites = new char[] { '⇑', '⇒', '⇓', '⇐' };

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

        public void Die()
        {
            throw new NotImplementedException();
        }

        public void TakeDamages(int amount)
        {
            throw new NotImplementedException();
        }

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

            LookingAt = direction;
            PreviousPosition.X = Position.X;
            PreviousPosition.Y = Position.Y;

            switch (direction)
            {
                case Direction.Top:
                    if (Position.Y <= 0)
                    {
                        return hasRotated;
                    }

                    Position.Y--;
                    break;

                case Direction.Right:
                    if (Position.X >= Game.CurrentMap.Width - 1)
                    {
                        return hasRotated;
                    }

                    Position.X++;
                    break;

                case Direction.Bottom:
                    if (Position.Y >= Game.CurrentMap.Height - 1)
                    {
                        return hasRotated;
                    }

                    Position.Y++;
                    break;

                case Direction.Left:
                    if (Position.X <= 0)
                    {
                        return hasRotated;
                    }

                    Position.X--;
                    break;
            }

            return true;
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
