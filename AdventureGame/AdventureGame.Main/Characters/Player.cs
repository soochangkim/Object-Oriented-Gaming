using System;
using AdventureGame.Main.GameManagers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using AdventureGame.Main.Items;
using MonogameLevel;
using AdventureGame.Main.Screens;

namespace AdventureGame.Main.Characters
{
    [Flags]
    public enum CharacterType
    {
        Jodi = 0,
        Soochang = 1
    }

    public class Player : Character
    {
        public const int MAX_LIFE = 2;

        protected Vector2 startPosition = new Vector2(100, 500);
        protected int _havingFruits;
        private int recoverTime = 500;
        private int recoverCounter;
        private bool _hasWeapon;
        private bool isShot;
        private float extraSpeed;
        public bool Beinghit { get; set; }
        public bool DeatFlag { get; set; }
        private int shotCounter = 0;
        private int shotInterval = 1000;
        protected KeyboardState oldKey;

        public Player(Game game,
            CharacterType type,
            Texture2D spriteSheet)
            : base(game, spriteSheet)
        {
            numFrames = new int[] { 3, 3, 1, 3, 10, 10, 10, 10 };
            _speed = new Vector2(0, 0);
            previousDirection = currentDirection = MoveDirection.Right;
            _destinationRectangle.X = (int)startPosition.X;
            _destinationRectangle.Y = (int)startPosition.Y;
            sizeFrame = new Point(150, 150);
            Life = Values.StartLife;
            Initialize();
            tickToUpdate = 80;
        }

        private void jumpStart()
        {
            NowJumping = true;
            _beforeJumpHeight = _destinationRectangle.Y;
            _speed.Y = -10f;
        }

        public void shotUpdate(GameTime gameTime)
        {
            shotCounter += gameTime.ElapsedGameTime.Milliseconds;
            if(shotCounter >= shotInterval)
            {
                shotCounter = 0;
                isShot = false;
            }
        }

        private void setDeatFlag()
        {
            if (Life == 0 && !DeatFlag)
            {
                AdventureGame.SoundsManager.PlayBackgroudnMusic(ScreenType.GameOver, false);
                _speed.Y = -10f;
                _speed.X = 0f;
                DeatFlag = true;
                currentDirection = MoveDirection.Down;
                return;
            }
        }
        public override void Update(GameTime gameTime, Vector2 distance)
        {
            if (!DeatFlag)
            {
                shotUpdate(gameTime);

                if (Beinghit && recoverCounter <= recoverTime * 3 / 5)
                {
                    recoverCounter += gameTime.ElapsedGameTime.Milliseconds;
                    _visible ^= true;
                }
                else if(Beinghit && recoverCounter <= recoverTime)
                {
                    _visible = true;
                    recoverCounter += gameTime.ElapsedGameTime.Milliseconds;
                }
                else
                {
                    recoverCounter = 0;
                    Beinghit = false;
                }

                if (_destinationRectangle.Y > Utility.Stage.Y)
                {
                    lostLife();
                    _speed = Vector2.Zero;
                    if (Life == 0)
                        setDeatFlag();
                    else
                    {
                        _destinationRectangle.X -= 100;
                        _destinationRectangle.Y = 300;
                    }
                }

                move(distance);

                KeyboardState ks = Keyboard.GetState();

                if (ks.IsKeyDown(Keys.Z))
                {
                    extraSpeed = Math.Min(extraSpeed + 0.1f, 2);
                }
                else
                {
                    extraSpeed = Math.Max(extraSpeed - 0.1f, 1);
                }

                if (ks.IsKeyDown(Keys.Right))
                {
                    _speed.X = 3f * extraSpeed;
                    currentDirection = MoveDirection.RunningRight;
                }


                if (ks.IsKeyDown(Keys.Left))
                {
                    _speed.X = -3f * extraSpeed;
                    currentDirection = MoveDirection.RunningLeft;
                }

                if (ks.IsKeyUp(Keys.Left) || ks.IsKeyUp(Keys.Right))
                {
                    if (_speed.X > 0)
                    {
                        _speed.X -= 0.3f;
                    }
                    else
                    {
                        _speed.X += 0.3f;
                    }
                }

                if (ks.IsKeyUp(Keys.Right) && oldKey.IsKeyDown(Keys.Right))
                {
                    currentDirection = MoveDirection.Right;
                }

                if (ks.IsKeyUp(Keys.Left) && oldKey.IsKeyDown(Keys.Left))
                {
                    currentDirection = MoveDirection.Left;
                }
                

                // Jumping manangin part

                if (NowJumping)
                    _speed.Y += 0.3f;

                if (NowJumping && distance.Y == 0)
                {
                    NowJumping = (_speed.Y < 0);
                    _speed.Y = 0.3f;
                }

                if (oldKey.IsKeyDown(Keys.Up) && ks.IsKeyUp(Keys.Up))
                    _speed.Y = 0;
                else if (ks.IsKeyDown(Keys.Up))
                {
                    if (!NowJumping)
                    {
                        jumpStart();
                    }
                }

                // Shooting update part
                if (ks.IsKeyDown(Keys.Space))
                {
                    if (!isShot)
                    {
                        Bullet b = tryShot();
                        if (b != null)
                        {
                            ActionScreen.Bullets.Add(b);
                            AdventureGame.SoundsManager.Play(SoundTypes.Shot);
                        }
                    }
                }

                if (ActionScreen._stage == 0)
                    currentDirection = MoveDirection.RunningRight;
                else
                    _hasWeapon = true;
                oldKey = ks;
            }
            else
            {
                _speed.Y = Math.Max(_speed.Y + 0.5f, Values.JumpingSpeed);
                move(new Vector2(0, _speed.Y));
            }
            
        }

