using Microsoft.Xna.Framework;
using TowerOffense.Objects.Common;
using TowerOffense.Scenes.Gameplay.Objects;

namespace TowerOffense.Scenes.Gameplay {
    public class GameplayScene : Scene {

        private EntityManager _entityManager;
        private WaveManager _waveManager;
        private ShopWindow _shopWindow;

        public GameplayScene() {
            _entityManager = new EntityManager(this);
            _waveManager = new WaveManager(this, _entityManager, "Content/Waves.json", new Point(24, 24), new Point(80, 50));
            _waveManager.Closeable = false;
        }

        public override void Initialize() {
            AddObject(_entityManager);
            AddObject(_waveManager);

            _waveManager.OpenShop();
        }

        public override void Deactivate() {

        }

        public override void Reactivate() {

        }
    }
}