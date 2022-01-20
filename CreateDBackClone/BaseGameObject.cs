using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CreateDBackClone
{
    public class BaseGameObject
    {
        public int ID { get; set; }
        public Point CellDimensions { get; set; }
        public Vector2 Position { get; set; }
        public bool Visible { get; set; }
        public bool Alive { get; set; }
        public Texture2D Texture { get; set; }
        public float LayerDepth { get; set; }

        public BaseGameObject()
        {
            Visible = true;
            Alive = true;
        }

        // Used without sprite sheets
        // Please note that color will usually be white and origin will usually be Vector2.Zero
        public virtual void Render(SpriteBatch spriteBatch, Vector2 position, Point point, Color color, float rotation, Vector2 origin, SpriteEffects spriteEffects, float layerDepth)
        {
            if (Visible)
            {
                Rectangle sourceRectangle = new Rectangle(point.X, point.Y, CellDimensions.X, CellDimensions.Y);
                Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, CellDimensions.X, CellDimensions.Y);
                spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, color, rotation, origin, spriteEffects, layerDepth);
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
                counterRectangle = new Rectangle(sheetLocation.X, sheetLocation.Y, CellDimensions.X, CellDimensions.Y);

            else
                counterRectangle = new Rectangle(sheetLocation.X * CellDimensions.X, sheetLocation.Y, CellDimensions.X, CellDimensions.Y);

            spriteBatch.Draw(Texture, position, counterRectangle, Color.White, rotation, pointRotatedAround, scale, SpriteEffects.None, layerDepth);
        }
    }
}
