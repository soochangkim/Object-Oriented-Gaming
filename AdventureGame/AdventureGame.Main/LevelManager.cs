using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonogameLevel
{
    public class LevelManager
    {
        private Texture2D levelSprite;

        public LevelManager(Texture2D levelSprite)
        {
            this.levelSprite = levelSprite;
        }

        public Dictionary<Vector2, Tile> LoadLevel(string levelFilePath)
        {
            Dictionary<Vector2, Tile> level = new Dictionary<Vector2, Tile>();
            TileManager manager = new TileManager(levelSprite);
            string[] lines = System.IO.File.ReadAllLines(levelFilePath);
            foreach (var line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    string[] tileInfo = line.Split(',');

                    var position = new Vector2(float.Parse(tileInfo[0]) * Values.TileWidth, float.Parse(tileInfo[1]) * Values.TileHeight);
                    level.Add(position, manager.GenerateTile(position, GetTileType(tileInfo[2])));
                }
            }

            return level;
        }

        private TileTypes GetTileType(string type)
        {
            switch (type)
            {
                case "single":
                    return TileTypes.Single;
                case "left":
                    return TileTypes.LeftEnd;
                case "middle":
                    return TileTypes.Middle;
                case "right":
                    return TileTypes.RightEnd;
                default:
                    return TileTypes.None;
            }
        }
    }
}
