/*
 *  ScoreBar.cs
 *  Assignment04
 *  ScareBar class will contains 3 simple string object and texture for score bar image
 *  Revision History:
 *      Craeted By Soochang Kim
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace pong
{
    /// <summary>
    /// To draw score bar
    /// </summary>
    public class ScoreBar : DrawableGameComponent
    {
        #region Properties
        public enum Area
        {
            player1,
            finishMessage,
            player2
        } 
        #endregion

        #region Fields
        public const int NUM_COL = 3;
        protected Game _game;
        protected SpriteBatch _spriteBatch;
        protected SpriteFont _spriteFont;
        protected Texture2D _texture;
        protected Vector2 _position;
        protected SimpleString[] _messages;
        protected Color _color; 
        #endregion

        /// <summary>
        /// Class construcotr
        /// </summary>
        /// <param name="game"> Game obejct </param>
        /// <param name="spriteBatch"> Graphic device interface </param>
        /// <param name="spriteFont"> Font to be used write message </param>
        /// <param name="texture"> Image of score bar </param>
        /// <param name="stage"> Initial stage </param>
        public ScoreBar(Game game,
            SpriteBatch spriteBatch,
            SpriteFont spriteFont,
            Texture2D texture,
            Vector2 stage) 
            : base(game)
        {
            this._spriteBatch = spriteBatch;
            this._texture = texture;
            this._position = new Vector2(0, stage.Y - this._texture.Height);
            this._messages = new SimpleString[NUM_COL];
            this._spriteFont = spriteFont;
            this._game = game;
            Initialize();
        }

        /// <summary>
        /// Initialize score bar
        /// </summary>
        public override void Initialize()
        {
            for (int i = 0; i < this._messages.Length; i++)
            {
                _color = (i == (int)Area.finishMessage) ? Color.Blue : Color.Black;
                this._messages[i] = new SimpleString(_game, _spriteBatch, _spriteFont, "",
                    _position, new Vector2(_texture.Width, _texture.Height), _color, (SimpleString.HorizontalAlign)i);
            }
            base.Initialize();
        }

        /// <summary>
        /// To update score bar message
        /// </summary>
        /// <param name="gameTime"> Gmae time </param>
        public override void Update(GameTime gameTime)
        {
            foreach(SimpleString ss in _messages)
            {
                ss.Update(gameTime);
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// To draw scorebar
        /// </summary>
        /// <param name="gameTime"> Game time</param>
        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(_texture, _position, Color.White);
            _spriteBatch.End();
            triggerToDrawString(gameTime);

            base.Draw(gameTime);
        }


        /// <summary>
        /// To trigger to draw every string
        /// </summary>
        /// <param name="gameTime"> Game time </param>
        private void triggerToDrawString(GameTime gameTime)
        {
            foreach (SimpleString str in _messages)
            {
                str.Draw(gameTime);
            }
        }

        /// <summary>
        /// This will set string message
        /// </summary>
        /// <param name="area"> Area to be displayed within score bar </param>
        /// <param name="message"> Message to be displayed </param>
        public void setString(Area area, string message)
        {
            _messages[(int)area].Message = message;
        }
    }
}
