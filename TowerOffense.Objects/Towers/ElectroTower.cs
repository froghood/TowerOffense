using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using TowerOffense.Objects.Base;
using TowerOffense.Objects.Common;
using TowerOffense.Scenes;

namespace TowerOffense.Objects.Towers {
    public class ElectroTower : Tower {
        public ElectroTower(
            Scene scene,
            EntityManager entityManager,
            Vector2? position = null) : base(
            scene,
            entityManager,
            new Point(100, 100),
            position: position) {

            TitleBarColor = new Color(167, 236, 255);
            FocusedBorderColor = TitleBarColor;
            BorderColor = new Color(84, 118, 128);

            Range = 360f;
            AttackSpeed = 0.5f;
            Damage = 2.5f;
            SellPrice = 1;
        }

        public override void Update(GameTime gameTime) {

            _enemiesInRange = GetEnemiesInRange();

            var first = _enemiesInRange.Where(e => e.State != EnemyState.Neutralized).FirstOrDefault();

            _targetedEnemies = (first != null) ?
            new List<Enemy> { first } :
            new List<Enemy>();

            switch (State) {
                case TowerState.Idle:
                    if (StateTime >= AttackSpeed && _targetedEnemies.Count > 0) {
                        ChangeState(TowerState.Attacking);
                        foreach (var enemy in _targetedEnemies) {

                            enemy.Damage(this, Damage);
                        }
                    }
                    break;
                case TowerState.Attacking:
                    if (StateTime >= 0.25f) {
                        ChangeState(TowerState.Idle);
                    }
                    break;
            }

            base.Update(gameTime);
        }

        public override void Render(GameTime gameTime) {
            base.Render(gameTime);
        }
    }
}