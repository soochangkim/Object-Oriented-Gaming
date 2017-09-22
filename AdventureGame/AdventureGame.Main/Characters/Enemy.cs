using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AdventureGame.Main.Items;
using AdventureGame.Main.GameManagers;
using MonogameLevel;

namespace AdventureGame.Main.Characters
{
    public class Enemy : Character
    {
        protected int _score;
        protected bool _isFoundPlayer = false;
        protected int _tickToMove = 20;
        protected double _searchDist = 600;
        protected bool _foundPlayer;
        protected bool _isShot;
        protected int _shotCounter;
        protected Vector2 _shotSpeed;
        protected bool _hasWeapon;

        public Enemy(Game game,
            Texture2D spriteSheet
            )
            : base(game, spriteSheet)
        {
            Life = 1;
        }

        public override void handleCollision(CollisionType collisionType, object other)
        {
            if (collisionType == CollisionType.PlayerBulletWithEnemy)
                Life--;
            if (Life == 0)
            {
                if (other is Bullet)
                {
                    Bullet b = (Bullet)other;
                    ((Player)b.getCharacter()).Score += Values.SoldierScore;
                    hide();
                }
            }
        }
        public virtual Bullet shot(SoundManager sm)
        {
            if (_hasWeapon && !_isShot)
            {
                _isShot = true;
                sm.Play(SoundTypes.Shot);
                return new Bullet(_game.Content.Load<Texture2D>("bullets/playerBullet.fw"), BulletOwnerType.Enemy, this, currentDirection, getShotStartPosition(), _shotSpeed * 5);
            }
            return null;
        }

        public virtual bool searchPlayer(Player p)
        {
            Rectangle bound = p.getBoundToCheckCollision();
            Point point = Point.Zero;
            if (bound.X < _destinationRectangle.X)
            {
                point.X = _destinationRectangle.X;
                point.Y = _destinationRectangle.Y;
            }
            else
            {
                point.X = _destinationRectangle.X + _destinationRectangle.Width;
                point.Y = _destinationRectangle.Y;
            }

            _foundPlayer = Utility.getDistance(point, bound.Center) < _searchDist ? true : false;
            return _foundPlayer;
        }
    }
}
