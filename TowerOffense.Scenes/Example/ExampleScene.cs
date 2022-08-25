using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using TowerOffense.Scenes.Example.Objects;

namespace TowerOffense.Scenes.Example {
    public class ExampleScene : Scene {
        public ExampleScene() : base() {

            var worm = new Worm(this, new Point(400, 400), new Point(120, 120), 10);
            //worm.ClosingEnabled = false;
            //worm.UserMovingEnabled = false;

            worm.Controllable = false;

            AddObject(worm);
            AddObjects(worm.GetSegments());

            // var windowA = new ExampleWindow(this, 120, 120);
            // var windowB = new ExampleWindow(this, 120, 200);
            // var windowC = new ExampleWindow(this, 120, 200);
            // var windowD = new ExampleWindow(this, 120, 120);
            // var windowE = new ExampleWindow(this, 120, 120);

            // AddObject(windowA);
            // AddObject(windowB);
            // AddObject(windowC);
            // AddObject(windowD);
            // AddObject(windowE);

            // AddObject(new WindowDeleter(this, windows[5], 10000));
        }


    }
}