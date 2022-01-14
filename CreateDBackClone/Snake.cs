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

        public Snake(Texture2D texture, Vector2 position, float layerDepth, int cellWidth, int cellHeight, int snakeLength)
        {
            _texture = texture;
            _texture.SetData(new[] { Color.White });
            Position = position;  // In this class Position will represent the position of the head
            LayerDepth = layerDepth;
            _cellDimensions = new Point(cellWidth, cellHeight);  // These dimensions are the dimensions for each individual segment of the snake
            _snakeLength = snakeLength;
            SnakeList = new List<Vector2>();

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
                    position.X += state.ThumbSticks.Left.X * 10.0f;

                if (capabilities.HasLeftYThumbStick)
                    position.Y -= state.ThumbSticks.Left.Y * 10.0f;

                Position = position;
                SnakeList.Insert(0, Position);  // Add a new head to the front of the list
                SnakeList.RemoveAt(_snakeLength - 1);  // Remove the tail from the list
            }   
        }

        private void CreateSnake()
        {
            for (int i = 0; i < _snakeLength; i++)
            {
                if (i == 0)
                    SnakeList.Add(new Vector2(Position.X, Position.Y));
                else
                    SnakeList.Add(new Vector2(Position.X, Position.Y + (i * _cellDimensions.X)));
            }
        }
    }
}
