using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TowerOffense.Objects.Base;

namespace TowerOffense.Scenes.Gameplay.Objects {
    public class Portal : SceneWindow {



        public Portal(Scene scene) :
        base(scene, size: new Point(120, 120)) {
            Closeable = false;
            Draggable = false;
            TitleBarColor = new Color(100, 0, 128);
            FocusedBorderColor = TitleBarColor;
            BorderColor = new Color(50, 0, 64);

            var displayWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            var displayHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            var random = new Random();

            Position = new Vector2() {
                X = random.Next(displayWidth - Size.X),
                Y = random.Next(displayHeight - Size.Y)
            };



        }

        public Vector2 GetSpawnPosition() {
            return Position + InnerWindowCenterOffset;
        }
    }
}