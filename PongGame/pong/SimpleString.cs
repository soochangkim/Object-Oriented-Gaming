/*
 *  SimpleString.cs
 *  Assignment04
 *  This class will display string at the given position
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
    /// Class to display string on the screen
    /// </summary>
    public class SimpleString : DrawableGameComponent
    {
        #region Fields for SimpleString Class
        protected SpriteBatch _spriteBatch;
        protected HorizontalAlign _horizontalAlign;
        protected VerticalAlign _verticalAlign;
        protected SpriteFont _spriteFont;
        protected string _message;
        protected string _oldMessage;
        protected Vector2 _position;
        protected Vector2 _size;
        protected Vector2 _drawPosition;
        protected Color _color;
        #endregion

        #region Properties for the class
        public string Message
        {
            get
            {
                return _message;
            }

            set
            {
                _message = value;
            }
        }

        /// <summary>
        /// Enum type for horizontal align text
        /// </summary>
        public enum HorizontalAlign
        {
            Left,
            Center,
            Right
        }
        /// <summary>
        /// Enum type for vertical align text
        /// </summary>
        /// 
        public enum VerticalAlign
        {
            Top,
            Center,
            Bottom
        } 
        #endregion

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="game"> Object to initialise DrawalbeGameComponent class </param>
        /// <param name="spriteBatch"> Instance of grapic device interface </param>
        /// <param name="spriteFont"> Fonts </param>
        /// <param name="message"> Message to be displayed </param>
        /// <param name="position"> Position of the message </param>
        /// <param name="size"> Size of text area </param>
        /// <param name="color"> Text Color </param>
        /// <param name="horizontalAlign"> Horizontal Align </param>
        /// <param name="verticalAlign"> Vertical Align </param>
        public SimpleString(Game game,
            SpriteBatch spriteBatch,
            SpriteFont spriteFont,
            string message,
            Vector2 position,
            Vector2 size,
            Color color,
            HorizontalAlign horizontalAlign = HorizontalAlign.Left,
            VerticalAlign verticalAlign = VerticalAlign.Center)
            : base(game)
        {
            this._spriteBatch = spriteBatch;
            this._spriteFont = spriteFont;
            this._oldMessage = this._message = message;
            this._size = size;
            this._horizontalAlign = horizontalAlign;
            this._position = position;                
            this._color = color;
            this._verticalAlign = verticalAlign;
            Initialize();
        }

        /// <summary>
        /// To get X position by horizontal align
        /// </summary>
        /// <param name="horizontalAlign"> Align to display message </param>
        /// <param name="position"> Start point of the given text area </param>
        /// <returns></returns>
        private float getXPosition(HorizontalAlign horizontalAlign, Vector2 position)
        {
            float x = 0f;
            Vector2 szMessage;
            switch (horizontalAlign)
            {
                case HorizontalAlign.Left:
                    x = position.X;
                    break;
                case HorizontalAlign.Center:
                    szMessage = _spriteFont.MeasureString(_message);
                    x = position.X + (_size.X / 2 - szMessage.X / 2);
                    break;
                case HorizontalAlign.Right:
                    szMessage = _spriteFont.MeasureString(_message);
                    x = position.X + (_size.X - szMessage.X);
                    break;
                default:
                    break;
            }
            return x;
        }

        /// <summary>
        /// To get Y position by vertical align
        /// </summary>
        /// <param name="verticalAlign"> Align to display message </param>
        /// <param name="position"> Start point of the given text area </param>
        /// <returns></returns>
        private float getYPosition(VerticalAlign verticalAlign, Vector2 position)
        {
            float y = 0f;
            Vector2 szMessage;
            switch (verticalAlign)
            {
                case VerticalAlign.Top:
                    y = position.Y;
                    break;
                case VerticalAlign.Center:
                    szMessage = _spriteFont.MeasureString(_message);
                    y = position.Y + (_size.Y / 2 - szMessage.Y / 2);
                    break;
                case VerticalAlign.Bottom:
                    break;
                default:
                    break;
            }
            return y;
        }

        /// <summary>
        /// Initialize the object
        /// </summary>
        public override void Initialize()
        {
            _drawPosition = new Vector2(getXPosition(_horizontalAlign, _position), getYPosition(_verticalAlign, _position));
            base.Initialize();
        }

        /// <summary>
        /// Update the object information
        /// </summary>
        /// <param name="gameTime"> Game time </param>
        public override void Update(GameTime gameTime)
        {
            if(_oldMessage != _message)
            {
                Initialize();
                _oldMessage = _message;
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// Draw string on the screen
        /// </summary>
        /// <param name="gameTime"> Game Time </param>
        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.DrawString(_spriteFont, _message, _drawPosition, _color);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
