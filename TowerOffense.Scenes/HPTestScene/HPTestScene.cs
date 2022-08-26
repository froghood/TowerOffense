using TowerOffense.Scenes.HPTest .Objects;
namespace TowerOffense.Scenes.HPTestScene {

    public class HPTestScene : Scene {
        private HPKiller hpk;
        public HPTestScene(){
            hpk = new HPKiller(this);
            this.AddObject(hpk);
        }

        public override void Initialize() {
            System.Console.WriteLine("HPTestScene Initialized");
        }
    }
}