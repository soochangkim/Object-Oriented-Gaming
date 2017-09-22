using AdventureGame.Main.Characters;
using AdventureGame.Main.GameManagers;
using AdventureGame.Main.Interfaces;
using AdventureGame.Main.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using MonogameLevel;

namespace AdventureGame.Main.Screens
{
    public enum PuaseMenuState
    {
        Restart,
        Continue,
        Exit
    }

    public class ActionScreen : GameScreen
    {
        private const int MAX_STAGE = 2;
        private LevelManager levelManager;
        private Dictionary<Vector2, Tile> level;
        public static int _stage;
        private string[] _strLevel = new string[MAX_STAGE]
        {
            "level/FirstLevel.lvl",
            "level/SecondLevel.lvl"
        };

        private bool _isHowToPlayPage;
        public static Camera camera;
        protected bool _initated;
        protected Player _player;
        protected SpriteBatch _spriteBatch;
        protected SpriteFont _spriteFont;
        protected StatusBar _status;
        protected List<Enemy> _enemies;
        protected static List<Bullet> _bullets;
        private static int _highScore;
        public static int HighScore
        {
            get
            {
                return _highScore;
            }
            set
            {
                _highScore = value;
            }
        }
        public static List<Bullet> Bullets
        {
            get { return _bullets; }
        }
        protected List<Item> _items;
        protected List<Button> _buttons;
        protected CollisionManager _collisionManager;
        protected PositionManager _positionManager;
        private Background scrolling1;
        private Background scrolling2;
        private bool _stageFinished;
        private bool _isPaused;
        private bool _isGameOver;
        private Texture2D first;
        private Texture2D second;
        private Texture2D scorollingEnemy;
        private Texture2D apple;
        private Texture2D heart;
        private Texture2D soldier;

        private KeyboardState _oldKS;
        private int _soundTimer;
        private string[] _pauseButtons = new string[]
        {
            "Paused",
            "Restart",
            "Continue",
            "How To Play",
            "Go Main Page",
            "Exit"
        };

        public ActionScreen(Game game, SpriteBatch spriteBatch)
            : base(game)
        {
            _spriteBatch = spriteBatch;
            _spriteFont = Game.Content.Load<SpriteFont>("fonts/hilightFont");
            first = Game.Content.Load<Texture2D>("backgrounds/landscape1");
            second = Game.Content.Load<Texture2D>("backgrounds/landscape");
            scorollingEnemy = Game.Content.Load<Texture2D>("enemies/enemy1");
            apple = Game.Content.Load<Texture2D>("items/apple.fw");
            heart = Game.Content.Load<Texture2D>("items/heart.fw");
            soldier = Game.Content.Load<Texture2D>("enemies/Enemy_metal_slug.fw");
            _enemies = new List<Enemy>();
            _bullets = new List<Bullet>();
            _items = new List<Item>();
            _buttons = new List<Button>();
            LoadMenuButtons(game);
        }


        public void LoadMenuButtons(Game game)
        {
            Button temp;
            for (int i = 0; i < _pauseButtons.Length; i++)
            {
                temp = new Button();
                temp.LoadContent(_pauseButtons[i], game.GraphicsDevice, _spriteFont);
                _buttons.Add(temp);
            }
        }


        public override void Initialize()
        {
            
            _isGameOver = false;
            if (_player == null)
                _player = new Player(Game, CharacterType.Soochang, Game.Content.Load<Texture2D>(@"characters/soochang.fw"));
            else
                _player.setSpeed(Vector2.Zero);
            camera = new Camera(GraphicsDevice.Viewport);
            
            scrolling1 = new Background(first, new Rectangle(0, 0, (int)Utility.Stage.X, 768));
            scrolling2 = new Background(second, new Rectangle((int)Utility.Stage.X, 0, (int)Utility.Stage.X, 768));

            levelManager = new LevelManager(Game.Content.Load<Texture2D>(@"levels/game_sprite_level"));
            level = levelManager.LoadLevel(_strLevel[_stage]);

            _positionManager = new PositionManager(level, _player);
            _positionManager.loadTiles();

            _collisionManager = new CollisionManager(_player, _enemies, _items, _bullets);
            _status = new StatusBar(Game, _player);

            if (_stage == 0)
            {
                _items.Add(new Item(apple, ItemType.Apple, new Vector2(500,600)));
                _items.Add(new Item(heart, ItemType.Life, new Vector2(800, 550)));
            }
            else
            {
                _enemies.Add(new Soldier(Game, soldier));
            }
            
            _player.setPosition(_positionManager.getInitialPosition(_stage));
            base.Initialize();
            _initated = true;
        }


