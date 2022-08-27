using Microsoft.Xna.Framework;
using TowerOffense.Objects.Base;
using TowerOffense.Objects.Common;
using TowerOffense.Scenes;

namespace TowerOffense.Objects.Towers {
    public class GravityTower : Tower {
        public GravityTower(
            Scene scene,
            EntityManager entityManager,
            Point? position = null) : base(
            scene,
            entityManager,
            new Point(100, 100),
            position: position) {

            _range = 400f;
            _attackSpeed = 1;
            _damage = 1;
            _sellPrice = 1;
        }

        public override void Update(GameTime gameTime) {

            UpdateEnemiesInRange();

            while (_attackTimer >= _attackSpeed) {


                if (_enemiesInRange.Count > 0) _enemiesInRange[0].Enemy.Damage(_damage);
                _attackTimer -= _attackSpeed;
            }

            base.Update(gameTime);
        }

        public override void Render(GameTime gameTime) {
            base.Render(gameTime);
        }
    }
}