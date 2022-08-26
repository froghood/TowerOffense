using Microsoft.Xna.Framework;
using TowerOffense;
using TowerOffense.Objects.Base;

namespace TowerOffense.Scenes.HPTest.Objects {
    public class HPKiller : SceneObject {

        private PlayerManager _pm = PlayerManager.Instance; // christ rename
        private SceneWindow _window;
        private double timeCount = 0;
        private double timeLimit = 2;

        public HPKiller(Scene scene) : base(scene) { }

        public override void Update(GameTime gameTime) {
            timeCount += gameTime.ElapsedGameTime.TotalSeconds;
            if ( timeCount > timeLimit ){
                _pm.SubtractHP(5);
                timeCount = 0;
            }
        }
    }
}