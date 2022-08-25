using Microsoft.Xna.Framework;
using TowerOffense.Scenes.Title.Objects;

namespace TowerOffense.Scenes.Title {
    public class TitleScene : Scene {

        private TitleWindow _titleWindow;

        public TitleScene() : base() {
            _titleWindow = new TitleWindow(this, new Point(800, 600), new Point(480, 270));
            _titleWindow.Form.FormClosed += (_, _) => TOGame.Instance.Exit();
        }

        public override void Initialize() {
            AddObject(_titleWindow);
        }

        public override void Deactivate() {
            _titleWindow.Hide();
        }

        public override void Reactivate() {
            _titleWindow.Show();
        }
    }
}