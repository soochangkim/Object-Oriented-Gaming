using Microsoft.Xna.Framework;
using MonogameLevel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureGame.Main.GameManagers
{
    public static class MoveCheckHelper
    {
        public static bool CheckBottom(this Rectangle r1, Rectangle r2, Vector2 speed)
        {
            return (r1.Bottom + speed.Y >= r2.Top - 3 &&
                r1.Bottom + speed.Y <= r2.Top + (r2.Height * 2 / 3) &&
                r1.Right >= r2.Left + r2.Width / 10 &&
                r1.Left <= r2.Right - r2.Width / 10);
        }

        public static bool CheckTop(this Rectangle r1, Rectangle r2, Vector2 speed)
        {
            return (r1.Top + speed.Y <= r2.Bottom + (r2.Height / 2) &&
                r1.Top + speed.Y >= r2.Top - 3 &&
                r1.Right >= r2.Left + r2.Width / 10 &&
                r1.Left <= r2.Right - r2.Width / 10);
        }
        
        public static bool CheckRight(this Rectangle r1, Rectangle r2, Vector2 speed)
        {
            return (r1.Right + speed.X <= r2.Left + r2.Width / 3 &&
                r1.Right + speed.X >= r2.Left - 5 &&
                r1.Top <= r2.Bottom - r2.Height / 10 &&
                r1.Bottom >= r2.Top + r2.Height / 10);
        }

        public static bool CheckLeft(this Rectangle r1, Rectangle r2, Vector2 speed)
        {
            return (r1.Left + speed.X >= r2.Right - r2.Width / 3 &&
                r1.Left + speed.X <= r2.Right + 5 &&
                r1.Top <= r2.Bottom - r2.Height / 10 &&
                r1.Bottom >= r2.Top + r2.Height / 10);
        }
    }
}
