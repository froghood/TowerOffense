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

        public TitleWindow(Scene scene, Vector2 position, Point size) : base(scene, size, position) {

            var playButtonSize = new Point(96, 64);

            var playButtonPosition = new Vector2() {
                X = InnerSize.X * 0.5f - playButtonSize.X * 0.5f,
                Y = InnerSize.Y * 0.75f - playButtonSize.Y * 0.5f
            }.ToPoint();

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
            Draw(TOGame.Assets.Textures["Sprites/Title"], Vector2.Zero, Color.White);
            _playButton.Render(gameTime);
            base.Render(gameTime);
        }


    }
}