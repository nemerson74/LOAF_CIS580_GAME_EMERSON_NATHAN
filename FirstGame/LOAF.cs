using FirstGame.Collisions;
using InputExample;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FirstGame
{
    public class LOAF : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private RedWorker redWorker;
        private Button startButton;
        private Button loadButton;
        private Button creditsButton;
        private BoundingPoint cursor;

        private SpriteFont friedolin;
        private SpriteFont exitText;
        private SpriteFont startText;
        private SpriteFont loadText;
        private SpriteFont creditsText;

        private InputManager inputManager;


        public LOAF()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //_graphics.PreferredBackBufferWidth = 1000; // Set your desired width
            //_graphics.PreferredBackBufferHeight = 1000; // Set your desired height
            //_graphics.ApplyChanges();
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            redWorker = new RedWorker() { Position = new Vector2(33, _graphics.GraphicsDevice.Viewport.Height / 2 - 15), Direction = Direction.Right };
            startButton = new Button() { Position = new Vector2(_graphics.GraphicsDevice.Viewport.Width/2 - 345, _graphics.GraphicsDevice.Viewport.Height / 2 - 90), Text = "Start" };
            loadButton = new Button() { Position = new Vector2(_graphics.GraphicsDevice.Viewport.Width / 2 - 245, _graphics.GraphicsDevice.Viewport.Height / 2 - 90), Text = "Load" };
            creditsButton = new Button() { Position = new Vector2(_graphics.GraphicsDevice.Viewport.Width / 2 - 145, _graphics.GraphicsDevice.Viewport.Height / 2 - 90), Text = "Credits" };
            inputManager = new InputManager();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            friedolin = Content.Load<SpriteFont>("friedolin");
            exitText = Content.Load<SpriteFont>("hamburger");
            startText = Content.Load<SpriteFont>("hamburger");
            loadText = Content.Load<SpriteFont>("hamburger");
            creditsText = Content.Load<SpriteFont>("hamburger");
            redWorker.LoadContent(Content);
            startButton.LoadContent(Content);
            loadButton.LoadContent(Content);
            creditsButton.LoadContent(Content);
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            inputManager.Update(gameTime);
            if (inputManager.Exit) Exit();

            
            redWorker.Update(gameTime);
            cursor = new BoundingPoint(inputManager.Position / 2);
            startButton.Update(cursor.CollidesWith(startButton.Bounds));
            loadButton.Update(cursor.CollidesWith(loadButton.Bounds));
            creditsButton.Update(cursor.CollidesWith(creditsButton.Bounds));
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkSlateGray);

            // TODO: Add your drawing code here
            _spriteBatch.Begin(transformMatrix: Matrix.CreateScale(2));
            redWorker.Draw(gameTime, _spriteBatch);
            startButton.Draw(_spriteBatch);
            loadButton.Draw(_spriteBatch);
            creditsButton.Draw(_spriteBatch);
            _spriteBatch.DrawString(friedolin, "Life of a Foreman", new Vector2(70, 80), Color.Goldenrod);
            _spriteBatch.DrawString(exitText, "Press ESC to Exit", new Vector2(20, 20), Color.Beige);
            _spriteBatch.DrawString(startText, "Start", new Vector2(_graphics.GraphicsDevice.Viewport.Width / 2 - 345 + 20, _graphics.GraphicsDevice.Viewport.Height / 2 - 90 + 15), Color.Black);
            _spriteBatch.DrawString(loadText, "Load", new Vector2(_graphics.GraphicsDevice.Viewport.Width / 2 - 245 + 22, _graphics.GraphicsDevice.Viewport.Height / 2 - 90 + 15), Color.Black);
            _spriteBatch.DrawString(creditsText, "Credits", new Vector2(_graphics.GraphicsDevice.Viewport.Width / 2 - 145 + 17, _graphics.GraphicsDevice.Viewport.Height / 2 - 90 + 15), Color.Black);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
