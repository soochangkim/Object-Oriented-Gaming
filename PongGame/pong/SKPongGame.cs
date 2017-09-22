/*
 *  SKPongGame.cs
 *  Assignment04
 *  Ball class to manage basic ball's movement
 *  Revision History:
 *      Craeted By Soochang Kim
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace pong
{
    /// <summary>
    /// This is the main type for Pong game
    /// </summary>
    public class SKPongGame : Game
    {
        #region Fields for SKPongGame Class
        /// <summary>
        /// Given fields for the graphics
        /// </summary>
        GraphicsDeviceManager graphics;
        SpriteBatch _spriteBatch;

        /// <summary>
        /// Fields to play pong game
        /// </summary>
        Ball _ball;
        ScoreBar _scoreBar;
        SpriteFont _spriteFont;
        CollisionManager _mgrCollision;
        ScoreManager _mgrScore;
        Player _p1;
        Player _p2;
        Vector2 _stage; 
        #endregion

        /// <summary>
        /// Pong Game constructor
        /// </summary>
        public SKPongGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            // initialise basic field to manage screen, fonts
            _spriteFont = Content.Load<SpriteFont>("fonts/simpleFont");
            _stage = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

            // create score bar
            Texture2D textScoreBar = Content.Load<Texture2D>("images/Scorebar");
            _scoreBar = new ScoreBar(this, _spriteBatch, _spriteFont, textScoreBar, _stage);
            Components.Add(_scoreBar);

            // resize stage by substracting the size of scorebar
            _stage = new Vector2(_stage.X, _stage.Y - textScoreBar.Bounds.Height);

            // create player 1
            _p1 = new Player(this, _spriteBatch, Player.Type.Left, "Soochang Kim", _stage);
            Components.Add(_p1);

            // create player 2
            _p2 = new Player(this, _spriteBatch, Player.Type.Right, "Happy OOP", _stage);
            Components.Add(_p2);
                        
            // create ball
            Texture2D txtBall = Content.Load<Texture2D>("images/Ball");
            _ball = new Ball(this, _spriteBatch, _stage, txtBall);
            Components.Add(_ball);

            // create collision manager
            _mgrCollision = new CollisionManager(this, new Bat[] { _p1.Bat, _p2.Bat }, _ball);
            Components.Add(_mgrCollision);

            // create score manager
            _mgrScore = new ScoreManager(this, new Player[] { _p1, _p2 }, _ball, _scoreBar, _stage);
            Components.Add(_mgrScore);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            // reinitialise if the game is restarted
            if(_mgrScore.IsFinished && Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                reInitAll();
            }
            base.Update(gameTime);
        }


        /// <summary>
        /// To initialise all of the object to play game
        /// </summary>
        private void reInitAll()
        {
            _p1.Initialize();
            _p2.Initialize();
            _scoreBar.Initialize();
            _mgrScore.Initialize();
            _mgrScore.IsFinished = false;
            _ball.Enabled = true;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
