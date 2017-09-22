﻿/*
 *  ScoreManager.cs
 *  Assignment04
 *  This class will count score for the game and init sound when game is finished
 *  Revision History:
 *      Craeted By Soochang Kim
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace pong
{
    /// <summary>
    /// Logical class to manage score
    /// </summary>
    class ScoreManager : GameComponent
    {
        #region Fields for Score Manager
        protected Player[] _players;
        protected Player _winner;
        protected Ball _ball;
        protected ScoreBar _scoreBar;
        protected Vector2 _stage;
        protected bool _isFinished;
        protected SoundEffect _soundEffect;
        protected SoundEffect _winningEffect;
        protected bool _isPlaying;
        #endregion


        #region Properties
        public bool IsFinished
        {
            get
            {
                return _isFinished;
            }

            set
            {
                _isFinished = value;
            }
        } 
        #endregion

        /// <summary>
        /// Class consturctor
        /// </summary>
        /// <param name="game"> Game obejct </param>
        /// <param name="players"> Players to be in this game </param>
        /// <param name="ball"> Ball to be check </param>
        /// <param name="scoreBar"> Score bar to display score </param>
        /// <param name="stage"> Stage </param>
        public ScoreManager(Game game, Player[] players, Ball ball, ScoreBar scoreBar, Vector2 stage) 
            : base(game)
        {
            this._players = players;
            this._ball = ball;
            this._scoreBar = scoreBar;
            this._stage = stage;
            this._isFinished = false;
            this._soundEffect = game.Content.Load<SoundEffect>("sounds/ding");
            this._winningEffect = game.Content.Load<SoundEffect>("sounds/applause1");
            Initialize();
        }

        /// <summary>
        /// To initialize class
        /// </summary>
        public override void Initialize()
        {
            foreach(Player p in _players)
            {
                msgGenerate(p);
                
            }
            _isPlaying = false;
            base.Initialize();
        }

        /// <summary>
        /// To update the score
        /// </summary>
        /// <param name="gameTime"> Game time object </param>
        public override void Update(GameTime gameTime)
        {
            updateScore();

            // If someone won the game, make sound and finish the game
            if(checkWinner())
            {
                if (!_isPlaying)
                {
                    _winningEffect.Play();
                    _isPlaying = true;
                }
                _scoreBar.setString(ScoreBar.Area.finishMessage, $"{_winner.Name} wins!\nPress spacebar to restart");
                IsFinished = true;
                _ball.Enabled = false;
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// To check if someone won the game
        /// </summary>
        /// <returns> True if someone won the game</returns>
        private bool checkWinner()
        {
            foreach (Player p in _players)
            {
                if (p.Score == 2)
                {
                    _winner = p;
                    return true;
                }
            }
            return false;
        }


        /// <summary>
        /// To update score
        /// </summary>
        private void updateScore()
        {
            Rectangle ballBound = _ball.getBound();

            foreach (Player p in _players)
            {
                if (((p.PlayerType == Player.Type.Right) && (ballBound.X + ballBound.Width < 0)) ||
                    ((p.PlayerType == Player.Type.Left) && (ballBound.X > _stage.X)))
                {
                    _soundEffect.Play();
                    p.Score++;
                    msgGenerate(p);
                    _ball.Initialize();
                }
            }
        }

        /// <summary>
        /// Message will be generated by player type
        /// </summary>
        /// <param name="p"> Player </param>
        private void msgGenerate(Player p)
        {
            _scoreBar.setString(((p.PlayerType == Player.Type.Left) ? ScoreBar.Area.player1 : ScoreBar.Area.player2), $"{p.Name}:  {p.Score}");
        }

        /// <summary>
        /// To reinitialize
        /// </summary>
        public void reInitialise()
        {
            foreach(Player p in _players)
            {
                p.Score = 0;
            }
        }
    }
}
