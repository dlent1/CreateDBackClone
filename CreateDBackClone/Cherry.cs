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
        private int _timeLeft; // The amount of time to display the point value after the cherry is killed

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
            _timeLeft = 60;
        }

        public void Update()
        {
            if (!Alive && _timeLeft > 0)
                _timeLeft--;

            if (!Alive && _timeLeft == 0)
                Visible = false;
        }

        public void Render(SpriteBatch spriteBatch, float rotation, Vector2 pointRotatedAround, Vector2 scale)
        {
            Render(spriteBatch, _sheetLocation, Position, rotation, scale, pointRotatedAround, LayerDepth);
        }

        // Check up, down, left and right to see if the center line intersects the snake
        // If all 4 center lines do, the cherry has been circled
        public bool CheckIfLooped(List<Vector2> snakeList, int screenWidth, int screenHeight)
        {
            Vector2 center = new Vector2(Position.X + (.5f * CellDimensions.X), Position.Y + (.5f * CellDimensions.Y));

            if (CheckAbove(snakeList, center))
            {
                if (CheckBelow(snakeList, center, screenHeight))
                {
                    if (CheckLeft(snakeList, center))
                    {
                        if (CheckRight(snakeList, center, screenWidth))
                        {
                            _sheetLocation = new Point(_sheetLocation.X + CellDimensions.X, _sheetLocation.Y);
                            Alive = false;
                            return true;
                        }    
                    }
                }
            }
            
            return false;
        }

        private bool CheckAbove(List<Vector2> snakeList, Vector2 center)
        {
            for (int i = (int)center.Y; i >= 0; i--)
            {
                foreach (Vector2 segment in snakeList)
                {
                    if (center.X <= segment.X + 1 && center.X >= segment.X - 1 && 
                        i <= segment.Y + 1 && i >= segment.Y - 1)
                        return true;
                }
            }

            return false;
        }

        private bool CheckBelow(List<Vector2> snakeList, Vector2 center, int screenHeight)
        {
            for (int i = (int)center.Y; i <= screenHeight; i++)
            {
                foreach (Vector2 segment in snakeList)
                {
                    if (center.X <= segment.X + 1 && center.X >= segment.X - 1 &&
                        i <= segment.Y + 1 && i >= segment.Y - 1)
                        return true;
                }
            }

            return false;
        }

        private bool CheckLeft(List<Vector2> snakeList, Vector2 center)
        {
            for (int i = (int)center.X; i >= 0; i--)
            {
                foreach (Vector2 segment in snakeList)
                {
                    if (center.Y <= segment.Y + 1 && center.Y >= segment.Y - 1 &&
                        i <= segment.X + 1 && i >= segment.X - 1)
                        return true;
                }
            }

            return false;
        }

        private bool CheckRight(List<Vector2> snakeList, Vector2 center, int screenWidth)
        {
            for (int i = (int)center.X; i <= screenWidth; i++)
            {
                foreach (Vector2 segment in snakeList)
                {
                    if (center.Y <= segment.Y + 1 && center.Y >= segment.Y - 1 &&
                        i <= segment.X + 1 && i >= segment.X - 1)
                        return true;
                }
            }

            return false;
        }
    }
}
