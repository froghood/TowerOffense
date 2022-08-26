using Microsoft.Xna.Framework;
using TowerOffense.Objects.Base;
using TowerOffense.Scenes.Test.Objects;

namespace TowerOffense.Scenes.Test {
    public class TestScene : Scene {

        public TestScene() {

        }

        public override void Initialize() {

            for (int i = 0; i < 5; i++) {
                AddObject(new TestWindow(this, new Point(480, 480), new Point(5 + 20 * i, 5 + 20 * i)));
            }

            base.Initialize();
        }

        public override void Deactivate() {
            base.Deactivate();
        }

        public override void Reactivate() {
            base.Reactivate();
        }
    }
}