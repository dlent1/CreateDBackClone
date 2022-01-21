using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CreateDBackClone
{
    public class Snake: BaseGameObject
    {
        public List<Vector2> SnakeList { get; set; } // Element 0 will always be the head
        private int _snakeLength; // The number of segments in the snake

        public Snake(Texture2D texture, Vector2 position, float layerDepth, int cellWidth, int cellHeight, int snakeLength, int id)
        {
            Texture = texture;
            Texture.SetData(new[] { Color.White });
            Position = position;  // In this class Position will represent the position of the head
            LayerDepth = layerDepth;
            CellDimensions = new Point(cellWidth, cellHeight);  // These dimensions are the dimensions for each individual segment of the snake
            _snakeLength = snakeLength;
            SnakeList = new List<Vector2>();
            ID = id;

            CreateSnake();
        }

        public void Render(SpriteBatch spriteBatch, float rotation)
        {
            foreach(Vector2 snake in SnakeList)
                Render(spriteBatch, snake, Point.Zero, Color.White, rotation, Vector2.Zero, SpriteEffects.None, LayerDepth);
        }

        public void HandleGameInput()
        {
            Vector2 position = Position; // Can't use BaseGameObject Position as Vector2's can only be modified in place if they are a variable instead of a property
            GamePadCapabilities capabilities = GamePad.GetCapabilities(PlayerIndex.One);

            if (capabilities.GamePadType == GamePadType.GamePad && capabilities.IsConnected)
            {
                GamePadState state = GamePad.GetState(PlayerIndex.One);

                // XBox Controller Left thumb stick
                if (capabilities.HasLeftXThumbStick)
                    position.X += (int)(state.ThumbSticks.Left.X * 2.0f);

                if (capabilities.HasLeftYThumbStick)
                    position.Y -= (int)(state.ThumbSticks.Left.Y * 2.0f);

                Position = position;
                SnakeList.RemoveAt(_snakeLength - 1);  // Remove the tail from the list
                SnakeList.Insert(0, Position);  // Add a new head to the front of the list
            }   
        }

        public bool CheckForCollisionWithOther(List<BaseGameObject> gameObjects)
        {
            foreach (BaseGameObject gameObject in gameObjects)
            {
                if (gameObject is Cherry)
                {
                    if (((Cherry)gameObject).Alive)
                    {
                        if (RectangleToRectangleCollision(gameObject))
                        {
                            if (PerPixelCollision(gameObject))
                                return true;
                        }
                    } 
                }
            }

            return false;
        }

        public bool CheckForCollisionWithSelf()
        {
            for (int i = _snakeLength / 2; i < _snakeLength; i++)
            {
                BaseGameObject sprite = new BaseGameObject();
                sprite.CellDimensions = CellDimensions;
                sprite.Position = SnakeList[i];

                if (RectangleToRectangleCollision(sprite))
                    return true;
            }
            
            return false;
        }

        private bool RectangleToRectangleCollision(BaseGameObject sprite)
        {
            if (sprite.Position.X + sprite.CellDimensions.X >= SnakeList[0].X
                && sprite.Position.X <= SnakeList[0].X + CellDimensions.X
                && sprite.Position.Y + sprite.CellDimensions.Y >= SnakeList[0].Y
                && sprite.Position.Y <= SnakeList[0].Y + CellDimensions.Y)
                return true;

            return false;
        }

        private bool PerPixelCollision(BaseGameObject sprite)
        {
            // Get Color data of each Texture
            // All snake pixels are black, so we don't need to create a color array for it.
            Color[] cherryColors = new Color[sprite.CellDimensions.X * sprite.CellDimensions.Y];
            sprite.Texture.GetData(0, new Rectangle(0, 0, sprite.CellDimensions.X, sprite.CellDimensions.Y), cherryColors, 0, sprite.CellDimensions.X * sprite.CellDimensions.Y);

            // Calculate the intersecting rectangle
            int x1 = (int)(Math.Max(Position.X, sprite.Position.X));
            int x2 = (int)(Math.Min(Position.X + CellDimensions.X, sprite.Position.X + sprite.CellDimensions.X));

            int y1 = (int)(Math.Max(Position.Y, sprite.Position.Y));
            int y2 = (int)(Math.Min(Position.Y + CellDimensions.Y, sprite.Position.Y + sprite.CellDimensions.Y));

            // For each single pixel in the intersecting rectangle
            for (int y = y1; y < y2; ++y)
            {
                for (int x = x1; x < x2; ++x)
                {
                    // Get the color from the texture
                    Color colorB = cherryColors[(x - (int)sprite.Position.X) + (y - (int)sprite.Position.Y) * sprite.CellDimensions.X];

                    if (colorB.A != 0) // If the intersected pixel in the cherry is not transparent
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private void CreateSnake()
        {
            for (int i = 0; i < _snakeLength; i++)
                SnakeList.Add(new Vector2(Position.X, Position.Y));
        }
    }
}
