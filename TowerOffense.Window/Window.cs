using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerOffense.Window {
    class TOWindow {

        public SwapChainRenderTarget RenderTarget { get => _renderTarget; }
        public Point Position { get => _window.Position; set => _window.Position = value; }

        private GameWindow _window;
        private Form _form;
        private SwapChainRenderTarget _renderTarget;

        public TOWindow(Game game, int width, int height) {

            _window = GameWindow.Create(game, width, height);
            _form = (Form)Form.FromHandle(_window.Handle);

            _form.Visible = true;
            _form.ShowIcon = false;
            _form.MinimizeBox = false;
            _form.MaximizeBox = false;
            _form.Text = "";
            _form.FormBorderStyle = FormBorderStyle.FixedSingle;
            _form.TopMost = true;

            _renderTarget = new SwapChainRenderTarget(
                game.GraphicsDevice,
                _form.Handle,
                _form.Bounds.Width,
                _form.Bounds.Height,
                false,
                SurfaceFormat.Color,
                DepthFormat.Depth24Stencil8,
                1,
                RenderTargetUsage.PlatformContents,
                PresentInterval.Default);
        }
    }
}