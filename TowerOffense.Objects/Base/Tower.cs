using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TowerOffense.Extensions;
using TowerOffense.Objects.Common;
using TowerOffense.Scenes;

namespace TowerOffense.Objects.Base {
    public abstract class Tower : Entity {

        protected float _range;
        protected float _damage;
        protected float _attackSpeed;
        protected float _attackTimer;
        protected int _sellPrice;

        protected List<(Enemy Enemy, float Distance)> _enemiesInRange = new();

        public Tower(Scene scene,
            EntityManager entityManager,
            Point size,
            bool center = false,
            Point? position = null,
            int titleBarHeight = 24,
            int borderThickness = 1) : base(
                scene,
                entityManager,
                size,
                position,
                titleBarHeight,
                borderThickness) {

            TitleBarColor = new Color(180, 255, 215);
            FocusedBorderColor = TitleBarColor;
            BorderColor = new Color(90, 128, 108);
            //TODO: implement
        }

        public void UpdateEnemiesInRange() {
            _enemiesInRange = _entityManager.GetEnemies().Select(enemy => {
                var manhattanDistance = enemy.CenterPosition - this.CenterPosition;
                var distance = MathF.Sqrt(manhattanDistance.X * manhattanDistance.X + manhattanDistance.Y * manhattanDistance.Y);
                return (Enemy: enemy, Distance: distance);
            }).Where(e => e.Distance <= _range).OrderBy(t => t.Distance).ToList();
        }

        public override void Update(GameTime gameTime) {

            _attackTimer += gameTime.DeltaTime();

            base.Update(gameTime);
        }

        public override void Render(GameTime gameTime) {

            var arrow = TOGame.Assets.Textures["Sprites/TowerTargetArrow"];

            var activeEnemies = _enemiesInRange.Where(enemy => enemy.Enemy.EnemyState != EnemyState.Neutralized).ToList();

            for (int i = activeEnemies.Count - 1; i >= 0; i--) {
                var enemyData = activeEnemies[i];

                var manhattanDistance = enemyData.Enemy.CenterPosition - this.CenterPosition;

                var angle = MathF.Atan2(manhattanDistance.Y, manhattanDistance.X);
                var angleVector = new Vector2() {
                    X = MathF.Cos(angle),
                    Y = MathF.Sin(angle)
                } * enemyData.Enemy.InnerSize.ToVector2() / 2f;
                var origin = new Vector2() {
                    X = 0f,
                    Y = arrow.Height / 2f
                };

                TOGame.SpriteBatch.Draw(
                    texture: arrow,
                    position: InnerWindowCenterOffset + angleVector,
                    sourceRectangle: arrow.Bounds,
                    color: i == 0 ? new Color(255, 200, 200) : Color.White,
                    rotation: angle,
                    origin: origin,
                    scale: i == 0 ? 1f : 0.5f,
                    effects: SpriteEffects.None,
                    1f
                );
            }

            base.Render(gameTime);
        }
    }
}