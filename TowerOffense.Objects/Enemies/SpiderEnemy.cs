using System;
using Microsoft.Xna.Framework;
using TowerOffense.Extensions;
using TowerOffense.Objects.Base;
using TowerOffense.Objects.Common;
using TowerOffense.Scenes;
using TowerOffense.Scenes.Gameplay.Objects;

namespace TowerOffense.Objects.Enemies {
    public class SpiderEnemy : Enemy {

        private float _moveTime;
        private Vector2 _velocity;
        private float _speed = 200;
        private float _angle;

        public SpiderEnemy(
            Scene scene,
            EntityManager entityManager,
            Point position,
            bool fromPortal
        ) : base(
            scene,
            entityManager,
            new Point(60, 60),
            position,
            fromPortal
        ) {

            MaxHealth = 5f;
            Health = MaxHealth;

            Damaged += (_, _) => {
                System.Console.WriteLine("damaged");
            };

            StateChanged += (sender, state) => {

                System.Console.WriteLine($"new: {state}, old: {EnemyState}");

                switch (state) {
                    case EnemyState.Active:
                        _speed = 200;
                        if (EnemyState == EnemyState.Neutralized) Health = MaxHealth;
                        break;
                    case EnemyState.Attacking:
                        _speed = 0;
                        break;
                    case EnemyState.Neutralized:
                        _speed = 0;
                        break;
                }
            };
        }

        public override void Update(GameTime gameTime) {

            var deltaTime = gameTime.DeltaTime();


            switch (EnemyState) {
                case EnemyState.Active:
                    if (StateTime >= 10) {
                        ChangeState(EnemyState.Attacking);
                    }
                    break;
                case EnemyState.Attacking:
                    if (StateTime >= 2) {
                        ChangeState(EnemyState.Active);
                    }
                    break;
                case EnemyState.Neutralized:
                    if (StateTime >= 10) {
                        ChangeState(EnemyState.Active);
                    }
                    break;
            }

            if (EnemyState == EnemyState.Active) {

                _moveTime += deltaTime;

                while (_moveTime > 1) {
                    _moveTime -= 1;
                    _angle = new Random().NextSingle() * MathF.Tau;
                }

                _velocity = new Vector2() {
                    X = MathF.Cos(_angle) * _speed * deltaTime,
                    Y = MathF.Sin(_angle) * _speed * deltaTime
                };

                SmoothPosition += _velocity;
                SmoothPosition = new Vector2() {
                    X = MathF.Max(0f, MathF.Min(SmoothPosition.X, 1920f - Size.X)),
                    Y = MathF.Max(0f, MathF.Min(SmoothPosition.Y, 1080f - Size.Y))
                };
            }

            base.Update(gameTime);
        }
    }
}