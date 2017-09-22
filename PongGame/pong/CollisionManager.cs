/*
 *  CollisionManager.cs
 *  Assignment04
 *  This class will manage any kind of collision between two object
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
    /// This class will manage collision among object within stage
    /// </summary>
    public class CollisionManager : GameComponent
    {
        #region Fields
        protected Bat[] _bats;
        protected Ball _ball;
        protected int _counter; 
        #endregion

        /// <summary>
        /// Constructor for the class
        /// </summary>
        /// <param name="game"> Game object </param>
        /// <param name="bats"> Bats for each player </param>
        /// <param name="ball"> Ball object </param>
        public CollisionManager(Game game,
            Bat[] bats,
            Ball ball) 
            : base(game)
        {
            this._bats = bats;
            this._ball = ball;
            this._counter = 0;
        }

        /// <summary>
        /// To initialize object
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// To update object
        /// </summary>
        /// <param name="gameTime"> Game time </param>
        public override void Update(GameTime gameTime)
        {
            //counter to prevent multiple collision within a few second
            _counter++;

            foreach (Bat bat in _bats)
            {
                Rectangle batBound = bat.getBound();
                Rectangle ballBound = _ball.getBound();

                // check if ball hit the bar 
                if (batBound.Intersects(ballBound) && _counter > 60)
                {
                    // if ball hit tip of the bar
                    if (ballBound.Y + ballBound.Height >= batBound.Y && ballBound.Y + ballBound.Height <= batBound.Y + batBound.Height * 1 / 10)
                    {
                        if(_ball.Speed.Y > 0)
                        {
                            _ball.Speed = new Vector2(-_ball.Speed.X, -_ball.Speed.Y);
                        }
                        else
                        {
                            _ball.Speed = new Vector2(-_ball.Speed.X, _ball.Speed.Y);
                        }
                    }
                    // if ball hit bottom of the bar
                    else if (ballBound.Y <= batBound.Y + batBound.Height && ballBound.Y >= batBound.Y + batBound.Height * 9 / 10)
                    {
                        if (_ball.Speed.Y < 0)
                        {
                            _ball.Speed = new Vector2(-_ball.Speed.X, -_ball.Speed.Y);
                        }
                        else
                        {
                            _ball.Speed = new Vector2(-_ball.Speed.X, _ball.Speed.Y);
                        } 
                    }
                    // if ball hit other places
                    else
                    {
                        _ball.Speed = new Vector2(-_ball.Speed.X, _ball.Speed.Y);
                    }
                    _ball.bounce();
                    _counter = 0;
                }
            }
            base.Update(gameTime);
        }
    }
}
