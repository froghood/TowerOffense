using Microsoft.Xna.Framework;
using TowerOffense.Objects.Base;

namespace TowerOffense.Scenes.Gameplay.Objects {
    public class HealthWindow : SceneWindow {
        public HealthWindow(Scene scene, Point size, Vector2? position = null) : base(scene, size, position) {

            Closeable = false;

        }

        public override void Render(GameTime gameTime) {



            base.Render(gameTime);
        }
    }
}