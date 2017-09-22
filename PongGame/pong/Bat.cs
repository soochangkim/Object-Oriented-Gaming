/*
 *  Bat.cs
 *  Assignment04
 *  Bat class to manage basic bat movement with given key input
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
    /// This class will draw bat
    /// </summary>
    public class Bat : DrawableGameComponent
    {
        #region Fields
        protected SpriteBatch _spriteBatch;
        protected Texture2D _texture;
        protected Vector2 _position;
        protected Vector2 _stage;
        protected float _distanceMove = 5f;
        protected Keys _up;
        protected Keys _down;
        #endregion

        #region Properties
        public Vector2 Position
        {
            get
            {
                return _position;
            }

            set
            {
                _position = value;
            }
        } 
        #endregion

        /// <summary>
        /// Constructor for the class
        /// </summary>
        /// <param name="game"> Game object </param>
        /// <param name="spriteBatch"> GDI to draw </param>
        /// <param name="texture"> Image to be drawed </param>
        /// <param name="position"> Position to be drown </param>
        /// <param name="stage"> Stage of the game </param>
        /// <param name="up"> Key to move up </param>
        /// <param name="down"> Key to move down </param>
        public Bat(Game game,
            SpriteBatch spriteBatch,
            Texture2D texture,
            Vector2 position,
            Vector2 stage,
            Keys up,
            Keys down) 
            : base(game)
        {
            this._spriteBatch = spriteBatch;
            this._texture = texture;
            this._position = position;
            this._stage = stage;
            this._up = up;
            this._down = down;
        }

        /// <summary>
        /// To initialize bat object
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// To update bat object
        /// </summary>
        /// <param name="gameTime"> Game time</param>
        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            // if key to move up is down 
            if(ks.IsKeyDown(_up) && _position.Y > 0)
            {
                _position.Y -= _distanceMove;
            }
            // if key to move down is down 
            if (ks.IsKeyDown(_down) && _position.Y + _texture.Height < _stage.Y)
            {
                _position.Y += _distanceMove;
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// To draw image
        /// </summary>
        /// <param name="gameTime"> Game Time </param>
        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(_texture, _position, Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        /// <summary>
        /// To get bound of bat
        /// </summary>
        /// <returns> Boundary of bat </returns>
        public Rectangle getBound()
        {
            return new Rectangle(_position.ToPoint(), new Point(_texture.Width, _texture.Height));
        }
    }
}
