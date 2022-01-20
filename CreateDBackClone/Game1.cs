using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CreateDBackClone
{
    public class Game1 : Game
    {
        private const int CHERRYWIDTH = 72;
        private const int CHERRYHEIGHT = 72;
        private const int CHERRYPOINTS = 50;
        private const int SNAKEWIDTH = 2;
        private const int SNAKEHEIGHT = 2;
        private const int SNAKELENGTH = 250;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private List<SoundEffect> _soundEffects;
        private List<BaseGameObject> _gameObjects;
        private Vector2 _snakeStartPosition;
        private int _nextID;
        
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _soundEffects = new List<SoundEffect>();
            _gameObjects = new List<BaseGameObject>();
            _snakeStartPosition = new Vector2(600, 150);
            _nextID = 0;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _soundEffects.Add(Content.Load<SoundEffect>("blip"));
            _soundEffects.Add(Content.Load<SoundEffect>("Blip2"));

            _gameObjects.Add(new Snake(new Texture2D(_graphics.GraphicsDevice, 1, 1, false, SurfaceFormat.Color), _snakeStartPosition, 0.5f, SNAKEWIDTH, SNAKEHEIGHT, SNAKELENGTH, 0));
            _gameObjects.Add(new Cherry(Content.Load<Texture2D>("cherrySpriteSheet"), new Vector2(300, 300), 0.5f, CHERRYWIDTH, CHERRYHEIGHT, Point.Zero, 1, 2, CHERRYPOINTS, 1));

            _nextID = 2;

            _soundEffects[0].Play();
            
        }

        protected override void Update(GameTime gameTime)
        {
            Snake snake = ((Snake)_gameObjects[0]);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            snake.HandleGameInput();

            if (snake.CheckForCollisionWithOther(_gameObjects))
                _soundEffects[1].Play();

            if (snake.CheckForCollisionWithSelf())
            {
                foreach (BaseGameObject gameObject in _gameObjects)
                {
                    if (gameObject is Cherry && gameObject.Alive)
                        ((Cherry)gameObject).CheckIfLooped(snake.SnakeList);
                }
            }    

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(SpriteSortMode.FrontToBack);

            foreach (BaseGameObject gameObject in _gameObjects)
            {
                if (gameObject is Cherry)
                    ((Cherry)gameObject).Render(_spriteBatch, 0.0f, Vector2.Zero, Vector2.One);

                if (gameObject is Snake)
                    ((Snake)gameObject).Render(_spriteBatch, 0.0f);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
