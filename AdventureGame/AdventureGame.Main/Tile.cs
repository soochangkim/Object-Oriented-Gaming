using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonogameLevel
{
    public class Tile
    {
        private readonly int spriteWidth;
        private readonly int spriteHeight;
        private readonly int spritePositionX;
        private readonly int spritePositionY;
        private Vector2 position;
        private readonly Texture2D tileImage;

        public Tile(Vector2 position, int spritePositionX, int spritePositionY, int spriteWidth, int spriteHeight, Texture2D tileImage)
        {
            this.spriteWidth = spriteWidth;
            this.spriteHeight = spriteHeight;
            this.tileImage = tileImage;
            this.spritePositionX = spritePositionX;
            this.spritePositionY = spritePositionY;
            this.position = position;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tileImage, position, new Rectangle(spritePositionX, spritePositionY, spriteWidth, spriteHeight), Color.White);
        }
    }
}
