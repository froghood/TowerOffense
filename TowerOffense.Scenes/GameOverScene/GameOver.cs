namespace TowerOffense.Scenes.GameOver {
    public class GameOverScene : Scene {
        public GameOverScene() : base() {
            System.Console.WriteLine("you dead");
        }

        public override void Initialize() {
            System.Console.WriteLine("GameOverScene Initialized");
        }
    }
}