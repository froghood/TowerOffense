using Microsoft.Xna.Framework;
using TowerOffense.Objects.Base;
using TowerOffense.Objects.Common;

namespace TowerOffense.Scenes.Gameplay.Objects.Shop {
    public class ShopWindow : SceneWindow {

        private EntityManager _entityManager;
        private WaveManager _waveManager;

        public ShopWindow(Scene scene, EntityManager entityManager, WaveManager waveManager, Vector2 position, Point size, int titleBarHeight = 24, int borderThickness = 1) : base(scene, size, position, titleBarHeight, borderThickness) {

            _entityManager = entityManager;
            _waveManager = waveManager;

            ClearColor = new Color(30, 30, 30);
        }
    }
}