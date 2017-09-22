using AdventureGame.Main.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using AdventureGame.Main.Items;
using AdventureGame.Main.Characters;
using Microsoft.Xna.Framework;

namespace AdventureGame.Main.Screens
{
    public class StatusBar : IDrawableComponent
    {
        private bool _initiated;
        private const int LIFE_X_POSITION = 20;
        private const int LIFE_Y_POSITION = 20;
        private bool _enabled;
        private bool _visible;
        private Player _player;
        private Item _life;
        private string[] _message;
        private Vector2[] _position;
        
        public StatusBar(
            Game game,
            Player player)
        {
            _life = new Item(game.Content.Load<Texture2D>(@"items/heart.fw"), ItemType.Life, Vector2.Zero);
            _player = player;
            _message = new string[4];
            SetMessage();
            _position = new Vector2[_message.Length];
        }

        public void Initialize()
        {
            _initiated = true;
            _visible = true;
            _enabled = true;

            _life.show();
            _life.ImgDestination =
                new Rectangle(
                    LIFE_X_POSITION,
                    LIFE_Y_POSITION,
                    _life.ImgDestination.Width,
                    _life.ImgDestination.Height
                );
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch, SpriteFont spriteFont)
        {
            _life.Draw(gameTime, spriteBatch);
            for(int i = 0; i < _message.Length; i++)
            {
                spriteBatch.DrawString(spriteFont, _message[i], _position[i], Color.Black);
            }
        }

        public bool isEnabled()
        {
            return _enabled;
        }

        public bool isVisible()
        {
            return _visible;
        }

        public void setEnabled(bool enabled)
        {
            _enabled = enabled;
        }

        public void setVisible(bool visible)
        {
            _visible = visible;
        }

        private void SetMessage()
        {
            _message[0] = $" X {_player.Life}";
            _message[1] = $"Score: {_player.Score}";
            _message[2] = $"Stage {ActionScreen._stage + 1}";
            _message[3] = $"High Score: {ActionScreen.HighScore}";
        }

        public void Update(GameTime gameTime, Vector2 dist)
        {
            if (!_initiated) Initialize();

            _life.Update(gameTime, Vector2.Zero);
            if (ActionScreen.HighScore < _player.Score)
            {
                ActionScreen.HighScore = _player.Score;
            }

            SetMessage();

            _life.ImgDestination.X = (int)ActionScreen.camera.Left + LIFE_X_POSITION;
            _life.ImgDestination.Y = (int)ActionScreen.camera.Top + LIFE_Y_POSITION;
            _position[0] =
                new Vector2(
                    _life.ImgDestination.X + _life.ImgDestination.Width,
                    _life.ImgDestination.Y + (_life.ImgDestination.Center.Y - _life.ImgDestination.Y) / 2);
            _position[1] =
                new Vector2(
                    _position[0].X + Utility.HilightFont.MeasureString(_message[0]).X + Utility.HilightFont.LineSpacing,
                    _position[0].Y);
            _position[2] =
                new Vector2(
                    ActionScreen.camera.X - Utility.HilightFont.MeasureString(_message[2]).X / 2,
                    _position[1].Y);
            _position[3] = 
                new Vector2(
                    ActionScreen.camera.Right - Utility.HilightFont.MeasureString(_message[3]).X - 10,
                    _position[1].Y);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _life.Draw(gameTime, spriteBatch);
        }
    }
}
