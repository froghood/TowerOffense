using Microsoft.Xna.Framework;
using TowerOffense.Scenes.Wave.Objects;

namespace TowerOffense.Scenes.Wave {
    public class WaveScene : Scene {

        private WaveHandler _waveHandler;

        public WaveScene() {
            _waveHandler = new WaveHandler(this, new Point(24, 24), new Point(120, 80));
            _waveHandler.ClosingEnabled = false;
        }

        public override void Initialize() {

            AddObject(_waveHandler);

            _waveHandler.NextWave();

            base.Initialize();
        }

        public override void Deactivate() {
            base.Deactivate();
        }

        public override void Reactivate() {
            _waveHandler.NextWave();
            base.Reactivate();
        }
    }
}