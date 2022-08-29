using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TowerOffense.Extensions;
using TowerOffense.Objects.Base;
using TowerOffense.Objects.Common;
using TowerOffense.Scenes;

namespace TowerOffense.Objects.Enemies {
    public class Worm : Enemy {

        public WormSegment Behind { get; set; }

        private const float ActiveDuration = 15f;
        private const float AttackingDuration = 2.25f;
        private const float NeutralizedDuration = 5f;
        private const int DamageAmount = 15;

        private float _time;
        private float _angle;
        private float _speed = 150f;
        private float _steeringSpeed = 0.75f;


        private FastNoiseLite _noise;
        private float _startAngle;
        private Vector2 _angleVector;
        private float _noiseAngle;
        private Vector2 _noiseForce;
        private Vector2 _borderForce;
        private Vector2 _steeringSigns;
        private List<WormSegment> _segments;
        private float _steeringSignsAngle;
        private Vector2 _targetForce;
        private Vector2 _targetForceNormalized;

        public Worm(
            Scene scene,
            EntityManager entityManager,
            int numSegments,
            Vector2? position = null,
            bool fromPortal = true)
            : base(
                scene,
                entityManager,
                new Point(120, 120),
                position,
                fromPortal) {

            _noise = new FastNoiseLite();
            _noise.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2);
            _noise.SetSeed(TOGame.Random.Next(int.MaxValue));

            _startAngle = TOGame.Random.NextSingle() * MathF.Tau;

            _segments = new List<WormSegment>();

            MaxHealth = 300f;
            Health = MaxHealth;
            Prize = 5;

            Closed += (_, _) => {
                foreach (var segment in _segments) {
                    segment.Close();
                }
            };

            Damaged += (sender, amount) => {
                if (Health - amount <= 0f) {
                    ChangeState(EnemyState.Neutralized, NeutralizedDuration);
                }
            };

            StateChanged += (sender, state) => {
                switch (state) {
                    case EnemyState.Active:
                        _speed = 150f;
                        if (State == EnemyState.Neutralized) {
                            System.Console.WriteLine("t");
                            Health = MaxHealth;
                        }
                        if (State == EnemyState.Attacking) {
                            TOGame.Player.Damage(DamageAmount);
                            var sound = TOGame.Assets.Sounds["Sounds/WormBite"].CreateInstance();
                            sound.Volume = TOGame.Settings.Volume;
                            sound.Play();
                        }
                        break;
                    case EnemyState.Neutralized:
                        _speed = 0;
                        break;
                }
            };

            for (int i = 0; i < numSegments; i++) {
                var segment = _entityManager.CreateEnemy<WormSegment>(CenterPosition, true);
                segment.Worm = this;
                Scene.AddObject(segment);
                _segments.Add(segment);

                if (i == 0) continue;
                if (i < numSegments - 1) {
                    _segments[i - 1].Ahead = segment;
                    segment.Behind = _segments[i - 1];
                } else {
                    _segments[i - 1].Ahead = segment;
                    segment.Ahead = this;
                    segment.Behind = _segments[i - 1];
                    Behind = segment;
                }
            }

            ChangeState(EnemyState.Active, ActiveDuration);


        }

        public void AddSegment(WormSegment wormSegment) {
            _segments.Add(wormSegment);
        }

        public override void Update(GameTime gameTime) {

            var deltaTime = gameTime.DeltaTime();
            _time += deltaTime;

            switch (State) {
                case EnemyState.Active:
                    if (StateTime >= ActiveDuration) {
                        if (!TOGame.Player.RunFinished) ChangeState(EnemyState.Attacking, AttackingDuration);
                    }
                    break;
                case EnemyState.Attacking:
                    if (StateTime >= AttackingDuration) {
                        ChangeState(EnemyState.Active, ActiveDuration);
                    }
                    break;
                case EnemyState.Neutralized:
                    if (StateTime >= NeutralizedDuration && !StatePaused) {
                        ChangeState(EnemyState.Active, ActiveDuration);
                    }
                    break;
            }

            if (State != EnemyState.Neutralized && _segments.Count == 0) {
                ChangeState(EnemyState.Neutralized, float.MaxValue, true);
            }

            if (State != EnemyState.Neutralized) {

                var noiseAngle = _startAngle + _noise.GetNoise(_time * 3f, 0) * MathF.Tau;
                _noiseForce = new Vector2() {
                    X = MathF.Cos(noiseAngle),
                    Y = MathF.Sin(noiseAngle)
                };

                _borderForce = -new Vector2() {
                    X = (Position.X + InnerWindowCenterOffset.X - TOGame.DisplaySize.X / 2f) / (TOGame.DisplaySize.X / 2f),
                    Y = (Position.Y + InnerWindowCenterOffset.Y - TOGame.DisplaySize.Y / 2f) / (TOGame.DisplaySize.Y / 2f),
                } * 1.5f;

                _targetForce = _noiseForce += _borderForce;

                _targetForceNormalized = _targetForce;
                _targetForceNormalized.Normalize();

                _steeringSigns = new Vector2() {
                    X = MathF.Sign(_targetForceNormalized.X - _angleVector.X),
                    Y = MathF.Sign(_targetForceNormalized.Y - _angleVector.Y)
                };

                _steeringSignsAngle = MathF.Atan2(_steeringSigns.Y, _steeringSigns.X);
                var _steerVector = new Vector2() {
                    X = _targetForceNormalized.X - _angleVector.X,
                    Y = _targetForceNormalized.Y - _angleVector.Y
                };

                var directionAngle = MathF.Atan2(_steerVector.Y, _steerVector.X);

                var steerVector = new Vector2() {
                    X = MathF.Min(Math.Abs(_steerVector.X), Math.Abs(MathF.Cos(directionAngle) * _steeringSpeed * deltaTime)) * _steeringSigns.X,
                    Y = MathF.Min(Math.Abs(_steerVector.Y), Math.Abs(MathF.Sin(directionAngle) * _steeringSpeed * deltaTime)) * _steeringSigns.Y
                };

                _angleVector += steerVector;
                _angle = MathF.Atan2(_angleVector.Y, _angleVector.X);

                Position += _angleVector * _speed * deltaTime;
            }

            _segments.RemoveAll(e => e.IsDestroyed);

            base.Update(gameTime);
        }

        public override void Render(GameTime gameTime) {

            string sprite = "";
            int frame;

            switch (State) {
                case EnemyState.Active:
                    frame = Convert.ToInt32(StateTime * 4f) % 2;
                    sprite = $"Sprites/WormHeadActive{frame + 1}";
                    break;
                case EnemyState.Attacking:
                    if (StateTime < 2) sprite = $"Sprites/WormHeadStartup";
                    else {
                        frame = Math.Min(Convert.ToInt32((StateTime - 2) * 12f), 2);
                        sprite = $"Sprites/WormHeadAttack{frame + 1}";
                    }
                    break;
                case EnemyState.Neutralized:
                    frame = Convert.ToInt32(StateTime * 2f) % 2;
                    sprite = $"Sprites/WormHeadNeutralized{frame + 1}";
                    break;
            }

            var texture = TOGame.Assets.Textures[sprite];
            TOGame.SpriteBatch.Draw(texture, InnerWindowOffset, Color.White);

            base.Render(gameTime);
        }
    }
}