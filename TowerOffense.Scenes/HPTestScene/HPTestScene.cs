using TowerOffense.Scenes.HPTest.Objects;
using TowerOffense.Objects;
using Microsoft.Xna.Framework;
namespace TowerOffense.Scenes.HPTestScene {

    public class HPTestScene : Scene {
        private HPKiller hpk;
        private HPDisplay hpd;
        public HPTestScene(){
            hpk = new HPKiller(this);
            hpd = new HPDisplay(this,new Point(0,0),new Point(120,120));
            this.AddObject(hpk);
            this.AddObject(hpd);
        }

        public override void Initialize() {
            System.Console.WriteLine("HPTestScene Initialized");
        }
    }
}