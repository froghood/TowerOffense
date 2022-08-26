using Microsoft.Xna.Framework;
using TowerOffense.Objects.Base;
using TowerOffense.Scenes;
namespace TowerOffense.Objects {
    public class HPDisplay : SceneWindow {
        private PlayerManager PlayerManager = PlayerManager.Instance;
        public HPDisplay(Scene scene, Point position, Point size) : base(scene, position, size) { }

        public override void Update(GameTime gameTime){
            this.Text = PlayerManager.HP.ToString();
        }
        public override void Render(GameTime gameTime){
        }
    }
}