using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AdventureGame.Main.Screens
{
    class HelpScreen : GameScreen
    {
        private SpriteBatch spriteBatch;
        private Texture2D texture;
        public HelpScreen(Game game,
            SpriteBatch spriteBatch)
            : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.texture = game.Content.Load<Texture2D>(@"backgrounds/Help");
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, new Vector2(0, 0), Color.White);
            spriteBatch.End();
        }
    }
}
