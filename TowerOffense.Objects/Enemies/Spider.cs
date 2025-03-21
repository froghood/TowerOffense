using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TowerOffense.Extensions;
using TowerOffense.Objects.Base;
using TowerOffense.Objects.Common;
using TowerOffense.Scenes;
using TowerOffense.Scenes.Gameplay.Objects;

namespace TowerOffense.Objects.Enemies {
    public class Spider : Enemy {
        private const float ActiveDuration = 10f;
        private const float AttackingDuration = 1.25f;
        private const float NeutralizedDuration = 8f;
        private const int DamageAmount = 5;
        private float _moveTime;
        private Vector2 _velocity;
        private float _speed = 400;
        private float _angle;

        private Random _random;

        public Spider(
            Scene scene,
            EntityManager entityManager,
            Vector2 position,
            bool fromPortal
        ) : base(
            scene,
            entityManager,
            new Point(60, 60),
            position,
            fromPortal
        ) {

            _random = new Random();
            _angle = _random.NextSingle() * MathF.Tau;

            MaxHealth = 5f;
            Health = MaxHealth;
            Prize = 1;

            Damaged += (sender, amount) => {
                if (Health - amount <= 0f) {
                    ChangeState(EnemyState.Neutralized, NeutralizedDuration);
                }
            };

            StateChanged += (sender, state) => {
                switch (state) {
                    case EnemyState.Active:
                        _speed = 400;
                        if (State == EnemyState.Neutralized) Health = MaxHealth;
                        if (State == EnemyState.Attacking) {
                            TOGame.Player.Damage(DamageAmount);
                            var sound = TOGame.Assets.Sounds["Sounds/SpiderBite"].CreateInstance();
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

            ChangeState(EnemyState.Active, 10f);
        }

        public override void Update(GameTime gameTime) {

            var deltaTime = gameTime.DeltaTime();


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
                    if (StateTime >= NeutralizedDuration) {
                        ChangeState(EnemyState.Active, ActiveDuration);
                    }
                    break;
            }

            if (State == EnemyState.Active) {

                _moveTime += deltaTime;

                while (_moveTime > 1) {
                    _moveTime -= 1 + TOGame.Random.NextSingle() * 0.5f - 0.25f;
                    _angle = new Random().NextSingle() * MathF.Tau;
                }



                _velocity = new Vector2() {
                    X = MathF.Cos(_angle) * _speed * deltaTime,
                    Y = MathF.Sin(_angle) * _speed * deltaTime
                } * (MathF.Floor(_moveTime * 2f + 1f) % 2f);

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
                    sprite = $"Sprites/SpiderActive{frame + 1}";
                    break;
                case EnemyState.Attacking:
                    if (StateTime < 1) sprite = $"Sprites/SpiderStartup";
                    else {
                        frame = Math.Min(Convert.ToInt32((StateTime - 1) * 12f), 2);
                        sprite = $"Sprites/SpiderAttack{frame + 1}";
                    }
                    break;
                case EnemyState.Neutralized:
                    frame = Convert.ToInt32(StateTime * 2f) % 2;
                    sprite = $"Sprites/SpiderNeutralized{frame + 1}";
                    break;
            }

            var texture = TOGame.Assets.Textures[sprite];
            TOGame.SpriteBatch.Draw(texture, InnerWindowOffset, Color.White);

            base.Render(gameTime);
        }
    }
}