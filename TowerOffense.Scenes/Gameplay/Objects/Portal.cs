using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TowerOffense.Extensions;
using TowerOffense.Objects.Base;

namespace TowerOffense.Scenes.Gameplay.Objects {
    public class Portal : SceneWindow {

        private float _time;

        public Portal(Scene scene) :
        base(scene, size: new Point(120, 120)) {
            Closeable = false;
            Draggable = false;
            TitleBarColor = new Color(100, 0, 128);
            FocusedBorderColor = TitleBarColor;
            BorderColor = new Color(50, 0, 64);

            var random = new Random();

            Position = new Vector2() {
                X = random.Next(TOGame.DisplaySize.X - Size.X),
                Y = random.Next(TOGame.DisplaySize.Y - Size.Y)
            };



        }

        public Vector2 GetSpawnPosition() {
            return Position + InnerWindowCenterOffset;
        }

        public override void Render(GameTime gameTime) {


            _time += gameTime.DeltaTime();


            int frame = Convert.ToInt32(_time * 5f) % 5;
            string texture = $"Sprites/Portal{frame + 1}";



            TOGame.SpriteBatch.Draw(TOGame.Assets.Textures[texture], InnerWindowOffset, Color.White);

            base.Render(gameTime);
        }
    }
}