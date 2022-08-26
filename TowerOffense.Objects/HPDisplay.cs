using Microsoft.Xna.Framework;
using TowerOffense.Objects.Base;
using TowerOffense.Scenes;
using Microsoft.Xna.Framework.Graphics;

namespace TowerOffense.Objects {
    public class HPDisplay : SceneWindow {
        private PlayerManager _playerManager = PlayerManager.Instance;
        private SpriteFont _font;
        private string _text;

        public HPDisplay(Scene scene, Point position, Point size) : base(scene, position, size) {
            _font = TOGame.Instance.Content.Load<SpriteFont>("Fonts/HpDisplay");
        }

        public override void Update(GameTime gameTime){
            _text = _playerManager.HP.ToString();
        }
        public override void Render(GameTime gameTime){
            Vector2 textMiddlePoint = _font.MeasureString(_text) / 2;
            Vector2 position = new Vector2(16,16); // some work to center this would be cool
            TOGame.SpriteBatch.DrawString(_font, _text, position, Color.Red);
        }
    }
}

// other font drawing info https://docs.monogame.net/articles/content/adding_ttf_fonts.html