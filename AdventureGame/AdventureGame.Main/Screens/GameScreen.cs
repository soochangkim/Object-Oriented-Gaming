using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace AdventureGame.Main.Screens
{
    public enum ScreenType
    {
        Start,
        Action,
        About,
        Credit,
        Help,
        HowToPlay,
        Rangking,
        GameOver,
        GameClear,
        StageClear,
        FinalBoss
    }
    public class GameScreen : DrawableGameComponent
    {
        protected bool _statusChanged;
        protected List<GameComponent> _components = new List<GameComponent>();

        public List<GameComponent> Components
        {
            get
            {
                return _components;
            }

            set
            {
                _components = value;
            }
        }

        public GameScreen(Game game)
            : base(game)
        { }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (GameComponent component in _components)
            {
                if (component.Enabled)
                    component.Update(gameTime);
            }
            base.Update(gameTime);

        }

        public override void Draw(GameTime gameTime)
        {
            DrawableGameComponent temp;
            foreach (GameComponent component in _components)
            {
                if (component is DrawableGameComponent)
                {
                    temp = (DrawableGameComponent)component;
                    if (temp.Visible)
                        temp.Draw(gameTime);
                }
            }
            base.Draw(gameTime);
        }

        public virtual void hide()
        {
            this.Enabled = false;
            this.Visible = false;
        }

        public virtual void show()
        {
            this.Enabled = true;
            this.Visible = true;
        }
    }
}
