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

        }

        public override void Update(GameTime gameTime) {

            var deltaTime = gameTime.DeltaTime();
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

            base.Update(gameTime);

        }
    }
}