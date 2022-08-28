using System;
using Microsoft.Xna.Framework;
using TowerOffense.Objects.Base;
using TowerOffense.Objects.Common;

namespace TowerOffense.Scenes.Gameplay.Objects.Shop {
    public class ShopButton : Button {

        public ShopButton(
            SceneWindow window,
            Type towerType,
            Rectangle bounds,
            string name,
            string[] lines) : base(
            window,
            bounds) {




        }

        public ShopButton(SceneWindow window, Point position, Point size) : base(window, position, size) {


        }

        public override void Render(GameTime gameTime) {



            base.Render(gameTime);
        }
    }
}