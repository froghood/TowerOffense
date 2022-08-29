using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TowerOffense.Objects.Base;
using TowerOffense.Objects.Common;
using TowerOffense.Objects.Towers;

namespace TowerOffense.Scenes.Gameplay.Objects.Shop {
    public class Shop : SceneWindow {

        public Vector2 PurchaseOffset { get; set; }

        private EntityManager _entityManager;
        private WaveManager _waveManager;

        private ShopButton _gravityTowerButton;
        private ShopButton _electroTowerButton;
        private ShopButton _nuclearTowerButton;

        public Shop(Scene scene, EntityManager entityManager, WaveManager waveManager) :
        base(scene, new Point(490, 340), TOGame.DisplaySize.ToVector2() / 2f) {

            Position -= InnerWindowCenterOffset;
            PurchaseOffset = Vector2.One * 24;

            _entityManager = entityManager;
            _waveManager = waveManager;

            _gravityTowerButton = new ShopButton(
                this,
                _entityManager,
                new Rectangle((InnerWindowOffset + new Vector2(10, 50)).ToPoint(), new Point(150, 150)),
                typeof(GravityTower),
                6,
                "Gravity",
                new[] {
                    "infinite range",
                    "targets enemies about to attack",
                    "low damage" },
                new Color(225, 185, 255));
            _gravityTowerButton.Texture = TOGame.Assets.Textures["Sprites/GravityTowerSplash"];

            _electroTowerButton = new ShopButton(
                this,
                _entityManager,
                new Rectangle((InnerWindowOffset + new Vector2(170, 50)).ToPoint(), new Point(150, 150)),
                typeof(ElectroTower),
                10,
                "Electro",
                new[] {
                    "average range",
                    "targets enemies with high health",
                    "high damage" },
                TitleBarColor = new Color(167, 236, 255));
            _electroTowerButton.Texture = TOGame.Assets.Textures["Sprites/ElectroTowerSplash"];

            _nuclearTowerButton = new ShopButton(
                this,
                _entityManager,
                new Rectangle((InnerWindowOffset + new Vector2(330, 50)).ToPoint(), new Point(150, 150)),
                typeof(NuclearTower),
                10,
                "Nuclear",
                new[] {
                    "short range",
                    "targets all enemies in range",
                    "high group damage" },
                TitleBarColor = new Color(160, 255, 150));
            _nuclearTowerButton.Texture = TOGame.Assets.Textures["Sprites/NuclearTowerSplash"];

            ClearColor = new Color(30, 30, 40);
            TitleBarColor = new Color(255, 255, 255);
            FocusedBorderColor = TitleBarColor;
            BorderColor = new Color(128, 128, 128);
        }

        public override void Update(GameTime gameTime) {

            _gravityTowerButton.Update(gameTime);
            _electroTowerButton.Update(gameTime);
            _nuclearTowerButton.Update(gameTime);

            base.Update(gameTime);
        }

        public override void Render(GameTime gameTime) {

            _gravityTowerButton.Render(gameTime);
            _electroTowerButton.Render(gameTime);
            _nuclearTowerButton.Render(gameTime);

            var spriteFont = TOGame.Instance.Content.Load<SpriteFont>("Fonts/MilkyNice");
            string text = "Tower  Shop";
            Vector2 textSize = spriteFont.MeasureString(text);

            TOGame.SpriteBatch.DrawString(
                spriteFont,
                text,
                (Vector2.UnitX * InnerWindowCenterOffset.X +
                Vector2.UnitY * 0) + InnerWindowOffset,
                Color.White,
                0f,
                textSize * Vector2.UnitX / 2f,
                1.2f,
                SpriteEffects.None,
                0f);

            base.Render(gameTime);
        }
    }
}