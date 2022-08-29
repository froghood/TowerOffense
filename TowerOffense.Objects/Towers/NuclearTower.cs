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
            Damage = 0.3f;
            SellPrice = 8;
        }

        public override void Update(GameTime gameTime) {

            _enemiesInRange = GetEnemiesInRange();
            _targetedEnemies = _enemiesInRange.Where(e => e.State != EnemyState.Neutralized).ToList();

            if (State == TowerState.Idle && _targetedEnemies.Count > 0 && !TOGame.Player.Dead) ChangeState(TowerState.Attacking);
            else if (State == TowerState.Attacking && (_targetedEnemies.Count == 0 || TOGame.Player.Dead)) ChangeState(TowerState.Idle);

            switch (State) {
                case TowerState.Idle:
                    break;
                case TowerState.Attacking:
                    if (_attackTimer >= AttackSpeed) {
                        _attackTimer -= AttackSpeed + TOGame.Random.NextSingle() * 0.05f - 0.025f;
                        foreach (var enemy in _targetedEnemies) {
                            enemy.Damage(this, Damage);
                        }
                        var sound = TOGame.Assets.Sounds["Sounds/NuclearTowerAttack"].CreateInstance();
                        sound.Volume = TOGame.Settings.Volume;
                        sound.Play();
                    }
                    break;
            }

            _attackTimer = MathF.Min(AttackSpeed, _attackTimer + gameTime.DeltaTime());

            base.Update(gameTime);
        }

        public override void Render(GameTime gameTime) {

            string texture = "";
            int frame = 0;

            switch (State) {
                case TowerState.Idle:
                    frame = Convert.ToInt32(StateTime) % 2;
                    texture = $"Sprites/NuclearTower{frame + 1}";
                    break;
                case TowerState.Attacking:
                    frame = Convert.ToInt32(StateTime * 5) % 2;
                    texture = $"Sprites/NuclearTowerAttack{frame + 1}";
                    break;
            }

            TOGame.SpriteBatch.Draw(TOGame.Assets.Textures[texture], InnerWindowOffset, Color.White);

            base.Render(gameTime);
        }
    }
}