using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AdventureGame.Main.Screens
{
    public class HowToPlayScreen : GameScreen
    {
        private SpriteBatch spriteBatch;
        private Texture2D texture;

        public HowToPlayScreen(Game game,
            SpriteBatch spriteBatch)
            : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.texture = game.Content.Load<Texture2D>(@"backgrounds/How to Play");
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, new Vector2(0, 0), Color.White);
            spriteBatch.End();
        }
    }
}
