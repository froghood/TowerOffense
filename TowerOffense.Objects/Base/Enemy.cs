using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TowerOffense.Extensions;
using TowerOffense.Objects.Common;
using TowerOffense.Scenes;

namespace TowerOffense.Objects.Base {
    public abstract class Enemy : Entity {

        public EnemyState State { get => _enemyState; }
        public float StateTime { get => _stateTime; }
        public float StateDuration { get => _stateDuration; }
        public bool StatePaused { get => _statePaused; }

        public float MaxHealth { get; set; }
        public float Health { get; set; }
        public int Prize { get; set; } = 1;

        private float _stateTime;
        private float _stateDuration;
        private bool _statePaused;

        private float _shakeTime;
        private float _shakeAmount;
        private EnemyState _enemyState;

        private List<Particle> _particles = new();

        public Enemy(
            Scene scene,
            EntityManager entityManager,
            Point size,
            Vector2? position = null,
            bool fromPortal = true
        ) : base(
            scene,
            entityManager,
            size,
            position) {

            if (fromPortal) Position -= InnerWindowCenterOffset;

            Damaged += (sender, amount) => {
                _shakeTime = 0.333f;
                _shakeAmount = (10f + amount * 2f) * 0.667f;
            };

            Closed += (_, _) => {
                TOGame.Player.Pay(Prize);
            };

            Draggable = false;
            Closeable = false;
            TitleBarColor = new Color(255, 70, 120);
            BorderColor = new Color(128, 35, 60);
            FocusedBorderColor = TitleBarColor;

            StateChanged += (sender, state) => {
                switch (state) {
                    case EnemyState.Neutralized:
                        Draggable = true;
                        Closeable = true;
                        TitleBarColor = new Color(180, 120, 140);
                        BorderColor = new Color(90, 60, 70);
                        FocusedBorderColor = TitleBarColor;
                        break;
                    case EnemyState.Attacking:
                        Draggable = false;
                        Closeable = false;
                        break;
                    default:
                        Draggable = false;
                        Closeable = false;
                        TitleBarColor = new Color(255, 70, 120);
                        BorderColor = new Color(128, 35, 60);
                        FocusedBorderColor = TitleBarColor;
                        break;
                }
            };


        }

        public override void Update(GameTime gameTime) {
            _stateTime += gameTime.DeltaTime();
            _shakeTime = MathF.Max(0f, _shakeTime - gameTime.DeltaTime());

            Offset = new Vector2() {
                X = MathF.Cos(_stateTime * MathF.Tau * 10) * (_shakeAmount * _shakeTime * 2),
                Y = 0f
            };

            foreach (var particle in _particles) {
                particle.Update(gameTime);
            }
            _particles.RemoveAll(p => p.Destroyed);

            base.Update(gameTime);
        }

        public override void Render(GameTime gameTime) {

            var spriteFont = TOGame.Instance.Content.Load<SpriteFont>("Fonts/Pixelfont");
            var text = Math.Max(0, _stateDuration - _stateTime).ToString("0.00");

            if (State != EnemyState.Attacking && !TOGame.Player.Dead && !_statePaused) {
                TOGame.SpriteBatch.Draw(Pixel,
                new Rectangle(
                    InnerWindowOffset.ToPoint(),
                    new Point((int)((float)InnerSize.X * (_stateTime / _stateDuration)), 2)),
                Color.White);
            }

            if (State == EnemyState.Attacking) {
                var colorMod = (MathF.Sin(StateTime * 28f) + 1f) / 3f;
                TitleBarColor = new Color(1f, colorMod, colorMod);
                FocusedBorderColor = TitleBarColor;
                BorderColor = TitleBarColor * 0.5f;
            }

            TOGame.SpriteBatch.Draw(Pixel,
                new Rectangle(
                    (InnerWindowOffset + Vector2.UnitY * (InnerSize.Y - 2f)).ToPoint(),
                    new Point((int)((float)InnerSize.X * (Health / MaxHealth)), 2)),
                new Color(0, 255, 0));

            foreach (var particle in _particles) {
                particle.Render(gameTime);
            }

            base.Render(gameTime);
        }

        public void Damage(Tower tower, float amount) {
            Damaged.Invoke(this, amount);

            var manhattanDistance =
                (tower.Position + tower.InnerWindowCenterOffset) -
                (this.Position + this.InnerWindowCenterOffset);
            var angle = MathF.Atan2(manhattanDistance.Y, manhattanDistance.X);
            var angleVector = new Vector2() {
                X = MathF.Cos(angle),
                Y = MathF.Sin(angle)
            };
            angleVector = angleVector / MathF.Max(MathF.Abs(angleVector.X), MathF.Abs(angleVector.Y));
            angleVector *= InnerSize.ToVector2() / 2f;

            _particles.Add(new Particle(this, InnerSize.ToVector2() / 2f + angleVector * 0.9f, tower.TitleBarColor));

            Health -= amount;
        }

        public void ChangeState(EnemyState enemyState, float duration, bool pause = false) {
            StateChanged?.Invoke(this, enemyState);
            _enemyState = enemyState;
            _stateTime = 0f;
            _stateDuration = duration;
            _statePaused = pause;
        }

        public List<Tower> GetTowersInRange(float range) {
            return _entityManager.GetTowers().Select(tower => {
                var manhattanDistance =
                (tower.Position + tower.InnerWindowCenterOffset) -
                (Position + InnerWindowCenterOffset);
                var distance = MathF.Sqrt(manhattanDistance.X * manhattanDistance.X + manhattanDistance.Y * manhattanDistance.Y);
                return (Tower: tower, Distance: distance);
            }).Where(e => e.Distance <= range).OrderBy(e => e.Distance).Select(e => e.Tower).ToList();
        }

        protected event EventHandler<EnemyState> StateChanged;
        protected event EventHandler<float> Damaged;
    }

    public enum EnemyState {
        Active,
        Neutralized,
        Attacking
    }
}