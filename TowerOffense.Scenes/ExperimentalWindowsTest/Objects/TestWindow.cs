using System;
using Microsoft.Xna.Framework;
using TowerOffense.Objects.Base;

namespace TowerOffense.Scenes.Test.Objects {
    public class TestWindow : SceneWindow {

        private Random _random;

        public TestWindow(Scene scene, Point position, Point size) : base(scene, position, size) {
            _random = new Random();
            Draggable = false;
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
        }

        public override void Render(GameTime gameTime) {
            base.Render(gameTime);
        }


    }
}