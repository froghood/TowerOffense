using Microsoft.Xna.Framework;
using TowerOffense;
using TowerOffense.Objects.Base;

namespace TowerOffense.Scenes.HpTest.Objects {
    public class HpKiller : SceneObject {

        private PlayerManager _playerManager = PlayerManager.Instance;
        private double timeCount = 0;
        private double timeLimit = 1;

        public HpKiller(Scene scene) : base(scene) { }

        public override void Update(GameTime gameTime) {
            timeCount += gameTime.ElapsedGameTime.TotalSeconds;
            if ( timeCount > timeLimit ){
                _playerManager.SubtractHp(5);
                timeCount = 0;
            }
        }
    }
}
