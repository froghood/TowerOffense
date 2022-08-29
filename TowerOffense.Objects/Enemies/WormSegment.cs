using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TowerOffense.Extensions;
using TowerOffense.Objects.Base;
using TowerOffense.Objects.Common;
using TowerOffense.Scenes;
using TowerOffense.Scenes.Gameplay.Objects;

namespace TowerOffense.Objects.Enemies {
    public class WormSegment : Enemy {

        public Worm Worm { get; set; }
        public Enemy Ahead { get; set; }
        public WormSegment Behind { get; set; }

        private const float ActiveDuration = 10f;
        private const float AttackingDuration = 1.25f;
        private const float NeutralizedDuration = 8f;
        private const int DamageAmount = 2;

        public WormSegment(
            Scene scene,
            EntityManager entityManager,
            Vector2 position,
            bool fromPortal
        ) : base(
            scene,
            entityManager,
            new Point(80, 80),
            position,
            fromPortal
        ) {

            MaxHealth = 15f;
            Health = MaxHealth;
            Prize = 0;

            StateChanged += (sender, state) => {
                switch (state) {
                    case EnemyState.Active:
                        break;
                    case EnemyState.Attacking:
                        break;
                    case EnemyState.Neutralized:
                        Draggable = false;
                        break;
                }
            };

            Damaged += (sender, amount) => {
                if (Health - amount <= 0f) {
                    ChangeState(EnemyState.Neutralized, NeutralizedDuration);
                }
            };

            Closed += (_, _) => {
                if (Behind != null) Behind.Ahead = Ahead;
                if (Ahead.GetType() == typeof(Worm)) {
                    (Ahead as Worm).Behind = Behind;
                } else {
                    (Ahead as WormSegment).Behind = Behind;
                }
            };

            ChangeState(EnemyState.Active, float.MaxValue, true);
        }

        public override void Update(GameTime gameTime) {

            var deltaTime = gameTime.DeltaTime();

            if (State == EnemyState.Neutralized && StateTime >= NeutralizedDuration) {
                ChangeState(EnemyState.Active, float.MaxValue, true);
            }

            var mhDistance = new Vector2(Ahead.CenterPosition.X - CenterPosition.X, Ahead.CenterPosition.Y - CenterPosition.Y);
            var distance = MathF.Max(MathF.Sqrt(mhDistance.X * mhDistance.X + mhDistance.Y * mhDistance.Y) - 80f, 0);

            if (distance > 0f) {
                var angle = MathF.Atan2(mhDistance.Y, mhDistance.X);
                Position += new Vector2() {
                    X = MathF.Cos(angle) * MathF.Min(distance, 600f * deltaTime),
                    Y = MathF.Sin(angle) * MathF.Min(distance, 600f * deltaTime)
                };
            }

            base.Update(gameTime);
        }

        public override void Render(GameTime gameTime) {

            string sprite = "";
            int frame;

            switch (State) {
                case EnemyState.Active:
                    frame = Convert.ToInt32(StateTime * 4f) % 2;
                    sprite = $"Sprites/WormBodyActive{frame + 1}";
                    break;
                case EnemyState.Neutralized:
                    frame = Convert.ToInt32(StateTime * 2f) % 2;
                    sprite = $"Sprites/WormBodyNeutralized{frame + 1}";
                    break;
            }

            var texture = TOGame.Assets.Textures[sprite];
            TOGame.SpriteBatch.Draw(texture, InnerWindowOffset, Color.White);

            base.Render(gameTime);
        }
    }
}