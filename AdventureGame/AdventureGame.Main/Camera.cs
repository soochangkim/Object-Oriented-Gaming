﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureGame.Main
{
    public class Camera
    {
        private Matrix transform;
        public Matrix Transform
        {
            get
            {
                return transform;
            }
        }

        private Vector2 center;
        private Viewport viewport;

        private float zoom = 1f;
        private float rotation = 0;

        public float Left
        {
            get
            {
                return center.X - viewport.Width / 2;
            }
        }

        public float Right
        {
            get
            {
                return center.X + viewport.Width / 2;
            }
        }

        public float X
        {
            get
            {
                return center.X;
            }
            set
            {
                center.X = value;
            }
        }

        public float Y
        {
            get
            {
                return center.Y;
            }
            set
            {
                center.Y = value;
            }
        }

        public float Bottom
        {
            get
            {
                return center.Y + viewport.Height / 2;
            }
        }

        public float Top
        {
            get
            {
                return center.Y - viewport.Height / 2;
            }
        }

        public float Zoom
        {
            get
            {
                return zoom;
            }
            set
            {
                zoom = Math.Max(value, 0.1f);
            }
        }

        public float Rotation
        {
            get
            {
                return rotation;
            }
            set
            {
                rotation = value;
            }
        }

        public Camera(Viewport viewport)
        {
            this.viewport = viewport;
        }

        public void Update(Vector2 position)
        {
            center = new Vector2(position.X, position.Y);
            transform = Matrix.CreateTranslation(new Vector3(-center.X, -center.Y, 0)) *
                Matrix.CreateRotationZ(rotation) *
                Matrix.CreateScale(new Vector3(Zoom, Zoom, 0)) *
                Matrix.CreateTranslation(new Vector3(viewport.Width / 2, viewport.Height / 2, 0));
        }
    }
}
