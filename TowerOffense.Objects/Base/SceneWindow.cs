using System;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TowerOffense.Scenes;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;

namespace TowerOffense.Objects.Base {
    public abstract class SceneWindow : SceneObject {


        public Point Position {
            get => new Point(_form.Location.X, _form.Location.Y);
            set => _form.Location = new System.Drawing.Point(value.X, value.Y);
        }

        public Point InnerSize {
            get => new Point() {
                X = _form.ClientSize.Width - _borderThickness * 2,
                Y = _form.ClientSize.Height - _titleBarHeight - _borderThickness * 2
            };
        }

        public Point Size {
            get => new Point(_form.ClientSize.Width, _form.ClientSize.Height);
        }

        /// <summary>
        /// altering this could potentially break things, use at your own risk.
        /// </summary>
        public GameWindow Window { get => _window; }

        /// <summary>
        /// altering this could potentially break things, use at your own risk.
        /// </summary>
        public Form Form { get => _form; }

        public Color ClearColor { get => _clearColor; set => _clearColor = value; }
        public SwapChainRenderTarget RenderTarget { get => _renderTarget; }
        public bool IsBeingDragged { get => _isBeingDragged; }
        public bool Draggable { get => _draggable; set => _draggable = value; }
        public bool Closeable { get => _closeable; set => _closeable = value; }
        public int TitleBarHeight { get => _titleBarHeight; }
        public Color TitleBarColor { get => _titleBarColor; set => _titleBarColor = value; }
        public int BorderThickness { get => _borderThickness; }
        public Color BorderColor { get => _borderColor; set => _borderColor = value; }
        public Color FocusedBorderColor { get => _focusedBorderColor; set => _focusedBorderColor = value; }
        public Point InnerWindowOffset { get => new Point(_borderThickness, _borderThickness + _titleBarHeight); }
        public Point MouseInnerPosition { get => new Point(_mouseState.X - _borderThickness, _mouseState.Y - _titleBarHeight - _borderThickness); }
        public MouseState MouseState { get => _mouseState; }
        public bool IsMouseHovering { get => _isMouseHovering; }

        public event EventHandler Closed;

        private Form _form;
        private TOGame _game;
        private GameWindow _window;
        private Color _clearColor;

        private int _titleBarHeight;
        private Color _titleBarColor = Color.White;
        private int _borderThickness;
        private Color _borderColor = new Color(80, 80, 80);
        private Color _focusedBorderColor = new Color(180, 180, 180);

        private bool _draggable = true;
        private bool _closeable = true;

        private MouseState _mouseState;

        private bool _isBeingDragged;
        private Point _dragOffset;

        private Rectangle _closeBounds;
        private Texture2D _closeTexture;
        private bool _closeIsHovered;
        private bool _closeIsPressed;

        private bool _isMouseHovering;
        private bool _mousePressedFlag;

        private Texture2D _pixel;

        private SwapChainRenderTarget _renderTarget;

