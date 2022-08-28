using TowerOffense.Scenes.HpTest.Objects;
using TowerOffense.Objects;
using Microsoft.Xna.Framework;
namespace TowerOffense.Scenes.HpTest {

    public class HpTestScene : Scene {
        private HpKiller hpKiller;
        private HpDisplay hpDisplay;
        public HpTestScene() {
            hpKiller = new HpKiller(this);
            hpDisplay = new HpDisplay(this, new Vector2(0, 0), new Point(120, 120));
            this.AddObject(hpKiller);
            this.AddObject(hpDisplay);
        }

        public override void Initialize() {
            System.Console.WriteLine("HpTestScene Initialized");
        }
    }
}
