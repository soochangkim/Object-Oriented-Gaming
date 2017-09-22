using Microsoft.Xna.Framework;
using System.Collections.Generic;
using AdventureGame.Main.Characters;
using System;
using AdventureGame.Main.Items;
using AdventureGame.Main.Interfaces;
using MonogameLevel;
using System.Collections;
using AdventureGame.Main.Screens;

namespace AdventureGame.Main.GameManagers
{
    public class CollisionManager
    {
        private readonly Player _player;
        private readonly List<Enemy> _enemies;
        private readonly List<Bullet> _bullets;
        private readonly List<Item> _itmes;
        private List<Collision> _collisions;

        public CollisionManager(Player player,
            List<Enemy> enemies,
            List<Item> items,
            List<Bullet> bullets)
        {
            _player = player;
            _enemies = enemies;
            _bullets = bullets;
            _itmes = items;
            _collisions = new List<Collision>();
        }

        public void checkAllCollision()
        {
            if (_collisions.Count != 0)
                _collisions.Clear();
            CheckCollisions(_enemies, _player, CollisionType.PlayerWithEnemy);
            CheckCollisions(_itmes, _player, CollisionType.PlayerWithItem);
            checkCollisionWithBullets();
            handleAllCollision();
            CheckAddOn(_enemies, _player);
        }

        public void CheckAddOn(List<Enemy> enemies, Player p)
        {
            Bullet b;
            foreach (Enemy e in enemies)
            {
                if (e.searchPlayer(p))
                {
                    b = e.shot(AdventureGame.SoundsManager);
                    if (b != null)
                    {
                        b.setBulletRotation(Utility.getEnemyBulletRotation(p.getBoundToCheckCollision().Center, e.getBoundToCheckCollision().Center));
                        _bullets.Add(b);
                    }
                }
            }
        }

        public void checkCollisionWithBullets()
        {
            foreach (Bullet b in _bullets)
            {
                if (!b.isEnabled())
                    continue;

                if (b.BulletOwner == BulletOwnerType.Player)
                {
                    CheckCollisions(_enemies, b, CollisionType.PlayerBulletWithEnemy);
                }
                else
                {
                    checkCollisionPlayerWithEnemyBullet(b);
                }
            }
        }

        public void CheckCollisions<T1, T2>(List<T1> objects, T2 singleObject, CollisionType collsionType)
        {
            IGetBound bound1 = (IGetBound)singleObject;
            IGetBound bound2;

            foreach (T1 item in objects)
            {
                bound2 = (IGetBound)item;
                if (bound1.getBoundToCheckCollision().
                    Intersects(bound2.getBoundToCheckCollision()))
                {
                    _collisions.Add(new Collision(item, singleObject, collsionType));
                }
            }
        }

        private void checkCollisionPlayerWithEnemyBullet(Bullet b)
        {
            if (_player.getBoundToCheckCollision().
                Intersects(b.getBoundToCheckCollision()))

                _collisions.Add(
                    new Collision(
                        _player,
                        b,
                        CollisionType.PlayerWithEnemyBullet
                        ));
        }

        public void handleAllCollision()
        {
            foreach (Collision collision in _collisions)
            {
                collision.CollisionHandle();
            }
        }
    }
}
