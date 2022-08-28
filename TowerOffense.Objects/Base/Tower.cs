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

        public TowerState State { get => _state; }
        protected float StateTime { get => _stateTime; }

        protected float Range { get; set; }
        protected float Damage { get; set; }
        protected float AttackSpeed { get; set; }
        protected float _attackTimer;
        protected int SellPrice { get; set; }

        protected List<Enemy> _enemiesInRange = new();
        protected List<Enemy> _targetedEnemies = new();

        private float _stateTime;
        private TowerState _state;

        public Tower(Scene scene,
            EntityManager entityManager,
            Point size,
            bool center = false,
            Vector2? position = null,
            int titleBarHeight = 24,
            int borderThickness = 1) : base(
            scene,
            entityManager,
            size,
            position,
            titleBarHeight,
            borderThickness) { }

        public List<Enemy> GetEnemiesInRange() {
            return _entityManager.GetEnemies().Select(enemy => {
                var manhattanDistance =
                (enemy.Position + enemy.InnerWindowCenterOffset) -
                (this.Position + this.InnerWindowCenterOffset);
                var distance = MathF.Sqrt(manhattanDistance.X * manhattanDistance.X + manhattanDistance.Y * manhattanDistance.Y);
                return (Enemy: enemy, Distance: distance);
            }).Where(e => e.Distance <= Range).OrderBy(e => e.Distance).Select(e => e.Enemy).ToList();
        }

        public void ChangeState(TowerState towerState) {
            StateChanged?.Invoke(this, towerState);
            _state = towerState;
            _stateTime = 0f;
        }

        public override void Update(GameTime gameTime) {

            _stateTime += gameTime.DeltaTime();

            base.Update(gameTime);
        }

        public override void Render(GameTime gameTime) {

            var arrow = TOGame.Assets.Textures["Sprites/TowerTargetArrow"];

            foreach (var enemy in _enemiesInRange) {


                var manhattanDistance =
                (enemy.Position + enemy.InnerWindowCenterOffset) -
                (this.Position + this.InnerWindowCenterOffset);

                var angle = MathF.Atan2(manhattanDistance.Y, manhattanDistance.X);
                var angleVector = new Vector2() {
                    X = MathF.Cos(angle),
                    Y = MathF.Sin(angle)
                };
                angleVector /= MathF.Max(MathF.Abs(angleVector.X), MathF.Abs(angleVector.Y));
                angleVector *= InnerSize.ToVector2() / 2f;
                var origin = new Vector2() {
                    X = arrow.Width + 2,
                    Y = arrow.Height / 2f
                };

                bool isTarget = _targetedEnemies.Contains(enemy);
                TOGame.SpriteBatch.Draw(
                    texture: arrow,
                    position: InnerWindowCenterOffset + angleVector,
                    sourceRectangle: arrow.Bounds,
                    color: isTarget ? TitleBarColor : new Color(255, 255, 255, 100),
                    rotation: angle,
                    origin: origin,
                    scale: isTarget ? 1f : 0.5f,
                    effects: SpriteEffects.None,
                    0f
                );
            }

            base.Render(gameTime);
        }

        protected event EventHandler<TowerState> StateChanged;
    }

    public enum TowerState {
        Idle,
        Attacking
    }
}