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
        public Map Origin { get; set; }

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
            Origin = Game.Adventure.CurrentMap;
            PreviousPosition = new Point();
            Position = Origin.SpawnPosition;
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
                    if (Position.X >= Origin.Width - 1)
                    {
                        return hasRotated;
                    }

                    Position.X++;
                    break;

                case Direction.Bottom:
                    if (Position.Y >= Origin.Height - 1)
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
                Origin.MapDisplay[PreviousPosition.Y][PreviousPosition.X].ToString(),
                PreviousPosition.X + Origin.PositionInBuffer.X,
                PreviousPosition.Y + Origin.PositionInBuffer.Y
                );

            Origin.DrawDrawables();

            DisplayTools.WriteInBufferAt(Sprite, Position.X + Origin.PositionInBuffer.X, Position.Y + Origin.PositionInBuffer.Y);
        }
    }
}
