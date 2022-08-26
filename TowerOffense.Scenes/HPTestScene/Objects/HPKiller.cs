using Microsoft.Xna.Framework;
using TowerOffense;
using TowerOffense.Objects.Base;

namespace TowerOffense.Scenes.HPTest.Objects {
    public class HPKiller : SceneObject {

        private PlayerManager _playerManager = PlayerManager.Instance;
        private SceneWindow _window;
        private double timeCount = 0;
        private double timeLimit = 1;

        public HPKiller(Scene scene) : base(scene) { }

        public override void Update(GameTime gameTime) {
            timeCount += gameTime.ElapsedGameTime.TotalSeconds;
            if ( timeCount > timeLimit ){
                _playerManager.SubtractHP(5);
                timeCount = 0;
            }
        }
    }
}