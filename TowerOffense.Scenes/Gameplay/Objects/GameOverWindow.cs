using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TowerOffense.Objects.Base;
using TowerOffense.Objects.Common;

namespace TowerOffense.Scenes.Gameplay.Objects.Shop {
    public class GameOverWindow : TowerOffense.Objects.Base.SceneWindow {

        private WaveManager _waveManager;

        public GameOverWindow(Scene scene, WaveManager waveManager) : base(scene, new Point(322, 300)) {

            Position = TOGame.DisplaySize.ToVector2() / 2f - InnerWindowCenterOffset;

            ClearColor = new Color(40, 30, 30);
            TitleBarColor = new Color(220, 0, 0);
            FocusedBorderColor = TitleBarColor;
            BorderColor = new Color(110, 0, 0);

            _waveManager = waveManager;
        }

        public override void Render(GameTime gameTime) {

            TOGame.SpriteBatch.Draw(TOGame.Assets.Textures["Sprites/GameOver"], InnerWindowOffset, Color.White);

            var spriteFont = TOGame.Instance.Content.Load<SpriteFont>("Fonts/MilkyNice");
            string text = "died on wave";
            var textSize = spriteFont.MeasureString(text);

            TOGame.SpriteBatch.DrawString(spriteFont, text,
            InnerWindowCenterOffset - textSize / 2f + Vector2.UnitY * 88,
            TitleBarColor);

            text = $"{_waveManager.Wave}";
            textSize = spriteFont.MeasureString(text);

            TOGame.SpriteBatch.DrawString(spriteFont, text,
            InnerWindowCenterOffset - textSize / 2f + Vector2.UnitY * 120,
            TitleBarColor);

            base.Render(gameTime);
        }
    }
}