using System;
using System.Linq;
using Microsoft.Xna.Framework;
using TowerOffense.Extensions;
using TowerOffense.Objects.Base;
using TowerOffense.Objects.Common;
using TowerOffense.Scenes;

namespace TowerOffense.Objects.Towers {
    public class NuclearTower : Tower {
        public NuclearTower(
            Scene scene,
            EntityManager entityManager,
            Vector2? position = null) : base(
            scene,
            entityManager,
            new Point(100, 100),
            position: position) {

            TitleBarColor = new Color(160, 255, 150);
            FocusedBorderColor = TitleBarColor;
            BorderColor = new Color(80, 128, 75);

            Range = 240f;
            AttackSpeed = 0.25f;
            Damage = 0.333f;
            SellPrice = 1;
        }

        public override void Update(GameTime gameTime) {

            _enemiesInRange = GetEnemiesInRange();
            _targetedEnemies = _enemiesInRange.Where(e => e.State != EnemyState.Neutralized).ToList();

            if (State == TowerState.Idle && _targetedEnemies.Count > 0) ChangeState(TowerState.Attacking);
            else if (State == TowerState.Attacking && _targetedEnemies.Count == 0) ChangeState(TowerState.Idle);

            switch (State) {
                case TowerState.Idle:
                    break;
                case TowerState.Attacking:
                    if (_attackTimer >= AttackSpeed) {
                        _attackTimer -= AttackSpeed;
                        foreach (var enemy in _targetedEnemies) {
                            enemy.Damage(this, Damage);
                        }
                    }
                    break;
            }

            _attackTimer = MathF.Min(AttackSpeed, _attackTimer + gameTime.DeltaTime());

            base.Update(gameTime);
        }

        public override void Render(GameTime gameTime) {
            base.Render(gameTime);
        }
    }
}