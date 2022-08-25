using Microsoft.Xna.Framework;
using TowerOffense.Scenes.Title.Objects;

namespace TowerOffense.Scenes.Title {
    public class TitleScene : Scene {

        private TitleWindow _titleWindow;

        public TitleScene() : base() {
            _titleWindow = new TitleWindow(this, new Point(800, 600), new Point(60, 60));
            _titleWindow.Size = new Point(60, 60);
            _titleWindow.Form.FormClosed += (_, _) => TOGame.Instance.Exit();
            //_titleWindow.Form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        }

        public override void Initialize() {
            AddObject(_titleWindow);
        }

        public override void Deactivate() {
            System.Console.WriteLine("Deactivate");
            _titleWindow.Hide();
        }

        public override void Reactivate() {
            System.Console.WriteLine("Reactivate");
            _titleWindow.Show();
        }
    }
}