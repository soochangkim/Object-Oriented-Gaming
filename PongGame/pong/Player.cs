/*
 *  Player.cs
 *  Assignment04
 *  This class has player information and the player's bat
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
    /// Player class will take a role for each player
    /// </summary>
    public class Player : DrawableGameComponent
    {

        #region Fields for Player class
        protected SpriteBatch _spriteBatch;
        protected Texture2D _texture;
        protected Keys _up;
        protected Keys _down;
        protected Type _playerType;
        protected string _name;
        protected Bat _bat;
        protected Vector2 _stage;
        protected List<GameComponent> _components;
        protected int _score;
        #endregion

        #region Properties for Player class
        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
            }
        }

        public Bat Bat
        {
            get
            {
                return _bat;
            }

            set
            {
                _bat = value;
            }
        }

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

        public Type PlayerType
        {
            get
            {
                return _playerType;
            }

            set
            {
                _playerType = value;
            }
        }

        public int Score
        {
            get
            {
                return _score;
            }

            set
            {
                _score = value;
            }
        }

        public enum Type
        {
            Left,
            Right
        }
        #endregion

        /// <summary>
        /// Consturcotr for class
        /// </summary>
        /// <param name="game"> Game object </param>
        /// <param name="spriteBatch"> GDI to draw image </param>
        /// <param name="type"> Type of player </param>
        /// <param name="name"> Name of player </param>
        /// <param name="stage"> Stage to hang around </param>
        public Player(Game game, 
            SpriteBatch spriteBatch, 
            Type type,
            string name,
            Vector2 stage)
            : base(game)
        {
            Components = new List<GameComponent>();
            this._spriteBatch = spriteBatch;
            this._name = name;
            this._playerType = type;
            setKeys(type);
            loadTexture(game, _playerType);
            this._stage = stage;
            this._bat = new Bat(game, spriteBatch, _texture, setBatPosition(_playerType), _stage, _up, _down);
            this._components.Add(this._bat);
            this._score = 0;
        }

        /// <summary>
        /// To set the key for player
        /// </summary>
        /// <param name="type"> Type of player </param>
        private void setKeys(Type type)
        {
            switch (type)
            {
                case Type.Left:
                    _up = Keys.A;
                    _down = Keys.Z;
                    break;
                case Type.Right:
                    _up = Keys.Up;
                    _down = Keys.Down;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// To load image dependon user type
        /// </summary>
        /// <param name="game"> Game object to load image </param>
        /// <param name="type"> Type of player </param>
        private void loadTexture(Game game, Type type)
        {
            switch (type)
            {
                case Type.Left:
                    _texture = game.Content.Load<Texture2D>("images/BatLeft");
                    break;
                case Type.Right:
                    _texture = game.Content.Load<Texture2D>("images/BatRight");
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// To set bar position depend on user type
        /// </summary>
        /// <param name="type"> Type of player</param>
        /// <returns> Position of bat </returns>
        private Vector2 setBatPosition(Type type)
        {
            Vector2 temp = new Vector2();
            switch (type)
            {
                case Type.Left:
                    temp.X = 0;
                    temp.Y = _stage.Y / 2 - _texture.Height / 2;
                    break;
                case Type.Right:
                    temp.X = _stage.X - _texture.Width;
                    temp.Y = _stage.Y / 2 - _texture.Height / 2;
                    break;
                default:
                    break;
            }
            return temp;
        }

        /// <summary>
        /// To initialize Player
        /// </summary>
        public override void Initialize()
        {
            _score = 0;
            _bat.Position = setBatPosition(_playerType);
            base.Initialize();
        }

        /// <summary>
        /// To update player infomation
        /// </summary>
        /// <param name="gameTime"> Game time </param>
        public override void Update(GameTime gameTime)
        {
            foreach(GameComponent item in Components)
            {
                item.Update(gameTime);
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// To draw game object
        /// </summary>
        /// <param name="gameTime"> Game Time </param>
        public override void Draw(GameTime gameTime)
        {
            DrawableGameComponent dgc;
            foreach(GameComponent item in Components)
            {
                if(item is DrawableGameComponent)
                {
                    dgc = (DrawableGameComponent)item;
                    dgc.Draw(gameTime);
                }
            }
            base.Draw(gameTime);
        }
    }
}
