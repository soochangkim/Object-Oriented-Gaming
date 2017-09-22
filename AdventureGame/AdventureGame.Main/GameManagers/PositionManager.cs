using AdventureGame.Main.Characters;
using Microsoft.Xna.Framework;
using MonogameLevel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureGame.Main.GameManagers
{
    [Flags]
    public enum PositionCheck
    {
        CheckTop,
        CheckBottom,
        CheckRight,
        CheckLeft
    }

    public class PositionManager
    {
        private const int ROW = 1;
        private const int COL = 0;
        private Dictionary<Vector2, Tile> _level;
        private List<Rectangle> _tiles;
        private Character _character;
        private int[] _position;
        private int[] _gameEndPosition = { 3000, 1300 };

        private Vector2[] _initialPosition = new Vector2[2]
        {
            new Vector2(100,500),
            new Vector2(3, 500)
        };

        public PositionManager(Dictionary<Vector2, Tile> level, Character character)
        {
            _level = level;
            _character = character;
            _position = new int[2];
            _tiles = new List<Rectangle>();
        }
        public void loadTiles()
        {
            if (_tiles == null)
                _tiles = new List<Rectangle>();
            foreach (KeyValuePair<Vector2, Tile> item in _level)
            {
                _tiles.Add(new Rectangle((int)item.Key.X, (int)item.Key.Y, Values.TileWidth, Values.TileHeight));
            }
        }

        public Vector2 getMovingDistance(Vector2 speed, Rectangle r)
        {
            Vector2 temp = Vector2.Zero;
            temp.X = speed.X >= 0 ? checkRight(speed, r) : checkLeft(speed, r);
            temp.Y = speed.Y >= 0 ? checkBottom(speed, r) : checkTop(speed, r);
            return temp;
        }

        private float checkRight(Vector2 speed, Rectangle r)
        {
            Rectangle rect = _tiles.FirstOrDefault(a => MoveCheckHelper.CheckRight(r, a, speed));
            return (rect == Rectangle.Empty) ? speed.X : rect.Left - r.Right;
        }

        private float checkLeft(Vector2 speed, Rectangle r)
        {
            Rectangle rect = _tiles.FirstOrDefault(a => MoveCheckHelper.CheckLeft(r, a, speed));
            return (rect == Rectangle.Empty) ? speed.X : rect.Right - r.Left;
        }

        private float checkTop(Vector2 speed, Rectangle r)
        {
            Rectangle rect = _tiles.FirstOrDefault(a => MoveCheckHelper.CheckTop(r, a, speed));
            return (rect == Rectangle.Empty) ? speed.Y : rect.Bottom - r.Top;
        }

        private float checkBottom(Vector2 speed, Rectangle r)
        {
            Rectangle rect = _tiles.FirstOrDefault(a => MoveCheckHelper.CheckBottom(r, a, speed));
            return (rect == Rectangle.Empty) ? Values.JumpingSpeed : rect.Top - r.Bottom;
        }

        public void setLevel(Dictionary<Vector2, Tile> level)
        {
            _level = level;
        }

        public Vector2 getInitialPosition(int stage)
        { 
            return _initialPosition[stage];
        }
        public void Clear()
        {
            _level.Clear();
        }
        public int getStageEndPosition(int stage)
        {
            return _gameEndPosition[stage];
        }
    }
}
