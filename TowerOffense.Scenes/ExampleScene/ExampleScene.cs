using System.Collections.Generic;
using Microsoft.Xna.Framework;
using TowerOffense.Scenes.ExampleScene.Objects;
using TowerOffense.Window;

namespace TowerOffense.Scenes.ExampleScene {
    public class ExampleScene : Scene {
        public ExampleScene() : base() {

            var window = new ExampleWindow(this, 60, 60);
            AddObject(window);

            // var windows = new List<ExampleWindow>();
            // for (int i = 0; i < 1; i++) {
            //     var window = new ExampleWindow(this, 60, 60);
            //     //window.Form.ClientSize = new(60, 60);
            //     windows.Add(window);
            //     AddObject(window);
            // }

            //AddObject(new WindowDeleter(this, windows[5], 10000));
        }
    }
}