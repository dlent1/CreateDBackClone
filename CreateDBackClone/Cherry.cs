using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CreateDBackClone
{
    public class Cherry: BaseGameObject
    {
        public Point CellDimensions
        {
            get { return _cellDimensions; }
        }

        private int _totalRows;
        private int _totalColumns;
        private int _points;
        private Point _sheetLocation;

        public Cherry(Texture2D texture, Vector2 position, float layerDepth, int cellWidth, int cellHeight, Point sheetLocation, int totalRows, int totalColumns, int points)
        {
            _cellDimensions = new Point(cellWidth, cellHeight);
            _texture = texture;
            Position = position;
            LayerDepth = layerDepth;
            _sheetLocation = sheetLocation;
            _totalRows = totalRows;
            _totalColumns = totalColumns;
        }

        public void Render(SpriteBatch spriteBatch, float rotation, Vector2 pointRotatedAround, Vector2 scale)
        {
            Render(spriteBatch, _sheetLocation, Position, rotation, scale, pointRotatedAround, LayerDepth);
        }
    }
}
