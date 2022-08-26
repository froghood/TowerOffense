using Microsoft.Xna.Framework;
using TowerOffense;
using TowerOffense.Objects.Base;

namespace TowerOffense.Scenes.HPTest.Objects {
    public class HPKiller : SceneObject {

        private PlayerManager _pm = PlayerManager.Instance; // christ rename
        private SceneWindow _window;

        public HPKiller(Scene scene) : base(scene) { }

        public override void Update(GameTime gameTime) {
            _pm.SubtractHP(5);
        }
    }
}