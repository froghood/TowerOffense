using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TowerOffense.Extensions;
using TowerOffense.Objects.Common;
using TowerOffense.Scenes;

namespace TowerOffense.Objects.Base {
    public abstract class Enemy : Entity {

        public EnemyState State { get => _enemyState; }
        protected float StateTime { get => _stateTime; }

        protected float MaxHealth { get; set; }
        protected float Health { get; set; }

        private float _stateTime;
        private float _stateDuration;
        private float _shakeTime;
        private float _shakeAmount;
        private EnemyState _enemyState;

        private List<Particle> _particles = new();

        public Enemy(
            Scene scene,
            EntityManager entityManager,
            Point size,
            Vector2? position = null,
            bool fromPortal = true,
            int titleBarHeight = 24,
            int borderThickness = 1
        ) : base(
            scene,
            entityManager,
            size,
            position,
            titleBarHeight,
            borderThickness) {

            if (fromPortal) Position -= InnerWindowCenterOffset;

            Damaged += (sender, amount) => {
                _shakeTime = 0.333f;
                _shakeAmount = (10f + amount * 2f) * 0.667f;
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
                        TitleBarColor = new Color(255, 162, 187);
                        BorderColor = new Color(128, 81, 94);
                        FocusedBorderColor = TitleBarColor;
                        break;
                    case EnemyState.Attacking:
                        Draggable = false;
                        Closeable = false;
                        TitleBarColor = new Color(255, 0, 60);
                        BorderColor = new Color(128, 0, 30);
                        FocusedBorderColor = TitleBarColor;
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

            var spriteFont = TOGame.Instance.Content.Load<SpriteFont>("Fonts/Daydream");
            var text = Math.Max(0, _stateDuration - _stateTime).ToString("0.00");

            if (State != EnemyState.Attacking) {
                TOGame.SpriteBatch.DrawString(
                    spriteFont,
                    text,
                    (State == EnemyState.Active) ?
                        InnerWindowOffset + Vector2.UnitX * 2 :
                        InnerWindowOffset + Vector2.UnitX * (Size.X - spriteFont.MeasureString(text).X - BorderThickness),
                    Color.White,
                    0f,
                    Vector2.One,
                    1f,
                    SpriteEffects.None,
                    0f);
            }


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

        public void ChangeState(EnemyState enemyState, float duration) {
            StateChanged?.Invoke(this, enemyState);
            _enemyState = enemyState;
            _stateTime = 0f;
            _stateDuration = duration;
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