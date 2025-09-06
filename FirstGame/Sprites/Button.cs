using FirstGame.Collisions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstGame
{
    internal class Button
    {
        #region Private
        private Texture2D texture;
        private float scale = 0.5f;
        private bool hover;
        private string text = "Unset";
        private BoundingRectangle bounds = new BoundingRectangle(new Vector2(0, 0), 150, 90);
        #endregion
        #region Properties
        /// <summary>
        /// Where is the button?
        /// </summary>
        public Vector2 Position;

        /// <summary>
        /// True if cursor is over the button
        /// </summary>
        public bool Hover
        {
            set { hover = value; }
            get { return hover; }
        }

        /// <summary>
        /// Text for the button
        /// </summary>
        public string Text {
            get { return text; }
            set { text = value; }
        }

        /// <summary>
        /// The bounds of the button
        /// </summary>
        public BoundingRectangle Bounds
        {
            get { return bounds; }
        }
        #endregion

        /// <summary>
        /// Loads the button sprite texture
        /// </summary>
        /// <param name="content">the content manager to load with</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Button");

            bounds.Width = texture.Width * scale;
            bounds.Height = texture.Height * scale;
        }

        /// <summary>
        /// Update the button based on mouse location
        /// </summary>
        /// <param name="collision">is the mouse hovering?</param>
        public void Update(bool collision)
        {
            bounds.X = Position.X;
            bounds.Y = Position.Y;
            hover = collision;
        }

        /// <summary>
        /// Draws the button
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw with</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // draw the sprite
            //var source = new Rectangle((int)Position.X, (int)Position.Y, 150, 90);
            if (hover)
            {
                spriteBatch.Draw(texture, Position, null, Color.Green, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0);
                /*
                spriteBatch.Draw(texture,
                new Rectangle((int)bounds.X, (int)bounds.Y, (int)bounds.Width, (int)bounds.Height),
                Color.Red * 0.4f);
                */
            }
            else
            {
                spriteBatch.Draw(texture, Position, null, Color.White, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0);
            }
        }
    }
}