        private void loadStage(bool isFirst)
        {
            _stageFinished = false;
            if (_initated)
                clear(isFirst);
            _stage++;
            Initialize();
        }

        private void clear(bool andPlayer)
        {
            _items.Clear();
            _enemies.Clear();
            _bullets.Clear();
            ScrollingEnemy.numOfEnemy = 0;
            _positionManager.Clear();
            level = null;
            _player = (andPlayer) ? null : _player;
            _stageFinished = false;
            _initated = false;
        }

        public override void hide()
        {
            if (_initated)
                clear(true);
            _stage = 0;
            Visible = false;
            Enabled = false;
        }

        
        public override void Update(GameTime gameTime)
        {
            if (Enabled)
            {
                if (!_initated) Initialize();

                if(_isHowToPlayPage && Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    AdventureGame.howToPlayScreen.hide();
                    _buttons[3].setIsClicked(false);
                    _isHowToPlayPage = false;
                }

                if (_player.getBoundToCheckCollision().X > _positionManager.getStageEndPosition(_stage) && !_stageFinished)
                {
                    _stageFinished = true;
                    _player.setVisible(true);
                    if (_stage == MAX_STAGE - 1)
                        AdventureGame.SoundsManager.PlayBackgroudnMusic(ScreenType.StageClear, true);
                    else
                        AdventureGame.SoundsManager.PlayBackgroudnMusic(ScreenType.StageClear, true);
                    _player.CurrentDirection = MoveDirection.Right;
                }

                if (_stageFinished && _soundTimer < 6000)
                {
                    _soundTimer += gameTime.ElapsedGameTime.Milliseconds;
                }

                else if (_stageFinished)
                {
                    _soundTimer = 0;

                    if (_stage != MAX_STAGE - 1)
                        loadStage(false);
                    else
                    {
                        hide();
                        AdventureGame.startScreen.show();
                        AdventureGame.SoundsManager.PlayBackgroudnMusic(ScreenType.Start, true);
                        return;
                    }

                    _stageFinished = false;
                    AdventureGame.SoundsManager.PlayBackgroudnMusic(ScreenType.FinalBoss, true);
                }

                if (!_stageFinished)
                {
                    KeyboardState ks = Keyboard.GetState();

                    if (ks.IsKeyDown(Keys.Escape) && _oldKS.IsKeyUp(Keys.Escape))
                    {
                        _isPaused ^= true;
                        Game.IsMouseVisible ^= true;
                    }

                    _oldKS = ks;

                    if (!_isPaused)
                    {
                        if (_player.Life == 0 && _player._destinationRectangle.Y > camera.Bottom)
                        {
                            _isGameOver = true;
                            _isPaused ^= true;
                            Game.IsMouseVisible ^= true;
                        }

                        for (int i = 0; i < _buttons.Count; i++)
                        {
                            _buttons[i].setPosition(new Vector2(camera.X, camera.Top + Game.GraphicsDevice.Viewport.Height / 7 * (i + 1)));
                        }

                        Vector2 tempCameraPosition = Vector2.Zero;
                        if (_player._destinationRectangle.X < Utility.Stage.X / 2)
                            tempCameraPosition.X = Utility.Stage.X / 2;
                        else
                            tempCameraPosition.X = _player._destinationRectangle.X;

                        tempCameraPosition.Y = Utility.Stage.Y / 2;


                        camera.Update(tempCameraPosition);

                        //This will be main update part
                        Vector2 temp = _positionManager.getMovingDistance(_player.getSpeed(), _player.getBoundToCheckCollision());
                        if (_stage == 0)
                            temp = new Vector2(4, temp.Y);
                        _player.Update(gameTime, temp);
                        UpdateList(gameTime, _enemies);
                        UpdateList(gameTime, _items);
                        UpdateList(gameTime, _bullets);
                        
                        scrolling1.Update(-temp/4);
                        scrolling2.Update(-temp/4);

                        _collisionManager.checkAllCollision();

                        //Only when stage 1
                        if (_stage == 0)
                            generateScrollingEnemy(gameTime);
                        _status.Update(gameTime, new Vector2(camera.Left, 0));

                        base.Update(gameTime);
                    }
                    else
                    {
                        MouseState ms = Mouse.GetState();
                        _player.setVisible(true);
                        _buttons[0].setMessage((_isGameOver) ? "Game Over" : "Paused");

                        foreach (Button btn in _buttons)
                        {
                            btn.Update(ms);
                        }
                        if (_isGameOver) _buttons[2].setColor(Color.Gray);

                        if (_buttons[1].IsClicked())
                        {
                            AdventureGame.SoundsManager.StopBackgroundMusic();
                            AdventureGame.SoundsManager.PlayBackgroudnMusic((_stage == MAX_STAGE -1) ? ScreenType.FinalBoss : ScreenType.Action, true);
                            clear(true);
                            Initialize();
                            turnOnOffPauseMenu(1);

                        }
                        if (_buttons[2].IsClicked() && !_isGameOver)
                        {
                            turnOnOffPauseMenu(2);
                        }
                        if( _buttons[3].IsClicked())
                        {
                            _isHowToPlayPage = true;
                            AdventureGame.howToPlayScreen.show();
                        }
                        if (_buttons[4].IsClicked())
                        {
                            hide();
                            AdventureGame.startScreen.show();
                            AdventureGame.SoundsManager.PlayBackgroudnMusic(ScreenType.Start, true);
                            turnOnOffPauseMenu(2);
                        }
                        if (_buttons[5].IsClicked())
                        {
                            turnOnOffPauseMenu(3);
                            Game.Exit();
                        }
                    }
                }
            }
        }

