using System;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TowerOffense.Scenes;
using TowerOffense.Scenes.Objects;

namespace TowerOffense.Window {
    public abstract class WindowObject : SceneObject {

        public SwapChainRenderTarget RenderTarget { get => _renderTarget; }
        public Point Position { get => _window.Position; set => _window.Position = value; }
        public Color ClearColor { get; set; } = Color.Black;

        public Form Form { get => _form; }
        public GameWindow Window { get => _window; }

        //private GameWindow _window;
        private Form _form;
        private Control _control;
        private SwapChainRenderTarget _renderTarget;

        public WindowObject(Scene scene, int width, int height) : base(scene) {
            var game = TOGame.Instance;
            //_window = GameWindow.Create(game, 0, 0);
            //_window.AllowUserResizing = false;
            _form = new Form();
            _form.MinimumSize = new(1, 1);
            _form.ClientSize = new(width, height);

            _control = Control.FromHandle(_form.Handle);

            _form.ShowIcon = false;
            _form.MinimizeBox = false;
            _form.MaximizeBox = false;
            _form.Text = "";
            _form.FormBorderStyle = FormBorderStyle.None;
            _form.TopMost = true;

            _form.FormClosed += (sender, e) => {
                Destroy();
            };

            _renderTarget = new SwapChainRenderTarget(
                game.GraphicsDevice,
                _form.Handle,
                _form.ClientSize.Width,
                _form.ClientSize.Height,
                false,
                SurfaceFormat.Color,
                DepthFormat.Depth24Stencil8,
                1,
                RenderTargetUsage.PlatformContents,
                PresentInterval.Default);

            _form.Visible = true;
        }

        private void t(object sender, EventArgs e) {
            System.Console.WriteLine(e);
        }

        public override void Destroy() {
            base.Destroy();
            _form.Dispose();
            _renderTarget.Dispose();
        }

        public void GetMousePosition() {
            Cursor.Position
        }

        public abstract void Render(GameTime gameTime);
    }
}