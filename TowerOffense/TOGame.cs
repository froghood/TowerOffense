using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using Keys = Microsoft.Xna.Framework.Input.Keys;

using TowerOffense.Window;
using System;
using TowerOffense.Scenes;
using Microsoft.Xna.Framework.Content;

namespace TowerOffense {
    public class TOGame : Game {

        public static TOGame Instance;

        public static SceneManager Scenes { get => Instance._scenes; }
        public static SpriteBatch SpriteBatch { get => Instance._spriteBatch; }
        public static Random Random { get => Instance._random; }

        private SceneManager _scenes;
        private Queue<Action> _commandQueue;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Random _random;

        public TOGame() {

            Instance = this;

            _scenes = new SceneManager();
            _commandQueue = new Queue<Action>();
            _graphics = new GraphicsDeviceManager(this);
            _random = new Random();

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        /// <summary>
        /// A callback that is invoked at the beginning of the next game tick
        /// </summary>
        /// <param name="action"></param>
        public static void Command(Action action) {
            Instance._commandQueue.Enqueue(action);
        }

        protected override void Initialize() {
            base.Initialize();
        }

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime) {

            while (_commandQueue.Count > 0) {
                _commandQueue.Dequeue().Invoke();
            }

            Scenes.CurrentScene.Update(gameTime);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {

            _scenes.CurrentScene.Render(gameTime);

            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(Color.Black);

            base.Draw(gameTime);
        }
    }
}
