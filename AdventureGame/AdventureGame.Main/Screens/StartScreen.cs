using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AdventureGame.Main.Screens
{
    public class StartScreen : GameScreen
    {
        protected string[] _menuItems = {"Start Game", "How to Play", "Help", "About", "Exit" };
        protected MenuComponent _menu;
        protected SpriteBatch _spriteBatch;
        private Background _background;
        private Texture2D _backgroundImage;
        public MenuComponent Menu
        {
            get
            {
                return _menu;
            }

            set
            {
                _menu = value;
            }
        }

        public StartScreen(Game game,
            SpriteBatch spriteBatch)
            : base(game)
        {
            _menu = new MenuComponent(game, spriteBatch, game.Content.Load<SpriteFont>(@"fonts/regularFont"), game.Content.Load<SpriteFont>(@"fonts/hilightFont"), _menuItems);
            _spriteBatch = spriteBatch;
            _backgroundImage = game.Content.Load<Texture2D>(@"backgrounds/Title");
            _background = new Background(_backgroundImage, new Rectangle(0, 0, 1024, 768));
            Components.Add(_menu);
        }

        public override void Initialize()
        {

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            
            base.Update(gameTime); 
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _background.Draw(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
