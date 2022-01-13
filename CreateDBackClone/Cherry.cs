using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CreateDBackClone
{
    public class Cherry: BaseGameObject
    {
        private int _totalRows;
        private int _totalColumns;
        private int _points;
        private Point _sheetLocation;

        public Cherry(Texture2D texture, Vector2 position, float layerDepth, int cellWidth, int cellHeight, Point sheetLocation)
        {
            _cellDimensions = new Point(cellWidth, cellHeight);
            _texture = texture;
            Position = position;
            LayerDepth = layerDepth;
            _sheetLocation = sheetLocation;
        }

        public void Render(SpriteBatch spriteBatch, float rotation, Vector2 scale, Vector2 pointRotatedAround)
        {
            Render(spriteBatch, _sheetLocation, Position, rotation, scale, pointRotatedAround, LayerDepth);
        }
    }
}
