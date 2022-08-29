using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TowerOffense.Objects.Base;
using TowerOffense.Objects.Common;
using TowerOffense.Scenes;
using TowerOffense.Scenes.Gameplay;

namespace TowerOffense.Scenes.Title.Objects {
    public class TitleWindow : SceneWindow {

        private Button _playButton;
        private Button _creditsButton;

        private CreditsWindow _creditsWindow;

        public TitleWindow(Scene scene)
        : base(scene, new Point(540, 380), TOGame.DisplaySize.ToVector2() / 2f) {

            Position -= InnerWindowCenterOffset;

            ClearColor = new Color(30, 30, 40);

            var playButtonSize = new Point(96, 64);
            var playButtonPosition = (new Vector2() {
                X = InnerSize.X * 0.38f - playButtonSize.X * 0.5f,
                Y = InnerSize.Y * 0.84f - playButtonSize.Y * 0.5f
            } + InnerWindowOffset).ToPoint();
            _playButton = new Button(this, playButtonPosition, playButtonSize);
            _playButton.Clicked += (_, _) => {
                TOGame.Scenes.PushScene<GameplayScene>();
            };
            _playButton.Texture = TOGame.Assets.Textures["Sprites/PlayButton"];
            _playButton.HoverTexture = TOGame.Assets.Textures["Sprites/PlayButtonHover"];

            var creditsButtonSize = new Point(128, 64);
            var creditsButtonPosition = (new Vector2() {
                X = InnerSize.X * 0.62f - creditsButtonSize.X * 0.5f,
                Y = InnerSize.Y * 0.84f - creditsButtonSize.Y * 0.5f
            } + InnerWindowOffset).ToPoint();
            _creditsButton = new Button(this, creditsButtonPosition, creditsButtonSize);
            _creditsButton.Clicked += (_, _) => {
                System.Console.WriteLine("credits clicked");
                System.Console.WriteLine(Scene.GetType().Name);

                _creditsWindow = new CreditsWindow(Scene);
                Scene.AddObject(_creditsWindow);
            };
            _creditsButton.Texture = TOGame.Assets.Textures["Sprites/CreditsButton"];
            _creditsButton.HoverTexture = TOGame.Assets.Textures["Sprites/CreditsButtonHover"];
        }

        public override void Update(GameTime gameTime) {
            _playButton.Update(gameTime);
            _creditsButton.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Render(GameTime gameTime) {

            var texture = TOGame.Assets.Textures["Sprites/Title"];

            TOGame.SpriteBatch.Draw(
                TOGame.Assets.Textures["Sprites/Title"],
                Vector2.UnitX * InnerWindowCenterOffset - Vector2.UnitX * texture.Width / 2f +
                Vector2.UnitY * (InnerWindowOffset.Y + 14), Color.White);

            _playButton.Render(gameTime);
            _creditsButton.Render(gameTime);
            base.Render(gameTime);
        }

        public override void Hide() {
            _creditsWindow?.Close();
            base.Hide();
        }
    }
}