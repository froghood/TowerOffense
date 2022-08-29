using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TowerOffense.Objects.Base;

namespace TowerOffense.Scenes.Gameplay.Objects {
    public class MoneyWindow : SceneWindow {
        public MoneyWindow(Scene scene)
        : base(scene, new Point(160, 40), new Vector2(TOGame.DisplaySize.X / 2 + 160, 24)) {

            ClearColor = new Color(104, 66, 74);
            TitleBarColor = new Color(186, 120, 93);
            FocusedBorderColor = TitleBarColor;
            BorderColor = FocusedBorderColor * 0.5f;

            Closeable = false;

        }

        public override void Render(GameTime gameTime) {

            var spriteFont = TOGame.Instance.Content.Load<SpriteFont>("Fonts/Daydream");
            string text = $"${TOGame.Player.Money}";
            var fontSize = spriteFont.MeasureString(text);

            TOGame.SpriteBatch.DrawString(spriteFont,
                text,
                InnerWindowCenterOffset,
                new Color(40, 25, 43),
                0f,
                fontSize / 2f,
                0.25f,
                SpriteEffects.None,
                0f);



            base.Render(gameTime);

            base.Render(gameTime);
        }
    }
}