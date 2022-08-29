using Microsoft.Xna.Framework;
using TowerOffense.Objects.Common;
using TowerOffense.Objects.Towers;
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

            TOGame.Player.Restart(100, 15);
            TOGame.Player.OnDeath += (_, _) => {
                var gameOverWindow = new GameOverWindow(this);
                gameOverWindow.Closed += (_, _) => {
                    TOGame.Scenes.PopScene();
                };
                AddObject(gameOverWindow);
            };

            TOGame.Player.OnWin += (_, _) => {
                var gameOverWindow = new GameOverWindow(this);
                gameOverWindow.Closed += (_, _) => {
                    TOGame.Scenes.PopScene();
                };
                AddObject(gameOverWindow);
            };

            AddObject(_entityManager);
            AddObject(_waveManager);

            AddObject(new HealthWindow(this));
            AddObject(new MoneyWindow(this));

            _waveManager.OpenShop();
        }

        public override void Deactivate() {

        }

        public override void Reactivate() {

        }

        public override void Terminate() {
            foreach (var sceneWindow in SceneWindows) sceneWindow.Close();
        }
    }
}