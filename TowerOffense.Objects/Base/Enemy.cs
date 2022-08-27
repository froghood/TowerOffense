using Microsoft.Xna.Framework;
using TowerOffense.Objects.Common;
using TowerOffense.Scenes;

namespace TowerOffense.Objects.Base {
    public abstract class Enemy : Entity {
        public Enemy(Scene scene, EntityManager entityManager, Point position, Point size, int titleBarHeight = 24, int borderThickness = 1) : base(scene, entityManager, position, size, titleBarHeight, borderThickness) {
            Closeable = false;
            Draggable = false;
            TitleBarColor = new Color(200, 150, 150);
            BorderColor = new Color(100, 75, 75);
            FocusedBorderColor = new Color(200, 150, 150);
        }
    }
}