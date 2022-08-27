using Microsoft.Xna.Framework;
using TowerOffense.Objects.Common;
using TowerOffense.Scenes;

namespace TowerOffense.Objects.Base {
    public abstract class Tower : Entity {
        public Tower(Scene scene,
            EntityManager entityManager,
            Point size,
            bool center = false,
            Point? position = null,
            int titleBarHeight = 24,
            int borderThickness = 1) : base(
                scene,
                entityManager,
                size,
                position,
                titleBarHeight,
                borderThickness) {

            //TODO: implement
        }
    }
}