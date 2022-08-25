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
            set {
                var _value = new System.Drawing.Size(value.X, value.Y);
                var difference = _form.Size - _form.ClientSize;
                _form.MinimumSize = new(1, 1);
                _form.MaximumSize = _value + difference;
                _form.ClientSize = _value;
            }
        }

        public Color ClearColor { get; set; } = Color.Black;
        public GameWindow Window { get => _window; }
        public Form Form { get => _form; }

        public bool Controllable {
            get => _controllable;
            set {
                _controllable = value;
                _form.ControlBox = value;
                _form.FormBorderStyle = value ? FormBorderStyle.FixedSingle : FormBorderStyle.FixedToolWindow;
            }
        }

        // public bool ClosingEnabled {
        //     get => _closingEnabled;
        //     set {
        //         var hSystemMenu = GetSystemMenu(this.Window.Handle, false);
        //         EnableMenuItem(hSystemMenu, SC_CLOSE, (uint)(MF_ENABLED | (value ? MF_ENABLED : MF_GRAYED)));
        //         _closingEnabled = value;
        //     }
        // }

        // public bool UserMovingEnabled {
        //     get => _userMovingEnabled;
        //     set {
        //         var hSystemMenu = GetSystemMenu(this.Window.Handle, false);
        //         EnableMenuItem(hSystemMenu, SC_Move, (uint)(MF_ENABLED | (value ? MF_ENABLED : MF_GRAYED)));
        //         _userMovingEnabled = value;
        //     }
        // }


        private GameWindow _window;
        private Form _form;
        private SwapChainRenderTarget _renderTarget;

        private bool _controllable = true;

        // private bool _closingEnabled = true;
        // private bool _userMovingEnabled = true;

        // cursed hack to grey close button
        // [DllImport("user32.dll")]
        // static extern bool EnableMenuItem(IntPtr hMenu, uint uIDEnableItem, uint uEnable);
        // [DllImport("user32.dll")]
        // static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        // private const UInt32 SC_CLOSE = 0xF060;
        // private const UInt32 SC_Move = 0xF010;
        // private const UInt32 MF_ENABLED = 0x00000000;
        // private const UInt32 MF_GRAYED = 0x00000001;
        // private const UInt32 MF_DISABLED = 0x00000002;
        // private const uint MF_BYCOMMAND = 0x00000000;

        public SceneWindow(Scene scene, Point position, Point size) : base(scene) {
            var game = TOGame.Instance;

            _window = GameWindow.Create(TOGame.Instance, 0, 0);

            //_form = new Form();
            _form = (Form)Form.FromHandle(_window.Handle);



            _form.Location = new System.Drawing.Point(position.X, position.Y);
            Size = size;

            //_form.ControlBox = false;

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
    }
}