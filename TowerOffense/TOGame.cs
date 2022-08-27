using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using Keys = Microsoft.Xna.Framework.Input.Keys;

using System;
using TowerOffense.Scenes;
using Microsoft.Xna.Framework.Content;

namespace TowerOffense {
    public class TOGame : Game {

        public static TOGame Instance;

        public static AssetManager Assets { get => Instance._assets; }
        public static SceneManager Scenes { get => Instance._scenes; }
        public static SpriteBatch SpriteBatch { get => Instance._spriteBatch; }
        public static Random Random { get => Instance._random; }
        public static PlayerManager PlayerManager { get => Instance._playerManager; }

        private AssetManager _assets;
        private SceneManager _scenes;
        private Queue<Action> _commandQueue;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Random _random;
        private PlayerManager _playerManager;

        public TOGame() : base() {

            Instance = this;

            _assets = new AssetManager(Content);
            _scenes = new SceneManager();
            _commandQueue = new Queue<Action>();
            _graphics = new GraphicsDeviceManager(this);
            _random = new Random();
            _playerManager = PlayerManager.Instance;

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


            IsFixedTimeStep = false;

            var form = (Form)Form.FromHandle(Window.Handle);
            form.FormBorderStyle = FormBorderStyle.None;
            form.VisibleChanged += (_, _) => {
                if (form.Visible) form.Visible = false;
            };

            base.Initialize();
        }

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // prob should make something more dynamic
            _assets.LoadTexture("Sprites/Title");
            _assets.LoadTexture("Sprites/PlayButton");
            _assets.LoadTexture("Sprites/PlayButtonHover");
            _assets.LoadTexture("Sprites/Close");
            _assets.LoadTexture("Sprites/TowerTargetArrow");
            _assets.LoadTexture("Sprites/GravityTower1");
            _assets.LoadTexture("Sprites/GravityTower2");
            _assets.LoadTexture("Sprites/GravityTowerAttack");

            base.LoadContent();
        }

        protected override void Update(GameTime gameTime) {

            while (_commandQueue.Count > 0) _commandQueue.Dequeue().Invoke();

            Scenes.CurrentScene.Update(gameTime);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {

            _scenes.CurrentScene.Render(gameTime);
            GraphicsDevice.SetRenderTarget(null);

            base.Draw(gameTime);
        }
    }
}
