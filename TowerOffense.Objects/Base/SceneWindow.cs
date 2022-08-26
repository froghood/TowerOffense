using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TowerOffense.Scenes;
using TowerOffense;
using System.Runtime.InteropServices;
using System;

namespace TowerOffense.Objects.Base {
    public abstract class SceneWindow : SceneObject {

        public SwapChainRenderTarget RenderTarget { get => _renderTarget; }

        public Point Position {
            get => new Point(_form.Location.X, _form.Location.Y);
            set => _form.Location = new System.Drawing.Point(value.X, value.Y);
        }

        public Point Size {
            get => new Point(_form.ClientSize.Width, _form.ClientSize.Height);
            set => _form.ClientSize = new System.Drawing.Size(value.X, value.Y);
        }

        public Color ClearColor { get; set; } = Color.Black;

        public bool Controllable {
            get => _controllable;
            set {
                _controllable = value;
                _form.ControlBox = value;
                _form.FormBorderStyle = value ? FormBorderStyle.FixedSingle : FormBorderStyle.FixedToolWindow;
            }
        }

        public bool ClosingEnabled {
            get => _closingEnabled;
            set {
                _closingEnabled = value;
                EnableMenuItem(GetSystemMenu(_form.Handle, false), 0xF060, value ? 0u : 1u);
            }
        }

        public GameWindow Window { get => _window; }
        public Form Form { get => _form; }

        private GameWindow _window;
        private Form _form;
        private SwapChainRenderTarget _renderTarget;
        private bool _controllable = true;
        private bool _closingEnabled = true;

        public SceneWindow(Scene scene, Point position, Point size) : base(scene) {

            var game = TOGame.Instance;

            _window = GameWindow.Create(TOGame.Instance, 0, 0);
            _form = (Form)Form.FromHandle(_window.Handle);

            _form.Location = new(position.X, position.X);
            _form.ClientSize = new(size.X, size.Y);
            _form.ShowIcon = false;
            _form.MinimizeBox = false;
            _form.MaximizeBox = false;
            _form.Text = "";
            _form.FormBorderStyle = FormBorderStyle.FixedSingle;
            _form.TopMost = true;
            _form.Visible = true;

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

        // for disabling close button
        [DllImport("user32")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("user32")]
        private static extern bool EnableMenuItem(IntPtr hMenu, uint itemId, uint uEnable);

    }
}