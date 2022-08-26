using System;
using Microsoft.Xna.Framework;
using TowerOffense.Objects.Base;

namespace TowerOffense.Scenes.Wave.Objects {
    public class WaveHandler : SceneWindow {

        public int Wave { get => _wave; }

        private int _wave;
        private TimeSpan _time;

        public WaveHandler(Scene scene, Point position, Point size) : base(scene, position, size) {
        }

        public void NextWave() {
            _wave++;
            _time = TimeSpan.Zero;
        }

        public override void Update(GameTime gameTime) {
        }

        public override void Render(GameTime gameTime) {
        }


    }
}