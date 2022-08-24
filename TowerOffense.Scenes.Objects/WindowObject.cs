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

        private GameWindow _window;
        private Form _form;
        private SwapChainRenderTarget _renderTarget;

        public WindowObject(Scene scene, int width, int height) : base(scene) {
            var game = TOGame.Instance;
            _window = GameWindow.Create(game, width, height);
            _form = (Form)Form.FromHandle(_window.Handle);
            _form.Visible = true;
            _form.ShowIcon = false;
            _form.MinimizeBox = false;
            _form.MaximizeBox = false;
            _form.Text = "";
            _form.FormBorderStyle = FormBorderStyle.FixedSingle;
            _form.TopMost = true;

            _form.FormClosed += (sender, e) => {
                Destroy();
            };

            _renderTarget = new SwapChainRenderTarget(
                game.GraphicsDevice,
                _window.Handle,
                _window.ClientBounds.Width,
                _window.ClientBounds.Height,
                false,
                SurfaceFormat.Color,
                DepthFormat.Depth24Stencil8,
                1,
                RenderTargetUsage.PlatformContents,
                PresentInterval.Default);
        }

        public override void Destroy() {
            base.Destroy();
            _form.Dispose();
            _renderTarget.Dispose();
        }

        public abstract void Render(GameTime gameTime);
    }
}