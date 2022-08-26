using Microsoft.Xna.Framework;
using TowerOffense.Objects.Base;
using TowerOffense.Scenes;

namespace TowerOffense.Objects.Common {
    public class Entity : SceneWindow {

        private Vector2 _smoothPosition;

        public Entity(Scene scene, Point position, Point size, int titleBarHeight = 24, int borderThickness = 1) : base(scene, position, size, titleBarHeight, borderThickness) {
            _smoothPosition = position.ToVector2();
        }

        public override void Update(GameTime gameTime) {
            Position = _smoothPosition.ToPoint();
            base.Update(gameTime);
        }

        public override void Render(GameTime gameTime) {
            base.Render(gameTime);
        }
    }
}