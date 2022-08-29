using Microsoft.Xna.Framework;
using TowerOffense.Objects.Base;
using TowerOffense.Objects.Common;

namespace TowerOffense.Scenes.Gameplay.Objects.Shop {
    public class GameOverWindow : TowerOffense.Objects.Base.SceneWindow {

        public GameOverWindow(Scene scene) : base(scene, new Point(300, 300)) {

            Position = TOGame.DisplaySize.ToVector2() / 2f - InnerWindowCenterOffset;

            ClearColor = new Color(30, 30, 30);
        }
    }
}