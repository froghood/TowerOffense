using Microsoft.Xna.Framework;
using TowerOffense.Objects.Common;
using TowerOffense.Objects.Towers;
using TowerOffense.Scenes.Gameplay.Objects;
using TowerOffense.Scenes.Gameplay.Objects.Shop;

namespace TowerOffense.Scenes.Gameplay {
    public class GameplayScene : Scene {

        private EntityManager _entityManager;
        private WaveManager _waveManager;
        private ShopWindow _shopWindow;

        public GameplayScene() {
            _entityManager = new EntityManager(this);
            _waveManager = new WaveManager(this, _entityManager, "Content/Waves.json", new Vector2(24, 24), new Point(80, 50));
            _waveManager.Closeable = false;
        }

        public override void Initialize() {
            AddObject(_entityManager);
            AddObject(_waveManager);


            AddObject(_entityManager.CreateTower<NuclearTower>(new Vector2(20, 20)));
            AddObject(_entityManager.CreateTower<ElectroTower>(new Vector2(40, 40)));
            AddObject(_entityManager.CreateTower<GravityTower>(new Vector2(60, 60)));

            _waveManager.OpenShop();
        }

        public override void Deactivate() {

        }

        public override void Reactivate() {

        }
    }
}