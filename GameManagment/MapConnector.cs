using System;
using System.Collections.Generic;
using System.Text;

namespace RPG
{
    public class MapConnector : ITriggerable, IDrawable
    {
        private static readonly string[] SPRITES = { "⍐", "⍈", "⍗", "⍇" };

        private ContextualMenu _changeMapMenu;
        private Direction _direction;
        private Map _target { get; set; }

        public Point Position { get; }
        public bool Activable => true;

        public string Sprite => SPRITES[(int)_direction];

        public MapConnector(Map target, Point position, Direction direction = Direction.Top)
        {
            _changeMapMenu = new ContextualMenu(horizontal: true, centered: true, padding: 4, selectedStyle: ContextualMenu.SelectedStyle.Underlined);
            _changeMapMenu.AddMenuItem("Oui", ChangeMap);
            _changeMapMenu.AddMenuItem("Non", () => { });

            _direction = direction;
            _target = target;

            Position = position;
        }

        public void OnTrigger()
        {
            _changeMapMenu.Execute();
            Console.Clear();
        }

        public void Draw()
        {
            DisplayTools.WriteInBufferAt(Sprite, Position.X + DisplayTools.WidthMargin, Position.Y + DisplayTools.HeightMargin);
        }

        #region Actions

        private void ChangeMap() => Game.Adventure.CurrentMap = _target;

        #endregion
    }
}
