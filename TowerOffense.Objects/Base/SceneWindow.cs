using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TowerOffense.Scenes;

namespace TowerOffense.Objects.Base {
    public abstract class SceneWindow : SceneObject {


        public SwapChainRenderTarget RenderTarget { get => _renderTarget; }

        public Point Position {
            get => new Point(_form.Location.X, _form.Location.Y);
            set => _form.Location = new System.Drawing.Point(value.X, value.Y);
        }

        public Point Size {
            get => new Point(_form.ClientSize.Width, _form.ClientSize.Height);
            set => new System.Drawing.Size(value.X, value.Y);
        }

        public Color ClearColor { get; set; } = Color.Black;
        public GameWindow Window { get => _window; }
        public Form Form { get => _form; }

        private GameWindow _window;
        private Form _form;
        private SwapChainRenderTarget _renderTarget;

        public SceneWindow(Scene scene, Point position, Point size) : base(scene) {
            var game = TOGame.Instance;

            _window = GameWindow.Create(TOGame.Instance, 0, 0);

            _form = (Form)Form.FromHandle(_window.Handle);

            _form.MinimumSize = new(1, 1);
            _form.ClientSize = new(width, height);

            _form.ShowIcon = false;
            _form.MinimizeBox = false;
            _form.MaximizeBox = false;
            _form.Text = "";
            _form.FormBorderStyle = FormBorderStyle.FixedSingle;
            _form.TopMost = true;
            
            
            _form.Visible = true;
            _form.MinimumSize = new(1, 1);
            _form.Location = new System.Drawing.Point(position.X, position.Y);
            _form.ClientSize = new System.Drawing.Size(size.X, size.Y);
            //_form.Size = _form.ClientSize;
            _form.ShowIcon = false;
            _form.MinimizeBox = false;
            _form.MaximizeBox = false;
            _form.Text = "";
            _form.FormBorderStyle = FormBorderStyle.Sizable;
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
        }

        public virtual void Hide() {
            _form.Hide();
        }

        public virtual void Show() {
            _form.Show();
        }

        public override void Destroy() {
            base.Destroy();
            _form.Dispose();
            _renderTarget.Dispose();
        }


        public abstract void Render(GameTime gameTime);
    }
}