using System;
using Microsoft.Xna.Framework;
using TowerOffense.Objects.Base;

namespace TowerOffense.Scenes.Example.Objects {
    public class WindowDeleter : SceneObject {

        private double _time;
        private double _timeThreshold;
        private SceneWindow _window;

        public WindowDeleter(Scene scene, SceneWindow window, double timeThreshold) : base(scene) {
            _timeThreshold = timeThreshold;
            _window = window;
        }

        public override void Update(GameTime gameTime) {
            _time += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (_time > 10000d) {
                _window.Destroy();
                Destroy();
            }
        }
    }
}