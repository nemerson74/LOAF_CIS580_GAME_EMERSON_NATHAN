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
    Texture2D nailTexture;
    Texture2D woodTexture;

    private Vector2 anchor;
    private Vector2 prevAnchor;
    private float angle = 0.5f;
    private float angularVelocity;
    private float angularAcceleration;

    //hammer physics tuning
    private float gravity = 100f;
    private float damping = 0.995f;

    private float massHandle = 0.5f;
    private float massHead = 3.0f;

    private float rHandle = 9f;
    private float rHead = 14f;

    private float maxVelocity = 12f;

    private float clickTorqueStrength = 500000f;

    public CarpentryScene(Game game) : base(game) { }

    public override void Initialize()
    {
        base.Initialize();
        prevAnchor = Vector2.Zero;
    }

    public override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(Game.GraphicsDevice);
        hammerTexture = Content.Load<Texture2D>("hammer");
        nailTexture = Content.Load<Texture2D>("nail");
        woodTexture = Content.Load<Texture2D>("wood");
    }

    public override void Update(GameTime gameTime)
    {
        var loaf = Game as LOAF;
        if (loaf == null) return;
        var input = loaf.InputManager;

        //follow the mouse
        anchor = input.Position / 2f;

        //delta time
        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (dt <= 0f) dt = 1f / 60f;

        // (m * g * r) for handle and head depending on angle
        float gravityTorque = -((massHandle * gravity * rHandle) + (massHead * gravity * rHead)) * (float)Math.Sin(angle);

        // (m * r^2) for handle and head
        float inertia = (massHandle * rHandle * rHandle) + (massHead * rHead * rHead);
        if (inertia <= 0.0001f) inertia = 0.0001f;

        //mouse button control for torque
        float clickTorque = 0f;
        if (input.LeftMouseDown)
        {
            clickTorque += clickTorqueStrength * dt;
        }
        if (input.RightMouseDown)
        {
            clickTorque -= clickTorqueStrength * dt;
        }

        // total acceleration from mouse and gravity
        angularAcceleration = (clickTorque + gravityTorque) / inertia;

        // integrate angular motion
        angularVelocity += angularAcceleration * dt;
        angularVelocity = MathHelper.Clamp(angularVelocity, -maxVelocity, maxVelocity);
        angle += angularVelocity * dt;

        // damping
        angularVelocity *= damping;

        // Return to title with Escape
        if (input.IsKeyDown(Keys.Escape))
        {
            LOAF.ChangeScene(new TitleScene(loaf));
            return;
        }
    }

    public override void Draw(GameTime gameTime)
    {
        Game.GraphicsDevice.Clear(Color.DarkSlateGray);
        _spriteBatch.Begin(transformMatrix: Matrix.CreateScale(2), samplerState: SamplerState.PointClamp);

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

        _spriteBatch.Draw(
            woodTexture,
            new Vector2(
                Game.GraphicsDevice.Viewport.Width / 2 - 100,
                Game.GraphicsDevice.Viewport.Height / 2 - 60
                ),
            null,
            Color.White,
            0f,
            Vector2.Zero,
            6f,
            SpriteEffects.None,
            0f
        );

        _spriteBatch.Draw(
            nailTexture,
            new Vector2(
                Game.GraphicsDevice.Viewport.Width / 2 - 140,
                Game.GraphicsDevice.Viewport.Height / 2 - 80
            ),
            null,
            Color.White,
            0f,
            Vector2.Zero,
            3f,
            SpriteEffects.None,
            0f
        );

        SpriteFont font = Content.Load<SpriteFont>("hamburger");
        float fontScale = 0.75f;
        _spriteBatch.DrawString(font, "ESC to return to title", new Vector2(20, 0), Color.Yellow, 0f, Vector2.Zero, fontScale, SpriteEffects.None, 0f);

        _spriteBatch.End();
    }
}