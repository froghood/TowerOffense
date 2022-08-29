using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TowerOffense.Objects.Base;
using TowerOffense.Objects.Common;
using TowerOffense.Scenes;
using TowerOffense.Scenes.Gameplay;

namespace TowerOffense.Scenes.Title.Objects {
    public class CreditsWindow : SceneWindow {

        public CreditsWindow(Scene scene)
        : base(scene, new Point(800, 512), TOGame.DisplaySize.ToVector2() / 2f) {
            Position -= InnerWindowCenterOffset;
            ClearColor = new Color(30, 30, 40);
        }

        public override void Render(GameTime gameTime) {

            var texture = TOGame.Assets.Textures["Sprites/Credits"];

            TOGame.SpriteBatch.Draw(
                texture,
                InnerWindowOffset,
                Color.White);
            base.Render(gameTime);
        }
    }
}