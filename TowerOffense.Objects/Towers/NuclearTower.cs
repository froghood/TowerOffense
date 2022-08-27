using System.Linq;
using Microsoft.Xna.Framework;
using TowerOffense.Objects.Base;
using TowerOffense.Objects.Common;
using TowerOffense.Scenes;

namespace TowerOffense.Objects.Towers {
    public class NuclearTower : Tower {
        public NuclearTower(
            Scene scene,
            EntityManager entityManager,
            Point? position = null) : base(
            scene,
            entityManager,
            new Point(100, 100),
            position: position) {

            TitleBarColor = new Color(160, 255, 150);
            FocusedBorderColor = TitleBarColor;
            BorderColor = new Color(80, 128, 75);

            _range = 240f;
            _attackSpeed = 0.2f;
            _damage = 0.3f;
            _sellPrice = 1;
        }

        public override void Update(GameTime gameTime) {

            _enemiesInRange = GetEnemiesInRange();
            _targetedEnemies = _enemiesInRange.Where(e => e.EnemyState != EnemyState.Neutralized).ToList();

            while (_attackTimer >= _attackSpeed) {
                foreach (var enemy in _targetedEnemies) {
                    enemy.Damage(_damage);
                }
                _attackTimer -= _attackSpeed;
            }

            base.Update(gameTime);
        }

        public override void Render(GameTime gameTime) {
            base.Render(gameTime);
        }
    }
}