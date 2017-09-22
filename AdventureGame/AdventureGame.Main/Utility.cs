using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AdventureGame.Main
{
    public static class Utility 
    {
        public const int TICK_PER_SECOND = 32;
        public static Vector2 Stage { get; set; }
        public static SpriteFont HilightFont { get; set; }
        public static Random Random { get; set; } = new Random();
        public static void Initialize(GraphicsDeviceManager graphic)
        {
            Stage = new Vector2(graphic.PreferredBackBufferWidth, graphic.PreferredBackBufferHeight);
        }

        public static Vector2 GetCenter()
        {
            return new Vector2(Stage.X / 2, Stage.Y / 2);
        }
        public static double getDistance(Point a, Point b)
        {
            return Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
        }

        public static float getEnemyBulletRotation(Point p, Point e)
        {
            return (float)Math.Atan2(p.Y - e.Y, p.X - e.X);
        }

        public static Point getDist(Point p, Point e)
        {
            return new Point(p.X - e.X, p.Y - e.Y);
        }

        public static Vector2 getShotSpeed(Point p, Point e)
        {
            double hy = getDistance(p, e);
            Point dist = getDist(p, e);
            return new Vector2((float)(dist.X / hy), (float)(dist.Y / hy));
        }

        public static Vector2 getRandomSpeed(int min, int max, bool randomDirection = false)
        {
            if (randomDirection) { }
            return new Vector2(Random.Next(min, max), 0);
        }
    }
}
