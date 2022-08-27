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
            TitleBarColor = new Color(130, 100, 130);
            FocusedBorderColor = TitleBarColor;
            BorderColor = new Color(65, 50, 65);

            var displayWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            var displayHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            var random = new Random();

            Position = new Point() {
                X = random.Next(displayWidth - Size.X),
                Y = random.Next(displayHeight - Size.Y)
            };



        }

        public Point GetSpawnPosition() {
            return (Position.ToVector2() + InnerWindowOffset + InnerSize.ToVector2() / 2f).ToPoint();
        }
    }
}