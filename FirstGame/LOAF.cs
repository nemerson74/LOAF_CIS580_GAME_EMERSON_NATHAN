using FirstGame.Collisions;
using FirstGame.Scenes;
using InputExample;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FirstGame
{
    public class LOAF : Game
    {
        private GraphicsDeviceManager _graphics;

        /// <summary>
        /// Input Manager to pass to the scenes.
        /// </summary>
        public InputManager InputManager { get; private set; }

        // The scene that is currently active.
        private static Scene s_activeScene;

        // The next scene to switch to, if there is one.
        private static Scene s_nextScene;


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

            InputManager = new InputManager();
            base.Initialize();
            // Start the game with the title scene.
            ChangeScene(new TitleScene(this));
        }

        protected override void LoadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            InputManager.Update(gameTime);
            if (InputManager.Exit) Exit();

            // if there is a next scene waiting to be switch to, then transition
            // to that scene.
            if (s_nextScene != null)
            {
                TransitionScene();
            }

            // If there is an active scene, update it.
            if (s_activeScene != null)
            {
                s_activeScene.Update(gameTime);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkSlateGray);

            // If there is an active scene, draw it.
            if (s_activeScene != null)
            {
                s_activeScene.Draw(gameTime);
            }
            base.Draw(gameTime);
        }

        public static void ChangeScene(Scene next)
        {
            // Only set the next scene value if it is not the same
            // instance as the currently active scene.
            if (s_activeScene != next)
            {
                s_nextScene = next;
            }
        }

        private static void TransitionScene()
        {
            // If there is an active scene, dispose of it.
            if (s_activeScene != null)
            {
                s_activeScene.Dispose();
            }

            // Force the garbage collector to collect to ensure memory is cleared.
            GC.Collect();

            // Change the currently active scene to the new scene.
            s_activeScene = s_nextScene;

            // Null out the next scene value so it does not trigger a change over and over.
            s_nextScene = null;

            // If the active scene now is not null, initialize it.
            // Remember, just like with Game, the Initialize call also calls the
            // Scene.LoadContent
            if (s_activeScene != null)
            {
                s_activeScene.Initialize();
            }
        }
    }
}
