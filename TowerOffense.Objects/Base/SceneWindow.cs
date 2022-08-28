using System;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TowerOffense.Scenes;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;

namespace TowerOffense.Objects.Base {
    public abstract class SceneWindow : SceneObject {


        public Vector2 Position { get; set; }

        public Vector2 Offset { get; set; }

        public Point CenterPosition {
            get => new Point(_form.Location.X, _form.Location.Y) + (InnerWindowOffset + InnerSize.ToVector2() / 2).ToPoint();
            set {
                var centerPosition = value - (InnerWindowOffset + InnerSize.ToVector2() / 2).ToPoint();
                _form.Location = new System.Drawing.Point(centerPosition.X, centerPosition.Y);
            }
        }

        public Vector2 SmoothPosition { get; set; }

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

        public Color ClearColor { get; set; } = Color.Black;
        public SwapChainRenderTarget RenderTarget { get => _renderTarget; }
        public bool Centered { get; set; }
        public bool IsBeingDragged { get => _isBeingDragged; }
        public bool Draggable { get; set; } = true;
        public bool Closeable { get; set; } = true;
        public int TitleBarHeight { get => _titleBarHeight; }
        public Color TitleBarColor { get; set; } = Color.White;
        public int BorderThickness { get => _borderThickness; }
        public Color BorderColor { get; set; } = Color.White;
        public Color FocusedBorderColor { get; set; } = new Color(180, 180, 180);
        public Vector2 InnerWindowOffset { get => new Vector2(_borderThickness, _borderThickness + _titleBarHeight); }
        public Vector2 InnerWindowCenterOffset {
            get => InnerWindowOffset + InnerSize.ToVector2() / 2;
        }
        public Point MouseInnerPosition { get => new Point(_mouseState.X - _borderThickness, _mouseState.Y - _titleBarHeight - _borderThickness); }
        public MouseState MouseState { get => _mouseState; }
        public bool IsMouseHovering { get => _isMouseHovering; }

        public event EventHandler Closed;

        private Form _form;
        private GameWindow _window;

        private int _titleBarHeight;
        private int _borderThickness;

        private MouseState _mouseState;
        private bool _isMouseHovering;

        private bool _isBeingDragged;
        private Point _dragOffset;

        private Rectangle _closeBounds;
        private Texture2D _closeTexture;
        private bool _closeIsHovered;
        private bool _closeIsPressed;

        private bool _mousePressedFlag;

        private Texture2D _pixel;

        private SwapChainRenderTarget _renderTarget;

