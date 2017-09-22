using AdventureGame.Main.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AdventureGame.Main
{
    public class Background
    {
        public Texture2D texture;
        public Rectangle rectangle;

        public Background(Texture2D newTexture, Rectangle newRectangle)
        {
            texture = newTexture;
            rectangle = newRectangle;
        }

        /// <summary>
        /// This method will move back ground
        /// </summary>
        /// <param name="move">Distance to move</param>
        public virtual void Update(Vector2 move)
        {
            if (rectangle.X < ActionScreen.camera.Left - rectangle.Width)
            {
                rectangle.X = rectangle.X + rectangle.Width * 2;
            }
            else if (rectangle.X > ActionScreen.camera.Right)
            {
                rectangle.X = rectangle.X - rectangle.Width * 2;
            }

            if(ActionScreen.camera.X != Utility.Stage.X / 2)
                rectangle.X += (int)move.X;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }
    }
}
