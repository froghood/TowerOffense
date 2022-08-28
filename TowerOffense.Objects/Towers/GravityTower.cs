using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TowerOffense.Objects.Base;
using TowerOffense.Objects.Common;
using TowerOffense.Scenes;

namespace TowerOffense.Objects.Towers {
    public class GravityTower : Tower {
        public GravityTower(
            Scene scene,
            EntityManager entityManager,
            Vector2? position = null) : base(
            scene,
            entityManager,
            new Point(100, 100),
            position: position) {

            TitleBarColor = new Color(225, 185, 255);
            FocusedBorderColor = TitleBarColor;
            BorderColor = new Color(118, 93, 128);

            Range = float.MaxValue;
            AttackSpeed = 0.25f;
            Damage = 0.5f;
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

            Texture2D texture;

            switch (State) {
                case TowerState.Idle:
                    var frame = Convert.ToInt32(StateTime * 2f) % 2;
                    texture = TOGame.Assets.Textures[$"Sprites/GravityTower{frame + 1}"];
                    TOGame.SpriteBatch.Draw(texture, InnerWindowOffset, Color.White);
                    break;
                case TowerState.Attacking:
                    texture = TOGame.Assets.Textures["Sprites/GravityTowerAttack"];
                    TOGame.SpriteBatch.Draw(texture, InnerWindowOffset, Color.White);
                    break;
            }

            base.Render(gameTime);
        }
    }
}