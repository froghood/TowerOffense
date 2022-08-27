using System;
using Microsoft.Xna.Framework;
using TowerOffense.Extensions;
using TowerOffense.Objects.Common;
using TowerOffense.Scenes;

namespace TowerOffense.Objects.Base {
    public abstract class Enemy : Entity {

        protected float _attackTimer;

        public Enemy(
            Scene scene,
            EntityManager entityManager,
            Point size,
            Point? position = null,
            bool fromPortal = true,
            int titleBarHeight = 24,
            int borderThickness = 1) : base(
                scene,
                entityManager,
                size,
                position,
                titleBarHeight,
                borderThickness) {

            if (fromPortal) SmoothPosition = Position.ToVector2() - InnerWindowCenterOffset;

            Draggable = false;
            TitleBarColor = new Color(200, 150, 150);
            BorderColor = new Color(100, 75, 75);
            FocusedBorderColor = new Color(200, 150, 150);
        }

        public override void Update(GameTime gameTime) {

            _attackTimer = MathF.Max(0f, _attackTimer -= gameTime.DeltaTime());
            if (_attackTimer == 0f) {
                //Attack();
            }

            base.Update(gameTime);
        }
    }
}