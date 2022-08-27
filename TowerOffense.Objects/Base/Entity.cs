using Microsoft.Xna.Framework;
using TowerOffense.Objects.Base;
using TowerOffense.Scenes;

namespace TowerOffense.Objects.Common {
    public class Entity : SceneWindow {


        protected Vector2 SmoothPosition { get; set; }


        protected readonly EntityManager _entityManager;

        public Entity(
            Scene scene,
            EntityManager entityManager,
            Point size,
            Point? position = null,
            int titleBarHeight = 24,
            int borderThickness = 1) : base(
                scene,
                size,
                position,
                titleBarHeight,
                borderThickness) {
            _entityManager = entityManager;
            SmoothPosition = position.HasValue ? position.Value.ToVector2() : Vector2.Zero;
        }

        public override void Update(GameTime gameTime) {
            if (!IsBeingDragged) {
                Position = SmoothPosition.ToPoint();
            } else {
                SmoothPosition = Position.ToVector2();
            }
            base.Update(gameTime);
        }
    }
}