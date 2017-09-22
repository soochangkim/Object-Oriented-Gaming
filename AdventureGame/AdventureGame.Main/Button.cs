using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using AdventureGame.Main.Screens;

namespace AdventureGame.Main
{
    public class Button
    {
        private Vector2 btnPosition;
        private Rectangle btnRectangle;
        private Vector2 btnOrigin;
        private string btnMessage;
        private float btnRotation = 0.0f;
        private float btnScale = 2.0f;
        private bool isClicked = false;
        private bool isVisible = false;
        public Vector2 btnSize;
        private Color color = Color.Black;

        public Button()
        {
        }

        public void LoadContent(string btnMessage, GraphicsDevice graphics, SpriteFont spriteFont)
        {
            this.btnMessage = btnMessage;
            btnSize = spriteFont.MeasureString(this.btnMessage);
            btnOrigin = new Vector2(btnSize.X / 2, btnSize.Y / 2);
        }

        public void Update(MouseState mouse)
        {
            Rectangle mouseRectangle = new Rectangle(mouse.X + (int)ActionScreen.camera.Left, mouse.Y + (int)ActionScreen.camera.Top, 1, 1);

            btnRectangle = new Rectangle(
                (int)(btnPosition.X  - btnOrigin.X * btnScale), 
                (int)(btnPosition.Y - btnOrigin.Y * btnScale), 
                (int)(btnSize.X * btnScale), 
                (int)(btnSize.Y * btnScale));
            if (btnMessage == "Paused" || btnMessage == "Game Over")
            {
                color = Color.Red;
                return;
            }
            if (mouseRectangle.Intersects(btnRectangle))
            {
                if (ButtonState.Pressed == mouse.LeftButton)
                    isClicked = true;
                else
                    color = Color.Red;
            }
            else
            {
                isClicked = false;
                color = Color.Black;
            }
                
        }

        public bool IsClicked()
        {
            return isClicked;
        }
        public bool IsVisible()
        {
            return isVisible;
        }
        public void setPosition(Vector2 position)
        {
            btnPosition = position;
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont spriteFont)
        {
            btnSize = spriteFont.MeasureString(this.btnMessage);
            btnOrigin = new Vector2(btnSize.X / 2, btnSize.Y / 2);
            spriteBatch.DrawString(spriteFont, btnMessage, btnPosition , color, 
                btnRotation, btnOrigin, btnScale, SpriteEffects.None, 0.0f);
        }

        public void setVisible(bool v)
        {
            isVisible = v;
        }

        public void setIsClicked(bool v)
        {
            isClicked = v;
        }

        public void setColor(Color color)
        {
            this.color = color;
        }

        public void setMessage(string btnMessage)
        {
            this.btnMessage = btnMessage;
        }
    }
}