        public SceneWindow(Scene scene, Point position, Point size) : this(scene, position, size, 24, 1) { }
        public SceneWindow(Scene scene, Point position, Point size, int titleBarHeight, int borderThickness) : base(scene) {
            _game = TOGame.Instance;

            _window = GameWindow.Create(_game, 0, 0);
            _form = (Form)Form.FromHandle(_window.Handle);

            _form.FormBorderStyle = FormBorderStyle.None;
            _form.MaximizeBox = false;
            _form.MinimizeBox = false;
            _form.Text = "";
            _form.ShowIcon = false;
            _form.ControlBox = false;
            _form.TopMost = true;
            _form.Visible = true;
            Position = position;

            _titleBarHeight = titleBarHeight;
            _borderThickness = borderThickness;

            _form.ClientSize = new System.Drawing.Size() {
                Width = size.X + _borderThickness * 2,
                Height = size.Y + _titleBarHeight + _borderThickness * 2
            };

            _form.FormClosing += (sender, e) => {
                if (!_closeable) {
                    e.Cancel = true;
                    return;
                }
                Close();
            };

            _clearColor = Color.Black;
            CalculateCloseBounds();

            _closeTexture = TOGame.Assets.Textures["Sprites/Close"];

            _pixel = new Texture2D(_game.GraphicsDevice, 1, 1);
            _pixel.SetData(new[] { Color.White });

            _renderTarget = new SwapChainRenderTarget(
                _game.GraphicsDevice,
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



        public override void Update(GameTime gameTime) {

            // window dragging & close button
            if (!_draggable) _isBeingDragged = false;
            if (_isBeingDragged) _form.Location = new System.Drawing.Point(Cursor.Position.X - _dragOffset.X, Cursor.Position.Y - _dragOffset.Y);

            _closeIsHovered = (_isMouseHovering &&
                _mouseState.X >= _closeBounds.Left - 1 && _mouseState.X < _closeBounds.Right + 1 &&
                _mouseState.Y >= _closeBounds.Top - 1 && _mouseState.Y < _closeBounds.Bottom);

            switch (_mouseState.LeftButton) {
                case ButtonState.Pressed:
                    if (!_form.Focused) return;
                    if (_mousePressedFlag) break;
                    if (!_closeIsPressed && !_isBeingDragged && _closeIsHovered) _closeIsPressed = true;
                    if (_closeIsPressed) break;
                    if (!_isBeingDragged &&
                    _mouseState.X >= 0 && _mouseState.X < _form.ClientSize.Width &&
                    _mouseState.Y >= 0 && _mouseState.Y < _titleBarHeight + _borderThickness) {
                        _isBeingDragged = true;
                        _dragOffset = new Point(_mouseState.X, _mouseState.Y);
                    }
                    _mousePressedFlag = true;
                    break;
                case ButtonState.Released:
                    _mousePressedFlag = false;
                    if (_closeIsPressed) {
                        if (_closeIsHovered) _form.Close();
                        _closeIsPressed = false;
                    }
                    _isBeingDragged = false;
                    break;
            }
        }

        public virtual void Render(GameTime gameTime) {

            //title bar
            TOGame.SpriteBatch.Draw(_pixel, new Rectangle(_borderThickness, _borderThickness, _form.ClientSize.Width - _borderThickness, _titleBarHeight), _titleBarColor);

            //border
            var borderColor = _form.Focused ? _focusedBorderColor : _borderColor;
            TOGame.SpriteBatch.Draw(_pixel, new Rectangle(0, 0, _borderThickness, _form.ClientSize.Height), borderColor);
            TOGame.SpriteBatch.Draw(_pixel, new Rectangle(_form.ClientSize.Width - _borderThickness, 0, _borderThickness, _form.ClientSize.Height), borderColor);
            TOGame.SpriteBatch.Draw(_pixel, new Rectangle(0, _form.ClientSize.Height - _borderThickness, _form.ClientSize.Width, _borderThickness), borderColor);
            TOGame.SpriteBatch.Draw(_pixel, new Rectangle(0, 0, _form.ClientSize.Width, _borderThickness), borderColor);

            //close
            var closeColor = (_closeable, _isBeingDragged, _closeIsHovered, _closeIsPressed) switch {
                (true, false, true, false) => new Color(0, 0, 0, 50),
                (true, false, true, true) => new Color(0, 0, 0, 70),
                _ => new Color(0, 0, 0, 0)
            };

            TOGame.SpriteBatch.Draw(_pixel, _closeBounds, closeColor);
            TOGame.SpriteBatch.Draw(_closeTexture, new Vector2() {
                X = _closeBounds.X + _closeBounds.Width / 2 - _closeTexture.Width / 2,
                Y = _closeBounds.Y + _closeBounds.Height / 2 - _closeTexture.Height / 2
            }, _closeable ? Color.White : new Color(255, 255, 255, 70));
        }

        public void Draw(Texture2D texture, Vector2 position, Color color) {
            TOGame.SpriteBatch.Draw(texture, position + new Vector2(_borderThickness, _titleBarHeight + _borderThickness), color);
        }

        public virtual void Close() {
            Closed?.Invoke(this, EventArgs.Empty);
            Destroy();
        }

        public void Hide() {
            _form.Hide();
        }

        public void Show() {
            _form.Show();
        }

        public override void Destroy() {
            _form.Dispose();
            _renderTarget.Dispose();
            base.Destroy();
        }

        public void UpdateMouseState(MouseState mouseState, bool resetMouseOverlapping) {
            _mouseState = mouseState;
            if (resetMouseOverlapping) _isMouseHovering = false;
        }

        public bool UpdateMouseHovering() {
            _isMouseHovering = (_mouseState.X >= 0 && _mouseState.X < _form.ClientSize.Width &&
            _mouseState.Y >= 0 && _mouseState.Y < _form.ClientSize.Height);
            return _isMouseHovering;
        }

        private void CalculateCloseBounds() {
            var closeSize = new Point() {
                X = Math.Min(_form.ClientSize.Width - _borderThickness * 2, _titleBarHeight),
                Y = _titleBarHeight
            };
            var closePosition = new Point() {
                X = Math.Max(_borderThickness, _form.ClientSize.Width - _borderThickness - closeSize.X),
                Y = _borderThickness,
            };
            _closeBounds = new Rectangle(closePosition, closeSize);
        }
    }
}