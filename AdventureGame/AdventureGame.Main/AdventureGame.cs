using System.Collections.Generic;
using AdventureGame.Main.Characters;
using AdventureGame.Main.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonogameLevel;
using AdventureGame.Main.GameManagers;

namespace AdventureGame.Main
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class AdventureGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private ActionScreen actionScreen;
        private AboutScreen aboutScreen;
        private HelpScreen helpScreen;
        public static HowToPlayScreen howToPlayScreen;
        public static StartScreen startScreen;
        public static SoundManager SoundsManager { get; set; }

        public AdventureGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            Window.Title = "Jodi and Soochang's Adventure";

            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
            graphics.ApplyChanges();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            Utility.Initialize(graphics);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            startScreen = new StartScreen(this, spriteBatch);
            Components.Add(startScreen);
            actionScreen = new ActionScreen(this, spriteBatch);
            Components.Add(actionScreen);

            Utility.HilightFont = Content.Load<SpriteFont>(@"fonts/hilightFont");
            SoundsManager = new SoundManager(this);
            SoundsManager.LoadContent();

            
            aboutScreen = new AboutScreen(this, spriteBatch);
            Components.Add(aboutScreen);
            helpScreen = new HelpScreen(this, spriteBatch);
            Components.Add(helpScreen);
            howToPlayScreen = new HowToPlayScreen(this, spriteBatch);
            Components.Add(howToPlayScreen);
            HideAllScreens();
            startScreen.show();
            SoundsManager.PlayBackgroudnMusic(ScreenType.Start, true);
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();

            if (startScreen.Enabled && Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                SoundsManager.StopAll();
                HideAllScreens();
                switch (startScreen.Menu.SelectedItem)
                {
                    case 0:
                        SoundsManager.PlayBackgroudnMusic(ScreenType.Action, true);
                        actionScreen.show();
                        break;
                    case 1:
                        howToPlayScreen.show();
                        SoundsManager.PlayBackgroudnMusic(ScreenType.HowToPlay, true);
                        break;
                    case 2:
                        helpScreen.show();
                        SoundsManager.PlayBackgroudnMusic(ScreenType.Help, true);
                        break;
                    case 3:
                        aboutScreen.show();
                        SoundsManager.PlayBackgroudnMusic(ScreenType.About, true);
                        break;
                    case 4:
                        Exit();
                        break;
                    default:
                        break;
                }
            }
            else if(!startScreen.Enabled)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Escape) && !actionScreen.Enabled)
                {
                    HideAllScreens();
                    startScreen.show();
                    SoundsManager.PlayBackgroudnMusic(ScreenType.Start, true);
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            base.Draw(gameTime);
        }

        public void HideAllScreens()
        {
            foreach (var component in Components)
            {
                if (component is GameScreen)
                {
                    ((GameScreen)component).hide();
                }
            }
        }

        private void ShowAllScreens()
        {
            foreach (var component in Components)
            {
                if (component is GameScreen)
                {
                    ((GameScreen)component).hide();
                }
            }
        }
    }
}
