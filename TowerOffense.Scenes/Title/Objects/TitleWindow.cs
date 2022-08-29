using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TowerOffense.Objects.Base;
using TowerOffense.Objects.Common;
using TowerOffense.Scenes;
using TowerOffense.Scenes.Example;
using TowerOffense.Scenes.Gameplay;

namespace TowerOffense.Scenes.Title.Objects {
    public class TitleWindow : SceneWindow {

        private Button _playButton;

        public TitleWindow(Scene scene)
        : base(scene, new Point(540, 380), TOGame.DisplaySize.ToVector2() / 2f) {

            Position -= InnerWindowCenterOffset;

            ClearColor = new Color(30, 30, 40);

            var playButtonSize = new Point(96, 64);

            var playButtonPosition = (new Vector2() {
                X = InnerSize.X * 0.5f - playButtonSize.X * 0.5f,
                Y = InnerSize.Y * 0.8f - playButtonSize.Y * 0.5f
            } + InnerWindowOffset).ToPoint();

            _playButton = new Button(this, playButtonPosition, playButtonSize);

            _playButton.Clicked += (_, _) => {
                TOGame.Scenes.PushScene<GameplayScene>();
            };

            _playButton.Texture = TOGame.Assets.Textures["Sprites/PlayButton"];
            _playButton.HoverTexture = TOGame.Assets.Textures["Sprites/PlayButtonHover"];
        }

        public override void Update(GameTime gameTime) {
            _playButton.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Render(GameTime gameTime) {

            var texture = TOGame.Assets.Textures["Sprites/Title"];

            TOGame.SpriteBatch.Draw(
                TOGame.Assets.Textures["Sprites/Title"],
                Vector2.UnitX * InnerWindowCenterOffset - Vector2.UnitX * texture.Width / 2f +
                Vector2.UnitY * (InnerWindowOffset.Y + 14), Color.White);
            _playButton.Render(gameTime);
            base.Render(gameTime);
        }


    }
}