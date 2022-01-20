using System;
using System.Collections.Generic;
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

        public Cherry(Texture2D texture, Vector2 position, float layerDepth, int cellWidth, int cellHeight, Point sheetLocation, int totalRows, int totalColumns, int points, int id)
        {
            CellDimensions = new Point(cellWidth, cellHeight);
            Texture = texture;
            Position = position;
            LayerDepth = layerDepth;
            _sheetLocation = sheetLocation;
            _totalRows = totalRows;
            _totalColumns = totalColumns;
            ID = id;
        }

        public void Render(SpriteBatch spriteBatch, float rotation, Vector2 pointRotatedAround, Vector2 scale)
        {
            Render(spriteBatch, _sheetLocation, Position, rotation, scale, pointRotatedAround, LayerDepth);
        }

        public bool CheckIfLooped(List<Vector2> snakeList)
        {
            Vector2 center = new Vector2(Position.X + (.5f * CellDimensions.X), Position.Y + (.5f * CellDimensions.Y));

            // Check up, down, left and right to see if the center line intersects the snake
            // If all 4 center lines do, the cherry has been circled
            return false;
        }
    }
}
