using Microsoft.Xna.Framework;
using TowerOffense.Objects.Base;
using TowerOffense.Scenes;

namespace TowerOffense.Objects.Common {
    public class Entity : SceneWindow {


        public Vector2 SmoothPosition { get; protected set; }
        protected Vector2 OffsetPosition { get; set; }


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
                Position = SmoothPosition.ToPoint() + OffsetPosition.ToPoint();
                base.Update(gameTime);
            } else {
                base.Update(gameTime);
                SmoothPosition = Position.ToVector2();
            }
        }
    }
}