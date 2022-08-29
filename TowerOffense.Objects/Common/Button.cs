using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TowerOffense.Objects.Base;

namespace TowerOffense.Objects.Common {
    public class Button : WindowObject {

        public bool Hovering {
            get => (SceneWindow.MouseHovering &&
            SceneWindow.MouseInnerPosition.X >= Bounds.Left && SceneWindow.MouseInnerPosition.X < Bounds.Right &&
            SceneWindow.MouseInnerPosition.Y >= Bounds.Top && SceneWindow.MouseInnerPosition.Y < Bounds.Bottom);
        }
        public Rectangle Bounds { get; set; }
        public Texture2D Texture { get; set; }
        public Texture2D HoverTexture { get; set; }

        private bool _isHovering;
        private ButtonState _previousMouseButtonState = ButtonState.Released;

        public event EventHandler Clicked;

        public Button(SceneWindow window) : base(window) { }
        public Button(SceneWindow window, Point position, Point size) : this(window, new Rectangle(position, size)) { }
        public Button(SceneWindow window, Rectangle bounds) : this(window) {
            Bounds = bounds;
        }

        public override void Update(GameTime gameTime) {

            if (Hovering && SceneWindow.MouseState.LeftButton == ButtonState.Pressed && _previousMouseButtonState == ButtonState.Released) {
                Clicked?.Invoke(this, EventArgs.Empty);
            }

            _previousMouseButtonState = SceneWindow.MouseState.LeftButton;

            base.Update(gameTime);
        }

        public override void Render(GameTime gameTime) {

            SceneWindow.Draw(Hovering ? HoverTexture : Texture, Bounds.Location.ToVector2(), Color.White);

            base.Render(gameTime);
        }
    }
}