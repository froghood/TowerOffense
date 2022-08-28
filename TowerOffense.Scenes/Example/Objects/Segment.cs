using Microsoft.Xna.Framework;
using TowerOffense.Objects.Base;

namespace TowerOffense.Scenes.Example.Objects {
    public class Segment : SceneWindow {

        private SceneWindow _sceneWindow;

        public Segment(Scene scene, Vector2 position, Point size, SceneWindow sceneWindow) : base(scene, size, position) {
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
        }

        public override void Render(GameTime gameTime) {
            base.Render(gameTime);
        }


    }
}