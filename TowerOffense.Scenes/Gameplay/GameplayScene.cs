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

            AddObject(new FpsWindow(this));

            TOGame.Player.Restart(100, 15); //15
            TOGame.Player.OnDeath += (_, _) => {
                var gameOverWindow = new GameOverWindow(this, _waveManager);
                gameOverWindow.Closed += (_, _) => {
                    TOGame.Scenes.PopScene();
                };
                AddObject(gameOverWindow);
                var sound = TOGame.Assets.Sounds["Sounds/GameOver"].CreateInstance();
                sound.Volume = TOGame.Settings.Volume;
                sound.Play();

            };

            TOGame.Player.OnWin += (_, _) => {
                var victoryWindow = new VictoryWindow(this);
                victoryWindow.Closed += (_, _) => {
                    TOGame.Scenes.PopScene();
                };
                AddObject(victoryWindow);
                var sound = TOGame.Assets.Sounds["Sounds/Victory"].CreateInstance();
                sound.Volume = TOGame.Settings.Volume;
                sound.Play();
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