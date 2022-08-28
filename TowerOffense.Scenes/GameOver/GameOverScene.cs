namespace TowerOffense.Scenes.GameOver {
    public class GameOverScene : Scene {

        private int _wave;

        public GameOverScene(bool victory, int wave = 0) : base() {
            _wave = wave;
        }

        public override void Initialize() {
            System.Console.WriteLine($"gameover on wave {_wave}");
        }
    }
}