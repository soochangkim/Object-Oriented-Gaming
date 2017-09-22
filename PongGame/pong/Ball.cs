/*
 *  Ball.cs
 *  Assignment04
 *  Ball class to manage basic ball's movement
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
using Microsoft.Xna.Framework.Audio;

namespace pong
{
    /// <summary>
    /// Ball class to draw ball image on the screen
    /// </summary>
    public class Ball : DrawableGameComponent
    {
        #region Fields for Ball Class
        Random random = new Random();
        protected SpriteBatch _spriteBatch;
        protected Texture2D _texture;
        protected Vector2 _position;
        protected Vector2 _stage;
        protected Vector2 _speed;
        protected SoundEffect _soundEffect;
        #endregion

        #region MyRegion
        public Vector2 Speed
        {
            get
            {
                return _speed;
            }

            set
            {
                _speed = value;
            }
        } 
        #endregion

        /// <summary>
        /// Ball class constructor
        /// </summary>
        /// <param name="game"> Game object </param>
        /// <param name="spriteBatch"> Graphics device interface to draw ball images </param>
        /// <param name="stage"> Size of stage to move for the ball </param>
        /// <param name="texture"> Image texture to be drown </param>
        public Ball(Game game,
            SpriteBatch spriteBatch,
            Vector2 stage,
            Texture2D texture) 
            : base(game)
        {
            this._spriteBatch = spriteBatch;
            this._texture = texture;
            this._stage = stage;
            this._position = new Vector2(_stage.X / 2 - _texture.Width / 2, _stage.Y / 2 - _texture.Height / 2);
            this._speed = new Vector2(0,0);
            this._soundEffect = game.Content.Load<SoundEffect>("sounds/ballBound");
        }

        /// <summary>
        /// To initialize ball class
        /// </summary>
        public override void Initialize()
        {
            this._position = new Vector2(_stage.X / 2 - _texture.Width / 2, _stage.Y / 2 - _texture.Height / 2);
            this._speed = new Vector2(0, 0);
            base.Initialize();
        }

        /// <summary>
        /// To update ball class
        /// </summary>
        /// <param name="gameTime"> Game Time object</param>
        public override void Update(GameTime gameTime)
        {
            
            //If ball does not move, set a new speed only when user typed 'Enter'
            if(Keyboard.GetState().IsKeyDown(Keys.Enter) && _speed == Vector2.Zero)
            {
                _speed.X = getRandom();
                _speed.Y = getRandom();
            }

            _position += _speed;
            //If the ball hit the wall, it will change the direction
            if(_position.Y < 0 || _position.Y >= _stage.Y - _texture.Height)
            {
                _speed = new Vector2(_speed.X, -_speed.Y);
                bounce();
            }
            base.Update(gameTime);
        }
        
        /// <summary>
        /// Method to get random number
        /// </summary>
        /// <returns> Generated random number </returns>
        private int getRandom()
        {
            return (random.Next() % 2 == 1) ? random.Next(3, 9) : -random.Next(3, 9);
        }

        /// <summary>
        /// To draw ball image
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(_texture, _position, Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        /// <summary>
        /// To get bound of ball rectangle
        /// </summary>
        /// <returns> Boundary of ball </returns>
        public Rectangle getBound()
        {
            return new Rectangle(_position.ToPoint(), new Point(_texture.Width, _texture.Height));
        }

        /// <summary>
        /// To make sound when the ball bounce
        /// </summary>
        public void bounce()
        {
            _soundEffect.Play();
        }
    }
}
