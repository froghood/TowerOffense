using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using Keys = Microsoft.Xna.Framework.Input.Keys;

using TowerOffense.Window;
using System;

namespace TowerOffense {
    public class TOGame : Game {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private List<TOWindow> _windows;

        private Random _random;

        public TOGame() {
            _graphics = new GraphicsDeviceManager(this);
            _windows = new List<TOWindow>();

            _random = new Random();

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize() {

            for (int i = 0; i < 25; i++) {
                var window = new TOWindow(this, 120, 120);
                window.Position = new Point(
                    _random.Next(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - 120),
                    _random.Next(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - 120)
                );
                _windows.Add(window);
            }

            base.Initialize();
        }

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // window movement test
            foreach (var window in _windows) {
                window.Position = window.Position += new Point(_random.Next(-1, 2), _random.Next(-1, 2));
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {

            foreach (var window in _windows) {
                GraphicsDevice.SetRenderTarget(window.RenderTarget);
                GraphicsDevice.Clear(new Color(40, 45, 50));

                window.RenderTarget.Present();
            }

            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(Color.Black);

            base.Draw(gameTime);
        }
    }
}
