using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AdventureGame.Main
{
    public class MenuComponent : DrawableGameComponent
    {
        protected const int TICK_TO_MOVE = 10;

        protected SpriteBatch _spriteBatch;
        protected SpriteFont _hilightFont;
        protected SpriteFont _regularFont;
        protected List<string> _menuItems;
        protected int _selectedItem = 0;
        protected Vector2 _position;
        protected Color _hilightColor = Color.Green;
        protected Color _regularColor = Color.Black;
        protected int count = 0;
        protected KeyboardState oldState;

        public int SelectedItem
        {
            get
            {
                return _selectedItem;
            }

            set
            {
                _selectedItem = value;
            }
        }

        public MenuComponent(Game game, 
            SpriteBatch spriteBatch, 
            SpriteFont regularFont,
            SpriteFont hilightFont,
            string[] menuItems)
            : base(game)
        {
            this._spriteBatch = spriteBatch;
            this._regularFont = regularFont;
            this._hilightFont = hilightFont;
            this._menuItems = menuItems.ToList();

            Initialize();
        }

        public override void Initialize()
        {
            this._position = Utility.GetCenter();
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            
            if (CheckKeyDownCondition(ks))
            {
                count = 0;
                if (ks.IsKeyDown(Keys.Up))
                {
                    _selectedItem--;
                }

                if (ks.IsKeyDown(Keys.Down))
                {
                    _selectedItem++;
                }

                _selectedItem = (_selectedItem + _menuItems.Count) % _menuItems.Count;
            }

            oldState = ks;
            base.Update(gameTime);
        }

        public bool CheckKeyDownCondition(KeyboardState ks)
        {
            return ++count == TICK_TO_MOVE || ks.IsKeyDown(Keys.Up) && oldState.IsKeyUp(Keys.Up) || ks.IsKeyDown(Keys.Down) && oldState.IsKeyUp(Keys.Down);
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            DrawMenuItems();
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        public void DrawMenuItems()
        {
            Vector2 tempPosition = _position;
            for (int i = 0; i < _menuItems.Count; i++)
            {
                if (i == _selectedItem)
                { 
                    _spriteBatch.DrawString(_hilightFont, _menuItems[i], tempPosition, _hilightColor, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
                    tempPosition.Y += _hilightFont.LineSpacing * 2;
                }
                else
                { 
                    _spriteBatch.DrawString(_regularFont, _menuItems[i], tempPosition, _regularColor, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
                    tempPosition.Y += _regularFont.LineSpacing * 2;
                }
            }
        }
    }
}