        public SceneWindow(
            Scene scene,
            Point size,
            Vector2? position = null,
            int titleBarHeight = 24,
            int borderThickness = 1) : base(scene) {

            var game = TOGame.Instance;

            _window = GameWindow.Create(game, 0, 0);

            _form = (Form)Form.FromHandle(_window.Handle);

            _form.FormBorderStyle = FormBorderStyle.None;
            _form.MaximizeBox = false;
            _form.MinimizeBox = false;
            _form.Text = "";
            _form.ShowIcon = false;
            _form.ControlBox = false;
            _form.TopMost = true;

            _titleBarHeight = titleBarHeight;
            _borderThickness = borderThickness;

            _form.ClientSize = new System.Drawing.Size() {
                Width = size.X + _borderThickness * 2,
                Height = size.Y + _titleBarHeight + _borderThickness * 2
            };

            Position = position.HasValue ? position.Value : Vector2.Zero;
            _form.Location = new System.Drawing.Point((int)Position.X, (int)Position.Y);

            _form.FormClosing += (sender, e) => {
                if (!Closeable) {
                    e.Cancel = true;
                    return;
                }
                Close();
            };

            CalculateCloseBounds();

            _closeTexture = TOGame.Assets.Textures["Sprites/Close"];

            _pixel = new Texture2D(game.GraphicsDevice, 1, 1);
            _pixel.SetData(new[] { Color.White });

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

        public override void Update(GameTime gameTime) {

            // window dragging & close button
            if (!Draggable) _isBeingDragged = false;

            _closeIsHovered = (_isMouseHovering &&
                _mouseState.X >= _closeBounds.Left - 1 && _mouseState.X < _closeBounds.Right + 1 &&
                _mouseState.Y >= _closeBounds.Top - 1 && _mouseState.Y < _closeBounds.Bottom);

            switch (_mouseState.LeftButton) {
                case ButtonState.Pressed:
                    // ensures only the first frame of ButtonState.Pressed gets through
                    if (!_form.Focused || _mousePressedFlag) break;

                    // true if the close button is being hovered but is not already being pressed or the window is being dragged
                    if (!_closeIsPressed && !_isBeingDragged && _closeIsHovered) _closeIsPressed = true;

                    if (_closeIsPressed) break; // dont drag if close is being pressed

                    if (!_isBeingDragged && // check if the mouse is within the bounds of the draggable area
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

            if (_isBeingDragged) Position = new Vector2(Cursor.Position.X - _dragOffset.X, Cursor.Position.Y - _dragOffset.Y);


            Position = new Vector2() {
                X = Math.Clamp(Position.X, 0, 1920f - Size.X),
                Y = Math.Clamp(Position.Y, 0, 1080f - Size.Y)
            };

            _form.Location = new System.Drawing.Point() {
                X = (int)Math.Clamp(Position.X + Offset.X, 0, 1920 - Size.X),
                Y = (int)Math.Clamp(Position.Y + Offset.Y, 0, 1080 - Size.Y)
            };
        }

        public virtual void Render(GameTime gameTime) {

            //title bar
            TOGame.SpriteBatch.Draw(_pixel, new Rectangle(_borderThickness, _borderThickness, _form.ClientSize.Width - _borderThickness, _titleBarHeight), TitleBarColor);

            //border
            var borderColor = _form.Focused ? FocusedBorderColor : BorderColor;
            TOGame.SpriteBatch.Draw(_pixel, new Rectangle(0, 0, _borderThickness, _form.ClientSize.Height), borderColor);
            TOGame.SpriteBatch.Draw(_pixel, new Rectangle(_form.ClientSize.Width - _borderThickness, 0, _borderThickness, _form.ClientSize.Height), borderColor);
            TOGame.SpriteBatch.Draw(_pixel, new Rectangle(0, _form.ClientSize.Height - _borderThickness, _form.ClientSize.Width, _borderThickness), borderColor);
            TOGame.SpriteBatch.Draw(_pixel, new Rectangle(0, 0, _form.ClientSize.Width, _borderThickness), borderColor);

            //close
            var closeColor = (Closeable, _isBeingDragged, _closeIsHovered, _closeIsPressed) switch {
                (true, false, true, false) => new Color(0, 0, 0, 50),
                (true, false, true, true) => new Color(0, 0, 0, 70),
                _ => new Color(0, 0, 0, 0)
            };

            TOGame.SpriteBatch.Draw(_pixel, _closeBounds, closeColor);
            TOGame.SpriteBatch.Draw(_closeTexture, new Vector2() {
                X = _closeBounds.X + _closeBounds.Width / 2 - _closeTexture.Width / 2,
                Y = _closeBounds.Y + _closeBounds.Height / 2 - _closeTexture.Height / 2
            }, Closeable ? Color.White : new Color(255, 255, 255, 45));

            _form.Visible = !_form.IsDisposed;
        }

        public void Draw(Texture2D texture, Vector2 position, Color color) {
            TOGame.SpriteBatch.Draw(texture, position + InnerWindowOffset, color);
        }

        public void DrawString(SpriteFont font, string text, Vector2 position, Color color) {
            TOGame.SpriteBatch.DrawString(font, text, position + InnerWindowOffset, color);
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

        public void UpdateMouseState(MouseState mouseState, bool resetMouseHovering) {
            _mouseState = mouseState;
            if (resetMouseHovering) _isMouseHovering = false;
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