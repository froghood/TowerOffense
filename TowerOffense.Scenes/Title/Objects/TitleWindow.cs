using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TowerOffense.Objects.Base;
using TowerOffense.Objects.Common;
using TowerOffense.Scenes;
using TowerOffense.Scenes.Example;

namespace TowerOffense.Scenes.Title.Objects {
    public class TitleWindow : SceneWindow {

        private Button _playButton;

        public TitleWindow(Scene scene, Point position, Point size) : base(scene, position, size) {



            var playButtonSize = new Point(96, 64);

            var playButtonPosition = new Vector2() {
                X = Size.X * 0.5f - playButtonSize.X * 0.5f,
                Y = Size.Y * 0.75f - playButtonSize.Y * 0.5f
            }.ToPoint();
            //var playButtonPosition = new Point(240 - 48, 204 - 32);

            _playButton = new Button(this, playButtonPosition, playButtonSize);

            _playButton.Clicked += (_, _) => {
                TOGame.Scenes.PushScene<ExampleScene>();
            };

            _playButton.Texture = TOGame.Assets.Textures["Sprites/PlayButton"];
            _playButton.HoverTexture = TOGame.Assets.Textures["Sprites/PlayButtonHover"];
        }

        public override void Update(GameTime gameTime) {
            _playButton.Update(gameTime);

            System.Console.WriteLine($"FormClientSize: {Form.ClientSize}, FormSize: {Form.Size}, WindowBounds: {Window.ClientBounds}");
            System.Console.WriteLine($"MinimumSize: {Form.MinimumSize}, MaximumSize: {Form.MaximumSize}");
        }

        public override void Render(GameTime gameTime) {
            TOGame.SpriteBatch.Draw(TOGame.Assets.Textures["Sprites/Title"], Vector2.Zero, Color.White);
            _playButton.Render(gameTime);
        }


    }
}