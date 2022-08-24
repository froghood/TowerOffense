using System;
using Microsoft.Xna.Framework;
using TowerOffense.Scenes.Objects;
using TowerOffense.Window;

namespace TowerOffense.Scenes.ExampleScene.Objects {
    public class WindowDeleter : SceneObject {

        private double _time;
        private double _timeThreshhold;
        private WindowObject _window;

        public WindowDeleter(Scene scene, WindowObject window, double timeThreshhold) : base(scene) {
            _timeThreshhold = timeThreshhold;
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