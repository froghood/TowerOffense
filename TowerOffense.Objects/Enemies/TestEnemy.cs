using System;
using Microsoft.Xna.Framework;
using TowerOffense.Extensions;
using TowerOffense.Objects.Base;
using TowerOffense.Objects.Common;
using TowerOffense.Scenes;
using TowerOffense.Scenes.Gameplay.Objects;

namespace TowerOffense.Objects.Enemies {
    public class TestEnemy : Enemy {

        private float _moveTime;
        private Vector2 _velocity;
        private float _speed = 200;
        private float _angle;

        public TestEnemy(
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
                switch (state) {
                    case EnemyState.Active:
                        _speed = 200;
                        Health = MaxHealth;
                        Draggable = false;
                        Closeable = false;
                        TitleBarColor = new Color(200, 80, 80);
                        BorderColor = new Color(100, 40, 40);
                        FocusedBorderColor = TitleBarColor;
                        break;
                    case EnemyState.Attacking:
                        _speed = 0;
                        break;
                    case EnemyState.Neutralized:
                        _speed = 0;
                        Draggable = true;
                        Closeable = true;
                        TitleBarColor = new Color(200, 160, 160);
                        BorderColor = new Color(100, 80, 80);
                        FocusedBorderColor = TitleBarColor;
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