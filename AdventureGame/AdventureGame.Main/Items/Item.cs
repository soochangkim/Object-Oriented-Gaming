using AdventureGame.Main.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureGame.Main.GameManagers;

namespace AdventureGame.Main.Items
{
    public enum ItemType
    {
        Apple,
        Banana,
        Life,
        Weapon
    }
    public class Item : ICollisionHandler, IGetBound, IDrawableComponent
    {
        public Rectangle ImgDestination;
        protected bool _enabled;
        protected bool _visible;
        protected Vector2 _frameSize;
        protected List<Rectangle> _sourceRectangles;
        protected Texture2D _spriteSheet;
        protected int _idxFrame;
        protected ItemType _itemCategory;
        protected Vector2 _position;
        protected int _tickToChangeIndexFrame;

        public ItemType ItemCategory
        {
            get
            {
                return _itemCategory;
            }
        }

        protected int _tickCounter = 0;

        public Item(Texture2D spriteSheet, ItemType type, Vector2 position)
        {
            _itemCategory = type;
            _spriteSheet = spriteSheet;
            _tickToChangeIndexFrame = 200;
            _position = position;
            show();
            Init(type);
            Initialize();
        }

        public Item(Texture2D spriteSheet, ItemType type)
        {
            _itemCategory = type;
            _spriteSheet = spriteSheet;
            _tickToChangeIndexFrame = 200;
            show();
            Init(type);
            Initialize();
        }
        public virtual void Initialize()
        {
            if (_sourceRectangles == null)
            {
                _sourceRectangles = new List<Rectangle>();
                if (_frameSize != null && _frameSize != Vector2.Zero)
                {
                    createSourceRectangle();
                }
            }
        }

        protected virtual void createSourceRectangle()
        {
            int x = 0;
            int y = 0;
            while (x < _spriteSheet.Width)
            {
                _sourceRectangles.Add(new Rectangle(new Point(x, y), _frameSize.ToPoint()));
                x += (int)_frameSize.X;
            }
        }

        public virtual void Update(GameTime gameTime, Vector2 dist)
        {
            if (_enabled)
            {
                ImgDestination.X = ImgDestination.X + (int)dist.X;
            }
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if(_visible)
            {
                _tickCounter += gameTime.ElapsedGameTime.Milliseconds;
                if(_tickCounter >= _tickToChangeIndexFrame)
                {
                    _tickCounter -= _tickToChangeIndexFrame;
                    _idxFrame = ++_idxFrame % _sourceRectangles.Count;
                }
                spriteBatch.Draw(_spriteSheet, ImgDestination, _sourceRectangles[_idxFrame], Color.White);
            }
        }

        public void Init(ItemType type)
        {
            switch (type)
            {
                case ItemType.Apple:
                    _frameSize = new Vector2(40, 40);
                    _tickToChangeIndexFrame = 160;
                    ImgDestination = new Rectangle(_position.ToPoint(), _frameSize.ToPoint());
                    break;
                case ItemType.Banana:
                    _frameSize = new Vector2(30, 51);
                    ImgDestination = new Rectangle(_position.ToPoint(), _frameSize.ToPoint());
                    break;
                case ItemType.Life:
                    _frameSize = new Vector2(50, 50);
                    _tickToChangeIndexFrame = 80;
                    ImgDestination = new Rectangle(_position.ToPoint(), _frameSize.ToPoint());
                    break;
                default:
                    break;
            }
        }

        public virtual Rectangle getBoundToCheckCollision()
        {
            return new Rectangle(ImgDestination.X + 4, ImgDestination.Y + 4, ImgDestination.Width - 4, ImgDestination.Height - 4);
        }

        public virtual void handleCollision(CollisionType collisionType, object other)
        {
            hide();
        }

        public bool isEnabled()
        {
            return _enabled;
        }
        public void setEnabled(bool enabled)
        {
            _enabled = enabled;
        }
        public bool isVisible()
        {
            return _visible;
        }
        public void setVisible(bool visible)
        {
            _visible = visible;
        }
        public virtual void hide()
        {
            _enabled = false;
            _visible = false;
        }
        public void show()
        {
            _enabled = true;
            _visible = true;
        }
    }
}
