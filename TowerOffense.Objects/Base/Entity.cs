using Microsoft.Xna.Framework;
using TowerOffense.Objects.Base;
using TowerOffense.Scenes;

namespace TowerOffense.Objects.Common {
    public class Entity : SceneWindow {

        public Vector2 SmoothPosition { get; set; }
        private EntityManager _entityManager;


        public Entity(Scene scene, EntityManager entityManager, Point position, Point size, int titleBarHeight = 24, int borderThickness = 1) : base(scene, position, size, titleBarHeight, borderThickness) {
            _entityManager = entityManager;
            SmoothPosition = position.ToVector2();
        }

        public override void Update(GameTime gameTime) {
            Position = SmoothPosition.ToPoint();
            base.Update(gameTime);
        }
    }
}