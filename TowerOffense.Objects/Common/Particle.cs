using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TowerOffense.Extensions;
using TowerOffense.Objects.Base;

namespace TowerOffense.Objects.Common {
    public class Particle : WindowObject {

        public bool Destroyed { get; set; }
        private Vector2 _position;
        private Color _color;

        private float _time;

        public Particle(SceneWindow sceneWindow, Vector2 position, Color color) : base(sceneWindow) {
            _position = position;
            _color = color;
        }

        public override void Update(GameTime gameTime) {
            _time += gameTime.DeltaTime();
            if (_time >= 0.15f) Destroyed = true;
        }

        public override void Render(GameTime gameTime) {
            if (Destroyed) return;
            var frame = (int)Math.Ceiling(_time / 0.15f * 3f);
            var texture = TOGame.Assets.Textures[$"Sprites/HitParticle{frame}"];
            TOGame.SpriteBatch.Draw(
                texture,
                _position + SceneWindow.InnerWindowOffset,
                texture.Bounds,
                _color,
                0f,
                texture.Bounds.Size.ToVector2() / 2f,
                0.1f,
                SpriteEffects.None,
                0f);
        }
    }
}