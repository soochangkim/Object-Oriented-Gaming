using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using AdventureGame.Main.Screens;
using AdventureGame.Main.Interfaces;

namespace AdventureGame.Main.Characters
{
    class ScrollingEnemy : Enemy
    {
        public static int numOfEnemy;
        public static int generationInterval = 500;
        public static int generationCounter = 0;
        private readonly Player player;
        private bool passed = false;
        public ScrollingEnemy(
            Game game,
            Texture2D spriteSheet,
            Player p,
            int tickToMove
            )
            : base(game, spriteSheet)
        {
            numOfEnemy++;
            _tickToMove = tickToMove;
            _speed = -Utility.getRandomSpeed(3, 9);
            _destinationRectangle.X = (int)ActionScreen.camera.Right;
            _destinationRectangle.Y = p._destinationRectangle.Y - 7;
            numFrames = new int[] { 4 };
            sizeFrame = new Point(114, 85);
            player = p;
            base.Initialize();
        }

        public override Rectangle getBoundToCheckCollision()
        {
            return _destinationRectangle;
        }

        public override void Update(GameTime gameTime, Vector2 dist)
        {
            if (_enabled)
            {
                if (_destinationRectangle.X + _destinationRectangle.Width < ActionScreen.camera.Left || dist.X == 0)
                {
                    numOfEnemy--;
                    hide();
                }

                if (player._destinationRectangle.X > _destinationRectangle.X && !passed)
                {
                    player.Score += 100;
                    passed = true;
                }
                _destinationRectangle.Y += (player._destinationRectangle.Y - 5 > _destinationRectangle.Y) ? 1 : -1;
                _destinationRectangle.X += (int)dist.X;
            }
        }
    }
}
