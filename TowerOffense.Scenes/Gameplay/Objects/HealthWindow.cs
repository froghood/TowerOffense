using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TowerOffense.Objects.Base;

namespace TowerOffense.Scenes.Gameplay.Objects {
    public class HealthWindow : SceneWindow {
        public HealthWindow(Scene scene)
        : base(scene, new Point(270, 60), new Vector2(TOGame.DisplaySize.X / 2f, 24)) {

            Position -= Vector2.UnitX * InnerWindowCenterOffset;

            ClearColor = new Color(104, 66, 74);
            TitleBarColor = new Color(186, 120, 93);
            FocusedBorderColor = TitleBarColor;
            BorderColor = FocusedBorderColor * 0.5f;

            Closeable = false;

        }

        public override void Render(GameTime gameTime) {

            TOGame.SpriteBatch.Draw(TOGame.Assets.Textures["Sprites/Heart"], InnerWindowOffset + Vector2.One * 8f, new Color(40, 25, 23));

            TOGame.SpriteBatch.Draw(
                Pixel,
                new Rectangle((new Vector2(58, 23) + InnerWindowOffset).ToPoint(), new Point(204, 14)),
                new Color(40, 25, 32));

            TOGame.SpriteBatch.Draw(
            Pixel,
            new Rectangle((new Vector2(60, 25) + InnerWindowOffset).ToPoint(), new Point(TOGame.PlayerManager.Health * 2, 10)),
            TitleBarColor);

            base.Render(gameTime);
        }
    }
}