        public virtual Bullet tryShot()
        {
            if (_hasWeapon)
            {
                isShot = true;
                return new Bullet(_game.Content.Load<Texture2D>("bullets/playerBullet.fw"), BulletOwnerType.Player, this, currentDirection, getShotStartPosition());
            }
            return null;
        }

        public void move(Vector2 distance)
        {
            _destinationRectangle.X = Math.Max(_destinationRectangle.X + (int)distance.X, 0);
            _destinationRectangle.Y += (int)distance.Y;
        }

        private void hitEnemy()
        {
            if (!Beinghit)
                lostLife();
            //AdventureGame.SoundsManager.Play();
            setDeatFlag();
            Beinghit = true;
            recoverCounter = 0 ;
        }

        private void getLife()
        {
            Life = Math.Min(++Life, Values.MaxLife);
        }

        private void lostLife()
        {
            Life = Math.Max(--Life, 0);
        }

        private void getFruit()
        {
            if (++_havingFruits == Values.NumberOfFruitToGetLife)
            {
                getLife();
                _havingFruits -= Values.NumberOfFruitToGetLife;
            }
        }

        private void getItem(object other)
        {
            Item item = (Item)other;

            switch (item.ItemCategory)
            {
                case ItemType.Apple:
                    Score += Values.FruitScore;
                    getFruit();
                    break;
                case ItemType.Banana:
                    Score += Values.FruitScore;
                    getFruit();
                    break;
                case ItemType.Life:
                    Score += Values.LifeScore;
                    getLife();
                    break;
                default:
                    break;
            }
        }

        public override void handleCollision(CollisionType collisionType, object other)
        {
            switch (collisionType)
            {
                case CollisionType.PlayerWithEnemy:
                    hitEnemy();
                    break;
                case CollisionType.PlayerWithEnemyBullet:
                    hitEnemy();
                    break;
                case CollisionType.PlayerWithItem:
                    getItem(other);
                    break;
                case CollisionType.PlayerBulletWithEnemy:
                    break;
                case CollisionType.None:
                    break;
                default:
                    break;
            }
        }

        public void setSpeed(Vector2 speed)
        {
            _speed = speed;
        }
    }
}
