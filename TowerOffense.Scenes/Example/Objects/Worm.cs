using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using TowerOffense.Objects.Base;

namespace TowerOffense.Scenes.Example.Objects {
    public class Worm : SceneWindow {

        private List<Segment> _segments;
        private Vector2 _position;
        private float _turnAngle;
        private float _angle;
        private float _speed = 3;
        private double _time;
        private Random _random;

        public Worm(Scene scene, Vector2 position, Point size, int numSegments, int titleBarHeight, int borderThickness) : base(scene, size, position, titleBarHeight, borderThickness) {
            _position = new Vector2(position.X, position.Y);
            _segments = new List<Segment>();
            _random = new Random();
            for (int i = 0; i < numSegments; i++) {
                _segments.Add(new Segment(Scene, position, new Point(60 - 5 * i, 60 - 5 * i), this));
            }
        }

        public override void Update(GameTime gameTime) {
            _segments.RemoveAll(obj => obj.IsDestroyed);
            _time += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (_time >= 2000) {
                _time -= 2000;
                _turnAngle = _random.NextSingle() * 0.05f - 0.025f;
            }

            _angle += _turnAngle;
            Position += new Vector2() {
                X = MathF.Cos(_angle) * _speed,
                Y = MathF.Sin(_angle) * _speed
            };

            base.Update(gameTime);
        }

        public override void Render(GameTime gameTime) {
            base.Render(gameTime);
        }

        public IEnumerable<Segment> GetSegments() {
            return _segments;
        }


    }
}