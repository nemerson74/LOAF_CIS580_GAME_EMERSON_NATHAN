using FirstGame.Collisions;
using FirstGame.Scenes;
using InputExample;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.IO;
using System.Reflection.Metadata;

namespace FirstGame.Scenes;

public class CarpentryScene : Scene
{
    private SpriteBatch _spriteBatch;

    //private BoundingPoint cursor;

    Texture2D hammerTexture;
    private Vector2 anchor;
    private Vector2 prevAnchor;
    private float angle = 0.5f;
    private float angularVelocity;
    private float angularAcceleration;
    private float length = 8f;
    private float gravity = 500f;

    public CarpentryScene(Game game) : base(game) { }

    public override void Initialize()
    {
        base.Initialize();
    }

    public override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(Game.GraphicsDevice);
        hammerTexture = Content.Load<Texture2D>("hammer");
    }

    public override void Update(GameTime gameTime)
    {
        var loaf = Game as LOAF;
        if (loaf == null) return;
        var input = loaf.InputManager;

        prevAnchor = anchor;
        anchor = input.Position / 2;

        //cursor = new BoundingPoint(input.Position / 2);
        anchor = input.Position / 2;
        Vector2 mouseVelocity = (anchor - prevAnchor) / (float)gameTime.ElapsedGameTime.TotalSeconds;
        angularAcceleration = -(gravity / length) * (float)Math.Sin(angle);
        angularVelocity += angularAcceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;
        Vector2 hammerDir = new Vector2((float)Math.Sin(angle), (float)Math.Cos(angle));
        Vector2 tangent = new Vector2(hammerDir.Y, -hammerDir.X);

        float tangentialSpeed = Vector2.Dot(mouseVelocity, tangent);

        angularVelocity += tangentialSpeed * 0.001f;
        angle += angularVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        angularVelocity *= 0.99f;
        // Right click to return
        if (input.PreviousRightMouseState == ButtonState.Released && input.CurrentRightMouseState == ButtonState.Pressed)
        {
            LOAF.ChangeScene(new TitleScene(loaf));
            return;
        }

        // Left click to select
        /*
        if (input.LeftMouseClicked)
        {
            if (carpentryButton.Hover)
            {
                carpentryButton.PlayClickSound();
                //LOAF.ChangeScene(new CarpentryScene(loaf));
            }
        }
        */
    }

    public override void Draw(GameTime gameTime)
    {
        Game.GraphicsDevice.Clear(Color.DarkSlateGray);
        _spriteBatch.Begin(transformMatrix: Matrix.CreateScale(2));

        Rectangle sourceRect = new Rectangle(0, 0, 16, 16);
        Vector2 origin = new Vector2(7, 15); // bottom center of 16x16 for handle anchor
        _spriteBatch.Draw(
            hammerTexture,
            anchor,
            sourceRect,
            Color.White,
            angle - MathF.PI,
            origin,
            5f,
            SpriteEffects.None,
            0f
        );

        SpriteFont font = Content.Load<SpriteFont>("hamburger");
        float fontScale = 0.75f;
        _spriteBatch.DrawString(font, "", new Vector2(20, 0), Color.Yellow, 0f, Vector2.Zero, fontScale, SpriteEffects.None, 0f);

        _spriteBatch.End();
    }
}