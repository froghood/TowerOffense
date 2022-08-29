using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TowerOffense.Objects.Base;

namespace TowerOffense.Scenes.Gameplay.Objects {
    public class HealthWindow : SceneWindow {
        public HealthWindow(Scene scene)
        : base(scene, new Point(270, 60), new Vector2(TOGame.DisplaySize.X / 2f, 24)) {

            Position -= Vector2.UnitX * InnerWindowCenterOffset;

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

            TOGame.SpriteBatch.Draw(TOGame.Assets.Textures["Sprites/Heart"], InnerWindowOffset + Vector2.One * 8f, TitleBarColor);

            TOGame.SpriteBatch.Draw(
                Pixel,
                new Rectangle((new Vector2(58, 23) + InnerWindowOffset).ToPoint(), new Point(204, 14)),
                new Color(128, 140, 160));

            TOGame.SpriteBatch.Draw(
                Pixel,
                new Rectangle((new Vector2(60, 25) + InnerWindowOffset).ToPoint(), new Point(TOGame.Player.Health * 2, 10)),
                TitleBarColor);

            base.Render(gameTime);
        }
    }
}