using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureGame.Main.GameManagers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonogameLevel;

namespace AdventureGame.Main.Characters
{
    [Flags]
    public enum DirectionType
    {
        RightIdle,
        LeftIdle,
        RightWalk,
        LeftWalk,
        RightShot,
        LeftShot
    }

    class Soldier : Enemy
    {
        #region FramSize for Soldier Enemy
        private static Point[][] frameSizes = new Point[][]
       {
            new Point[] {
                new Point(31, 38), new Point(29, 38), new Point(29, 38)
            },
            new Point[] {
                new Point(29, 38), new Point(29, 38), new Point(31, 38)
            },
            new Point[] {
                new Point(32, 40), new Point(29, 40), new Point(33, 40),
                new Point(34, 40), new Point(31, 40), new Point(32, 40),
                new Point(32, 40), new Point(36, 40), new Point(31, 40),
            },
            new Point[] {
                new Point(31, 41), new Point(36, 41), new Point(35, 41),
                new Point(33, 41), new Point(29, 41), new Point(33, 41),
                new Point(33, 41), new Point(30, 41), new Point(31, 41),
            },
            new Point[] {
                new Point(51, 37), new Point(52, 37)
            },
            new Point[] {
                new Point(52, 37), new Point(51, 37)
            }
       };
        #endregion
        DirectionType _dTypeNow;
        DirectionType _dTypePrev;
        private int frameCounter;
        private int frameUpdateTime = 200;

        public Soldier(Game game,
            Texture2D spriteSheet)
            : base(game, spriteSheet)
        {
            int fSize = frameSizes.Length;
            numFrames = new int[fSize];
            Life = 5;
            _hasWeapon = true;
            moveFrames = new List<Rectangle>[fSize];
            setRandomDirection();
            _speed = Vector2.Zero;
            base.Initialize();
            _destinationRectangle.X = 500;
            _destinationRectangle.Y = 500;
        }


        protected override void AddFrames(int direction, List<Rectangle> moveFrames)
        {
            int x = 0;
            int y = 41 * direction;
            for (int i = 0; i < frameSizes[direction].Length; i++)
            {
                moveFrames.Add(new Rectangle(
                                x, y, frameSizes[direction][i].X,
                                frameSizes[direction][i].Y));
                x += frameSizes[direction][i].X;
            }
        }
        public override void Update(GameTime gameTime, Vector2 dist)
        {
            if (_enabled)
            {
                _shotCounter += gameTime.ElapsedGameTime.Milliseconds;
                if (_shotCounter > 500)
                {
                    _shotCounter = 0;
                    _isShot = false;
                }

                if (!_foundPlayer && !unmovableFrames())
                {
                    _speed.X = (movingLeftFrames()) ? -1f : 1f;
                }
                else
                {
                    _speed = Vector2.Zero;
                    return;
                }

                
                if (dist != Vector2.Zero)
                {
                    if (dist.Y > 5)
                    {
                        _dTypeNow = (movingLeftFrames()) ? DirectionType.RightWalk : DirectionType.LeftWalk;
                        _speed.X = -_speed.X;
                        _destinationRectangle.X += (int)_speed.X * 5;
                        idxFrame = updateIndexFrame();
                    }
                    else
                    {
                        _destinationRectangle.X += (int)dist.X;
                        _destinationRectangle.Y += (int)dist.Y;
                    }
                }

                tickCounter += gameTime.ElapsedGameTime.Milliseconds;
                if (tickCounter >= 6000)
                {
                    tickCounter = 0;
                    if (!_isFoundPlayer)
                    {
                        setRandomDirection();
                        idxFrame = updateIndexFrame();
                    }
                }
            }
        }
        public void setRandomDirection()
        {
            DirectionType dt = DirectionType.LeftIdle;
            if (Utility.Random.Next() % 2 == 0)
            {
                dt = (Utility.Random.Next() % 2 == 0) ? DirectionType.LeftIdle : DirectionType.LeftWalk;
            }
            else
            {
                dt = (Utility.Random.Next() % 2 == 0) ? DirectionType.RightIdle : DirectionType.RightWalk;
            }
            _dTypeNow = dt;
        }

        private bool unmovableFrames()
        {
            return (_dTypeNow == DirectionType.LeftIdle || _dTypeNow == DirectionType.RightIdle);
        }

        private bool movingLeftFrames()
        {
            return _dTypeNow == DirectionType.LeftWalk;
        }

        private bool movingRight()
        {
            return _dTypeNow == DirectionType.RightWalk;
        }

        private bool shotFrames()
        {
            return _dTypeNow == DirectionType.LeftShot || _dTypeNow == DirectionType.RightShot;
        }

        private int updateIndexFrame()
        {
            _destinationRectangle.Width = frameSizes[(int)_dTypeNow][idxFrame].X;
            _destinationRectangle.Height = frameSizes[(int)_dTypeNow][idxFrame].Y;

            if (_dTypeNow != _dTypePrev)
                return 0;
            return ++idxFrame % frameSizes[(int)_dTypeNow].Length;
        }

        public override Rectangle getBoundToCheckCollision()
        {
            return _destinationRectangle;
        }
        
        private void setIndexFram()
        {
            if (_dTypeNow != _dTypePrev)
                idxFrame = 0;
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (_visible == true)
            {         

                frameCounter += gameTime.ElapsedGameTime.Milliseconds;
                if(frameCounter > frameUpdateTime)
                {
                    frameCounter = 0;
                    idxFrame = updateIndexFrame();
                    _dTypePrev = _dTypeNow;
                }

                currentDirection = getMoveDirection();

                spriteBatch.Draw(
                    spriteSheet,
                    _destinationRectangle,
                    moveFrames[(int)_dTypeNow][idxFrame],
                    Color.White);
            }
        }

        public MoveDirection getMoveDirection()
        {
            int val = (int)_dTypeNow;
            if (val < 4)
                return (MoveDirection)(val * 2);
            return MoveDirection.None;
        }

        public override bool searchPlayer(Player p)
        {
            if (_isFoundPlayer = base.searchPlayer(p))
            {
                Point pPlayer = p.getBoundToCheckCollision().Center;
                Point pEnemy = getBoundToCheckCollision().Center;
                Point where = Utility.getDist(pPlayer, pEnemy);
                _shotSpeed = Utility.getShotSpeed(pPlayer, pEnemy);
                if (where.X < 0)
                {
                    _dTypeNow = DirectionType.LeftShot;
                    currentDirection = MoveDirection.Left;
                }
                else
                {
                    _dTypeNow = DirectionType.RightShot;
                    currentDirection = MoveDirection.Right;
                }
                setIndexFram();
            }
            return _isFoundPlayer;
        }
    }
}
