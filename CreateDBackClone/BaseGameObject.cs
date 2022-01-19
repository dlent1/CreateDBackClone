using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CreateDBackClone
{
    public class BaseGameObject
    {
        public Vector2 Position { get; set; }
        public bool Visible { get; set; }
        public float LayerDepth { get; set; }
        protected Texture2D _texture;
        protected Point _cellDimensions; // The width and height of the cell


        public BaseGameObject()
        {
            Visible = true;
        }

        // Used without sprite sheets
        // Please note that color will usually be white and origin will usually be Vector2.Zero
        public virtual void Render(SpriteBatch spriteBatch, Vector2 position, Point point, Color color, float rotation, Vector2 origin, SpriteEffects spriteEffects, float layerDepth)
        {
            if (Visible)
            {
                Rectangle sourceRectangle = new Rectangle(point.X, point.Y, _cellDimensions.X, _cellDimensions.Y);
                Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, _cellDimensions.X, _cellDimensions.Y);
                spriteBatch.Draw(_texture, destinationRectangle, sourceRectangle, color, rotation, origin, spriteEffects, layerDepth);
            }
        }

        // This is used with text
        public virtual void Render(SpriteBatch spriteBatch, SpriteFont font, Vector2 position, Color textColor, string text, float scale, float layerDepth)
        {
            if (text != null)
                spriteBatch.DrawString(font, text, position, textColor, 0.0f, Vector2.Zero, scale, SpriteEffects.None, layerDepth);
        }

        // This is used with sprite sheets
        public virtual void Render(SpriteBatch spriteBatch, Point sheetLocation, Vector2 position, float rotation, Vector2 scale, Vector2 pointRotatedAround, float layerDepth)
        {
            Rectangle counterRectangle;

            if (sheetLocation.X == 0)
                counterRectangle = new Rectangle(sheetLocation.X, sheetLocation.Y, _cellDimensions.X, _cellDimensions.Y);

            else
                counterRectangle = new Rectangle(sheetLocation.X * _cellDimensions.X, sheetLocation.Y, _cellDimensions.X, _cellDimensions.Y);

            spriteBatch.Draw(_texture, position, counterRectangle, Color.White, rotation, pointRotatedAround, scale, SpriteEffects.None, layerDepth);
        }
    }
}
