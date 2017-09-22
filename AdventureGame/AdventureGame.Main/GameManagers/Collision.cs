using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureGame.Main.Interfaces;

namespace AdventureGame.Main.GameManagers
{
    [Flags]
    public enum CollisionType
    {
        PlayerWithEnemy,
        PlayerWithEnemyBullet,
        PlayerWithItem,
        PlayerBulletWithEnemy,
        None
    }

    public class Collision
    {
        public CollisionType CollsionType { get; set; }
        public object[] collisedObject;

        public Collision(
            object collise1,
            object collise2,
            CollisionType collsionType)
        {
            CollsionType = collsionType;
            collisedObject = new object[2] { collise1, collise2 };
        }

        public void CollisionHandle()
        {
            ICollisionHandler interfaceCollisionHandler;

            for (int i = 0; i < collisedObject.Length; i++)
            {
                interfaceCollisionHandler = (ICollisionHandler)collisedObject[i];
                interfaceCollisionHandler.handleCollision(CollsionType, collisedObject[i ^ 1]);
            }
        }
    }
}
