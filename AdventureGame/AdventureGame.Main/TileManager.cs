using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonogameLevel
{
    public enum TileTypes
    {
        None,
        Single,
        RightEnd,
        LeftEnd,
        Middle       
    }

    public class TileManager
    {
        private Texture2D sprite;

        public TileManager(Texture2D sprite)
        {
            this.sprite = sprite;
        }

        public Tile GenerateTile(Vector2 position, TileTypes type)
        {
            switch (type)
            {
                case TileTypes.Single:
                    return new Tile(position, 0, 0, Values.TileWidth, Values.TileHeight, sprite);
                    
                case TileTypes.LeftEnd:
                    return new Tile(position, Values.TileWidth, 0, Values.TileWidth, Values.TileHeight, sprite);

                case TileTypes.Middle:
                    return new Tile(position, Values.TileWidth * 2, 0, Values.TileWidth, Values.TileHeight, sprite);

                case TileTypes.RightEnd:
                    return new Tile(position, Values.TileWidth * 3, 0, Values.TileWidth, Values.TileHeight, sprite);

                default:
                    return null;
            }
        }
    }
}
