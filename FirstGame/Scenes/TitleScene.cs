using FirstGame.Collisions;
using FirstGame.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;

namespace FirstGame.Scenes;

public class TitleScene : Scene
{
    private SpriteBatch _spriteBatch;

    private RedWorker redWorker;
    private Button startButton;
    private Button loadButton;
    private Button creditsButton;
    private BoundingPoint cursor;

    private SpriteFont friedolin;
    private SpriteFont exitText;

    /// <summary>
    /// Initializes a new instance of the <see cref="TitleScene"/> class.
    /// </summary>
    /// <param name="game">The game instance that this scene is associated with.</param>
    public TitleScene(Game game) : base(game) { }

    public override void Initialize()
    {
        redWorker = new RedWorker() { Position = new Vector2(33, Game.GraphicsDevice.Viewport.Height / 2 - 15), Direction = Direction.Right };
        startButton = new Button() { Position = new Vector2(Game.GraphicsDevice.Viewport.Width / 2 - 345, Game.GraphicsDevice.Viewport.Height / 2 - 90), Text = "Start" };
        loadButton = new Button() { Position = new Vector2(Game.GraphicsDevice.Viewport.Width / 2 - 245, Game.GraphicsDevice.Viewport.Height / 2 - 90), Text = "Load" };
        creditsButton = new Button() { Position = new Vector2(Game.GraphicsDevice.Viewport.Width / 2 - 145, Game.GraphicsDevice.Viewport.Height / 2 - 90), Text = "Credits" };
        MediaPlayer.Play(LOAF.backgroundMusicTitle);
        MediaPlayer.IsRepeating = true;
        base.Initialize();
    }

    public override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(Game.GraphicsDevice);
        friedolin = Content.Load<SpriteFont>("friedolin");
        exitText = Content.Load<SpriteFont>("hamburger");
        redWorker.LoadContent(Content);
        startButton.LoadContent(Content);
        loadButton.LoadContent(Content);
        creditsButton.LoadContent(Content);
    }

    public override void Update(GameTime gameTime)
    {
        var LOAF = Game as LOAF;
        if (LOAF == null) return;
        redWorker.Update(gameTime);
        cursor = new BoundingPoint(LOAF.InputManager.Position / 2);
        startButton.Update(cursor.CollidesWith(startButton.Bounds));
        loadButton.Update(cursor.CollidesWith(loadButton.Bounds));
        creditsButton.Update(cursor.CollidesWith(creditsButton.Bounds));

        
        if (LOAF.InputManager.LeftMouseClicked)
        {
            if (startButton.Hover)
            {
                startButton.PlayClickSound();
                LOAF.ChangeScene(new MinigameSelectorScene(LOAF));
            }
            if (loadButton.Hover)
            {
                loadButton.PlayClickSound();
                //LOAF.ChangeScene(new LoadScene(LOAF));
            }
            if (creditsButton.Hover)
            {
                creditsButton.PlayClickSound();
                LOAF.ChangeScene(new CreditsScene(LOAF));
            }
        }
    }

    public override void Draw(GameTime gameTime)
    {
        Game.GraphicsDevice.Clear(Color.DarkSlateGray);

        _spriteBatch.Begin(transformMatrix: Matrix.CreateScale(2));

        redWorker.Draw(gameTime, _spriteBatch);
        startButton.Draw(_spriteBatch);
        loadButton.Draw(_spriteBatch);
        creditsButton.Draw(_spriteBatch);
        _spriteBatch.DrawString(friedolin, "Life of a Foreman", new Vector2(70, 80), Color.Goldenrod);
        _spriteBatch.DrawString(exitText, "Press ESC to Exit", new Vector2(20, 20), Color.Beige);

        _spriteBatch.End();
    }
}