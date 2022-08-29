using System;
using Microsoft.Xna.Framework;
using TowerOffense.Extensions;
using TowerOffense.Objects.Base;
using TowerOffense.Objects.Common;
using TowerOffense.Scenes;

namespace TowerOffense.Objects.Enemies {
    public class Beetle : Enemy {

        private const float ActiveDuration = 18f;
        private const float AttackingDuration = 2.25f;
        private const float NeutralizedDuration = 12f;
        private const int DamageAmount = 10;

        private float _time;
        private float _angle;
        private float _speed = 80f;

        private FastNoiseLite _noise;
        private Vector2 _velocity;

        public Beetle(
            Scene scene,
            EntityManager entityManager,
            Vector2? position = null,
            bool fromPortal = true)
            : base(
                scene,
                entityManager,
                new Point(120, 120),
                position,
                fromPortal) {

            MaxHealth = 80f;
            Health = MaxHealth;
            Prize = 8;

            _noise = new FastNoiseLite();
            _noise.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2);

            Damaged += (sender, amount) => {
                if (Health - amount <= 0f) {
                    ChangeState(EnemyState.Neutralized, NeutralizedDuration);
                }
            };

            StateChanged += (sender, state) => {
                switch (state) {
                    case EnemyState.Active:
                        _speed = 80f;
                        if (State == EnemyState.Neutralized) Health = MaxHealth;
                        if (State == EnemyState.Attacking) {
                            TOGame.Player.Damage(DamageAmount);
                            var sound = TOGame.Assets.Sounds["Sounds/BeetleBite"].CreateInstance();
                            sound.Volume = TOGame.Settings.Volume;
                            sound.Play();
                        }
                        break;
                    case EnemyState.Attacking:
                        _speed = 0;
                        break;
                    case EnemyState.Neutralized:
                        _speed = 0;
                        break;
                }
            };

            ChangeState(EnemyState.Active, ActiveDuration);


        }

        public override void Update(GameTime gameTime) {

            var deltaTime = gameTime.DeltaTime();

            switch (State) {
                case EnemyState.Active:
                    if (StateTime >= ActiveDuration) {
                        if (!TOGame.Player.Dead) ChangeState(EnemyState.Attacking, AttackingDuration);
                    }
                    break;
                case EnemyState.Attacking:
                    if (StateTime >= AttackingDuration) {
                        ChangeState(EnemyState.Active, ActiveDuration);
                    }
                    break;
                case EnemyState.Neutralized:
                    if (StateTime >= NeutralizedDuration) {
                        ChangeState(EnemyState.Active, ActiveDuration);
                    }
                    break;
            }

            if (State == EnemyState.Active) {

                _time += deltaTime;

                _angle = MathF.Atan2(_noise.GetNoise(_time * 5f, 0), _noise.GetNoise(0, _time * 5f));

                _time += deltaTime;

                _velocity = new Vector2() {
                    X = MathF.Cos(_angle) * _speed * deltaTime,
                    Y = MathF.Sin(_angle) * _speed * deltaTime
                };

                Position += _velocity;
            }

            base.Update(gameTime);
        }

        public override void Render(GameTime gameTime) {

            string sprite = "";
            int frame;

            switch (State) {
                case EnemyState.Active:
                    frame = Convert.ToInt32(StateTime * 4f) % 2;
                    sprite = $"Sprites/BeetleActive{frame + 1}";
                    break;
                case EnemyState.Attacking:
                    if (StateTime < 2) sprite = $"Sprites/BeetleStartup";
                    else {
                        frame = Math.Min(Convert.ToInt32((StateTime - 2) * 12f), 2);
                        sprite = $"Sprites/BeetleAttack{frame + 1}";
                    }
                    break;
                case EnemyState.Neutralized:
                    frame = Convert.ToInt32(StateTime * 2f) % 2;
                    sprite = $"Sprites/BeetleNeutralized{frame + 1}";
                    break;
            }

            var texture = TOGame.Assets.Textures[sprite];
            TOGame.SpriteBatch.Draw(texture, InnerWindowOffset, Color.White);

            base.Render(gameTime);
        }
    }
}