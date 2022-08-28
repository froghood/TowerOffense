using Microsoft.Xna.Framework;
using TowerOffense.Objects.Common;
using TowerOffense.Objects.Towers;
using TowerOffense.Scenes.GameOver;
using TowerOffense.Scenes.Gameplay.Objects;
using TowerOffense.Scenes.Gameplay.Objects.Shop;

namespace TowerOffense.Scenes.Gameplay {
    public class GameplayScene : Scene {

        private EntityManager _entityManager;
        private WaveManager _waveManager;
        //private ShopWindow _shopWindow;

        public GameplayScene() {
            _entityManager = new EntityManager(this);
            _waveManager = new WaveManager(this, _entityManager, "./Waves.json");
            _waveManager.Closeable = false;
        }

        public override void Initialize() {

            TOGame.PlayerManager.Restart(100, 3);
            TOGame.PlayerManager.OnDeath += (_, _) => {
                AddObject(new GameOverWindow(this));
            };

            AddObject(_entityManager);
            AddObject(_waveManager);


            AddObject(_entityManager.CreateTower<GravityTower>(new Vector2(20, 20)));
            AddObject(_entityManager.CreateTower<GravityTower>(new Vector2(40, 40)));
            AddObject(_entityManager.CreateTower<GravityTower>(new Vector2(60, 60)));
            AddObject(_entityManager.CreateTower<GravityTower>(new Vector2(80, 20)));
            AddObject(_entityManager.CreateTower<GravityTower>(new Vector2(100, 40)));
            AddObject(_entityManager.CreateTower<GravityTower>(new Vector2(120, 60)));
            AddObject(_entityManager.CreateTower<GravityTower>(new Vector2(140, 20)));
            AddObject(_entityManager.CreateTower<GravityTower>(new Vector2(160, 40)));
            AddObject(_entityManager.CreateTower<GravityTower>(new Vector2(180, 60)));

            _waveManager.OpenShop();
        }

        public override void Deactivate() {

        }

        public override void Reactivate() {

        }
    }
}