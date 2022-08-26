using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TowerOffense.Objects.Base;

namespace TowerOffense.Objects.Common {
    public class Button : WindowObject {

        public bool IsHovering { get => _isHovering; }
        public Rectangle Bounds { get; set; }
        public Texture2D Texture { get; set; }
        public Texture2D HoverTexture { get; set; }

        private bool _isHovering;

        public event EventHandler Clicked;

        public Button(SceneWindow window) : base(window) { }
        public Button(SceneWindow window, Point position, Point size) : this(window, new Rectangle(position, size)) { }
        public Button(SceneWindow window, Rectangle bounds) : this(window) {
            Bounds = bounds;
        }

        public override void Update(GameTime gameTime) {

            _isHovering = (SceneWindow.IsMouseHovering &&
            SceneWindow.MouseInnerPosition.X >= Bounds.Left && SceneWindow.MouseInnerPosition.X < Bounds.Right &&
            SceneWindow.MouseInnerPosition.Y >= Bounds.Top && SceneWindow.MouseInnerPosition.Y < Bounds.Bottom);

            if (_isHovering && SceneWindow.MouseState.LeftButton == ButtonState.Pressed) {
                Clicked?.Invoke(this, EventArgs.Empty);
            }

            base.Update(gameTime);
        }

        public override void Render(GameTime gameTime) {

            SceneWindow.Draw(_isHovering ? HoverTexture : Texture, Bounds.Location.ToVector2(), Color.White);

            base.Render(gameTime);
        }
    }
}