        public void turnOnOffPauseMenu(int idx)
        {
            _isPaused ^= true;
            Game.IsMouseVisible ^= true;
            _buttons[idx].setIsClicked(false);
        }


        public override void Draw(GameTime gameTime)
        {
            if (Visible && !_isHowToPlayPage)
            {
                _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.Transform);

                scrolling1.Draw(_spriteBatch);
                scrolling2.Draw(_spriteBatch);
                DrawList(gameTime, _enemies);
                DrawList(gameTime, _items);
                DrawList(gameTime, _bullets);
                _status.Draw(gameTime, _spriteBatch, _spriteFont);
                
                foreach (var tile in level)
                {
                    tile.Value.Draw(_spriteBatch);
                }

                _player.Draw(gameTime, _spriteBatch);

                if (_isPaused)
                {
                    foreach (Button btn in _buttons)
                    {
                        btn.Draw(_spriteBatch, _spriteFont);
                    }
                }

                if (_stage == MAX_STAGE - 1 && _stageFinished)
                {
                    string msg = "Congratulations,\nyou may now submit your final assignment!";
                    _spriteBatch.DrawString(_spriteFont, msg, new Vector2(camera.X, camera.Y), Color.Black, 0f, _spriteFont.MeasureString(msg)/2, 2.0f, SpriteEffects.None, 0f);
                }
                    
                _spriteBatch.End();
                base.Draw(gameTime);
            }
        }

        public void UpdateList<T>(GameTime gameTime, List<T> listItems)
        {
            IDrawableComponent item;
            for (int i = 0; i < listItems.Count; i++)
            {
                if (listItems[i] is IDrawableComponent)
                {
                    item = (IDrawableComponent)listItems[i];

                    if (!item.isEnabled() && !item.isVisible())
                    {
                        listItems.Remove(listItems[i]);
                    }
                    else
                    {
                        UpdateIDrawableComponent(gameTime, item);
                    }
                }
            }
        }
        private void UpdateIDrawableComponent(GameTime gameTime, IDrawableComponent component)
        {
            if (component.isEnabled())
            {
                Vector2 temp = Vector2.Zero;
                if (component is IGetBound && component is IMovable)
                    temp = _positionManager.getMovingDistance(((IMovable)component).getSpeed(), ((IGetBound)component).getBoundToCheckCollision());
                component.Update(gameTime, temp);
            }
        }
        private void DrawIDrawableComponent(GameTime gameTime, IDrawableComponent component)
        {
            if (component.isVisible())
            {
                component.Draw(gameTime, _spriteBatch);
            }
        }
        public void DrawList<T>(GameTime gameTime, List<T> listItems)
        {
            foreach (T listItem in listItems)
            {
                if (listItem is IDrawableComponent)
                {
                    DrawIDrawableComponent(gameTime, (IDrawableComponent)listItem);
                }
            }
        }
        private void generateScrollingEnemy(GameTime gameTime)
        {
            ScrollingEnemy.generationCounter += gameTime.ElapsedGameTime.Milliseconds;
            if (ScrollingEnemy.numOfEnemy < Values.MaxScrollingEnemy &&
                ScrollingEnemy.generationCounter >= ScrollingEnemy.generationInterval &&
                Utility.Random.Next(10) == 1)
            {
                ScrollingEnemy.generationCounter = 0;
                _enemies.Add(new ScrollingEnemy(Game, scorollingEnemy, _player, 2));
            }
        }
        public bool IsPaused()
        {
            return _isPaused;
        }
    }
}
