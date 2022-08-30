using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TowerOffense.Extensions;
using TowerOffense.Objects.Base;
using TowerOffense.Scenes;

namespace TowerOffense.Objects.Common {



    public class FpsWindow : SceneWindow {

        private Queue<float> _samples = new();
        private float _fps;

        public FpsWindow(Scene scene) : base(scene, new Point(96, 32)) {

            Position = new Vector2(TOGame.DisplaySize.X - Size.X - 24, 24);

            ClearColor = new Color(30, 30, 40);
        }

        public override void Update(GameTime gameTime) {

            _samples.Enqueue(gameTime.DeltaTime());
            while (_samples.Count > 100) _samples.Dequeue();

            _fps = 1f / (_samples.Sum() / _samples.Count());

            base.Update(gameTime);
        }

        public override void Render(GameTime gameTime) {

            var spriteFont = TOGame.Instance.Content.Load<SpriteFont>("Fonts/MilkyNice");
            string text = $"fps: {_fps.ToString("0.0")}";
            var fontSize = spriteFont.MeasureString(text);

            TOGame.SpriteBatch.DrawString(spriteFont,
                text,
                InnerWindowCenterOffset,
                Color.White * 0.5f,
                0f,
                fontSize / 2f,
                0.5f,
                SpriteEffects.None,
                0f);

            base.Render(gameTime);
        }
    }
}