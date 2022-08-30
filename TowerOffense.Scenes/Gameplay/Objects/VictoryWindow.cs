using Microsoft.Xna.Framework;
using TowerOffense.Objects.Base;
using TowerOffense.Objects.Common;

namespace TowerOffense.Scenes.Gameplay.Objects.Shop {
    public class VictoryWindow : SceneWindow {

        public VictoryWindow(Scene scene) : base(scene, new Point(480, 144)) {

            Position = TOGame.DisplaySize.ToVector2() / 2f - InnerWindowCenterOffset;
            ClearColor = new Color(40, 40, 30);
            TitleBarColor = new Color(255, 220, 128);
            FocusedBorderColor = TitleBarColor;
            BorderColor = new Color(128, 110, 64);

        }

        public override void Render(GameTime gameTime) {

            TOGame.SpriteBatch.Draw(TOGame.Assets.Textures["Sprites/Victory"], InnerWindowOffset, Color.White);

            base.Render(gameTime);
        }
    }
}