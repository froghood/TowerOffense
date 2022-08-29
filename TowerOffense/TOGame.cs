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
using TowerOffense.Extensions;
using System.IO;
using Microsoft.Xna.Framework.Audio;
using Newtonsoft.Json;

namespace TowerOffense {
    public class TOGame : Game {

        public static TOGame Instance;

        public static AssetManager Assets { get => Instance._assets; }
        public static SceneManager Scenes { get => Instance._scenes; }
        public static SpriteBatch SpriteBatch { get => Instance._spriteBatch; }
        public static Random Random { get => Instance._random; }
        public static PlayerManager PlayerManager { get => Instance._playerManager; }
        public static Settings Settings { get => Instance._settings; }

        public static Point DisplaySize {
            get => new Point() {
                X = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width,
                Y = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height
            };
        }

        private AssetManager _assets;
        private SceneManager _scenes;
        private Queue<Action> _commandQueue;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Random _random;
        private PlayerManager _playerManager;
        private Settings _settings;

        public TOGame() : base() {

            Instance = this;

            _assets = new AssetManager(Content);
            _scenes = new SceneManager();
            _commandQueue = new Queue<Action>();
            _graphics = new GraphicsDeviceManager(this);
            _random = new Random();
            _playerManager = new PlayerManager();

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
            _graphics.SynchronizeWithVerticalRetrace = false;

            var form = (Form)Form.FromHandle(Window.Handle);
            form.FormBorderStyle = FormBorderStyle.None;
            form.VisibleChanged += (_, _) => {
                if (form.Visible) form.Visible = false;
            };

            base.Initialize();
        }

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            var settingsJson = File.ReadAllText("./Settings.json");
            _settings = JsonConvert.DeserializeObject<Settings>(settingsJson);

            // sprites
            foreach (var path in Directory.GetFiles("./Content/Sprites", "*.png")) {
                System.Console.WriteLine(path);
                _assets.LoadTexture($"Sprites/{Path.GetFileNameWithoutExtension(path)}");
            }

            // sounds
            foreach (var path in Directory.GetFiles("./Content/Sounds", "*.mp3")) {
                _assets.LoadSound($"Sounds/{Path.GetFileNameWithoutExtension(path)}");
            }

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
