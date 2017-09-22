using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AdventureGame.Main.Screens
{
    public class AboutScreen : GameScreen
    {
        SpriteBatch spriteBatch;
        Texture2D texture;
        public AboutScreen(Game game, 
            SpriteBatch spriteBatch)
            : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.texture = game.Content.Load<Texture2D>("backgrounds/About");
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, new Vector2(0, 0), Color.White);
            spriteBatch.End();
        }
    }
}
