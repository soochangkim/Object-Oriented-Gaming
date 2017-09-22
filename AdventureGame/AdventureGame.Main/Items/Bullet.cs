using AdventureGame.Main.Items;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using AdventureGame.Main.Characters;
using AdventureGame.Main.Screens;
using AdventureGame.Main.Interfaces;

namespace AdventureGame.Main.GameManagers
{
    [Flags]
    public enum BulletOwnerType
    {
        Player,
        Enemy
    }

    public class Bullet : Item, IMovable
    {
        public const int MaxBullet = 2;
        protected BulletOwnerType _bulletOwner;
        public BulletOwnerType BulletOwner
        {
            get
            {
                return _bulletOwner;
            }
        }
        protected Character _owner;
        protected Vector2 _origin;
        protected float _rotation = 0.0f;
        protected Vector2 _speed;

        private struct BulletInfo
        {
            public Vector2 _size;
            public Vector2 _speed;

            public BulletInfo(Vector2 si, Vector2 sp)
            {
                _size = si;
                _speed = sp;
            }
        }

        private static BulletInfo[] _bullet = new BulletInfo[]
        {
            new BulletInfo(new Vector2(22, 14), new Vector2(8, 0)),
            new BulletInfo(new Vector2(22, 14), new Vector2(8, 0))
        };


        public Bullet(Texture2D spriteSheet,
            BulletOwnerType ownerType,
            Character owner,
            MoveDirection direction,
            Point start)
            : base(spriteSheet, ItemType.Weapon)
        {
            Init(spriteSheet, ownerType, owner, direction, start);
        }
        public Bullet(Texture2D spriteSheet,
            BulletOwnerType ownerType,
            Character owner,
            MoveDirection direction,
            Point start,
            Vector2 position)
            : base(spriteSheet, ItemType.Weapon, position)
        {
            Init(spriteSheet, ownerType, owner, direction, start);
            _speed = position;
        }
        public void Init(Texture2D spriteSheet,
            BulletOwnerType ownerType,
            Character owner,
            MoveDirection direction,
            Point start)
        {
            int xPos, yPos;
            _bulletOwner = ownerType;
            _spriteSheet = spriteSheet;
            _owner = owner;
            _itemCategory = ItemType.Weapon;
            _frameSize = _bullet[(int)BulletOwner]._size;
            _sourceRectangles = new List<Rectangle>();
            _sourceRectangles.Add(new Rectangle(0, 0, (int)_frameSize.X, (int)_frameSize.Y));

            _speed = _bullet[(int)ownerType]._speed;

            if (direction == MoveDirection.Left || direction == MoveDirection.RunningLeft)
            {
                xPos = -(int)(_frameSize.X / 2);
                yPos = (int)(_frameSize.Y / 2);
                _speed.X = -_speed.X;
                _rotation = 3.14159f;
            }
            else
            {
                xPos = -(int)(_frameSize.X / 2);
                yPos = (int)(_frameSize.Y / 2);
            }

            _origin = new Vector2(_frameSize.X / 2, _frameSize.Y / 2);
            ImgDestination = new Rectangle(start.X + xPos, start.Y - yPos, (int)_frameSize.X, (int)_frameSize.Y);
        }


        public void setBulletRotation(float r)
        {
            _rotation = r;
        }

        public override void Update(GameTime gameTime, Vector2 dist)
        {
            if (_enabled)
            {
                if (ImgDestination.X + ImgDestination.Width <= ActionScreen.camera.Left || 
                    ImgDestination.X > ActionScreen.camera.Right || 
                    dist.X == 0)
                    hide();
                ImgDestination.X += (int)dist.X;
                ImgDestination.Y += (int)_speed.Y;
            }
        }

        public Character getCharacter()
        {
            return _owner;
        }

        public override void handleCollision(CollisionType collisionType, object other)
        {
            if (other is Item)
                return;
            setBulletState((_bulletOwner == BulletOwnerType.Player) ^ (other is Player));
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_spriteSheet, ImgDestination, _sourceRectangles[_idxFrame], Color.White, _rotation, _origin, SpriteEffects.None, 0.0f);
        }
        private void setBulletState(bool isTarget)
        {
            if (isTarget)
                hide();
        }

        public Vector2 getSpeed()
        {
            return _speed;
        }
    }
}
