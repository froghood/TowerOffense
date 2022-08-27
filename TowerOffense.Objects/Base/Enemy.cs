using System;
using Microsoft.Xna.Framework;
using TowerOffense.Extensions;
using TowerOffense.Objects.Common;
using TowerOffense.Scenes;

namespace TowerOffense.Objects.Base {
    public abstract class Enemy : Entity {

        public EnemyState EnemyState { get => _enemyState; }
        protected float StateTime { get => _stateTime; }

        protected float MaxHealth { get; set; }
        protected float Health { get; set; }

        private float _stateTime;
        private float _shakeTime;
        private float _shakeAmount;
        private EnemyState _enemyState;

        public Enemy(
            Scene scene,
            EntityManager entityManager,
            Point size,
            Point? position = null,
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

            if (fromPortal) SmoothPosition = Position.ToVector2() - InnerWindowCenterOffset;

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

            OffsetPosition = new Vector2() {
                X = MathF.Cos(_stateTime * MathF.Tau * 10) * (_shakeAmount * _shakeTime * 2),
                Y = 0f
            };

            base.Update(gameTime);
        }

        public void Damage(float amount) {
            Health -= amount;
            Damaged.Invoke(this, amount);
            if (Health <= 0 && _enemyState != EnemyState.Neutralized) {
                ChangeState(EnemyState.Neutralized);
            }
        }

        public void ChangeState(EnemyState enemyState) {
            StateChanged?.Invoke(this, enemyState);
            _enemyState = enemyState;
            _stateTime = 0f;
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