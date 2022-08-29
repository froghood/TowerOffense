using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TowerOffense.Objects.Base;

namespace TowerOffense.Scenes.Gameplay.Objects {
    public class MoneyWindow : SceneWindow {
        public MoneyWindow(Scene scene)
        : base(scene, new Point(160, 40), new Vector2(TOGame.DisplaySize.X / 2 + 160, 24)) {

            // ClearColor = new Color(104, 66, 74);
            // TitleBarColor = new Color(186, 120, 93);
            // FocusedBorderColor = TitleBarColor;
            // BorderColor = FocusedBorderColor * 0.5f;

            ClearColor = new Color(30, 30, 40);
            TitleBarColor = Color.White;
            FocusedBorderColor = TitleBarColor;
            BorderColor = new Color(128, 128, 128);

            Closeable = false;

        }

        public override void Render(GameTime gameTime) {

            var spriteFont = TOGame.Instance.Content.Load<SpriteFont>("Fonts/MilkyNice");
            string text = $"${TOGame.Player.Money}";
            var fontSize = spriteFont.MeasureString(text);

            TOGame.SpriteBatch.DrawString(spriteFont,
                text,
                InnerWindowCenterOffset,
                Color.White,
                0f,
                fontSize / 2f,
                1f,
                SpriteEffects.None,
                0f);



            base.Render(gameTime);

            base.Render(gameTime);
        }
    }
}